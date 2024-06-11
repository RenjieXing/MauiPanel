
using BiliBiliMaui.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using BiliBiliMaui.Services;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.Input;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace BiliBiliMaui.ViewModels
{
    public partial class VideosModelViews : ObservableObject
    {
        VideosService _videosService { get; set; }

#if __ANDROID__
                   
        IMediaPicker? mediaPicker { get; set; }
#endif
        public void BindService(VideosService videosService, IMediaPicker? mediaPicker = null)
        {
            _videosService = videosService;
#if __ANDROID__
            this.mediaPicker = mediaPicker is not null ? mediaPicker : null;
#endif
        }

        public VideosModelViews()
        {
            Url = "Enter your Bvid";
            videosFirstPages = new VideosFirstPages();
            FileCount = 0;
            hD = [];
      
        }
      

        [ObservableProperty] public VideosFirstPages videosFirstPages;

        [ObservableProperty] public string videoframe;

        [ObservableProperty] public string url;

        [ObservableProperty] public int fileCount;

        [ObservableProperty] public double progress;


        [ObservableProperty] public ObservableCollection<Support_Formats> hD;
        public string Bvid = "";
        public string Avid = "";
        public int count = 0;

  





        public async Task<bool> IsLeagel()
        {
            string pattern = @"BV[0-9A-Za-z]{10}";
            Match match = Regex.Match(Url, pattern);

            if (match.Success)
            {
                Bvid = match.Value;
                return true;
            }
            else
            {
                if (!Url.Contains(@"https://b23.tv/"))
                {
                    Url = "不是有效的 BV 号。";
                    return false;
                }
                else
                {
                    string prefix = "https";

                    if (!Url.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    {
                        int index = Url.IndexOf("https://", StringComparison.OrdinalIgnoreCase);
                        if (index >= 0)
                        {
                            Url = string.Concat("https", Url.AsSpan(index + 5));
                        }
                    }

                    var result = await _videosService.appLink(Url, pattern);
                    if (result.Item1 == true)
                    {
                        Bvid = result.Item2;
                        return true;
                    }
                    else
                    {
                        Url = "不是有效的 BV 号。";
                        return false;
                    }
                }
            }
        }

        public async Task GetVideosFirstPages()
        {
            if (await IsLeagel())
            {
                Avid = BilBilConfigSetting.BvAvConverter.Bv2Av(Bvid)?.ToString();
                VideosFirstPages = await _videosService.GetVideoCid(Bvid);
                //有可能会有的封面图片
                var frame = VideosFirstPages.data.FirstOrDefault().first_frame;
                var frameLocal = Path.Combine(BilBilConfigSetting.SavePath, $"firstFrame{count++}." + frame.Split('.').Last());
                if (File.Exists(frameLocal))
                {
                    File.Delete(frameLocal);
                }
                await DownloadAndSaveImageAsync(frame, frameLocal);
                Videoframe = frameLocal;
            }
        }
        public async Task<videoStreamUrl?> GetVideoUrl(int index = 64)
        {

            if (await IsLeagel())
            {

                {
                    //获取视频流地址 TODO 这里只拿了第一个视频的地址,如果分P的话需要多次拿然后用ffmpeg去拼MP4
                    var s = await _videosService.GETBvStream(Bvid, VideosFirstPages.data[0].cid.ToString(), BilBilConfigSetting.SESSDATA, index.ToString());
                    if (index == 64)
                    {
                        HD.Clear();
                        s.data.support_formats.ToList().ForEach(x => HD.Add(x));
                    }
                    return s;
                }



            }
            return null;

        }
        public async Task DownloadAndSaveImageAsync(string imageUrl, string localFilePath)
        {
            using HttpClient client = new();
            using HttpResponseMessage response = await client.GetAsync(imageUrl);
            using Stream stream = await response.Content.ReadAsStreamAsync();
            using FileStream fileStream = File.Create(localFilePath);
            await stream.CopyToAsync(fileStream);
        }
        /// <summary>
        /// 最后组装视频
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetResult(videoStreamUrl videoStreamUrl, CancellationToken cancellationToken = default)
        {
            var FilePath = Path.Combine(BilBilConfigSetting.SavePath, Bvid + ".mp4");
            foreach (var item in videoStreamUrl.data.durl)
            {
                var url = item.url;
                url = Uri.UnescapeDataString(url);
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                httpClient.DefaultRequestHeaders.Referrer = new Uri("https://www.bilibili.com");
                using var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                using var downloadStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                var totalBytesRead = 0L;
                var buffer = new byte[8192];
                var isMoreToRead = true;
                // 创建一个文件流，以追加模式打开
                using var destinationStream = new FileStream(FilePath, FileMode.Append, FileAccess.Write, FileShare.None, 8192, true);
                do
                {
                    var bytesRead = await downloadStream.ReadAsync(buffer, cancellationToken);

                    if (bytesRead == 0)
                    {
                        isMoreToRead = false;
                    }
                    else
                    {
                        // 将数据直接写入到目标文件
                        await destinationStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);

                        totalBytesRead += bytesRead;
                        Progress = (double)totalBytesRead / videoStreamUrl.data.durl[0].size;
                    }
                } while (isMoreToRead);

#if ANDROID
                        string sourceFile = Path.Combine(BilBilConfigSetting.SavePath, Bvid + ".mp4");
                        string mainDir = FileSystem.Current.AppDataDirectory;
                        string moviesDir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMovies).AbsolutePath;
                        string destinationFile = Bvid + ".mp4";
                        FilePath = Path.Combine(moviesDir, destinationFile);
                        if (File.Exists(FilePath))
                        {
                            File.Delete(FilePath);
                        }
                        System.IO.File.Copy(sourceFile, FilePath);
                        File.Delete(sourceFile);
#endif




            }
            return FilePath;

        }
    }
}



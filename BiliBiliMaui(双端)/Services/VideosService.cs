using BiliBiliMaui.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BiliBiliMaui.Services
{
    public class VideosService
    {

        /// <summary>
        /// 获取视频的Cid,即分P的编号
        /// </summary>
        /// <param name="Bvid"></param>
        /// <returns></returns>
        public async Task<VideosFirstPages> GetVideoCid(string Bvid)
        {
            using var client = new HttpClient();
            try
            {
                var builder = new UriBuilder("https://api.bilibili.com/x/player/pagelist")
                {
                    Port = -1
                };
                var query = System.Web.HttpUtility.ParseQueryString(builder.Query);
                query["bvid"] = Bvid;
                builder.Query = query.ToString();
                string url = builder.ToString();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<VideosFirstPages>(responseBody)?? throw new NullReferenceException();
            }
            catch
            {
                return new VideosFirstPages();
            }
        }


        /// <summary>
        /// 拿取数据流地址
        /// </summary>
        /// <returns></returns>
        public async Task<videoStreamUrl> GETBvStream(string Bvid, string Cid, Cookie cookie, string qn = "64")
        {
            var baseAddress = new Uri("https://api.bilibili.com/x/player/playurl");
            var cookieContainer = new System.Net.CookieContainer();
            using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            using var client = new HttpClient(handler) { BaseAddress = baseAddress };

            if (cookie.Value != "")
                cookieContainer.Add(baseAddress, cookie);

            // 创建请求参数
            var content = new FormUrlEncodedContent(
            [
            new KeyValuePair<string, string>("bvid", Bvid),
                new KeyValuePair<string, string>("cid", Cid),
                new KeyValuePair<string, string>("qn", qn),
                new KeyValuePair<string, string>("fnval", "1"),
                new KeyValuePair<string, string>("fnver", "0"),
                new KeyValuePair<string, string>("fourk", "1")
        ]);

            // 发送GET请求
            var result = await client.GetAsync(baseAddress + "?" + await content.ReadAsStringAsync());
            string resultContent = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<videoStreamUrl>(resultContent)?? throw new NullReferenceException();
        }



        public async Task<(bool, string, string)> appLink(string url, string pattern)
        {
            try
            {
                using HttpClient client = new();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                byte[] bytes = await response.Content.ReadAsByteArrayAsync();
                string responseBodyWithEncoding = "";

#if __WINDOWS__


                responseBodyWithEncoding = response.RequestMessage?.RequestUri?.AbsoluteUri.ToString()?? throw new NullReferenceException();


#endif
#if __ANDROID__
                    responseBodyWithEncoding = Encoding.UTF8.GetString(bytes);
#endif
                responseBodyWithEncoding = Regex.Unescape(responseBodyWithEncoding);
                // 在此处处理响应内容
                Match match2 = Regex.Match(responseBodyWithEncoding, pattern);
                if (match2.Success)
                {
                    var Bvid = match2.Value;
                    return (true, Bvid, "");
                }
                else
                {
                    url = "不是有效的 App转发链接。";
                    return (false, "", url);
                }
            }
            catch
            {
                url = "网络请求失败";
                return (false, "", url);
            }
        }


        public async Task<videoStreamUrl> GETAvStream(string Avid, string Cid, Cookie cookie, string qn = "64")
        {
            var baseAddress = new Uri("https://api.bilibili.com/x/player/playurl");
            var cookieContainer = new System.Net.CookieContainer();
            using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            using var client = new HttpClient(handler) { BaseAddress = baseAddress };

            if (!string.IsNullOrEmpty(cookie.Value))
                cookieContainer.Add(baseAddress, cookie);

            // 创建请求参数
            var content = new FormUrlEncodedContent(
            [
            new KeyValuePair<string, string>("avid", Avid),
                new KeyValuePair<string, string>("cid", Cid),
                new KeyValuePair<string, string>("qn", qn),
                new KeyValuePair<string, string>("fnval", "1"),
                new KeyValuePair<string, string>("fnver", "0"),
                new KeyValuePair<string, string>("fourk", "1")
        ]);

            // 发送GET请求
            var result = await client.GetAsync(baseAddress + "?" + await content.ReadAsStringAsync());
            string resultContent = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<videoStreamUrl>(resultContent)?? throw new NullReferenceException();
        }




    }

}

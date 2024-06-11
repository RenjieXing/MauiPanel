using BiliBiliMaui.ViewModels;
using BiliBiliMaui.Services;

using Application = Microsoft.Maui.Controls.Application;
#if __ANDROID__
using Android.App;
using Android.Content;
using Android.Media;
#endif

namespace BiliBiliMaui.Views;

public partial class BilibiliScriptsPage : ContentPage
{

    static VideosService VideosService => (App.Current as App ?? throw new InvalidOperationException()).ServiceProvider.GetRequiredService<VideosService>();
    static VideosModelViews VideosModelViews => (App.Current as App ?? throw new InvalidOperationException()).ServiceProvider.GetRequiredService<VideosModelViews>();
#if __ANDROID__
    IMediaPicker mediaPicker => (App.Current as App ?? throw new InvalidOperationException()).ServiceProvider.GetRequiredService<IMediaPicker>();
#endif
    public BilibiliScriptsPage()
    {
        InitializeComponent();
        BindingContext = VideosModelViews;
#if __WINDOWS__
        VideosModelViews.BindService(VideosService);
#endif

#if __ANDROID__
        VideosModelViews.BindService(VideosService, mediaPicker);
#endif

    }
    private async void OnCounterClicked(object sender, EventArgs e)
    {
        //拿到封面图和分P信息
        await VideosModelViews.GetVideosFirstPages();

         await VideosModelViews.GetVideoUrl();

        if (!string.IsNullOrEmpty(VideosModelViews.Bvid))
        {
            down.IsEnabled = true;
            down.IsVisible = true;
        }

    }
    private async void Download(object sender, EventArgs e)
    {

        GetButton.IsEnabled = false;
        down.IsEnabled = false;
        down.IsVisible = false;
        VideosModelViews.HD.Clear();
        var temp = await VideosModelViews.GetVideoUrl(80);
        var path = await VideosModelViews.GetResult(temp ?? throw new InvalidOperationException());
#if __ANDROID__
        ScanFile(Android.App.Application.Context, path);
#endif
        GetButton.IsEnabled = true;

    }
#if __ANDROID__
    public void ScanFile(Context context, string filePath)
    {
        MediaScannerConnection.ScanFile(context, new string[] { filePath }, null, null);
    }
#endif
}
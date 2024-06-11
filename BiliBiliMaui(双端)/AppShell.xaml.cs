using BiliBiliMaui.Views;

namespace BiliBiliMaui
{
    public partial class AppShell : Shell
    {
        /// <summary>
        /// 根据页面名称和是否启用，找到对应的ShellContent并设置是否启用
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        public static bool WhichIsEnable(string pageName, bool isEnable)
        {

            var shell = (Shell)Application.Current.MainPage;
            foreach (var item in shell.Items)
            {
                foreach (var section in item.Items)
                {
                    foreach (var content in section.Items)
                    {
                        // 这里你可以获取到每一个 ShellContent 对象
                        content.Title = pageName;
                        content.IsEnabled = isEnable;
                        content.IsVisible = isEnable;

                        return true;
                    }
                }
            }
            return false;

        }
        public  AppShell(IServiceProvider serviceProvider)
        {
            Routing.RegisterRoute("BilibiliScriptsPage", typeof(BilibiliScriptsPage));
            // Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            InitializeComponent();


        }
    }
}

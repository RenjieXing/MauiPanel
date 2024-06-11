
namespace BiliBiliMaui
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; set; }
        public App(IServiceProvider serviceProvider)
        {

            //初始化配置文件
            BilBilConfigSetting.initConfig();
            this.ServiceProvider = serviceProvider;
            
            InitializeComponent();
            MainPage = new AppShell(serviceProvider);


        }
        /// <summary>
        /// 每次进入程序时清理缓存
        /// </summary>
        /// <param name="activationState"></param>
        /// <returns></returns>
        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);

            window.Created += (s, e) =>
            {
                BilBilConfigSetting.CleanHelper.Clean();
            };

            return window;
        }

       


}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BiliBiliMaui
{
    public static class BilBilConfigSetting
    {

        public static bool IsLogin { get; set; } = false;

        public static string RouteMain { get; set; } = "Maui";
        public static string RouteLogin { get; set; } = Path.Combine(RouteMain, "Login");
        public static string FileName { get; set; } = Path.Combine(RouteLogin, "LOGIN.png");
        ////字段未启用
        //public static string UserName { get; set; } = "1479892004@qq.com";
        ////字段未启用
        //public static string Password { get; set; } = "";

        public static string SavePath { get; set; } = Path.Combine(RouteMain, "SaveData");

        public static CookieContainer? BilibiliCookie { get; set; }

        public static Cookie SESSDATA => BilibiliCookie is not null ?
            BilibiliCookie.GetAllCookies().Single(it => it.ToString().Contains("SESSDATA=")) : new Cookie("SESSDATA", "");


        /// <summary>
        /// 初始化配置,特定于Windows系统
        /// </summary>
        public static void initConfig()
        {


#if __WINDOWS__
                    
                        FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), FileName);
                        SavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), SavePath);

                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        string mauiPath = Path.Combine(desktopPath, "Maui");
                        string loginPath = Path.Combine(mauiPath, "Login");
                        string saveDataPath = Path.Combine(mauiPath, "SaveData");

                        // 创建Maui文件夹
                        if (!Directory.Exists(mauiPath))
                        {
                            Directory.CreateDirectory(mauiPath);
                        }

                        // 创建Login文件夹
                        if (!Directory.Exists(loginPath))
                        {
                            Directory.CreateDirectory(loginPath);
                        }

                        // 创建SaveData文件夹
                        if (!Directory.Exists(saveDataPath))
                        {
                            Directory.CreateDirectory(saveDataPath);
                        }

#endif // WINDOWS
#if __ANDROID__

            FileName = Path.Combine(FileSystem.AppDataDirectory, FileName);
            SavePath = Path.Combine(FileSystem.AppDataDirectory, SavePath);

            string desktopPath = FileSystem.AppDataDirectory;
            string mauiPath = Path.Combine(desktopPath, "Maui");
            string loginPath = Path.Combine(mauiPath, "Login");
            string saveDataPath = Path.Combine(mauiPath, "SaveData");

            // 创建Maui文件夹
            if (!Directory.Exists(mauiPath))
            {
                Directory.CreateDirectory(mauiPath);
            }

            // 创建Login文件夹
            if (!Directory.Exists(loginPath))
            {
                Directory.CreateDirectory(loginPath);
            }

            // 创建SaveData文件夹
            if (!Directory.Exists(saveDataPath))
            {
                Directory.CreateDirectory(saveDataPath);
            }

#endif //ANDROID

        }

        public static class BvAvConverter
        {
            private static long XOR_CODE = 23442827791579;
            private static long MASK_CODE = 2251799813685247;
            private static long MAX_AID = 1L << 51;
            private static string ALPHABET = "FcwAPNKTMug3GV5Lj7EJnHpWsx4tb8haYeviqBz6rkCy12mUSDQX9RdoZf";
            private static readonly int[] ENCODE_MAP = { 8, 7, 0, 5, 1, 3, 2, 4, 6 };
            private static readonly int[] DECODE_MAP = ENCODE_MAP.Select((x, i) => new { x, i }).OrderBy(x => x.x).Select(x => x.i).ToArray();
            private static string PREFIX = "BV1";
            private static int PREFIX_LEN = 3;
            private static int CODE_LEN = 9;
            private static int BASE => BvAvConverter.ALPHABET.Length;

            public static string Av2Bv(long aid)
            {
                char[] bvid = new char[CODE_LEN];
                long tmp = (MAX_AID | aid) ^ XOR_CODE;
                for (int i = 0; i < CODE_LEN; i++)
                {
                    bvid[ENCODE_MAP[i]] = ALPHABET[(int)(tmp % BASE)];
                    tmp /= BASE;
                }
                return PREFIX + new string(bvid);
            }

            public static long? Bv2Av(string bvid)
            {
                if (bvid.Substring(0, PREFIX_LEN) != PREFIX)
                    return null;

                bvid = bvid.Substring(PREFIX_LEN);
                long tmp = 0;
                for (int i = 0; i < CODE_LEN; i++)
                {
                    int idx = ALPHABET.IndexOf(bvid[DECODE_MAP[i]]);
                    tmp = tmp * BASE + idx;
                }
                return (tmp & MASK_CODE) ^ XOR_CODE;
            }
        }

        public static class CleanHelper
        {
            public static void Clean()
            {
                // 清理缓存
                var cachePath = SavePath;
                if (Directory.Exists(cachePath))
                {
                    Directory.Delete(cachePath, true);
                }
                Directory.CreateDirectory(cachePath);

            }
        }
    }
}

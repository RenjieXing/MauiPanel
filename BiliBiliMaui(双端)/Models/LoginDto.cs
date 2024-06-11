using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable IDE1006 // 命名样式
#pragma warning disable CS8618 // 标识符的大小写
namespace BiliBiliMaui.Models
{
    public class LoginQrDto
    {
        public long code { get; set; }
        public string message { get; set; }
        public long ttl { get; set; }
        public Data2 data { get; set; }
    }
    public class Data2
    { 
        /// <summary>
        /// ORScan
        /// </summary>
        public string url { get; set; }
        public string qrcode_key { get; set; }
    }

    public class LoginHashSetDto
    {
        public long code { get; set; }
        public string message { get; set; }
        public long ttl { get; set; }
        public LoginHashSetDataDto data { get; set; }
    }

    public class LoginHashSetDataDto
    {
        public string type { get; set; }
        public string token { get; set; }
        public Geetest geetest { get; set; }
        public Tencent tencent { get; set; }
    }

    public class Geetest
    {
        public string challenge { get; set; }
        public string gt { get; set; }
    }

    public class Tencent
    {
        public string appid { get; set; }
    }









    public class LoginResultDto
    {
        public long code { get; set; }
        public string message { get; set; }
        public long ttl { get; set; }
        public LoginResultDataDto data { get; set; }
    }

    public class LoginResultDataDto
    {
        public string url { get; set; }
        public string refresh_token { get; set; }
        public string timestamp { get; set; }
        public long code { get; set; }
        public string message { get; set; }
    }
}

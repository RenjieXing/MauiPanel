using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable IDE1006 // 命名样式
#pragma warning disable CS8618 // 标识符的大小写
namespace BiliBiliMaui.Models
{

    public class videoStreamUrl
    {
        public long code { get; set; }
        public string message { get; set; }
        public long ttl { get; set; }
        public videoStreamUrlData data { get; set; }
    }

    public class videoStreamUrlData
    {
        public string from { get; set; }
        public string result { get; set; }
        public string message { get; set; }
        public long quality { get; set; }
        public string format { get; set; }
        public long timelength { get; set; }
        public string accept_format { get; set; }
        public string[] accept_description { get; set; }
        public long[] accept_quality { get; set; }
        public long video_codecid { get; set; }
        public string seek_param { get; set; }
        public string seek_type { get; set; }
        public Durl[] durl { get; set; }
        public Support_Formats[] support_formats { get; set; }
        public object high_format { get; set; }
        public long last_play_time { get; set; }
        public long last_play_cid { get; set; }
    }

    public class Durl
    {
        public long order { get; set; }
        public long length { get; set; }
        public long size { get; set; }
        public string ahead { get; set; }
        public string vhead { get; set; }
        public string url { get; set; }
        public string[] backup_url { get; set; }
    }

    public class Support_Formats
    {
        public long quality { get; set; }
        public string? format { get; set; }
        public string? new_description { get; set; }
        public string? display_desc { get; set; }
        public string? superscript { get; set; }
        public object? codecs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable IDE1006 // 命名样式
#pragma warning disable CS8618 // 标识符的大小写
namespace BiliBiliMaui.Models
{
    public class VideosFirstPages
    {
        public long code { get; set; }
        public string message { get; set; }
        public long ttl { get; set; }
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        public long cid { get; set; }
        public long page { get; set; }
        public string from { get; set; }
        public string part { get; set; }
        public long duration { get; set; }
        public string vid { get; set; }
        public string weblink { get; set; }
        public Dimension dimension { get; set; }

        public string first_frame { get; set; }
    }

    public class Dimension
    {
        public long width { get; set; }
        public long height { get; set; }
        public long rotate { get; set; }
    }
}

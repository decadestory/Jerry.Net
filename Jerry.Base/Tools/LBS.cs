using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Jerry.Base.Tools
{
    public class LBS
    {
        /// <summary>
        /// 根据ip获取地址长串
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static BaiduLocation GetLocation(string ip)
        {
            var url = "http://api.map.baidu.com/location/ip?ak=F454f8a5efe5e577997931cc01de3974&ip=" + ip + "&coor=bd09l";
            var wc = new System.Net.WebClient { Encoding = Encoding.Default };

            var json = wc.DownloadString(url);

            return JsonConvert.DeserializeObject<BaiduLocation>(json);

        }
    }

    #region 返回地理位置类
    public class BaiduLocation
    {

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("content")]
        public Content Content { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
    public class Content
    {

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("address_detail")]
        public AddressDetail AddressDetail { get; set; }

        [JsonProperty("point")]
        public Point Point { get; set; }
    }
    public class AddressDetail
    {

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("city_code")]
        public int CityCode { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("street_number")]
        public string StreetNumber { get; set; }
    }
    public class Point
    {

        [JsonProperty("x")]
        public string X { get; set; }

        [JsonProperty("y")]
        public string Y { get; set; }
    }
    #endregion

}

using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Autotests.Utilities.ApiTestCore
{
    public class Api
    {
        private readonly string ApplicationBaseUrl;

        public Api(string value)
        {
            ApplicationBaseUrl = value;
        }

        public TResponse GET(string url, NameValueCollection data)
        {
            using (var client = new WebClient())
            {
                string dataString = "";
                foreach (string key in data.Keys)
                {
                    dataString = dataString + key + "=" + data[key] + "&";
                }
                dataString = dataString.Remove(dataString.Count() - 1);
                string response = client.DownloadString("http://" + ApplicationBaseUrl + ":80/" + url + "?" + dataString);
                return JsonSerializer(response);
            }
        }

        public TResponse POST(string url, NameValueCollection data)
        {
            using (var client = new WebClient())
            {
                byte[] responseJson = client.UploadValues("http://" + ApplicationBaseUrl + ":80/api/v1/" + url, data);
                return JsonSerializer(Encoding.UTF8.GetString(responseJson));
            }
        }

        private static TResponse JsonSerializer(string value)
        {
//            если тег response множественный то это идет рачте калькулятора 
            if (value.Contains(@"response"":["))
            {
                var json = new DataContractJsonSerializer(typeof (ResponseCalculation));
                return (ResponseCalculation) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
            var json2 = new DataContractJsonSerializer(typeof (ResponseAddObject));
            return (ResponseAddObject) json2.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
        }

        [DataContract]
        public class AddMessage
        {
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "key")]
            public string Key { get; set; }
        }

        [DataContract]
        public class MessageCalculation
        {
            [DataMember(Name = "delivery_company_name")]
            public string DeliveryCompanyName { get; set; }
        }

//            [DataMember(Name = "message")]
//            public MessageErrore MessageErrore { get; set; }
//            [DataMember(Name = "message")]
//            public string Message { get; set; }

        [DataContract]
        public class MessageErrore
        {
            [DataMember(Name = "city")]
            public string City { get; set; }
        }

        [DataContract]
        public class ResponseAddObject : TResponse
        {
            [DataMember(Name = "response")]
            public AddMessage ResponseMessage { get; set; }
        }

        [DataContract]
        public class ResponseCalculation : TResponse
        {
            [DataMember(Name = "response")]
            public MessageCalculation[] MessageCalculation { get; set; }
        }

        [DataContract]
        public class TResponse
        {
            [DataMember(Name = "success")]
            public bool Success { get; set; }
        }
    }
}
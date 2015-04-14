using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace Autotests.Utilities.ApiTestCore
{
    public class Api
    {
        private readonly string ApplicationBaseUrl;

        public Api(string value)
        {
            ApplicationBaseUrl = value;
        }

        public ApiResponse.Response GET(string url, NameValueCollection data)
        {
            using (var client = new WebClient())
            {
            
                string dataString = "";
                foreach (string key in data.Keys)
                {
                    dataString = dataString + key + "=" + data[key] + "&";
                }
                if (dataString.Count()!= 0) dataString = dataString.Remove(dataString.Count() - 1);

                if (ApplicationBaseUrl.Contains("@"))
                {
                    string[] words = ApplicationBaseUrl.Split(new char[] { ':', '@' });
                    client.Credentials = new NetworkCredential(words[0], words[1]);
                }
                string response = client.DownloadString("http://" + ApplicationBaseUrl + "/" + url + "?" + dataString);

                return JsonSerializer(response);
            }
        }

        public ApiResponse.Response POST(string url, NameValueCollection data)
        {
            using (var client = new WebClient())
            {
                if (ApplicationBaseUrl.Contains("@"))
                {
                    string[] words = ApplicationBaseUrl.Split(new char[] { ':', '@' });
                    client.Credentials = new NetworkCredential(words[0], words[1]);
                }
                byte[] responseJson = client.UploadValues("http://" + ApplicationBaseUrl + ":80/api/v1/" + url, data);
                return JsonSerializer(Encoding.UTF8.GetString(responseJson));
            }
        }

        private static ApiResponse.Response JsonSerializer(string value)
        {
//            ResponseCalculation если тег response множественный то это идет рачте калькулятора 
            if (value.Contains(@"response"":["))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseCalculation));
                return (ApiResponse.ResponseCalculation) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseFail
            if (value.Contains(@"success"":false,""response"":{""message"":""") )
            {
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponseFail));
                return (ApiResponse.ResponseFail)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseFailOrder
            if (value.Contains(@"{""success"":false,""response"":{""message"":{"))
            {
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponseFailOrder));
                return (ApiResponse.ResponseFailOrder)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseAddOrder
            if (value.Contains(@"success"":true,""response"":{""order"":"))
            {
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponseAddOrder));
                return (ApiResponse.ResponseAddOrder)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseStatus
            if (value.Contains(@"status") && value.Contains(@"status_description"))
            {
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponseStatus));
                return (ApiResponse.ResponseStatus)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseOrderInfo
            if (value.Contains(@"to_name") && value.Contains(@"to_phone"))
            {
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponseOrderInfo));
                return (ApiResponse.ResponseOrderInfo)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseTrueCancel
            if (value.Contains(@"success"":true,""response"":{""order_id") && value.Contains(@"ticket_id"))
            {
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponseTrueCancel));
                return (ApiResponse.ResponseTrueCancel)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseCompanyTerm
            if (value.Contains(@"success"":true,""response"":{") && value.Contains(@"term"))
            {
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponseCompanyTerm));
                return (ApiResponse.ResponseCompanyTerm)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseAddObject
            if (value.Contains(@"success"":true,""response"":{""_id"))
            {
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponseAddObject));
                return (ApiResponse.ResponseAddObject)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
            var json2 = new DataContractJsonSerializer(typeof (ApiResponse.Response));
            return (ApiResponse.Response) json2.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
        }
    }
}
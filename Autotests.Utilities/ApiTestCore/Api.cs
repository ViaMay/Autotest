using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Autotests.Utilities.ApiTestCore
{
    public class Api
    {
        public Api(string value)
        {
            ApplicationBaseUrl = value;
        }
        private readonly string ApplicationBaseUrl;

        public string GET(string url, string data)
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(url + "?" + data);
            }
        }

        public Response POST(string url, NameValueCollection data)
        {
            using (var client = new WebClient())
            {
                var responseJson = client.UploadValues("http://" + ApplicationBaseUrl + ":80/api/v1/" + url, data);
                return JsonSerializer(Encoding.UTF8.GetString(responseJson));
            }
        }

        [DataContract]
        public class Response
        {
            [DataMember(Name = "success")]
            public bool Success { get; set; }
            [DataMember(Name = "response")]
            public ResponseMessage ResponseMessage { get; set; }
        }
        [DataContract]
        public class ResponseMessage
        {
            [DataMember(Name = "message")]
            public MessageErrore MessageErrore { get; set; }
            [DataMember(Name = "_id")]
            public string Id { get; set; }
        }

        [DataContract]
        public class MessageErrore
        {
            [DataMember(Name = "city")]
            public string City { get; set; }
        }

        private static Response JsonSerializer(string value)
        {
            var json = new DataContractJsonSerializer(typeof(Response));
            return (Response)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
        }  
    }
}
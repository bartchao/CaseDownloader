using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace SourceDownloader.Utility
{

    public static class Downloader
    {

        public static T GetJsonByUrl<T>(string url) where T : class
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                //httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
                //{
                //    if (sslPolicyErrors == SslPolicyErrors.None)
                //    {
                //        return true;   //Is valid
                //    }

                //    if (cert.GetCertHashString() == "b3c9cb55da899860e9a3da20f594f1afce337767".ToUpper())
                //    {
                //        return true;
                //    }
                //    return false;
                //};
                using var httpClient = new HttpClient(httpClientHandler);
                try
                {
                    var stringTask = httpClient.GetStringAsync(url).Result;
                    var repositories = JsonSerializer.Deserialize<T>(stringTask);
                    return repositories;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e.Message);
                }


                return null;


            }
        }
        public static async Task DownloadTxtAsync(string url, string filename)
        {
            string prefix = "https://www.lawplus.com.tw/rest/search/download/report/";

            WebClient myWebClient = new WebClient();
            myWebClient.Headers.Add(HttpRequestHeader.UserAgent, "PostmanRuntime/7.28.4");
            url = HttpUtility.UrlEncode(url, System.Text.Encoding.UTF8);
            try
            {
                await myWebClient.DownloadFileTaskAsync(prefix+url, filename);
                
            }catch(WebException e)
            {
                Console.WriteLine(url);
                Console.WriteLine(e.Status);
                Console.WriteLine(e.Message);
            }
        }


    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChequeAPI.Helpers
{
    public class HttpHelper
    {
        public static string CallAPI<T>(string url, int APIcallType, T parameters)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                // List all Names.    
                HttpResponseMessage response = null;

                switch (APIcallType)
                {
                    case (int)Enums.APICall.Get:
                        response = GetAsync(url, client, parameters);
                        break;
                }

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return null;
                }
            }
        }
        static HttpResponseMessage GetAsync<T>(string url, HttpClient client, T Params)
        {               
            url = url + "?" + Params as string;
            // Add an Accept header for JSON format. 
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.Json));
            return client.GetAsync(url).Result;  // Blocking call!    
        }
    }
}

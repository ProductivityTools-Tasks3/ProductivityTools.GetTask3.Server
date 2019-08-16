using ProductivityTools.GetTask3.CommonConfiguration;
using ProductivityTools.GetTask3.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Client
{
    public class GetTaskHttpClient
    {
       // static string URL = @"https://GetTask3:5001/api/Task/";
        static string URL = Consts.EndpointAddress;


        public static async Task<T> Post2<T>(string controller, string action, object obj,Action<string> log)
        {
            log($"Performing Post under address {URL}");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL+controller+"/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            HttpResponseMessage response = await client.PostAsJsonAsync(action, obj);
            if (response.IsSuccessStatusCode)
            {
                T result = await response.Content.ReadAsAsync<T>();
                return result;
            }
            throw new Exception(response.ReasonPhrase);

        }

        private static T Post<T>(string action, string jsonContent)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            //HttpContent content = new StringContent("{Name: \"TaskOnSecondLevel2\",ParentId: 3 }", Encoding.UTF8, "application/json"); 
            HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json"); 
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // List data response.
            //HttpResponseMessage response = client.PostAsync("Add",content).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            HttpResponseMessage response = client.PostAsync(action, content).Result;  // Blocking call! Program will wait here until a 
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsAsync<T>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                client.Dispose();
                return dataObjects;
            }
            else
            {
                client.Dispose();
                throw new Exception(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
            }


            //Make any other calls using HttpClient here.

            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }
    }
}

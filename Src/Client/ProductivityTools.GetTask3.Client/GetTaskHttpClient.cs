using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Client
{
    public class GetTaskHttpClient
    {
        static string URL = @"https://localhost:44317/api/Task/";
        public static T Get<T>(string action, string query)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent content = new StringContent(@"{Name: ""TaskOnSecondLevel2"",ParentId: 3 }", Encoding.UTF32, "application/json"); ;


            // List data response.
            HttpResponseMessage response = client.PostAsJsonAsync("Add",content).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
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

using System;
using System.Net.Http;
using System.Web;

namespace LUIS_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write any utterance to test LUIS response, or \"-\" to exit:");

            string utterance;
            while ((utterance = Console.ReadLine()) != "-")
            {
                MakeRequest(utterance);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        static async void MakeRequest(string utterance)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            var luisAppId = "d25d92e4-cc7d-4449-b3cf-46142b6e1b42";
            var endpointKey = "284067cd474f42cd988fb3b2d33832f9";

            queryString["q"] = utterance;

            queryString["timezoneOffset"] = "0";
            queryString["verbose"] = "false";
            queryString["spellCheck"] = "false";
            queryString["staging"] = "false";

            var endpointUri = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/" + luisAppId + "?" + queryString;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", endpointKey);

                var response = await httpClient.GetAsync(endpointUri);

                var strResponseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine(strResponseContent.ToString());
            }
        }
    }
}

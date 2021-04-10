using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using System.Net.Http;
using System.Net;
using System.IO;
using Amazon.Lambda.APIGatewayEvents;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace newyorktimes
{
    public class Function
    {
        public static readonly HttpClient client = new HttpClient();

        static async Task<ExpandoObject> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {

            string newYorkApi = string.Format("https://api.nytimes.com/svc/books/v3/lists/current/hardcover-fiction.json?api-key=Kz6lZl5qkn6WrGSwQLing2HW7SsjTP0p");

            dynamic Object = new ExpandoObject();

            using var response = await client.GetAsync(newYorkApi);
            Object.List = await response.Content.ReadAsAsync<dynamic>();

            dynamic config = JsonConvert.DeserializeObject<ExpandoObject>(Object, new ExpandoObjectConverter());

            return config;
        }
    }
}


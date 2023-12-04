using InvestCloud._2DMatrix.ConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InvestCloud._2DMatrix.ConsoleApp.Services
{
    class NumbersApiClient : INumbersApiClient
    {
        public HttpClient GetNumbersHttpClient()
        {
            HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("https://recruitment-test.investcloud.com/");
            return client;
        }
    }
}

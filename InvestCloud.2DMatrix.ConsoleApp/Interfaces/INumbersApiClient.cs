using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InvestCloud._2DMatrix.ConsoleApp.Interfaces
{
    interface INumbersApiClient
    {
        HttpClient GetNumbersHttpClient();
    }
}

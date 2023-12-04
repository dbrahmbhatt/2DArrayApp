using InvestCloud._2DMatrix.ConsoleApp.Interfaces;
using InvestCloud._2DMatrix.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InvestCloud._2DMatrix.ConsoleApp.Services
{
    class MatrixProvider : IMatrixProvider
    {
        INumbersApiClient numbersApiClient;
        public MatrixProvider(INumbersApiClient numbersApiClient)
        {
            this.numbersApiClient = numbersApiClient;
        }
        public async Task<int[,]> GetMatrixDataset(string dataSetName)
        {
            HttpClient numbersClient = numbersApiClient.GetNumbersHttpClient();
            string jsonStringResult = await numbersClient.GetStringAsync(string.Format("api/numbers/init/{0}", Constants.DIMENTION_SIZE));
            ResultOfInt32 result = JsonSerializer.Deserialize<ResultOfInt32>(jsonStringResult);
            if (!result.Success)
                return null;

            string jsonRowsResult = await numbersClient.GetStringAsync(string.Format("api/numbers/{0}/{1}/{2}", dataSetName, Constants.ROW_TYPE, 0));
            ResultOfInt32Array rowsArray = JsonSerializer.Deserialize<ResultOfInt32Array>(jsonRowsResult);

            string jsonColumnsResult = await numbersClient.GetStringAsync(string.Format("api/numbers/{0}/{1}/{2}", dataSetName, Constants.COLUMN_TYPE, 0));
            ResultOfInt32Array columnsArray = JsonSerializer.Deserialize<ResultOfInt32Array>(jsonColumnsResult);

            int[,] matrixDataset = new int[Constants.DIMENTION_SIZE, Constants.DIMENTION_SIZE];
            for (int row = 0; row < Constants.DIMENTION_SIZE; row++)
            {
                int.TryParse(jsonStringResult, out matrixDataset[row, 0]);
                for (int col = 0; col < Constants.DIMENTION_SIZE; col++)
                {

                }
            }

            return matrixDataset;
        }
    }
}

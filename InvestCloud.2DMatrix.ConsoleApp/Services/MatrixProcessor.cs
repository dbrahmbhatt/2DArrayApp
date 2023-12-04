using InvestCloud._2DMatrix.ConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestCloud._2DMatrix.ConsoleApp.Services
{
    class MatrixProcessor : IMatrixProcessor
    {
        public int[,] MultiplyMatrix(int[,] inputA, int[,] inputB)
        {
            int datasetSize = inputA.GetUpperBound(0);
            int[,] resultMatrix = new int[datasetSize, datasetSize];
            for (int row = 0; row < datasetSize; row++)
            {
                for (int column = 0; column < datasetSize; column++)
                {
                    resultMatrix[row, column] = 0;
                    for (int i = 0; i < datasetSize; i++)
                        resultMatrix[row, column] += inputA[row, i] * inputB[i, column];
                }
            }
            return resultMatrix;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestCloud._2DMatrix.ConsoleApp.Interfaces
{
    interface IMatrixProcessor
    {
        int[,] MultiplyMatrix(int[,] inputA, int[,] inputB);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device1
{
    class Multiplicative_Inverse
    {
        int result = 0;
        int[,] matrix = new int[1000, 7];
        public int inverse(int m, int n)
        {
            matrix[0, 0] = 0;
            matrix[0, 1] = 1;
            matrix[0, 2] = 0;
            matrix[0, 3] = n;
            matrix[0, 4] = 0;
            matrix[0, 5] = 1;
            matrix[0, 6] = m;
            for (int i = 1; i < 1000; i++)
            {
                if (matrix[i - 1, 6] == 0)
                    return result = -1;
                matrix[i, 0] = matrix[i - 1, 3] / matrix[i - 1, 6];
                matrix[i, 1] = matrix[i - 1, 4];
                matrix[i, 2] = matrix[i - 1, 5];
                matrix[i, 3] = matrix[i - 1, 6];
                matrix[i, 4] = matrix[i - 1, 1] - (matrix[i, 0] * matrix[i - 1, 4]);
                matrix[i, 5] = matrix[i - 1, 2] - (matrix[i, 0] * matrix[i - 1, 5]);
                result = matrix[i, 5];
                matrix[i, 6] = matrix[i - 1, 3] - (matrix[i, 0] * matrix[i - 1, 6]);
                if (matrix[i, 6] == 1)
                    break;

            }
            if (result < 0)
                return result = (result % n) + n;
            return result;
        }
    }
}

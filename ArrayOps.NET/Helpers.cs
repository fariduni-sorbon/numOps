using System;
using System.Collections.Generic;

namespace NumOps
{
    public static class Helpers
    {

        public static int showDIm(int[,] matrix, int i)
        {
            return matrix.GetLength(i);
        }

        public static int Sum(int[,] matrix)
        {
            int res = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    res += matrix[i, j];
                }
            }

            return res;
        }

        public static int Avg(int[,] matrix)
        {
            var res = Sum(matrix) / matrix.Length;
            return res;
        }

        public static int Min(int[,] matrix)
        {
            int res = matrix[0, 0];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (res > matrix[i, j])
                    {
                        res = matrix[i, j];
                    }
                }
            }
            return res;
        }

        public static int Max(int[,] matrix)
        {
            int res = matrix[0, 0];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (res < matrix[i, j])
                    {
                        res = matrix[i, j];
                    }
                }
            }
            return res;
        }

        //--- Matrix Sorting ---------------------

        static void SortArray(int[] array) => Array.Sort<int>(array);

        static int[] MatrixToArray(int[,] matrix, int row, int column)
        {
            var res = new int[row * column];
            int counter = 0;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    res[counter] = matrix[i, j];
                    counter++;
                }
            }
            return res;
        }

        static int[,] ArrayToMatrix(int[] array, int row, int column)
        {
            int[,] res = new int[row, column];
            int counter = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    res[i, j] = array[counter];
                    counter++;
                }
            }
            return res;
        }

        public static List<DataItem> SortMatrix(int[,] matrix, int row, int column)
        {
            List<DataItem> res = new List<DataItem>();
            var arr = MatrixToArray(matrix, row, column);

            SortArray(arr);

            var twoDim = ArrayToMatrix(arr, row, column);

            for (int i = 0; i < twoDim.GetLength(0); i++)
            {
                var data = new DataItem
                {
                    a1 = twoDim[i, 0],
                    a2 = twoDim[i, 1],
                    a3 = twoDim[i, 2],
                    a4 = twoDim[i, 3],
                    a5 = twoDim[i, 4]
                };
                res.Add(data);
            }

            return res;
        }
    }
}

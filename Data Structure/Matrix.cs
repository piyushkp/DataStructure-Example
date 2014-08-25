using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tree
{
    public class Matrix
    {
        //Matrix Region Sum
        private int[][] matrix;
        private long[][] sumMatrix;
        public void MatrixRegionSum(int[][] matrix)
        {
            if (matrix == null)
            {
                throw new Exception("null matrix is not allowed.");
            }
            this.matrix = matrix;
            this.sumMatrix = new long[matrix.Length][];
            preComputeSums();
        }
        private void preComputeSums()
        {
            for (int col = 0; col < matrix[0].Length; col++)
            {
                sumMatrix[0][col] += matrix[0][col];
            }
            for (int row = 1; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[0].Length; col++)
                {
                    sumMatrix[row][col] = sumMatrix[row - 1][col]
                    + matrix[row][col];
                }
            }
            for (int row = matrix.Length - 1; row >= 0; row--)
            {
                for (int col = 1; col < matrix[0].Length; col++)
                {
                    sumMatrix[row][col] += sumMatrix[row][col - 1];
                }
            }
        }
        // (lx, ly) is the top left co-ordinate of the rectangle.
        // (rx, ry) is the bottom right co-ordinate of the rectangle.
        public long findSum(int lx, int ly, int rx, int ry)
        {
            if (!valid(lx, ly) || !valid(rx, ry))
            {
                throw new Exception(
                String.Format(
                "The co-ordinates: (%d, %d), (%d, %d) are not valid co-ordinates.",
                lx, ly, rx, ry));
            }
            long sum = sumMatrix[rx][ry];
            sum -= (ly == 0 ? 0 : sumMatrix[rx][ly - 1]);
            sum -= (lx == 0 ? 0 : sumMatrix[lx - 1][ry]);
            sum += (lx == 0 || ly == 0 ? 0 : sumMatrix[lx - 1][ly - 1]);
            return sum;
        }
        public bool valid(int x, int y)
        {
            return x >= 0 && x < matrix.Length && y >= 0 && y < matrix[0].Length;
        }
    }
}

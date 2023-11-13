using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibRun8.Utils
{
    public class Utils
    {
        public static byte[] ComputeMD5ForBytes(byte[] data)
        {
            byte[] bytes;
            using (MD5 md = MD5.Create())
            {
                bytes = Encoding.ASCII.GetBytes(BitConverter.ToString(md.ComputeHash(data)).Replace("-", string.Empty));
            }
            return bytes;
        }

        public static bool CompareByteArrays(byte[] arr1, byte[] arr2)
        {
            if (arr1 == null || arr2 == null)
            {
                return false;
            }
            if (arr1.Length == arr2.Length)
            {
                for (int i = 0; i < arr1.Length; i++)
                {
                    if (arr1[i] != arr2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public static T[][] ConvertToJaggedArray<T>(T[,] twoDArray)
        {
            int rows = twoDArray.GetLength(0);
            int cols = twoDArray.GetLength(1);

            // Initialize the jagged array
            T[][] jaggedArray = new T[rows][];

            // Populate the jagged array
            for (int i = 0; i < rows; i++)
            {
                jaggedArray[i] = new T[cols];
                for (int j = 0; j < cols; j++)
                {
                    jaggedArray[i][j] = twoDArray[i, j];
                }
            }

            return jaggedArray;
        }
    }
}

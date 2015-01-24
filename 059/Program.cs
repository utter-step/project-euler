using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Tools;

namespace _059
{
    class Program
    {
        private const string Filepath = "../../cipher1.txt";
        private const byte AsciiSpace = 0x20;
        private const byte AsciiLowerA = 0x61;
        private const byte AsciiLowerZ = 0x7a;

        static void Main(string[] args)
        {
            var cipherText = ReadFile();

            Decorators.Benchmark(Solve, cipherText);
        }

        private static int Solve(byte[] cipherText)
        {
            var decrypted = Decrypt(cipherText);

            int sum = 0;

            for (int i = 0; i < decrypted.Length; i++)
            {
                sum += decrypted[i];
            }

            return sum;
        }

        private static byte[] Decrypt(byte[] cipherText)
        {
            int oneThird = cipherText.Length / 3;

            var byte1 = new byte[oneThird];
            var byte2 = new byte[oneThird];
            var byte3 = new byte[oneThird];

            for (int i = 0; i < oneThird; i++)
            {
                byte1[i] = cipherText[i * 3];
                byte2[i] = cipherText[i * 3 + 1];
                byte3[i] = cipherText[i * 3 + 2];
            }

            var freq1 = new int[256];
            var freq2 = new int[256];
            var freq3 = new int[256];

            for (int i = 0; i < oneThird; i++)
            {
                freq1[byte1[i]]++;
                freq2[byte2[i]]++;
                freq3[byte3[i]]++;
            }

            var key = new byte[3];
            key[0] = (byte)(MaxIndex(freq1) ^ AsciiSpace);
            key[1] = (byte)(MaxIndex(freq2) ^ AsciiSpace);
            key[2] = (byte)(MaxIndex(freq3) ^ AsciiSpace);

            return XorArray(cipherText, key);
        }

        private static int MaxIndex(int[] array)
        {
            int max = 0;
            int res = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                    res = i;
                }
            }

            return res;
        }

        private static byte[] XorArray(byte[] array, byte[] key)
        {
            var res = new byte[array.Length];
            Buffer.BlockCopy(array, 0, res, 0, array.Length);

            for (int i = 0; i < res.Length; i++)
            {
                res[i] ^= key[i % key.Length];
            }

            return res;
        }

        private static byte[] ReadFile()
        {
            var file = new StreamReader(Filepath);

            return file.ReadToEnd().Split(',').Select(byte.Parse).ToArray();
        }
    }
}

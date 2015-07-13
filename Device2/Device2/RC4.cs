using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device2
{
    class RC4
    {
        int keylength = 0;
        private string HexString2Ascii(string hexString)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= hexString.Length - 2; i += 2)
            {
                sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
            }
            return sb.ToString();
        }
        int[] S = new int[256];
        char[] T = new char[256];
        int[] keyStream;

        public int[] generateKeyStream(int plaintextlength, char[] key)
        {

            for (int i = 0; i < 256; i++)
            {
                S[i] = i;
            }
            for (int i = 0; i < key.Length; i++)
            {
                T[i] = key[i];
            }
            int k = 0;
            for (int i = key.Length; i < 256; i++)
            {
                T[i] = T[k];
                k++;
            }
            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + T[i]) % 256;
                int tmp = S[i];
                S[i] = S[j];
                S[j] = tmp;
            }
            int a = 0, b = 0;
            keyStream = new int[plaintextlength];
            while (plaintextlength != 0)
            {
                a = (a + 1) % 256;
                b = (b + S[a]) % 256;
                int tmp = S[a];
                S[a] = S[b];
                S[b] = tmp;
                keyStream[a - 1] = S[(S[a] + S[b]) % 256];
                plaintextlength--;
            }
            return keyStream;
        }

        string ciphertextresult = "";
        public string encryption(char[] plaintext, int plaintextlength, char[] key, int[] result)
        {
            for (int i = 0; i < plaintext.Length; i++)
            {
                ciphertextresult += ((plaintext[i] ^ result[i]).ToString("X"));
                ciphertextresult += "%";
            }

            return ciphertextresult + plaintextlength;
        }
        string plaintext = "";
        public string decryption(string[] ciphertext, char[] key, int[] result)
        {
            for (int i = 0; i < result.Length; i++)
            {
                //string g=string.Concat(ciphertext[k],ciphertext[k+1]);
                string[] d = new string[200]; ;
                d[i] = HexString2Ascii(ciphertext[i]);
                plaintext = plaintext + ((char)((Convert.ToChar(d[i]) ^ result[i])));
            }
            return plaintext;
        }
    }
}

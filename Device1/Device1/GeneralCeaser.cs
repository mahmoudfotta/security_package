using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device1
{
    class GeneralCeaser : secpac
    {
        string ciphertext, plaintext;
        char[] charplain_ciphertext = new char[100];
        public string encryption(string plaintext, string key)
        {
            for (int i = 0; i < plaintext.Length; i++)
            {
                charplain_ciphertext[i] = Convert.ToChar((((int)plaintext[i] - 97 + Convert.ToInt32(key)) % 26) + 65);
            }
            ciphertext = new string(charplain_ciphertext);
            return ciphertext;
        }
        public string decryption(string ciphertext, string key)
        {
            for (int i = 0; i < ciphertext.Length; i++)
            {
                charplain_ciphertext[i] = Convert.ToChar(((((int)ciphertext[i] - 65 - Convert.ToInt32(key)) + 26) % 26) + 97);
            }
            plaintext = new string(charplain_ciphertext);
            return plaintext;
        }
    }
}

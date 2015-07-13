using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device1
{
    class Vigenère : secpac
    {
        string newkey = null;
        string ciphertext, plaintext;
        char[] charplain_ciphertext = new char[100];
        public string Autokey(string plaintext, string key)
        {
            newkey = string.Concat(key, plaintext);
            newkey = newkey.Remove(plaintext.Length);
            return newkey;
        }
        public string repetingkey(string plaintext, string key)
        {
            for (int i = 0; i < 20; i++)
            {
                newkey = string.Concat(key, key);
                if(newkey.Length < plaintext.Length)
                    newkey = string.Concat(newkey, key);
                if (newkey.Length > plaintext.Length)
                    break;
            }
            newkey = newkey.Remove(plaintext.Length);
            return newkey;
        }
        public string encryption(string plaintext, string key)
        {
            for (int i = 0; i < plaintext.Length; i++)
            {
                charplain_ciphertext[i] = Convert.ToChar(((((int)plaintext[i] - 97) + ((int)key[i] - 97)) % 26) + 65);//keyandtext.tolower
            }
            ciphertext = new string(charplain_ciphertext);
            return ciphertext;
        }

        public string decryption(string ciphertext, string key)
        {
            for (int i = 0; i < ciphertext.Length; i++)
            {
                charplain_ciphertext[i] = Convert.ToChar((((((int)ciphertext[i] - 65) - ((int)key[i] - 65)) + 26) % 26) + 97);//keyandtext.toupper
            }
            plaintext = new string(charplain_ciphertext);
            return plaintext;
        }
    }
}

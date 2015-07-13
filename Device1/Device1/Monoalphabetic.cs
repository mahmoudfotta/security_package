using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device1
{
    class Monoalphabetic
    {
        private string Outputtext;
        private Boolean Encryption = false;
        private Boolean Decryption = false;
        Dictionary<char, char> alphabet;
        public string EncryptionMethod(string key, string plaintext)
        {
            Encryption = true;
            Process(key, plaintext);
            Encryption = false;
            return Outputtext;
        }
        public string DecryptionMethod(string key, string ciphertext)
        {
            Decryption = true;
            Process(key, ciphertext);
            Decryption = false;
            return Outputtext;
        }
        private void Process(string key, string InputText)
        {
            Outputtext = "";
            CreatKey(key);
            if (Encryption)
            {
                for (int i = 0; i < InputText.Length; i++)
                {
                    Outputtext+= alphabet[InputText[i]];
                }
            }
            else if (Decryption)
            {
                Keyinvers();
                for (int i = 0; i < InputText.Length; i++)
                {
                    Outputtext += alphabet[InputText[i]];
                }
            }
        }
        private void CreatKey(string key)
        {
            alphabet = new Dictionary<char, char>();
            char c = 'a';
            for (int i = 0; i < 26; i++)
            {
                alphabet.Add(c, key[i]);
                c++;
            }
        }
        private void Keyinvers()
        {
            Dictionary<char, char> alphabet2 = new Dictionary<char, char>();
            char c = 'a';
            for (int i = 0; i < 26; i++)
            {
                alphabet2.Add(alphabet[alphabet.Keys.ElementAt(i)], alphabet.Keys.ElementAt(i));
            }
            alphabet = alphabet2;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device1
{
    class RailFence
    {
        private string Outputtext;
        private Boolean Encryption = false;
        private Boolean Decryption = false;
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
            int h, w, m = 0;
            h = Convert.ToInt32(key);
            w = InputText.Length / h; 
            char[] InputArray = InputText.ToCharArray();
            if (InputText.Length % h != 0)
            {
                w++;
                m = InputText.Length % h;
            }
            char[,] text = new char[h, w];
            int count;
            if (Encryption)
            {
                count = 0;
                Outputtext = "";
                for (int i=0;i<w;i++)
                {
                    for (int j=0;j<h;j++)
                    {
                        if (count!=InputText.Length)
                            text[j,i] = InputArray[count];
                        count++;
                    }
                }
                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        if (Outputtext.Length!=InputText.Length)
                            Outputtext += text[i,j];
                    }
                }
            }
            else if (Decryption)
            {
                count = 0;
                Outputtext = "";
                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        if (count != InputText.Length)
                            text[i, j] = InputArray[count];
                        count++;
                    }
                }
                for (int j = 0; j < w; j++)
                {
                    for (int i = 0; i < h; i++)
                    {
                        if (Outputtext.Length != InputText.Length)
                            Outputtext += text[i, j];
                    }
                }
            }
        }
    }
}

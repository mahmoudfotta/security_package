using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device2
{
    class Columnar
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
            string In_Text = "";
            for (int i = 0; i < InputText.Length; i++)
            {
                if (InputText[i] != '\0')
                    In_Text += InputText[i];
            }
            Outputtext = "";
            int h, w, m = 0;
            w = key.Length;
            h = In_Text.Length / w;
            char[] InputArray = In_Text.ToCharArray();
            if (In_Text.Length % w != 0)
            {
                h++;
                m = In_Text.Length % w;
            }
            char[,] text = new char[h, w];
            int[] KeyIndex = new int[w];
            for (int i = 0; i < w; i++)
            {
                KeyIndex[i] = key[i] - 48;
            }
            int count = 0;
            if (Encryption)
            {
                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        if (count != In_Text.Length)
                            text[i, j] = InputArray[count];
                        count++;
                    }
                }
                count = 0;
                for (int i = 0; i < w; i++)
                {
                    for (int k = 0; k < w; k++)
                    {
                        if (KeyIndex[k] == i + 1)
                        {
                            for (int j = 0; j < h; j++)
                            {
                                if (text[j, k] != '\0')
                                    Outputtext += text[j, k];
                            }
                        }
                    }
                }
            }
            else if (Decryption)
            {
                count = 0;
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        if (j == h - 1 && i >= w - m)
                            break;
                        text[j, (KeyIndex[i] - 1)] = InputArray[count];
                        count++;
                    }
                }
                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        int index = 0;
                        for (int k = 0; k < w; k++)
                        {
                            if ((KeyIndex[k] - 1) == j)
                            {
                                index = k;
                                break;
                            }
                        }
                        Outputtext += text[i, index];
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device1
{
    class PlayFair
    {
        char[,] matrix = new char[5, 5];
        char[] newkeychar = new char[50];
        char[] newkeychar2 = new char[26];
        char[] finalkeychar = new char[50];
        public char[,] generateMatrixKey(string key)//key to upper
        {
            newkeychar = key.ToCharArray().Distinct().ToArray();
            int k = 0;
            for (int i = 0; i <= 25; i++)
            {
                newkeychar2[i] = Convert.ToChar(i + 65);
            }
            finalkeychar = newkeychar.Concat(newkeychar2).ToArray();
            finalkeychar = finalkeychar.Distinct().ToArray();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (finalkeychar[k] != 'J')
                    {
                        matrix[i, j] = finalkeychar[k];
                    }
                    else
                        j--;
                    k++;
                }
            }
            return matrix;
        }
        string finalciphertext;
        public string encryption(string plaintext, char[,] keymatrix)
        {
            for (int i = 0; i < plaintext.Length - 1; i++)
            {
                if (plaintext[i] == plaintext[i + 1])
                {
                    plaintext = plaintext.Insert(i + 1, "X");
                }
            }
            if (plaintext.Length % 2 != 0)
            {
                plaintext = plaintext.Insert(plaintext.Length, "X");
            }
            int x1 = 0, x2 = 0, y1 = 0, y2 = 0;
            char[] plainchrarray = new char[plaintext.Length];
            char[] ciphertext = new char[plaintext.Length];
            int t = 0;
            plainchrarray = plaintext.ToCharArray();
            for (int z = 0; z < plainchrarray.Length - 1; z += 2)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (plainchrarray[z] == matrix[i, j])
                        {
                            x1 = i;
                            y1 = j;
                        }
                        if (plainchrarray[z + 1] == matrix[i, j])
                        {
                            x2 = i;
                            y2 = j;
                        }
                    }
                }

                if (x1 == x2)
                {
                    if (y1 + 1 == 5)
                        y1 = -1;
                    if (y2 + 1 == 5)
                        y2 = -1;
                    ciphertext[t] = matrix[x1, y1 + 1];
                    t++;
                    ciphertext[t] = matrix[x2, y2 + 1];
                    t++;
                }
                else if (y1 == y2)
                {
                    if (x1 + 1 == 5)
                        x1 = -1;
                    if (x2 + 1 == 5)
                        x2 = -1;
                    ciphertext[t] = matrix[x1 + 1, y1];
                    t++;
                    ciphertext[t] = matrix[x2 + 1, y2];
                    t++;
                }
                else
                {
                    ciphertext[t] = matrix[x1, y2];
                    t++;
                    ciphertext[t] = matrix[x2, y1];
                    t++;
                }

                x1 = 0;
                y1 = 0;
                x2 = 0;
                y2 = 0;
            }
            finalciphertext = new string(ciphertext);
            return finalciphertext;
        }
        string finalplaintext;
        public string decryption(string ciphertextt, char[,] keymatrix)
        {
            //ciphertext = new string();
            int x1 = 0, x2 = 0, y1 = 0, y2 = 0;
            char[] plainchrarray = new char[ciphertextt.Length];
            char[] plaintext = new char[ciphertextt.Length];
            int t = 0;
            plainchrarray = ciphertextt.ToCharArray();
            for (int z = 0; z < plainchrarray.Length - 1; z += 2)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (plainchrarray[z] == matrix[i, j])
                        {
                            x1 = i;
                            y1 = j;
                        }
                        if (plainchrarray[z + 1] == matrix[i, j])
                        {
                            x2 = i;
                            y2 = j;
                        }
                    }
                }

                if (x1 == x2)
                {
                    if (y1 - 1 == -1)
                        y1 = 5;
                    if (y2 - 1 == -1)
                        y2 = 5;
                    plaintext[t] = matrix[x1, y1 - 1];
                    t++;
                    plaintext[t] = matrix[x2, y2 - 1];
                    t++;
                }
                else if (y1 == y2)
                {
                    if (x1 - 1 == -1)
                        x1 = 5;
                    if (x2 - 1 == -1)
                        x2 = 5;
                    plaintext[t] = matrix[x1 - 1, y1];
                    t++;
                    plaintext[t] = matrix[x2 - 1, y2];
                    t++;
                }
                else
                {
                    plaintext[t] = matrix[x1, y2];
                    t++;
                    plaintext[t] = matrix[x2, y1];
                    t++;
                }

                x1 = 0;
                y1 = 0;
                x2 = 0;
                y2 = 0;
            }
            finalplaintext = new string(plaintext);
            return finalplaintext;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Device2
{
    class HillCipher
    {
        Dictionary<char, int> alphabet;
        public HillCipher()
        {
            alphabet = new Dictionary<char, int>();
            char c = 'a';
            alphabet.Add(c, 0);
            for (int i = 1; i < 26; i++)
            {
                alphabet.Add(++c, i);
            }
        }
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
            int[,] KeyMatrix = KeyParse(key);
            int R, I, M;
            M = KeyMatrix.GetLength(0);
            I = In_Text.Length / M;
            if (In_Text.Length % M != 0)
            {
                I += 1;
                R = In_Text.Length % M;
                for (int i = 0; i < R; i++)
                    In_Text += 'x';
            }
            int[,] InputTextIndex = new int[M, I];
            for (int i = 0; i < I; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    InputTextIndex[j, i] = alphabet[In_Text[(i * M) + j]];
                }
            }
            if (Encryption)
            {
                for (int i = 0; i < I; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        int sum = 0;
                        for (int k = 0; k < M; k++)
                            sum += KeyMatrix[j, k] * InputTextIndex[k, i];
                        Outputtext += alphabet.Keys.ElementAt(sum % 26);
                    }
                }
            }
            else if (Decryption)
            {
                int b = FindB(det(KeyMatrix));
                int h = KeyMatrix.GetLength(0);
                int w = KeyMatrix.GetLength(1);
                int[,] Key_Matrix = new int[h, w];
                int sign = 1;
                if (h == w)
                {
                    for (int i = 0; i < h; i++)
                    {
                        for (int j = 0; j < w; j++)
                        {
                            Key_Matrix[i, j] = sign*b * (det(Minor(KeyMatrix, i, j)));
                            Key_Matrix[i, j] = Key_Matrix[i, j] % 26;
                            if (Key_Matrix[i, j] < 0)
                                Key_Matrix[i, j] = 26 + Key_Matrix[i, j];
                            sign = sign * -1;
                        }
                    }
                    for (int i = 0; i < h; i++)
                    {
                        for (int j = 0; j < w; j++)
                        {
                            KeyMatrix[i, j] = Key_Matrix[j, i];
                        }
                    }
                    for (int i = 0; i < I; i++)
                    {
                        for (int j = 0; j < M; j++)
                        {
                            int sum = 0;
                            for (int k = 0; k < M; k++)
                                sum += KeyMatrix[j, k] * InputTextIndex[k, i];
                            Outputtext += alphabet.Keys.ElementAt(sum % 26);
                        }
                    }

                }
            }
        }
        private int det(int[,] matrix)
        {
            int determinant = 0;
            int h = matrix.GetLength(0);
            int w = matrix.GetLength(1);
            if (h == w)
            {
                if (h == 1)
                    return matrix[0, 0];
                else if (h == 3)
                {
                    determinant = matrix[0, 0] * (matrix[1, 1] * matrix[2, 2] - matrix[2, 1] * matrix[1, 2])
                            - 1 * matrix[0, 1] * (matrix[1, 0] * matrix[2, 2] - matrix[2, 0] * matrix[1, 2])
                                + matrix[0, 2] * (matrix[1, 0] * matrix[2, 1] - matrix[1, 1] * matrix[2, 0]);
                }
                else if (h == 2)
                {
                    determinant = (matrix[0, 0] * matrix[1, 1]) - (matrix[0, 1] * matrix[1, 0]);
                }
            }
            return determinant;
        }
        private int FindB(int Det)
        {
            Det = Det % 26;
            if (Det < 0)
                Det = 26 + Det;
            int result = 0;
            for (int i = 2; i < 26; i++)
            {
                if (((i * Det) % 26) == 1)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
        private int[,] KeyParse(string key)
        {
            int[,] result;
            string[] keyLine = key.Split('\n');
            string[] keyElement = keyLine[0].Split(' ');
            result = new int[keyLine.Length, keyElement.Length];
            for (int i = 0; i < keyLine.Length; i++)
            {
                keyElement = keyLine[i].Split(' ');
                for (int j = 0; j < keyElement.Length; j++)
                {
                    result[i, j] = Convert.ToInt32(keyElement[j]);
                }
            }
            return result;
        }
        private int[,] Minor(int[,] Matrix, int iRow, int iCol)
        {
            int[,] minor = new int[Matrix.GetLength(0) - 1, Matrix.GetLength(1) - 1];
            int m = 0, n = 0;
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                if (i == iRow)
                    continue;
                n = 0;
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (j == iCol)
                        continue;
                    minor[m, n] = Matrix[i, j];
                    n++;
                }
                m++;
            }
            return minor;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Device2
{
    class RSA 
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
            int P = 0, Q = 0, M, E = 0, D;
            M = Convert.ToInt32(In_Text);
            ParseKey(key, ref P, ref Q, ref E);
            int N = P * Q;
            int F_N = (P - 1) * (Q - 1);
            List<int> PrimeNumber = new List<int>();
            if (Encryption)
            {
                Outputtext = modpow(M, E, N).ToString();
                //Outputtext = ((Math.Pow(M, E)) % N).ToString();
            }
            else if (Decryption)
            {
                int[,] Arr = new int[100, 7];
                Arr[0, 1] = 1;
                Arr[0, 2] = 0;
                Arr[0, 3] = F_N;
                Arr[0, 4] = 0;
                Arr[0, 5] = 1;
                Arr[0, 6] = E;
                int count = 1;
                while (true)
                {
                    Arr[count, 0] = Arr[count - 1, 3] / Arr[count - 1, 6];
                    Arr[count, 1] = Arr[count - 1, 4];
                    Arr[count, 2] = Arr[count - 1, 5];
                    Arr[count, 3] = Arr[count - 1, 6];
                    Arr[count, 4] = Arr[count - 1, 1] - (Arr[count - 1, 4] * Arr[count, 0]);
                    Arr[count, 5] = Arr[count - 1, 2] - (Arr[count - 1, 5] * Arr[count, 0]);
                    Arr[count, 6] = Arr[count - 1, 3] % Arr[count - 1, 6];
                    if (Arr[count, 6] == 1)
                    {
                        D = Arr[count, 5];
                        break;
                    }
                    else if (Arr[count, 6] == 0)
                    {
                        D = -1;
                        break;
                    }
                    count++;
                }
                if (D != -1)
                {

                    Outputtext = modpow(M, D, N).ToString();
                }
            }
        }
        private void ParseKey(string key, ref int P, ref int Q, ref int E)
        {
            string[] arr = key.Split('\n');
            P = Convert.ToInt32(arr[0]);
            Q = Convert.ToInt32(arr[1]);
            E = Convert.ToInt32(arr[2]);
        }
        int modpow(int b, int exponent, int modulus)
        {

            int result = 1;

            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                {
                    // multiply in this bit's contribution while using modulus to keep result small
                    result = (result * b) % modulus;
                }
                // move to the next bit of the exponent, square (and mod) the base accordingly
                exponent >>= 1;
                b = (b * b) % modulus;
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device1
{
    interface secpac
    {
        string encryption(string plaintext, string key);
        string decryption(string plaintext, string key);
    }
}

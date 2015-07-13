using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device2
{
    class AES
    {
        private string Outputtext;
        private Boolean Encryption = false;
        private Boolean Decryption = false;
        string[,] ScheduleMatrix = new string[4, 44];
        #region S-Box and Inverse S-Box
        public string[,] sbox = new string[,] { 
            {"63","7c","77","7b","f2","6b","6f","c5","30","01","67","2b","fe","d7","ab","76"},
            {"ca","82","c9","7d","fa","59","47","f0","ad","d4","a2","af","9c","a4","72","c0"},
            {"b7","fd","93","26","36","3f","f7","cc","34","a5","e5","f1","71","d8","31","15"},
            {"04","c7","23","c3","18","96","05","9a","07","12","80","e2","eb","27","b2","75"},
            {"09","83","2c","1a","1b","6e","5a","a0","52","3b","d6","b3","29","e3","2f","84"},
            {"53","d1","00","ed","20","fc","b1","5b","6a","cb","be","39","4a","4c","58","cf"},
            {"d0","ef","aa","fb","43","4d","33","85","45","f9","02","7f","50","3c","9f","a8"},
            {"51","a3","40","8f","92","9d","38","f5","bc","b6","da","21","10","ff","f3","d2"},
            {"cd","0c","13","ec","5f","97","44","17","c4","a7","7e","3d","64","5d","19","73"},
            {"60","81","4f","dc","22","2a","90","88","46","ee","b8","14","de","5e","0b","db"},
            {"e0","32","3a","0a","49","06","24","5c","c2","d3","ac","62","91","95","e4","79"},
            {"e7","c8","37","6d","8d","d5","4e","a9","6c","56","f4","ea","65","7a","ae","08"},
            {"ba","78","25","2e","1c","a6","b4","c6","e8","dd","74","1f","4b","bd","8b","8a"},
            {"70","3e","b5","66","48","03","f6","0e","61","35","57","b9","86","c1","1d","9e"},
            {"e1","f8","98","11","69","d9","8e","94","9b","1e","87","e9","ce","55","28","df"},
            {"8c","a1","89","0d","bf","e6","42","68","41","99","2d","0f","b0","54","bb","16"}};
        public string[,] inverse_sbox = new string[,] { 
            {"52","09","6a","d5","30","36","a5","38","bf","40","a3","9e","81","f3","d7","fb"},
            {"7c","e3","39","82","9b","2f","ff","87","34","8e","43","44","c4","de","e9","cb"},
            {"54","7b","94","32","a6","c2","23","3d","ee","4c","95","0b","42","fa","c3","4e"},
            {"08","2e","a1","66","28","d9","24","b2","76","5b","a2","49","6d","8b","d1","25"},
            {"72","f8","f6","64","86","68","98","16","d4","a4","5c","cc","5d","65","b6","92"},
            {"6c","70","48","50","fd","ed","b9","da","5e","15","46","57","a7","8d","9d","84"},
            {"90","d8","ab","00","8c","bc","d3","0a","f7","e4","58","05","b8","b3","45","06"},
            {"d0","2c","1e","8f","ca","3f","0f","02","c1","af","bd","03","01","13","8a","6b"},
            {"3a","91","11","41","4f","67","dc","ea","97","f2","cf","ce","f0","b4","e6","73"},
            {"96","ac","74","22","e7","ad","35","85","e2","f9","37","e8","1c","75","df","6e"},
            {"47","f1","1a","71","1d","29","c5","89","6f","b7","62","0e","aa","18","be","1b"},
            {"fc","56","3e","4b","c6","d2","79","20","9a","db","c0","fe","78","cd","5a","f4"},
            {"1f","dd","a8","33","88","07","c7","31","b1","12","10","59","27","80","ec","5f"},
            {"60","51","7f","a9","19","b5","4a","0d","2d","e5","7a","9f","93","c9","9c","ef"},
            {"a0","e0","3b","4d","ae","2a","f5","b0","c8","eb","bb","3c","83","53","99","61"},
            {"17","2b","04","7e","ba","77","d6","26","e1","69","14","63","55","21","0c","7d"}};
        #endregion
        #region MixColumns Transform Matrix
        public string[,] MixColumnFactor = new string[,]{
            { "02","03", "01", "01" },
            { "01","02", "03", "01"},
            { "01","01", "02", "03"},
            { "03","01", "01", "02"}
            };

        public string[,] Inverse_MixColumnFactor = new string[,]{
            { "0E","0B", "0D", "09" },
            { "09","0E", "0B", "0D"},
            { "0D","09", "0E", "0B"},
            { "0B","0D", "09", "0E"}
            };
        #endregion
        #region Rcon
        public string[,] Rcon = new string[,]{
        {"01","02","04","08","10","20","40","80","1b","36"},
        {"00","00","00","00","00","00","00","00","00","00"},
        {"00","00","00","00","00","00","00","00","00","00"},
        {"00","00","00","00","00","00","00","00","00","00"}};
        #endregion
        #region Galois Field Table
        public string[,] Etable = new string[,] {
            {"01","03","05","0F","11","33","55","FF","1A","2E","72","96","A1","F8","13","35"},
            {"5F","E1","38","48","D8","73","95","A4","F7","02","06","0A","1E","22","66","AA"},
            {"E5","34","5C","E4","37","59","EB","26","6A","BE","D9","70","90","AB","E6","31"},
            {"53","F5","04","0C","14","3C","44","CC","4F","D1","68","B8","D3","6E","B2","CD"},
            {"4C","D4","67","A9","E0","3B","4D","D7","62","A6","F1","08","18","28","78","88"},
            {"83","9E","B9","D0","6B","BD","DC","7F","81","98","B3","CE","49","DB","76","9A"},
            {"B5","C4","57","F9","10","30","50","F0","0B","1D","27","69","BB","D6","61","A3"},
            {"FE","19","2B","7D","87","92","AD","EC","2F","71","93","AE","E9","20","60","A0"},
            {"FB","16","3A","4E","D2","6D","B7","C2","5D","E7","32","56","FA","15","3F","41"},
            {"C3","5E","E2","3D","47","C9","40","C0","5B","ED","2C","74","9C","BF","DA","75"},
            {"9F","BA","D5","64","AC","EF","2A","7E","82","9D","BC","DF","7A","8E","89","80"},
            {"9B","B6","C1","58","E8","23","65","AF","EA","25","6F","B1","C8","43","C5","54"},
            {"FC","1F","21","63","A5","F4","07","09","1B","2D","77","99","B0","CB","46","CA"},
            {"45","CF","4A","DE","79","8B","86","91","A8","E3","3E","42","C6","51","F3","0E"},
            {"12","36","5A","EE","29","7B","8D","8C","8F","8A","85","94","A7","F2","0D","17"},
            {"39","4B","DD","7C","84","97","A2","FD","1C","24","6C","B4","C7","52","F6","01"}
        };
        public string[,] Ltable = new string[,] {
            {"xx","00","19","01","32","02","1A","C6","4B","C7","1B","68","33","EE","DF","03"},
            {"64","04","E0","0E","34","8D","81","EF","4C","71","08","C8","F8","69","1C","C1"},
            {"7D","C2","1D","B5","F9","B9","27","6A","4D","E4","A6","72","9A","C9","09","78"},
            {"65","2F","8A","05","21","0F","E1","24","12","F0","82","45","35","93","DA","8E"},
            {"96","8F","DB","BD","36","D0","CE","94","13","5C","D2","F1","40","46","83","38"},
            {"66","DD","FD","30","BF","06","8B","62","B3","25","E2","98","22","88","91","10"},
            {"7E","6E","48","C3","A3","B6","1E","42","3A","6B","28","54","FA","85","3D","BA"},
            {"2B","79","0A","15","9B","9F","5E","CA","4E","D4","AC","E5","F3","73","A7","57"},
            {"AF","58","A8","50","F4","EA","D6","74","4F","AE","E9","D5","E7","E6","AD","E8"},
            {"2C","D7","75","7A","EB","16","0B","F5","59","CB","5F","B0","9C","A9","51","A0"},
            {"7F","0C","F6","6F","17","C4","49","EC","D8","43","1F","2D","A4","76","7B","B7"},
            {"CC","BB","3E","5A","FB","60","B1","86","3B","52","A1","6C","AA","55","29","9D"},
            {"97","B2","87","90","61","BE","DC","FC","BC","95","CF","CD","37","3F","5B","D1"},
            {"53","39","84","3C","41","A2","6D","47","14","2A","9E","5D","56","F2","D3","AB"},
            {"44","11","92","D9","23","20","2E","89","B4","7C","B8","26","77","99","E3","A5"},
            {"67","4A","ED","DE","C5","31","FE","18","0D","63","8C","80","C0","F7","70","07"},
        };
        #endregion
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
            string[,] TextMatrix = new string[4, 4];
            string[,] KeyMatrix = new string[4, 4];
            TextMatrix = SplitIntoMatrix(InputText);
            KeyMatrix = SplitIntoMatrix(key);
            string[,] sub = new string[4, 4];
            ScheduleMatrix = KeySchedule(KeyMatrix);
            if (Encryption)
            {
                sub = AddRoundKey(TextMatrix, 0);
                for (int i = 0; i < 9; i++)
                {
                    sub = SubBytes(sub);
                    sub = ShiftRows(sub);
                    sub = MixColumns(sub);
                    sub = AddRoundKey(sub, (i + 1));
                }
                sub = SubBytes(sub);
                sub = ShiftRows(sub);
                sub = AddRoundKey(sub, (10));
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                        Outputtext += sub[i, j];
                }
            }
            else if (Decryption)
            {
                sub = AddRoundKey(TextMatrix, 10);
                for (int i = 0; i < 9; i++)
                {
                    sub = ShiftRows(sub);
                    sub = SubBytes(sub);
                    sub = AddRoundKey(sub, (9 -i));
                    sub = MixColumns(sub);
                }
                sub = ShiftRows(sub);
                sub = SubBytes(sub);
                sub = AddRoundKey(sub, (0));
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                        Outputtext += sub[i, j];
                }
            }
        }
        //3243f6a8885a308d313198a2e0370734
        //2b7e151628aed2a6abf7158809cf4f3c
        //3925841d02dc09fbdc118597196a0b32
        private string[,] SplitIntoMatrix(string word)
        {
            string[,] result = new string[4, 4];
            char[] arr = word.ToCharArray();
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string temp = "";
                    temp += arr[count];
                    temp += arr[count + 1];
                    result[j, i] = temp;
                    count += 2;
                }
            }
            return result;
        }
        private string[,] SubBytes(string[,] input)
        {
            string[,] result = new string[4, 4];
            int row = 0, column = 0;
            if (Encryption)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        GetIndex(ref row, ref column, input[i, j]);
                        result[i, j] = sbox[row, column];
                    }
                }
            }
            else if (Decryption)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        GetIndex(ref row, ref column, input[i, j]);
                        result[i, j] = inverse_sbox[row, column];
                    }
                }
            }
            return result;
        }
        private string[,] ShiftRows(string[,] input)
        {
            for (int i = 1; i < 4; i++)
            {
                if (Encryption)
                {
                    input = CircularLeftShift(input, i, i);
                }
                else if (Decryption)
                {
                    input = CircularRightShift(input, i, i);
                }
            }
            return input;
        }
        private string[,] CircularLeftShift(string[,] input, int index, int count)
        {
            for (int i = 0; i < count; i++)
            {
                string temp = input[index, 0];
                input[index, 0] = input[index, 1];
                input[index, 1] = input[index, 2];
                input[index, 2] = input[index, 3];
                input[index, 3] = temp;
            }
            return input;
        }
        private string[,] CircularRightShift(string[,] input, int index, int count)
        {
            for (int i = 0; i < count; i++)
            {
                string temp = input[index, 3];
                input[index, 3] = input[index, 2];
                input[index, 2] = input[index, 1];
                input[index, 1] = input[index, 0];
                input[index, 0] = temp;
            }

            return input;
        }
        private string[,] MixColumns(string[,] input)
        {
            string[,] Result = new string[4, 4];
            string[] Column = new string[4];
            string[] Row = new string[4];
            if (Encryption)
            {
                for (int i = 0; i < input.GetLength(0); i++)
                {
                    for (int j = 0; j < input.GetLength(1); j++)
                    {
                        for (int k = 0; k < input.GetLength(0); k++)
                        {
                            Column[k] = input[k, i];
                            Row[k] = MixColumnFactor[j, k];
                        }
                        string Temp = MulColumns(Column, Row);
                        if (Temp.Length < 2)
                            Temp = "0" + Temp;
                        Result[j, i] = Temp;
                    }
                }
            }
            else if (Decryption)
            {
                for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k <4; k++)
                    {
                        Column[k] = input[k, i];
                        Row[k] = Inverse_MixColumnFactor[j, k];
                    }
                    string Temp = InverseMulColumns(Column, Row);
                    if (Temp.Length < 2)
                        Temp = "0" + Temp;
                    Result[j, i] = Temp;
                }
            }
            
            }
            return Result;
        }
        private string InverseMulColumns(string[] Column, string[] Row)
        {
            int[] Sum = new int[Column.Length];
            int Temp = 0;
            string TempString;
            List<string> EliminatedCell;
            for (int i = 0; i < Column.Length; i++)
            {
                EliminatedCell = AnalysisCell(Row[i]);
                for (int j = 0; j < EliminatedCell.Count; j++)
                {
                    switch (EliminatedCell[j])
                    {
                        case "01":
                            Temp = Convert.ToInt16(Column[i], 16);
                            break;
                        default:
                            {
                                Temp = Convert.ToInt16(Column[i], 16);
                                TempString = LeftShift(
                                    ConvertHexaToBinary(Column[i]), 1);
                                if (Temp < 128)
                                    Temp = Convert.ToInt16(TempString, 2);
                                else
                                    Temp = Convert.ToInt16(TempString, 2) ^ 27;
                                TempString = Convert.ToString(Temp, 2);
                                while (TempString.Length < 8)
                                    TempString = "0" + TempString;
                                if (EliminatedCell[j] != "02")
                                {
                                    int NumOfLoop;
                                    if (EliminatedCell[j] == "04")
                                        NumOfLoop = 1;
                                    else
                                        NumOfLoop = 2;
                                    for (int k = 0; k < NumOfLoop; k++)
                                    {
                                        Temp = Convert.ToInt16(TempString, 2);
                                        TempString = LeftShift(TempString, 1);
                                        if (Temp < 128)
                                            Temp = Convert.ToInt16(TempString, 2);
                                        else
                                            Temp = Convert.ToInt16(TempString, 2) ^ 27;
                                        TempString = Convert.ToString(Temp, 2);
                                        while (TempString.Length < 8)
                                            TempString = "0" + TempString;
                                    }
                                }
                            }
                            break;
                    }
                    if (j == 0)
                        Sum[i] = Temp;
                    else
                        Sum[i] = Sum[i] ^ Temp;
                }
                if (i != 0)
                    Sum[0] = Sum[0] ^ Sum[i];
            }
            return Convert.ToString(Sum[0], 16);
        }
        private string MulColumns(string[] Column, string[] Row)
        {
            int Sum = 0, Temp;
            for (int i = 0; i < Column.Length; i++)
            {
                switch (Row[i])
                {
                    case "01":
                        Temp = Convert.ToInt16(Column[i], 16);
                        break;
                    case "02":
                        {
                            Temp = Convert.ToInt16(Column[i], 16);
                            Column[i] = LeftShift(ConvertHexaToBinary(Column[i]), 1);
                            if (Temp < 128)
                                Temp = Convert.ToInt16(Column[i], 2);
                            else
                                Temp = Convert.ToInt16(Column[i], 2) ^ 27;
                        }
                        break;
                    case "03":
                        {
                            Temp = Convert.ToInt16(Column[i], 16);
                            Column[i] = LeftShift(
                                ConvertHexaToBinary(Column[i]), 1);
                            if (Temp < 128)
                                Temp = Convert.ToInt16(Column[i], 2) ^ Temp;
                            else
                                Temp = Convert.ToInt16(Column[i], 2) ^ 27 ^ Temp;
                        }
                        break;
                    default:
                        Temp = -1;
                        break;
                }
                if (i == 0)
                    Sum = Temp;
                else
                    Sum = Sum ^ Temp;
            }
            return Convert.ToString(Sum, 16);
        }
        private string LeftShift(string Input, int Num)
        {
            Input = Input.Remove(0, Num);
            for (int i = 0; i < Num; i++)
                Input += "0";
            return Input;
        }
        private string ConvertHexaToBinary(string input)
        {
            string ReturnString = "";
            for (int i = 0; i < input.Length; i++)
            {
                string Temp = "";
                Temp += input[i];
                Temp = Convert.ToString(Convert.ToInt32(Temp, 16), 2);
                while (Temp.Length < 4)
                    Temp = "0" + Temp;
                ReturnString += Temp;
            }
            return ReturnString;
        }
        /*
        private string[,] MixColumns(string[,] input)
        {
            //string X= GaloisFieldM("04", "09");
            //String M=(Xor(Xor(Xor("2c", "f8"), "e5"), "01")).ToLower();
            string[,] result = new string[4, 4];
            if (Encryption)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        string b1 = GaloisFieldM(input[0, j], MixColumnFactor[i, 0]);
                        string b2 = GaloisFieldM(input[1, j], MixColumnFactor[i, 1]);
                        string b3 = GaloisFieldM(input[2, j], MixColumnFactor[i, 2]);
                        string b4 = GaloisFieldM(input[3, j], MixColumnFactor[i, 3]);
                        result[i, j] = (Xor(Xor(Xor(b1, b2), b3), b4)).ToLower(); ;
                    }
                }

            }
            else if (Decryption)
            {

            }
            return result;
        }
        */
        private string[,] AddRoundKey(string[,] input, int RoundNum)
        {
            string[,] result = new string[4, 4];
            string[,] Round = new string[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = (RoundNum * 4); j < (RoundNum * 4) + 4; j++)
                {
                    Round[i, (j - (RoundNum * 4))] = ScheduleMatrix[i, j];
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string x_r = Xor(input[i, j], Round[i, j]).ToLower();
                    if (x_r.Length != 2)
                        result[i, j] = "0" + x_r;
                    else
                        result[i, j] = x_r;
                }
            }


            return result;
        }
        private List<string> AnalysisCell(string Cell)
        {
            Cell = Convert.ToString(Convert.ToInt16(Cell, 16), 2);
            string Temp1 = "00000000";
            string Temp2;
            List<string> ReturnList = new List<string>();
            for (int i = 0; i < Cell.Length; i++)
            {
                if (Cell[i] == '1')
                {
                    Temp2 = Temp1.Substring(0, i) + "1";
                    Temp2 += Temp1.Substring(i + 1, Cell.Length - Temp2.Length);
                    Temp2 = Convert.ToString(Convert.ToInt16(Temp2, 2), 16);
                    if (Temp2.Length < 2)
                        Temp2 = "0" + Temp2;
                    ReturnList.Add(Temp2);
                }
            }
            return ReturnList;
        }
        private string[,] KeySchedule(string[,] input)
        {
            string[,] result = new string[4, 44];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                    result[i, j] = input[i, j];
            }
            for (int i = 4; i < 44; i++)
            {
                string[] W_1 = new string[4];
                string[] W_4 = new string[4];
                for (int k = 0; k < 4; k++)
                {
                    W_1[k] = result[k, i - 1];
                    W_4[k] = result[k, i - 4];
                }
                if (i % 4 == 0)
                {
                    W_1 = CircularLeftShift(W_1, 1);
                    string[] Rco = new string[4];
                    Rco[0] = Rcon[0, (i / 4) - 1];
                    Rco[1] = Rcon[1, (i / 4) - 1];
                    Rco[2] = Rcon[2, (i / 4) - 1];
                    Rco[3] = Rcon[3, (i / 4) - 1];
                    for (int k = 0; k < 4; k++)
                    {
                        int row = 0, column = 0;
                        GetIndex(ref row, ref column, W_1[k]);
                        W_1[k] = sbox[row, column].ToLower();
                        string x_r = Xor(W_1[k], Rco[k]).ToLower();
                        if (x_r.Length != 2)
                            W_1[k] = "0" + x_r;
                        else
                            W_1[k] = x_r;
                    }
                }
                for (int k = 0; k < 4; k++)
                {
                    result[k, i] = Xor(W_1[k], W_4[k]).ToLower();
                }
            }
            return result;
        }
        private void GetIndex(ref int r, ref int c, string input)
        {
            char row = input[0];
            char column = input[1];
            r = Tointeger(input[0]);
            c = Tointeger(input[1]);
        }
        private int Tointeger(char ch)
        {
            int result = 0;
            if (ch >= '0' && ch <= '9')
            {
                result = Convert.ToInt32(ch);
                result -= 48;
            }
            else
            {
                ch -= 'a';
                result = Convert.ToInt32(ch);
                result += 10;
            }
            return result;
        }
        string Xor(string A, string B)
        {
            string p = HexString2Ascii(A);
            string f = HexString2Ascii(B);
            string z = (Convert.ToChar(p) ^ Convert.ToChar(f)).ToString("X");
            if (z.Length != 2)
                z = "0" + z;
            return z;

        }
        private string HexString2Ascii(string hexString)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= hexString.Length - 2; i += 2)
            {
                sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
            }
            return sb.ToString();
        }
        private string[] CircularLeftShift(string[] row, int count)
        {
            for (int i = 0; i < count; i++)
            {
                string temp = row[0];
                row[0] = row[1];
                row[1] = row[2];
                row[2] = row[3];
                row[3] = temp;
            }
            return row;
        }
        private string[] CircularRightShift(string[] row, int count)
        {
            for (int i = 0; i < count; i++)
            {
                string temp = row[3];
                row[3] = row[2];
                row[2] = row[1];
                row[1] = row[0];
                row[0] = temp;
            }

            return row;
        }
        /*
        private string GaloisFieldM(string input1, string input2)
        {
            if (input2 == "01")
                return input1;
            string result = "";
            int row = 0, column = 0;
            GetIndex(ref row, ref column, input1);
            string L1 = Ltable[row, column];
            GetIndex(ref row, ref column, input2);
            string L2 = Ltable[row, column];
            string sum = Plus(L1, L2).ToLower();
            GetIndex(ref row, ref column, sum);
            result = Etable[row, column].ToLower();
            return result;
        }
        private string Plus(string A, string B)
        {
            string p = HexString2Ascii(A);
            string f = HexString2Ascii(B);
            string z = (Convert.ToChar(p) + Convert.ToChar(f)).ToString("X");
            if (z.Length != 2)
                z = "0" + z;
            return z;
        }
        */
    }
}

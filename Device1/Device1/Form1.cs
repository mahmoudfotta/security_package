using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Device1
{
    public partial class Form1 : Form
    {
        public Socket s;
        public Socket clientSock;
        string msg;
        public Form1()
        {
            InitializeComponent();
        }
        private void btn_Start_Click(object sender, EventArgs e)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1000);
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            s.Bind(ep);
            s.Listen(100); //To determine the maximum number of connections you can specify, retrieve the MaxConnections value. Listen does not block.
            tb_msg.Text = "Waiting for a client to connect...";
        }
        private void btn_accept_Click(object sender, EventArgs e)
        {
            ///////////Accept Connection/////////////////
            clientSock = s.Accept();
            tb_msg.Text += "\r\nClient Connected";
        }
        private void btn_Send_Click(object sender, EventArgs e)
        {
            byte[] messageByteArray = Encoding.ASCII.GetBytes(msg);
            //Sockets deals with binary data only 
            //GetBytes function gets the binary representation of a string in ASCII format.
            clientSock.Send(messageByteArray);
            tb_msg.Text += "\r\nMessage has been sent.";
        }
        private void SenderClosed(object sender, FormClosedEventArgs e)
        {
            clientSock.Close();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            string Plaintext = "";
            string Key = "";
            if (textBox9.Text != "")
                Plaintext = textBox9.Text;
            else
                MessageBox.Show("Enter The Plain text");
            if (textBox8.Text != "")
                Key = textBox8.Text;
            else
                MessageBox.Show("Enter The Key");
            Monoalphabetic H = new Monoalphabetic();
            textBox7.Text = H.EncryptionMethod(Key, Plaintext);
        }
        //ZAQWSXCDERFVBGTYHNMJUIKLOP
        //BZDBTUWCZBZV
        private void button2_Click(object sender, EventArgs e)
        {
            Columnar C = new Columnar();
            string plaintext = "";
            string Key = "";
            if (textBox1.Text != "")
                plaintext = textBox1.Text;
            else
                MessageBox.Show("Enter The plain text");
            if (textBox2.Text != "")
                Key = textBox2.Text;
            else
                MessageBox.Show("Enter The Key");
            textBox3.Text = C.EncryptionMethod(Key, plaintext);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string Plaintext = "";
            string Key = "";
            if (textBox6.Text != "")
                Plaintext = textBox6.Text;
            else
                MessageBox.Show("Enter The Plain text");
            if (textBox5.Text != "")
                Key = textBox5.Text;
            else
                MessageBox.Show("Enter The Key");
            HillCipher H = new HillCipher();
            textBox4.Text = H.EncryptionMethod(Key, Plaintext);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            string Plaintext = "";
            string Key = "";
            if (textBox12.Text != "")
                Plaintext = textBox12.Text;
            else
                MessageBox.Show("Enter The Plain text");
            if (textBox11.Text != "")
                Key = textBox11.Text;
            else
                MessageBox.Show("Enter The Key");
            RailFence H = new RailFence();
            textBox10.Text = H.EncryptionMethod(Key, Plaintext);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            string Key = textBox13.Text.ToString();
            Key += "\n";
            Key += textBox14.Text.ToString();
            Key += "\n";
            Key += textBox16.Text.ToString();
            string M = textBox15.Text.ToString();
            RSA R = new RSA();
            msg = R.EncryptionMethod(Key, M);
            MessageBox.Show("Message is " + msg);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string ciphertext = "";
            string Key = "";
            if (textBox7.Text != "")
                ciphertext = textBox7.Text;
            else
                MessageBox.Show("Enter The ciper text");
            if (textBox8.Text != "")
                Key = textBox8.Text;
            else
                MessageBox.Show("Enter The Key");
            Monoalphabetic H = new Monoalphabetic();
            textBox9.Text = H.DecryptionMethod(Key, ciphertext);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Columnar C = new Columnar();
            string ciphertext = "";
            string Key = "";
            if (textBox3.Text != "")
                ciphertext = textBox3.Text;
            else
                MessageBox.Show("Enter The ciper text");
            if (textBox2.Text != "")
                Key = textBox2.Text;
            else
                MessageBox.Show("Enter The Key");
            textBox1.Text = C.DecryptionMethod(Key, ciphertext);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string ciphertext = "";
            string Key = "";
            if (textBox4.Text != "")
                ciphertext = textBox4.Text;
            else
                MessageBox.Show("Enter The ciper text");
            if (textBox5.Text != "")
                Key = textBox5.Text;
            else
                MessageBox.Show("Enter The Key");
            HillCipher H = new HillCipher();
            textBox6.Text = H.DecryptionMethod(Key, ciphertext);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            string cipertext = "";
            string Key = "";
            if (textBox10.Text != "")
                cipertext = textBox10.Text;
            else
                MessageBox.Show("Enter The ciper text");
            if (textBox11.Text != "")
                Key = textBox11.Text;
            else
                MessageBox.Show("Enter The Key");
            RailFence H = new RailFence();
            textBox12.Text = H.DecryptionMethod(Key, cipertext);
        }
        GeneralCeaser Ceaser = new GeneralCeaser();
        private void button10_Click(object sender, EventArgs e)
        {
            textBox18.Text = Ceaser.encryption(textBox17.Text.ToLower(), textBox19.Text);
        }
        private void button11_Click(object sender, EventArgs e)
        {
            textBox17.Text = Ceaser.decryption(textBox18.Text.ToUpper(), textBox19.Text);
        }
        Vigenère veg = new Vigenère();
        private void button12_Click(object sender, EventArgs e)
        {
            if (textBox22.Text != "" )
            {
                //textBox21.Text = null;
                textBox21.Text = veg.encryption(textBox20.Text.ToLower(), veg.repetingkey(textBox20.Text, textBox22.Text.ToLower()));
                //textBox21.Text = null;
            }
            if (textBox23.Text != "")
            {
                textBox21.Text = null;
                textBox21.Text = veg.encryption(textBox20.Text.ToLower(), veg.Autokey(textBox20.Text, textBox23.Text.ToLower()));
                //textBox21.Text = null;
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox22.Text != "")
            {
                //textBox21.Text = null;
                textBox20.Text = veg.decryption(textBox21.Text, veg.repetingkey(textBox20.Text.ToUpper(), textBox22.Text.ToUpper()));
                //textBox21.Text = null;
            }
            if (textBox23.Text != "")
            {
                textBox20.Text = veg.decryption(textBox21.Text, veg.Autokey(textBox20.Text.ToUpper(), textBox23.Text.ToUpper()));
                //textBox21.Text = null;
            }
        }
        PlayFair playf = new PlayFair();
        char[,] keymatrix;
        private void button14_Click(object sender, EventArgs e)
        {
            keymatrix = playf.generateMatrixKey(textBox26.Text.ToUpper());
            textBox25.Text = playf.encryption(textBox24.Text.ToUpper(), keymatrix);
        }
        private void button15_Click(object sender, EventArgs e)
        {
            keymatrix = playf.generateMatrixKey(textBox26.Text.ToUpper());
            textBox24.Text = (playf.decryption(textBox25.Text.ToUpper(), keymatrix)).ToLower();
        }
        int[] keystream = new int[300];
        string[] ciphertext = new string[200];
        RC4 rc4 = new RC4();
        private void button16_Click(object sender, EventArgs e)
        {
            keystream = rc4.generateKeyStream(textBox27.Text.Length, textBox28.Text.ToCharArray());
            msg = rc4.encryption(textBox27.Text.ToCharArray(), textBox27.Text.Length, textBox28.Text.ToCharArray(), keystream);
            MessageBox.Show("Message is " + msg);
        }
        Multiplicative_Inverse mi = new Multiplicative_Inverse();
        private void button17_Click(object sender, EventArgs e)
        {
            int m = Convert.ToInt32(textBox29.Text);
            int n = Convert.ToInt32(textBox30.Text);//l rqm l kbeer
            msg = mi.inverse(m, n).ToString();
            MessageBox.Show("Message is " + msg);
        }
        private void button18_Click(object sender, EventArgs e)
        {
            string Plaintext = "";
            string Key = "";
            if (textBox33.Text != "")
                Plaintext = textBox33.Text;
            else
                MessageBox.Show("Enter The Plain text");
            if (textBox32.Text != "")
                Key = textBox32.Text;
            else
                MessageBox.Show("Enter The Key");
            AES A = new AES();
            msg = A.EncryptionMethod(Key, Plaintext);
            MessageBox.Show("Message is "+msg);
        }

    }
}

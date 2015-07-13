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
namespace Device2
{
    public partial class Form1 : Form
    {
        Socket clientSock;
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            IPAddress host = IPAddress.Parse("127.0.0.1");
            //192.168.1.1 refers to the server
            //Put down the IP of the computer next to you.
            //Or 127.0.0.1 if you’ll run both client and server on your machine
            IPEndPoint hostEndpoint = new IPEndPoint(host, 1000);
            //8000 is the port number and must match that of the server.
            clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            clientSock.Connect(hostEndpoint);
            tb_clientmsg.Text = "Connected....\r\nReceiving....";
        }
        string message;
        private void btn_Recieve_Click(object sender, EventArgs e)
        {
            /////////Receiving////////////
            byte[] clientData = new byte[1024];
            //Prepare the byte array which will be filled with the message sent by the server.
            int receivedBytesLen = clientSock.Receive(clientData);
            //Receive will block (i.e.wait forever) until the server sends data
           message = Encoding.ASCII.GetString(clientData);
            //Parse the string out of the byte array.
        }
        private void ReciverClosed(object sender, FormClosedEventArgs e)
        {
            clientSock.Close();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            string Key = textBox13.Text.ToString();
            textBox17.Text = message;
            Key += "\n";
            Key += textBox14.Text.ToString();
            Key += "\n";
            Key += textBox16.Text.ToString();
            RSA R = new RSA();
            tb_clientmsg.Text += "\r\nThe message is :\r\n" + R.DecryptionMethod(Key, message);
        }
        int[] keystream = new int[300];
        RC4 rc4 = new RC4();
        private void button1_Click(object sender, EventArgs e)
        {
            string[] ciphertext2 = message.Split('%');
            string cipher = "";
            int length = Convert.ToInt32(ciphertext2[ciphertext2.Length - 1]);
            keystream = rc4.generateKeyStream(length, textBox1.Text.ToCharArray());
            for (int i = 0; i < ciphertext2.Length-1; i++)
            {
                cipher += ciphertext2[i];
            }
            textBox2.Text = cipher;
            tb_clientmsg.Text += rc4.decryption(ciphertext2, textBox1.Text.ToCharArray(), keystream);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string Key = "";
            textBox18.Text=message;
            if (textBox19.Text != "")
                Key = textBox19.Text;
            else
                MessageBox.Show("Enter The Key");
            AES A = new AES();
            tb_clientmsg.Text += "\r\nThe message is :\r\n" + A.DecryptionMethod(Key, message);
        }
        Multiplicative_Inverse mi = new Multiplicative_Inverse();

        private void button2_Click(object sender, EventArgs e)
        {
            //int m = Convert.ToInt32(textBox4.Text);
            int n = Convert.ToInt32(textBox4.Text);//l rqm l kbeer
            textBox3.Text = message;
            //textBox6.Text = mi.inverse(m, n).ToString();
            tb_clientmsg.Text += "\r\nThe message is :\r\n" + mi.inverse(Convert.ToInt32(message), n).ToString();
        }
    }
}

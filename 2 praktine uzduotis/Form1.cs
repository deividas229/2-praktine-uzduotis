using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace _2_praktine_uzduotis
{
    public partial class Form1 : Form
    {
        string directory = @"C:\Users\deivi\OneDrive\Desktop\kolegija\sauga\2 praktinis darbas\text.txt";
        public static string mysecurityKey = null;

        string encryptedText;
        string decryptedText;

        public Form1()
        {
            InitializeComponent();
            label5.Text = directory;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            encryptedText = Encrypt(textBox2.Text,textBox3.Text);
            textBox1.Text = encryptedText;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            decryptedText = Decrypt(textBox2.Text,textBox3.Text);
            textBox1.Text = decryptedText;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            File.WriteAllText(@"C:\Users\deivi\OneDrive\Desktop\kolegija\sauga\2 praktinis darbas\text.txt", String.Empty);
            using (StreamWriter file =
            new StreamWriter(@"C:\Users\deivi\OneDrive\Desktop\kolegija\sauga\2 praktinis darbas\text.txt", true))
            {
                file.WriteLine(textBox1.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string vtext = File.ReadAllText(@"C:\Users\deivi\OneDrive\Desktop\kolegija\sauga\2 praktinis darbas\text.txt");
            string filetext = vtext;
            textBox2.Text = filetext;
        }
        public static string Encrypt(string TextToEncrypt, string key)
        {
            TripleDESCryptoServiceProvider desCryptoProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider();

            byte[] byteHash;
            byte[] byteBuff;

            byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
            desCryptoProvider.Key = byteHash;
            desCryptoProvider.Mode = CipherMode.ECB; //CBC, CFB
            byteBuff = Encoding.UTF8.GetBytes(TextToEncrypt);

            string encoded =
                Convert.ToBase64String(desCryptoProvider.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            return encoded;
        }

        public static string Decrypt(string TextToDecrypt, string key)
        {
            TripleDESCryptoServiceProvider desCryptoProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider();

            byte[] byteHash;
            byte[] byteBuff;

            byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
            desCryptoProvider.Key = byteHash;
            desCryptoProvider.Mode = CipherMode.ECB; //CBC, CFB
            byteBuff = Convert.FromBase64String(TextToDecrypt);

            string plaintext = Encoding.UTF8.GetString(desCryptoProvider.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            return plaintext;
        }
    }
}

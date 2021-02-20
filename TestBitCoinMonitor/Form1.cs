using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;

namespace TestBitCoinMonitor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                var uri = String.Format("https://api.binance.com/api/v3/avgPrice?symbol={0}", "BTCBUSD");

                WebClient client = new WebClient();
                client.UseDefaultCredentials = true;
                var data = client.DownloadString(uri);

                textBox1.Text += DateTime.Now.ToString() + " " + Convert.ToString(data);

                Thread.Sleep(2000);

            }
        }
    }
}

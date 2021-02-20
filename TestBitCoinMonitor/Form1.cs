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
using Newtonsoft.Json;
using System.IO;

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
            //Seta valor do lucro
            double Lucro = Convert.ToDouble(txtLucro.Text);

            ValorBTC lista;
            //Var Compra/Venda
            double Compra = 0;
            double Venda = 0;
            //Var vlr BTC
            double Btc = 0;

            ////declarando a variavel do tipo StreamWriter para
            //abrir ou criar um arquivo para escrita
            StreamWriter x;

            ////Colocando o caminho fisico e o nome do arquivo a ser criado
            //finalizando com .txt
            string CaminhoNome = "C:\\Sistemas\\TestBitCoinMonitor\\arq01.txt";

            //utilizando o metodo para criar um arquivo texto
            //e associando o caminho e nome ao metodo
            x = File.CreateText(CaminhoNome);


            for (int i = 0; i < 1000; i++)
            {
                var uri = String.Format("https://api.binance.com/api/v3/avgPrice?symbol={0}", "BTCBUSD");

                WebClient client = new WebClient();
                client.UseDefaultCredentials = true;
                var data = client.DownloadString(uri);

                lista = JsonConvert.DeserializeObject<ValorBTC>(data);

                Btc = Math.Round(Convert.ToDouble(lista.price.Replace('.', ',')), 2);

                //Efetua compra caso Compra = 0
                if (Compra == 0)
                {
                    x.WriteLine("Compra realizada | " + DateTime.Now + " | Valor: R$ " + Btc.ToString("F"));
                    Compra = Btc;
                }

                //Verifica valor do BTC ultrapassou o valor de compra + lucro
                if (Btc > (Compra + Lucro))
                {
                    x.WriteLine("Venda realizada | " + DateTime.Now + " | Valor: R$" + Btc.ToString("F") + " | Lucro de R$: " + Convert.ToString(Btc - Compra));
                    //Zera valor de compra
                    Compra = 0;
                }


                Thread.Sleep(1000);

            }
            x.Close();
        }
    }
}

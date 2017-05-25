using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private TcpListener serwer = null;
        private TcpClient klient = null;


        private void start_Click(object sender, EventArgs e)
        {
            IPAddress adresIP = null;
            try
            {
                adresIP = IPAddress.Parse(adres.Text);

            }
            catch
            {
                MessageBox.Show("Błedny format adresu ip", "Błąd");
                adres.Text = String.Empty;
                return;
            }

            int port = System.Convert.ToInt32(my_port.Value);
            try
            {
                serwer = new TcpListener(adresIP, port);
                serwer.Start();

                klient = serwer.AcceptTcpClient();

                IPEndPoint IP = (IPEndPoint)klient.Client.RemoteEndPoint;
                info_o_polaczeniu.Items.Add("[" + IP.ToString() + "] : nawiazano polaczenie");



                info_o_polaczeniu.Items.Add("Nawiazano polaczenie");
                start.Enabled = false;
                stop.Enabled = true;

                klient.Close();
                serwer.Stop();
            }
            catch(Exception ex)
            {
                info_o_polaczeniu.Items.Add("Bład inicjalizacji serwera");
                MessageBox.Show(ex.ToString(), "Błąd");
            }



        }

        private void stop_Click(object sender, EventArgs e)
        {
            serwer.Stop();
            klient.Close();

            info_o_polaczeniu.Items.Add("Zakonczono prace serwera");
            stop.Enabled = false;
            start.Enabled = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            stop.Enabled = false;
        }
    }
}

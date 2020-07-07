using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace PingMaps
{
    public partial class frmPingMaps : Form
    {
        public frmPingMaps()
        {
            InitializeComponent();
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            Ping p = new Ping();
            PingReply r;
            string s;
            s = txtURL.Text;
            r = p.Send(s);
            if (r.Status == IPStatus.Success)
            {
                lblResultado1.Text = "Ping para " + s.ToString() + "[" + r.Address.ToString() + "]" + " Sucesso!";
                lblResultado2.Text = "Delay da resposta = " + r.RoundtripTime.ToString() + "ms" + "\n";
            }
            else
            {
                MessageBox.Show("Não foi possível encontrar uma máquina com este endereço!", "PING", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtURL_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtURL.Text) || txtURL.Text == "")
            {
                MessageBox.Show("Digite um IP ou URL válida!", "PING", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                string street = string.Empty;
                string city = string.Empty;
                string state = string.Empty;
                string zip = string.Empty;

                StringBuilder queryAddress = new StringBuilder();
                queryAddress.Append("http://maps.google.com/maps?q=");

                if (txtLogradouro.Text !=string.Empty)
                {
                    street = txtLogradouro.Text.Replace(' ', '+');
                    queryAddress.Append(street + ',' + '+');
                }

                if (txtCidade.Text != string.Empty)
                {
                    city = txtCidade.Text.Replace(' ', '+');
                    queryAddress.Append(city + ',' + '+');
                }

                if (txtEstado.Text != string.Empty)
                {
                    state = txtEstado.Text.Replace(' ', '+');
                    queryAddress.Append(state + ',' + '+');
                }

                if (txtCEP.Text != string.Empty)
                {
                    zip = txtCEP.Text.Replace(' ', '+');
                    queryAddress.Append(zip + ',' + '+');
                }

                wbMaps.Navigate(queryAddress.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Incapaz de retornar o mapa!");
            }
        }
    }
}

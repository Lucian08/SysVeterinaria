using AccesoDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysVeterinaria
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string nick = txtNick.Text;
            string clave = txtClave.Text;

            if (nick.Equals("") || clave.Equals(""))
            {
                MessageBox.Show(
                                "Debe ingresar parametros de autenticación",
                                ".: Sistema Veterinario",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                                );
                txtNick.Focus();
            }
            else {
                using (VeterinariaEntities contexto = new VeterinariaEntities()) {

                    if (contexto.Usuario.Where(x => x.Nick.Equals(nick) && x.Clave.Equals(clave)).Count() > 0)
                    {
                        Home hm = new Home();
                        hm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show(
                                "No existe un usuario con los datos ingresados",
                                ".: Sistema Veterinario",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                                ); 
                        txtClave.Clear();
                        txtNick.Focus();
                        txtNick.Select();
                    }
                }

            }


        }
    }
}

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
    public partial class Home : Form
    {

        Clientes frmCliente;
        Mascotas frmMascota;
        Atenciones frmAtenciones;
        Listado frmListado;

        public Home()
        {

            InitializeComponent();


            frmMascota = new Mascotas();
            frmMascota.MdiParent = this;

            frmCliente = new Clientes();
            frmCliente.MdiParent = this;

            frmAtenciones = new Atenciones();
            frmAtenciones.MdiParent = this;

            frmListado = new Listado();
            frmListado.MdiParent = this;
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {

                frmCliente.Show();
        }

        private void mascotaToolStripMenuItem_Click(object sender, EventArgs e)
        { 
                frmMascota.Show();
        }

        private void generarAtencionesToolStripMenuItem_Click(object sender, EventArgs e)
        { 
                frmAtenciones.Show();
        }

        private void atencionesToolStripMenuItem_Click(object sender, EventArgs e)
        { 
                frmListado.Show(); 
        }
    }
}

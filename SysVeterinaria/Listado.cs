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
    public partial class Listado : Form
    {
        public Listado()
        {
            InitializeComponent();
        }

        private void Listado_Load(object sender, EventArgs e)
        {
            cbxMascota.DisplayMember = cbxCliente.DisplayMember = "Nombre";
            cbxMascota.ValueMember = cbxCliente.ValueMember = "Id";

            CargarCliente();
            CargarMascota();
            rbCliente.Checked = true;

        }

        private void CargarCliente()
        {
            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
                cbxCliente.DataSource = contexto.Cliente.ToList();
            }
        }
        private void CargarMascota()
        {
            int idCliente = Convert.ToInt32(cbxCliente.SelectedValue);
            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
                cbxMascota.DataSource = contexto.Mascota.Where(x => x.Cliente == idCliente).ToList();
            }
        }

        private void rbCliente_CheckedChanged(object sender, EventArgs e)
        {
            cbxCliente.Enabled = true;
            cbxMascota.Enabled = false;
            dtDesde.Enabled = false;
            dtHasta.Enabled = false;

        }

        private void rbMascota_CheckedChanged(object sender, EventArgs e)
        {
            cbxCliente.Enabled = true;
            cbxMascota.Enabled = true;
            dtDesde.Enabled = false;
            dtHasta.Enabled = false;
        }

        private void rbFecha_CheckedChanged(object sender, EventArgs e)
        {
            cbxCliente.Enabled = false;
            cbxMascota.Enabled = false;
            dtDesde.Enabled = true;
            dtHasta.Enabled = true;
        }

        private void cbxCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarMascota();
        }

        private void button1_Click(object sender, EventArgs e)
        {  

            if (rbCliente.Checked)
            {

                using (VeterinariaEntities contexto = new VeterinariaEntities())
                {
                    int idCliente = Convert.ToInt32(cbxCliente.SelectedValue);
                   var listado =   contexto.ProcedureGetAtencionesCliente(idCliente).ToList();

                    dgvListado.Rows.Clear();

                    foreach (var item in listado)
                    {
                        dgvListado.Rows.Add(new object[] {
                            item.Numero,
                            item.Cliente,
                            item.Mascota,
                            item.Fecha,
                            item.SubTotal,
                            item.Iva,
                            item.Total
                        });

                    }
                }
            }
            else if (rbMascota.Checked)
            {
                using (VeterinariaEntities contexto = new VeterinariaEntities())
                {
                    int idMascota = Convert.ToInt32(cbxMascota.SelectedValue);

                    var listado = contexto.ProcedureGetAtencionesMascota(idMascota).ToList();

                    dgvListado.Rows.Clear();

                    foreach (var item in listado)
                    {
                        dgvListado.Rows.Add(new object[] {
                            item.Numero,
                            item.Cliente,
                            item.Mascota,
                            item.Fecha,
                            item.SubTotal,
                            item.Iva,
                            item.Total
                        });

                    }
                } 
            }
            else {
                using (VeterinariaEntities contexto = new VeterinariaEntities())
                {

                   var listado = contexto.ProcedureGetAtencionesFechas(dtDesde.Value, dtHasta.Value).ToList();


                    dgvListado.Rows.Clear();

                    foreach (var item in listado)
                    {
                        dgvListado.Rows.Add(new object[] {
                            item.Numero,
                            item.Cliente,
                            item.Mascota,
                            item.Fecha,
                            item.SubTotal,
                            item.Iva,
                            item.Total
                        });

                    }
                } 
            }
        } 
    } 
}

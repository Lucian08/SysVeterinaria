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
    public partial class Clientes : Form
    {

        private Cliente _cliente;

        public Clientes()
        {
            InitializeComponent();

        }
        private void Clientes_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            Botones(true);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void dgvListado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if( e.RowIndex>=0)
                BuscarCliente(e.RowIndex);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void CargarGrilla()
        {
            dgvListado.Rows.Clear();
            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
                List<Cliente> clientes = contexto.Cliente.ToList();

                foreach (Cliente cliente in clientes)
                {
                    dgvListado.Rows.Add(new object []
                    {
                       cliente.Id,
                       cliente.Rut,
                       cliente.Nombre,
                       cliente.Direccion,
                       cliente.Fono,
                       cliente.Email
                    });
                }

            }
        }
        private void Limpiar()
        {
            txtRut.Clear();
            txtNombre.Clear();
            txtDireccion.Clear();
            txtFono.Clear();
            txtEmail.Clear();
            Botones(true);
        }
        private void Botones(bool estado)
        {
            btnGuardar.Enabled = estado;
            btnEditar.Enabled = !estado;
            btnEliminar.Enabled = !estado; 
        }
        private void Guardar()
        {
            _cliente = new Cliente() {
                            Rut = Convert.ToInt32( txtRut.Text ),
                            Nombre = txtNombre.Text,
                            Direccion = txtDireccion.Text,
                            Fono = Convert.ToInt32( txtFono.Text),
                            Email = txtEmail.Text 
                          };

            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
                contexto.Cliente.Add(_cliente);
                contexto.SaveChanges();
            }

            MessageBox.Show(
                            $"Se guardo el cliente {_cliente.Nombre} exitosamente",
                            ".: Sistema Veterinaria:.",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                            );
            CargarGrilla();
            Limpiar();
        }

        private void BuscarCliente(int rows)
        {
            int id = Convert.ToInt32( dgvListado.Rows[rows].Cells[0].Value.ToString());
            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
                _cliente = contexto.Cliente.Find(id);
            }
            txtRut.Text =  _cliente.Rut.ToString();
            txtNombre.Text = _cliente.Nombre;
            txtDireccion.Text = _cliente.Direccion;
            txtEmail.Text = _cliente.Email;
            txtFono.Text = _cliente.Fono.ToString();

            Botones(false);
        }

        private void Editar()
        {
            _cliente.Rut = Convert.ToInt32(txtRut.Text);
            _cliente.Nombre = txtNombre.Text;
            _cliente.Direccion = txtDireccion.Text;
            _cliente.Fono = Convert.ToInt32(txtFono.Text);
            _cliente.Email = txtEmail.Text;


            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
                contexto.Entry(_cliente).State = System.Data.Entity.EntityState.Modified;
                contexto.SaveChanges(); 
            }

            MessageBox.Show(
                            $"Se edito la informacion del cliente {_cliente.Nombre} exitosamente",
                            ".: Sistema Veterinaria:.",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                            );
            CargarGrilla();
            Limpiar();
            Botones(true);
        }

        private void Eliminar()
        {
            DialogResult resultado = MessageBox.Show(
                                                     $"Esta seguro de elimnar {_cliente.Nombre} de sus registros",
                                                     ".:Sistema Veterinaria:.",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question
                                                     );
            if (resultado == DialogResult.Yes)
            {
                using (VeterinariaEntities contexto = new VeterinariaEntities())
                {
 
                    List<Atencion> atenciones = contexto.Atencion.Where(x => x.Cliente == _cliente.Id).ToList();
                    List<Mascota> mascotas = contexto.Mascota.Where(x => x.Cliente == _cliente.Id).ToList();


                    if (atenciones.Count() > 0 || mascotas.Count() > 0)
                    {
                        DialogResult result =  MessageBox.Show(
                                                            "El cliente tiene datos relacionados, esta seguro de eliminar de todos modos",
                                                            ".:Sistema Veterinaria:.",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question
                                                            );
                        if (result == DialogResult.Yes)
                        {
                            contexto.Atencion.RemoveRange(atenciones);
                            contexto.Mascota.RemoveRange(mascotas); 
                        } 
                    }

                    contexto.Entry(_cliente).State = System.Data.Entity.EntityState.Deleted;
                    contexto.SaveChanges();
                    MessageBox.Show(
                                $"Se elimino el cliente {_cliente.Nombre} exitosamente",
                                ".: Sistema Veterinaria:.",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                                );
                }


                CargarGrilla();
                Limpiar();
                Botones(true);
            }
            else {

                MessageBox.Show(
                                "El usuario cancelo la eliminacion",
                                ".:Sistema Veterinario:.",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                                );
            }

        }

        private void dgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BuscarCliente(e.RowIndex);
        }
    }
}

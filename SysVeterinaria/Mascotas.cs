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
    public partial class Mascotas : Form
    {

        private Mascota _mascota;

        public Mascotas()
        {
            InitializeComponent();
        }

        private void Mascotas_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void Mascotas_Load(object sender, EventArgs e)
        {
            CargarGrilla();

            cbxCliente.ValueMember  = cbxEspecie.ValueMember        = cbxCategoria.ValueMember = "Id";
            cbxEspecie.DisplayMember = cbxCategoria.DisplayMember   = cbxCliente.DisplayMember =  "Nombre";

            CargarComboBox();
            Botones(true);
            Limpiar();
        }

        private void CargarComboBox() {
     
            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
                cbxCliente.DataSource = contexto.Cliente.ToList();
                cbxEspecie.DataSource = contexto.Especie.ToList();  
            }
        }
        private void CargarGrilla()
        {
            dgvListado.Rows.Clear();
            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
             var resultado = contexto.Cliente
                                    .Join(contexto.Mascota,
                                          cliente => cliente.Id,  // Cliente.Id = Mascota.Cliente
                                          mascota => mascota.Cliente,
                                          (cliente, mascota) => new { cliente, mascota }
                                          )
                                   .Join(
                                        contexto.Categoria,
                                        mascotaJoin => mascotaJoin.mascota.Categoria,
                                        categoria => categoria.Id,
                                       (mascotaJoin, categoria) => new { mascotaJoin, categoria }
                                       )
                                  .Join(
                                       contexto.Especie,
                                       categoriaJoin => categoriaJoin.categoria.Especie,
                                       especie => especie.Id,
                                       (categoriaJoin, especie) => new {
                                           Id = categoriaJoin.mascotaJoin.mascota.Id,
                                           Cliente = categoriaJoin.mascotaJoin.cliente.Nombre,
                                           Nombre = categoriaJoin.mascotaJoin.mascota.Nombre,
                                           Sexo = (categoriaJoin.mascotaJoin.mascota.Sexo == 0) ? "H" : "M",
                                           Especie = especie.Nombre,
                                           Categoria = categoriaJoin.categoria.Nombre,
                                           Edad = categoriaJoin.mascotaJoin.mascota.Edad,
                                           Esterilizado = (categoriaJoin.mascotaJoin.mascota.Esterilazado == 0) ? "No" : "Si"
                                       })
                                 .ToList();

                foreach (var item in resultado)
                {
                    dgvListado.Rows.Add(new object[] {
                            item.Id,
                            item.Cliente,
                            item.Nombre,
                            item.Sexo,
                            item.Especie,
                            item.Categoria,
                            item.Edad,
                            item.Esterilizado
                    });
                }
            } 
        }

        private void cbxEspecie_SelectedIndexChanged(object sender, EventArgs e)
        {
            CambiaCategoria();
        }

        private void CambiaCategoria()
        {
            int idEspecie = Convert.ToInt32( cbxEspecie.SelectedValue.ToString() );

            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
                cbxCategoria.DataSource = contexto.Categoria.Where(x => x.Especie == idEspecie).ToList();  
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar() {
            txtNombre.Clear();
            txtEdad.Clear();

            rdMacho.Checked = true;
            chkEsterilizado.Checked = false;

            cbxCliente.SelectedValue = 1;
            cbxEspecie.SelectedValue = 1;
            cbxCategoria.SelectedValue = 1;

        }

        private void dgvListado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if( e.RowIndex>=0)
            SeleccionarMascota(e.RowIndex);
        }

        private void SeleccionarMascota(int row) {
            int idMascota = Convert.ToInt32( dgvListado.Rows[ row ].Cells[0].Value.ToString());
            int idEspecie = 0;
            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
                _mascota = contexto.Mascota.Find(idMascota);
                idEspecie = (int) _mascota.Categoria1.Especie;  
            }

            txtNombre.Text = _mascota.Nombre;
            txtEdad.Text = _mascota.Edad.ToString();

            cbxCliente.SelectedValue = _mascota.Cliente;
            cbxEspecie.SelectedValue =  idEspecie;
            cbxCategoria.SelectedValue = _mascota.Categoria;
            chkEsterilizado.Checked = (_mascota.Esterilazado == 0) ? false : true;
            if (_mascota.Sexo == 0)
            {
                rdHembra.Checked = true;
            }
            else {
                rdMacho.Checked = true;
            }
            Botones(false);
        }

        private void Botones(bool estado)
        {
            btnGuardar.Enabled = estado;
            btnEditar.Enabled = !estado;
            btnEliminar.Enabled = !estado;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void Guardar() {
            _mascota = new Mascota()
            {
                Nombre = txtNombre.Text,
                Edad = Convert.ToInt32(txtEdad.Text),
                Cliente = Convert.ToInt32(cbxCliente.SelectedValue),
                Categoria = Convert.ToInt32(cbxCategoria.SelectedValue),
                Esterilazado = (byte)((chkEsterilizado.Checked) ? 1 : 0),
                Sexo = (byte)((rdMacho.Checked) ? 1 : 0)
            };

            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
                contexto.Mascota.Add(_mascota);
                contexto.SaveChanges();
            }
            MessageBox.Show(
                $"Se guardo la mascota  {_mascota.Nombre} exitosamente",
                ".: Sistema Veterinaria:.",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
            CargarComboBox();
            CargarGrilla();
            Limpiar();
            Botones(true);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void Editar() {
            int idCliente = Convert.ToInt32(cbxCliente.SelectedValue);
            int idCategoria = Convert.ToInt32(cbxCategoria.SelectedValue);

            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
                _mascota.Nombre = txtNombre.Text;
                _mascota.Edad = Convert.ToInt32(txtEdad.Text);
                _mascota.Cliente = idCliente;
                _mascota.Cliente1 = contexto.Cliente.Find(idCliente) ;
                _mascota.Categoria = idCategoria;
                _mascota.Categoria1 = contexto.Categoria.Find(idCategoria) ;
                _mascota.Sexo = (byte)((chkEsterilizado.Checked) ? 1 : 0);
                _mascota.Esterilazado = (byte)((rdMacho.Checked) ? 1 : 0);

                contexto.Entry(_mascota).State = System.Data.Entity.EntityState.Modified;
                contexto.SaveChanges();
            }
            MessageBox.Show(
                $"Se edito la mascota {_mascota.Nombre} exitosamente",
                ".: Sistema Veterinaria:.",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
            Botones(true);
            Limpiar();
            CargarComboBox();
            CargarGrilla();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void Eliminar() {
            DialogResult resultado = MessageBox.Show(
                                                                $"Esta seguro de elimnar la mascota {_mascota.Nombre} de sus registros",
                                                                ".:Sistema Veterinaria:.",
                                                                MessageBoxButtons.YesNo,
                                                                MessageBoxIcon.Question
                                                                );
            if (resultado == DialogResult.Yes)
            {
                using (VeterinariaEntities contexto = new VeterinariaEntities())
                {

                    List<Atencion> atenciones = contexto.Atencion.Where(x => x.Mascota == _mascota.Id).ToList();
 
                    if (atenciones.Count() > 0)
                    {
                        DialogResult result = MessageBox.Show(
                                                            "La Mascota tiene datos relacionados, esta seguro de eliminar de todos modos",
                                                            ".:Sistema Veterinaria:.",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question
                                                            );
                        if (result == DialogResult.Yes)
                        {
                            contexto.Atencion.RemoveRange(atenciones); 
                        }
                    }

                    contexto.Entry(_mascota).State = System.Data.Entity.EntityState.Deleted;
                    contexto.SaveChanges();
                    MessageBox.Show(
                                $"Se elimino la Mascota {_mascota.Nombre} exitosamente",
                                ".: Sistema Veterinaria:.",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                                );
                }


                CargarGrilla();
                Limpiar();
                Botones(true);
            }
            else
            {

                MessageBox.Show(
                                "El usuario cancelo la eliminacion",
                                ".:Sistema Veterinario:.",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                                );
            }

        }

       
    }
}

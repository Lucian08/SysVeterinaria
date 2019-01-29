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
    public partial class Atenciones : Form
    {

        private Atencion _atencion;
        private Detalle _detalle;
        private int _atencionId;

        public Atenciones()
        {
            InitializeComponent();
        }

        private void Atenciones_Load(object sender, EventArgs e)
        {
            cbxCliente.ValueMember = cbxMascota.ValueMember = "Id";
            cbxCliente.DisplayMember = cbxMascota.DisplayMember = "Nombre";

            CargarCliente();
            CargarMascota();
            Botones( true);
            Limpiar();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            CrearAtencion();
            txtUnitario.Enabled = true;
            txtObservacion.Enabled = true;
            cbxCliente.Enabled = false;
            cbxMascota.Enabled = false;
            dtpEdad.Enabled = false;

        }



        private void button2_Click(object sender, EventArgs e)
        {
            GuardarDetalle();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            EditarDetalle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EliminarDetalle();
        }

        private void dgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbxCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarMascota();
        }

        private void CargarCliente()
        {
            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
                cbxCliente.DataSource = contexto.Cliente.ToList();
            }
        }
        private void CargarMascota() {
            int idCliente = Convert.ToInt32( cbxCliente.SelectedValue);
            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
                cbxMascota.DataSource = contexto.Mascota.Where(x=>x.Cliente == idCliente).ToList();
            }
        }
        private void Botones(bool estado)
        {
            btnGuardar.Enabled = estado;
            btnEditar.Enabled = !estado;
            btnEliminar.Enabled = !estado;

        }
        private void CrearAtencion()
        {
            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
                _atencion = new Atencion()
                {
                    Cliente = Convert.ToInt32( cbxCliente.SelectedValue),
                    Fecha = dtpEdad.Value,
                    Mascota = Convert.ToInt32( cbxMascota.SelectedValue)
                };

                contexto.Entry(_atencion).State = System.Data.Entity.EntityState.Added;
                contexto.SaveChanges();
                 

                MessageBox.Show(
                                $"Se creo la atencion exitosamente",
                                ".: Sistema Veterinaria:.",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                                );

                _atencionId = _atencion.Id;
                CargarDetalle();
            }
        }

        private void CargarDetalle() {
            dgvListado.Rows.Clear();
            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
                var resultado = contexto.Detalle
                                       .Join(contexto.Atencion,
                                             detalle => detalle.Atencion,
                                             atencion => atencion.Id,
                                             (detalle, atencion) => new { detalle, atencion }
                                             )
                                      .Join(
                                           contexto.Mascota,
                                           atencionJoin => atencionJoin.atencion.Mascota,
                                           mascota => mascota.Id,
                                           (atencionJoin, mascota) => new { atencionJoin, mascota }
                                           )
                                       .Join(
                                         contexto.Cliente,
                                         clienteJoin => clienteJoin.atencionJoin.atencion.Cliente1.Id,
                                         cliente => cliente.Id,
                                         (clienteJoin, cliente) => new { 
                                             Id = clienteJoin.atencionJoin.detalle.Id,
                                             IdAtencion=clienteJoin.atencionJoin.atencion.Id,
                                             Cliente = cliente.Nombre,
                                             Nombre = clienteJoin.mascota.Nombre,
                                             Fecha = clienteJoin.atencionJoin.atencion.Fecha,
                                             Observacion = clienteJoin.atencionJoin.detalle.Observacion,
                                             Unitario = clienteJoin.atencionJoin.detalle.Valor
                                         }
                                        )
                                      .Where(x => x.IdAtencion == _atencionId)
                                    .ToList();

                foreach (var item in resultado)
                {  
                    dgvListado.Rows.Add(new object[] {
                            item.Id,
                            item.Cliente,
                            item.Nombre,
                            item.Fecha,
                            item.Observacion,
                            item.Unitario 
                    });
                }
                
            }
        }

        private void CalcularMontos() {

            int total = 0;
            foreach (DataGridViewRow fila in dgvListado.Rows) {
               total += Convert.ToInt32( fila.Cells[5].Value.ToString()); 
            }
            int iva = Convert.ToInt32( Math.Round(total * 0.19) );
            int subTotal = total - iva;

            txtTotal.Text = total.ToString();
            txtIva.Text =  iva.ToString();
            txtSubtotal.Text = subTotal.ToString();
            LimpiarDetalle();
            txtObservacion.Focus(); 

        }
        private void Limpiar()
        {
            txtIva.Clear();
            txtSubtotal.Clear();
            txtTotal.Clear();

            txtObservacion.Enabled = false;
            txtUnitario.Enabled = false;
            dtpEdad.Value =   DateTime.Now;
            LimpiarDetalle();
        }

        private void LimpiarDetalle() {

            txtObservacion.Clear();
            txtUnitario.Clear();
        }
        private void GuardarDetalle()
        {
            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
                _detalle = new Detalle() {
                    Atencion = _atencionId, 
                    Observacion = txtObservacion.Text,
                    Valor = Convert.ToInt32(txtUnitario.Text)
                };


                contexto.Entry(_detalle).State = System.Data.Entity.EntityState.Added;  

                contexto.SaveChanges();
                CargarDetalle();
                LimpiarDetalle();
                MessageBox.Show(
                                $"Se guardo el detalle para la atencion Nº {_atencionId}",
                                ".:Sistema Veterinaria:.",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                                );
                CalcularMontos();

            }
        }
        private void EditarDetalle()
        {
            _detalle.Observacion = txtObservacion.Text;
            _detalle.Valor = Convert.ToInt32(txtUnitario.Text);

            using (VeterinariaEntities contexto = new VeterinariaEntities())
            {
                contexto.Entry(_detalle).State = System.Data.Entity.EntityState.Modified;
                contexto.SaveChanges();

            }
            MessageBox.Show(
                "Se edito la posicion seleccionada",
                ".:Sistema Veterinario:.",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
            _detalle = null;
            txtObservacion.Clear();
            txtUnitario.Clear();
            txtObservacion.Focus();
            CargarDetalle();
            Botones(true);
            CalcularMontos();
        }

        private void EliminarDetalle()
        {

            DialogResult mensaje = MessageBox.Show(
                                                $"Esta seguro de elimnar la posicion {_detalle.Id} del sistema",
                                                ".:Sistema Veterinario:.",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question
                                                );
            if (mensaje == DialogResult.Yes)
            {

                using (VeterinariaEntities contexto = new VeterinariaEntities())
                {
                    contexto.Entry(_detalle).State = System.Data.Entity.EntityState.Deleted;
                    contexto.SaveChanges();

                }
                MessageBox.Show(
                                "Se elimino la posicion seleccionada",
                                ".:Sistema Veterinario:.",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                                );
                _detalle = null;
                txtObservacion.Clear();
                txtUnitario.Clear();
                txtObservacion.Focus();
                CargarDetalle();
                Botones(true);
                CalcularMontos();
            }
            else {

                MessageBox.Show(
                                "El usuario cancelo la accion de eliminacion",
                                ".:Sistema Veterinario:.",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                                );
            }

        }

        private void dgvListado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            { 
                BuscarDetalleAtencion( e.RowIndex);
            }
        }

        private void BuscarDetalleAtencion(int row)
        {
            int idDetalle = Convert.ToInt32( dgvListado.Rows[row].Cells[0].Value.ToString());
            using (VeterinariaEntities contexto = new VeterinariaEntities()) {

                _detalle = contexto.Detalle.Find(idDetalle);
            }

            txtObservacion.Text = _detalle.Observacion;
            txtUnitario.Text = _detalle.Valor.ToString();

            Botones(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                                                     "Esta seguro de cambiar a otra antencion.",
                                                     ".:Sistema Veterinaria:.",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Information
                                                     );

            if (respuesta == DialogResult.Yes) {
                _atencion = null;
                _detalle = null;
                _atencionId = 0;
                cbxMascota.Enabled = true;
                cbxCliente.Enabled = true;
                txtObservacion.Enabled = false;
                txtUnitario.Enabled = false;
                txtIva.Clear();
                txtSubtotal.Clear();

                txtTotal.Clear();
                dgvListado.Rows.Clear();
                cbxCliente.Focus(); 
            }
            txtUnitario.Focus();
        }

       
    }
}

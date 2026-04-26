using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using service;
using dominio;

namespace TPWinForm_equipo_15A
{
    public partial class FormMarca : Form
    {
        public FormMarca()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormAccionMarca agregar = new FormAccionMarca();
            agregar.Text = "Agregar Marca";
            agregar.ShowDialog();
            MarcaService service = new MarcaService();
            dgvMarcas.DataSource = service.listar();
        }

        private void FormMarca_Load(object sender, EventArgs e)
        {
            MarcaService service = new MarcaService();
            dgvMarcas.DataSource = service.listar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Marca seleccion;
            seleccion = (Marca)dgvMarcas.CurrentRow.DataBoundItem;
            FormAccionMarca modificar = new FormAccionMarca(seleccion);
            modificar.Text = "Modificar Marca";
            modificar.ShowDialog();
            MarcaService service = new MarcaService();
            dgvMarcas.DataSource = service.listar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow != null)
            {
                DialogResult res = MessageBox.Show("Estas seguro de eliminar?", "Confirmar eliminacion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    Marca seleccion;
                    seleccion = (Marca)dgvMarcas.CurrentRow.DataBoundItem;
                    MarcaService service = new MarcaService();
                    service.eliminar(seleccion);
                    dgvMarcas.DataSource = service.listar();
                }
            }
            
        }
    }
}

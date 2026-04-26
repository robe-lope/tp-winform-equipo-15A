using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using service;

namespace TPWinForm_equipo_15A
{
    public partial class FormCategoria : Form
    {
        public FormCategoria()
        {
            InitializeComponent();
        }

        private void FormCategoria_Load(object sender, EventArgs e)
        {
            CategoriaService service = new CategoriaService();
            dgvListCategorias.DataSource = service.listar();
        }

        private void dgvListCategorias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvListCategorias_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

    

        private void btnModificar_Click(object sender, EventArgs e)
        {

            DataGridViewRow fila = dgvListCategorias.CurrentRow;
            Categoria aux = new Categoria();
            aux.Id = (int)fila.Cells["Id"].Value;
            aux.Descripcion = (string)fila.Cells["Descripcion"].Value;
            FormAccionCategoria formAccion = new FormAccionCategoria(aux);
            formAccion.Text = "Modificar Categoria";
            formAccion.ShowDialog();

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
           
            FormAccionCategoria formAccion = new FormAccionCategoria();
            formAccion.Text = "Agregar Categoria";
            formAccion.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvListCategorias.CurrentRow != null)
            {
                DialogResult res = MessageBox.Show("Estas seguro de eliminar?", "Confirmar eliminacion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    Categoria seleccion;
                    seleccion = (Categoria)dgvListCategorias.CurrentRow.DataBoundItem;
                    CategoriaService service = new CategoriaService();
                    service.eliminar(seleccion);
                    dgvListCategorias.DataSource = service.listar();
                }
            }
        }
    }
}

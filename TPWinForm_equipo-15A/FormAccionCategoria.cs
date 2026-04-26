using service;
using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPWinForm_equipo_15A
{
    public partial class FormAccionCategoria : Form
    {
        private Categoria categoria = null;
        public FormAccionCategoria()
        {
            InitializeComponent();
        }

        public FormAccionCategoria(Categoria cat)
        {
            InitializeComponent();
            categoria = cat;
            tbDescripcion.Text = categoria.Descripcion;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            CategoriaService service = new CategoriaService();
            try
            {
                if (tbDescripcion.Text == "")
                {
                    MessageBox.Show("Debe ingresar una descripcion");
                } else {
                    if (categoria != null)
                    {
                        categoria.Descripcion = tbDescripcion.Text;
                        service.modificar(categoria);
                        MessageBox.Show("Modificado con exito!");
                        this.Close();
                    }
                    else
                    {
                        categoria = new Categoria();
                        categoria.Descripcion = tbDescripcion.Text;
                        service.agregar(categoria);
                        MessageBox.Show("Agregado con exito!");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
     
        }

     
    }
}

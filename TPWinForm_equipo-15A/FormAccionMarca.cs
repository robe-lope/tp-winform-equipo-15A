using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using service;

namespace TPWinForm_equipo_15A
{
    public partial class FormAccionMarca : Form

    {
        private Marca marca;
        public FormAccionMarca()
        {
            InitializeComponent();
        }

        public FormAccionMarca(Marca marca)
        {
            InitializeComponent();
            this.marca = marca;
            txtDescripcion.Text = marca.Descripcion;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            MarcaService service = new MarcaService();
            try
            {
                if (txtDescripcion.Text == "")
                {
                    MessageBox.Show("Debe ingresar una descripcion");
                }
                else
                {
                    if (marca != null)
                    {
                        marca.Descripcion = txtDescripcion.Text;
                        service.modificar(marca);
                        MessageBox.Show("Modificado con exito!");
                        this.Close();
                    }
                    else
                    {
                        marca = new Marca();
                        marca.Descripcion = txtDescripcion.Text;
                        service.agregar(marca);
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

        private void FormAccionMarca_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

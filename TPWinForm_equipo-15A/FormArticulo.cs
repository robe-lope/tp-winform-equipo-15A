using dominio;
using service;
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

    public partial class FormArticulo : Form
    {
        private Articulo art = null;

        public FormArticulo()
        {
            InitializeComponent();
        }
        public FormArticulo(Articulo articulo)
        {
            InitializeComponent();
            art = articulo;
        }
        public FormArticulo(Articulo articulo, bool detalle)
        {
            InitializeComponent();
            art = articulo;
            if (detalle)
            {
                txtCodigo.ReadOnly = true;
                txtNombre.ReadOnly = true;
                txtDescripcion.ReadOnly = true;
                txtPrecio.ReadOnly = true;
                cmbCategoria.Enabled = false;
                cmbMarca.Enabled = false;
                txtUrlImagen.ReadOnly = true;
                btnGuardarArt.Visible = false;
                btnCancelarArt.Text = "Cerrar";
            }
        }
        private void btnGuardarArt_Click(object sender, EventArgs e)
        {
           
            if (validarCampos())
            {
                try
                {
                    ArticuloService service = new ArticuloService();
                    ImagenService imgService = new ImagenService();
                    Imagen img = new Imagen();
                    if (art == null)
                    {
                        art = new Articulo();
                    }

                    art.Descripcion = txtDescripcion.Text;
                    art.Nombre = txtNombre.Text;
                    art.Codigo = txtCodigo.Text;
                    art.Precio = decimal.Parse(txtPrecio.Text);
                    art.IdCategoria = (int)cmbCategoria.SelectedValue;
                    art.IdMarca = (int)cmbMarca.SelectedValue;


                    img.ImagenUrl = txtUrlImagen.Text;


                    if (art.Id == 0)
                    {
                        service.agregar(art);
                        int idNuevo = service.obtenerUltimoId();
                        img.IdArticulo = idNuevo;
                        imgService.agregar(img);
                    }
                    else
                    {
                        service.modificar(art);
                        img.IdArticulo = art.Id;
                        imgService.modificar(img);
                    }
                    this.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al guardar: " + ex.Message);
                }
               
            }
            
        }

        private void FormArticulo_Load(object sender, EventArgs e)
        {
            MarcaService serviceMarca = new MarcaService();
            CategoriaService categoriaService = new CategoriaService();


            try
            {
                cmbCategoria.ValueMember = "Id";
                cmbCategoria.DisplayMember = "Descripcion";
                cmbCategoria.DataSource = categoriaService.listar();
                cmbMarca.ValueMember = "Id";
                cmbMarca.DisplayMember = "Descripcion";
                cmbMarca.DataSource = serviceMarca.listar();
                if (art != null)
                {
                    txtCodigo.Text = art.Codigo;
                    txtNombre.Text = art.Nombre;
                    txtDescripcion.Text = art.Descripcion;
                    txtPrecio.Text = art.Precio.ToString();
                    cmbCategoria.SelectedValue = art.IdCategoria;
                    cmbMarca.SelectedValue = art.IdMarca;
                    txtUrlImagen.Text = art.UrlImagen;
                    cargarImg(art.UrlImagen);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void cargarImg(string imagen)
        {
            try
            {
                pbImg.Load(imagen);
            }
            catch (Exception ex)
            {
                pbImg.Image = pbImg.ErrorImage;
            }


        }

        private bool validarCampos()
        {
            if (string.IsNullOrEmpty(txtCodigo.Text) || string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtDescripcion.Text) || string.IsNullOrEmpty(txtPrecio.Text))
            {
                MessageBox.Show("Complete todos los campos");
                return false;
            }
            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio < 0)
            {
                MessageBox.Show("El precio debe ser un número válido y mayor o igual a cero");
                return false;
            }
            return true;
        }

        private void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            cargarImg(txtUrlImagen.Text);
        }

        private void btnCancelarArt_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
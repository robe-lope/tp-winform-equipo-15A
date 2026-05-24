using dominio;
using service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TPWinForm_equipo_15A
{
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulos;
        public Form1()
        {
            InitializeComponent();
            dgvArticulos.SelectionChanged += dgvArticulos_SelectionChanged;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormArticulo formArt = new FormArticulo();
            formArt.ShowDialog();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo artSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                FormArticulo formArt = new FormArticulo(artSeleccionado);
                formArt.Text = "Modificar Articulo";
                formArt.ShowDialog();
            }
            cargarGrilla();

        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo artSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                FormArticulo formArt = new FormArticulo(artSeleccionado, true);
                formArt.Text = "Detalle Articulo";
                formArt.ShowDialog();
            }
            cargarGrilla();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                DialogResult res = MessageBox.Show("Estas seguro de eliminar?", "Confirmar eliminacion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    Articulo artSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    ArticuloService service = new ArticuloService();
                    service.eliminar(artSeleccionado.Id);
                    listaArticulos = service.listar();
                    dgvArticulos.DataSource = listaArticulos;
                    ocultarColumnas();
                }
            }
            cargarGrilla();
        }

        private void btnMarcas_Click(object sender, EventArgs e)
        {
            FormMarca formMarca = new FormMarca();
            formMarca.ShowDialog();

        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            FormCategoria formCat = new FormCategoria();
            formCat.ShowDialog();
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null) cargarImg(((Articulo)dgvArticulos.CurrentRow.DataBoundItem).UrlImagen);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MarcaService serviceMarca = new MarcaService();
            List<Marca> marcas = serviceMarca.listar();
            marcas.Insert(0, new Marca { Id = 0, Descripcion = "-- Todas --" });
            cmbMarca.DataSource = marcas;
            cmbMarca.SelectedIndex = 0;
            cmbMarca.DisplayMember = "Descripcion";
            CategoriaService serviceCat = new CategoriaService();
            List<Categoria> categorias = serviceCat.listar();
            categorias.Insert(0, new Categoria { Id = 0, Descripcion = "-- Todas --" });
            cmbCategoria.DataSource = categorias;
            cmbCategoria.SelectedIndex = 0;
            cmbCategoria.DisplayMember = "Descripcion";
            cargarGrilla();
        }

        private void cmbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void filtrar()
        {
            List<Articulo> listaFiltrada = listaArticulos;
            Marca marcaSeleccionada = (Marca)cmbMarca.SelectedItem;
            Categoria categoriaSeleccionada = (Categoria)cmbCategoria.SelectedItem;

            if (marcaSeleccionada != null && marcaSeleccionada.Id != 0)
            {
                listaFiltrada = listaFiltrada.FindAll(x => x.IdMarca == marcaSeleccionada.Id);
            }

            if (categoriaSeleccionada != null && categoriaSeleccionada.Id != 0)
            {
                listaFiltrada = listaFiltrada.FindAll(x => x.IdCategoria == categoriaSeleccionada.Id);
            }


            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtBuscar.Text.ToUpper();

            if (filtro != "")
            {
                listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro) || x.Codigo.ToUpper().Contains(filtro) || x.Descripcion.ToUpper().Contains(filtro));
            }
            else
            {
                listaFiltrada = listaArticulos;
            }

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();

        }

        private void ocultarColumnas()
        {
            if (dgvArticulos.Columns.Count == 0) return;
            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["UrlImagen"].Visible = false;
            dgvArticulos.Columns["IdMarca"].Visible = false;
            dgvArticulos.Columns["IdCategoria"].Visible = false;
        }

        private void cargarImg(string imagen)
        {
            try
            {
                pbArtGrid.Load(imagen);
            }
            catch (Exception ex)
            {
                pbArtGrid.Image = pbArtGrid.ErrorImage;
            }


        }
        private void cargarGrilla()
        {
            ArticuloService service = new ArticuloService();
            listaArticulos = service.listar();
            dgvArticulos.DataSource = listaArticulos;
            dgvArticulos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ocultarColumnas();


        }
    }
}

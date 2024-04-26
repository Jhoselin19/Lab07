using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataLayer;
using System.Net.Sockets;
using Entities;

namespace Lab06
{
    /// <summary>
    /// Lógica de interacción para List.xaml
    /// </summary>
    /// 

    public partial class List : Window
    {
        public List()
        {
            InitializeComponent();
            LoadData();
            ListarClientes.SelectionChanged += ListarClientes_SelectionChanged;
        }

        private void LoadData()
        {
            List<Cliente> clientes = new List<Cliente>();

            try
            {
                DClient DClient = new DClient();
                clientes = DClient.LoadData();
                ListarClientes.ItemsSource = clientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void ListarClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verifica si hay alguna fila seleccionada
            if (ListarClientes.SelectedItem != null)
            {
                // Obtiene el cliente seleccionado del DataGrid
                Cliente clienteSeleccionado = (Cliente)ListarClientes.SelectedItem;

                // Asigna los valores de los campos del cliente seleccionado a los campos del formulario de edición
                txtIdClient.Text = clienteSeleccionado.idCliente;
                txtCompany.Text = clienteSeleccionado.NombreCompañia;
                txtContact.Text = clienteSeleccionado.NombreContacto;
                txtPosition.Text = clienteSeleccionado.CargoContacto;
                txtAddress.Text = clienteSeleccionado.Direccion;
                txtCity.Text = clienteSeleccionado.Ciudad;
                txtRegion.Text = clienteSeleccionado.Region;
                txtPostalCode.Text = clienteSeleccionado.CodPostal;
                txtCountry.Text = clienteSeleccionado.Pais;
                txtPhone.Text = clienteSeleccionado.Telefono;
                txtFax.Text = clienteSeleccionado.Fax;
            }
        }

        
        /// FALTA REALIZAR LA REFERENCIA ESTA CAPA A LA DE NEGOCIO, NO DIRECTAMENTE A LA DE DATOS
        private void UpdateClient(object sender, RoutedEventArgs e)
        {
            DClient DClient = new DClient();
            DClient.Update(new Cliente
            {
                idCliente = txtIdClient.Text,
                NombreCompañia = txtCompany.Text,
                NombreContacto = txtContact.Text,
                CargoContacto = txtPosition.Text,
                Direccion = txtAddress.Text,
                Ciudad = txtCity.Text,
                Region = txtRegion.Text,
                CodPostal = txtPostalCode.Text,
                Pais = txtCountry.Text,
                Telefono = txtPhone.Text,
                Fax = txtFax.Text,
            });
            LoadData();
        }

        private void DropClient(object sender, RoutedEventArgs e)
        {
            DClient DClient = new DClient();
            DClient.Drop(new Cliente
            {
                idCliente = txtIdClient.Text
            });
            LoadData();

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            txtIdClient.Text = string.Empty;
            txtCompany.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtRegion.Text = string.Empty;
            txtPostalCode.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtCountry.Text = string.Empty;
        }
    }
}

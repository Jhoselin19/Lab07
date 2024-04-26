using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DClient
    {
        public List<Cliente> LoadData()
        {
            List<Cliente> clientes = new List<Cliente>();

            try
            {
                //Cadena de conexión
                SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
                connection.Open();

                //Comandos de TRANSACT SQL
                SqlCommand command = new SqlCommand("ListClients", connection);
                command.CommandType = CommandType.StoredProcedure;

                //CONECTADA
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string idCliente = reader.IsDBNull(reader.GetOrdinal("idCliente")) ? string.Empty : reader.GetString("idCliente");
                    string NombreCompañia = reader.IsDBNull(reader.GetOrdinal("NombreCompañia")) ? string.Empty : reader.GetString("NombreCompañia");
                    string NombreContacto = reader.IsDBNull(reader.GetOrdinal("NombreContacto")) ? string.Empty : reader.GetString("NombreContacto");
                    string CargoContacto = reader.IsDBNull(reader.GetOrdinal("CargoContacto")) ? string.Empty : reader.GetString("CargoContacto");
                    string Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? string.Empty : reader.GetString("Direccion");
                    string Ciudad = reader.IsDBNull(reader.GetOrdinal("Ciudad")) ? string.Empty : reader.GetString("Ciudad");
                    string Region = reader.IsDBNull(reader.GetOrdinal("Region")) ? string.Empty : reader.GetString("Region");
                    string CodPostal = reader.IsDBNull(reader.GetOrdinal("CodPostal")) ? string.Empty : reader.GetString("CodPostal");
                    string Pais = reader.IsDBNull(reader.GetOrdinal("Pais")) ? string.Empty : reader.GetString("Pais");
                    string Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? string.Empty : reader.GetString("Telefono");
                    string Fax = reader.IsDBNull(reader.GetOrdinal("Fax")) ? string.Empty : reader.GetString("Fax");


                    clientes.Add(new Cliente
                    {
                        idCliente = idCliente,
                        NombreCompañia = NombreCompañia,
                        NombreContacto = NombreContacto,
                        CargoContacto = CargoContacto,
                        Direccion = Direccion,
                        Ciudad = Ciudad,
                        Region = Region,
                        CodPostal = CodPostal,
                        Pais = Pais,
                        Telefono = Telefono,
                        Fax = Fax
                    });


                }
                connection.Close();
            }
            catch (Exception ex)
            {

                //throw;
            }

                return clientes;

        }

        public void Update(Cliente cliente)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString.connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("UpdateClients", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //Valores
                    cmd.Parameters.AddWithValue("@ID", cliente.idCliente);
                    cmd.Parameters.AddWithValue("@NombreCompañia", cliente.NombreCompañia);
                    cmd.Parameters.AddWithValue("@NombreContacto", cliente.NombreContacto);
                    cmd.Parameters.AddWithValue("@CargoContacto", cliente.CargoContacto);
                    cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                    cmd.Parameters.AddWithValue("@Ciudad", cliente.Ciudad);
                    cmd.Parameters.AddWithValue("@Region", cliente.Region);
                    cmd.Parameters.AddWithValue("@CodPostal", cliente.CodPostal);
                    cmd.Parameters.AddWithValue("@Pais", cliente.Pais);
                    cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@Fax", cliente.Fax);

                    try 
                    { 
                        cmd.ExecuteNonQuery(); 
                    } 
                    catch (Exception ex)
                    { 
                    }

                    LoadData();
                }
            }

        }

        public void Drop(Cliente cliente) { 
            using (SqlConnection conn = new SqlConnection(ConnectionString.connectionString))
            {
                // Abrir la conexión
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("DeleteClientById", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", cliente.idCliente);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                    }
                    LoadData();
}
            }
        }
    }
}

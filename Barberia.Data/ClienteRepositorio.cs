using Microsoft.Data.SqlClient;
using Barberia.Domain;

namespace Barberia.Data
{
    // Clase para manejar las operaciones de base de datos de los clientes
    // Tiene los metodos para crear, leer, actualizar y eliminar clientes
    public class ClienteRepositorio
    {
        // Metodo para agregar un nuevo cliente a la base de datos
        public void Agregar(Cliente cliente)
        {
            // Abrimos la conexion a la base de datos
            using (SqlConnection conexion = new SqlConnection(ConexionDB.CadenaConexion))
            {
                // Escribimos la consulta SQL para insertar
                string consulta = "INSERT INTO Clientes (Nombre, Telefono, Email) VALUES (@Nombre, @Telefono, @Email)";

                // Creamos el comando con la consulta y la conexion
                SqlCommand comando = new SqlCommand(consulta, conexion);

                // Agregamos los parametros para evitar inyeccion SQL
                comando.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                comando.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                comando.Parameters.AddWithValue("@Email", cliente.Email);

                // Abrimos la conexion y ejecutamos
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        // Metodo para obtener todos los clientes de la base de datos
        public List<Cliente> ObtenerTodos()
        {
            // Lista donde vamos a guardar los clientes
            List<Cliente> clientes = new List<Cliente>();

            using (SqlConnection conexion = new SqlConnection(ConexionDB.CadenaConexion))
            {
                string consulta = "SELECT Id, Nombre, Telefono, Email FROM Clientes";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                conexion.Open();

                // Usamos un reader para leer los resultados fila por fila
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    // Creamos un objeto cliente con los datos de cada fila
                    Cliente cliente = new Cliente
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Telefono = reader.GetString(2),
                        Email = reader.GetString(3)
                    };

                    clientes.Add(cliente);
                }
            }

            return clientes;
        }

        // Metodo para obtener un cliente por su Id
        public Cliente? ObtenerPorId(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ConexionDB.CadenaConexion))
            {
                string consulta = "SELECT Id, Nombre, Telefono, Email FROM Clientes WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", id);

                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    return new Cliente
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Telefono = reader.GetString(2),
                        Email = reader.GetString(3)
                    };
                }
            }

            // Si no se encontro, retornamos null
            return null;
        }

        // Metodo para buscar clientes por nombre (busqueda parcial)
        public List<Cliente> BuscarPorNombre(string nombre)
        {
            List<Cliente> clientes = new List<Cliente>();

            using (SqlConnection conexion = new SqlConnection(ConexionDB.CadenaConexion))
            {
                // Usamos LIKE para buscar coincidencias parciales
                string consulta = "SELECT Id, Nombre, Telefono, Email FROM Clientes WHERE Nombre LIKE @Nombre";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");

                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Telefono = reader.GetString(2),
                        Email = reader.GetString(3)
                    };

                    clientes.Add(cliente);
                }
            }

            return clientes;
        }

        // Metodo para actualizar los datos de un cliente
        public void Actualizar(Cliente cliente)
        {
            using (SqlConnection conexion = new SqlConnection(ConexionDB.CadenaConexion))
            {
                string consulta = "UPDATE Clientes SET Nombre = @Nombre, Telefono = @Telefono, Email = @Email WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                comando.Parameters.AddWithValue("@Id", cliente.Id);
                comando.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                comando.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                comando.Parameters.AddWithValue("@Email", cliente.Email);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        // Metodo para eliminar un cliente por su Id
        public void Eliminar(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ConexionDB.CadenaConexion))
            {
                string consulta = "DELETE FROM Clientes WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", id);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
    }
}

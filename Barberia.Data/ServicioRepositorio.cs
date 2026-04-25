using Microsoft.Data.SqlClient;
using Barberia.Domain;

namespace Barberia.Data
{
    // Clase para manejar las operaciones de base de datos de los servicios
    public class ServicioRepositorio
    {
        // Metodo para agregar un nuevo servicio
        public void Agregar(Servicio servicio)
        {
            using (SqlConnection conexion = new SqlConnection(ConexionDB.CadenaConexion))
            {
                string consulta = "INSERT INTO Servicios (Nombre, Precio) VALUES (@Nombre, @Precio)";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                comando.Parameters.AddWithValue("@Nombre", servicio.Nombre);
                comando.Parameters.AddWithValue("@Precio", servicio.Precio);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        // Metodo para obtener todos los servicios
        public List<Servicio> ObtenerTodos()
        {
            List<Servicio> servicios = new List<Servicio>();

            using (SqlConnection conexion = new SqlConnection(ConexionDB.CadenaConexion))
            {
                string consulta = "SELECT Id, Nombre, Precio FROM Servicios";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Servicio servicio = new Servicio
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Precio = reader.GetDecimal(2)
                    };

                    servicios.Add(servicio);
                }
            }

            return servicios;
        }

        // Metodo para obtener un servicio por su Id
        public Servicio? ObtenerPorId(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ConexionDB.CadenaConexion))
            {
                string consulta = "SELECT Id, Nombre, Precio FROM Servicios WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", id);

                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    return new Servicio
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Precio = reader.GetDecimal(2)
                    };
                }
            }

            return null;
        }

        // Metodo para actualizar un servicio
        public void Actualizar(Servicio servicio)
        {
            using (SqlConnection conexion = new SqlConnection(ConexionDB.CadenaConexion))
            {
                string consulta = "UPDATE Servicios SET Nombre = @Nombre, Precio = @Precio WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                comando.Parameters.AddWithValue("@Id", servicio.Id);
                comando.Parameters.AddWithValue("@Nombre", servicio.Nombre);
                comando.Parameters.AddWithValue("@Precio", servicio.Precio);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        // Metodo para eliminar un servicio
        public void Eliminar(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ConexionDB.CadenaConexion))
            {
                string consulta = "DELETE FROM Servicios WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", id);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
    }
}

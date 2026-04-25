using Microsoft.Data.SqlClient;
using Barberia.Domain;

namespace Barberia.Data
{
    // Clase para manejar las operaciones de base de datos de las visitas
    public class VisitaRepositorio
    {
        // Metodo para registrar una nueva visita
        public void Agregar(Visita visita)
        {
            using (SqlConnection conexion = new SqlConnection(ConexionDB.CadenaConexion))
            {
                string consulta = "INSERT INTO Visitas (ClienteId, ServicioId, Fecha) VALUES (@ClienteId, @ServicioId, @Fecha)";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                comando.Parameters.AddWithValue("@ClienteId", visita.ClienteId);
                comando.Parameters.AddWithValue("@ServicioId", visita.ServicioId);
                comando.Parameters.AddWithValue("@Fecha", visita.Fecha);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        // Metodo para obtener todas las visitas con los nombres del cliente y servicio
        public List<Visita> ObtenerTodas()
        {
            List<Visita> visitas = new List<Visita>();

            using (SqlConnection conexion = new SqlConnection(ConexionDB.CadenaConexion))
            {
                // Usamos JOIN para traer los nombres del cliente y servicio
                string consulta = @"SELECT V.Id, V.ClienteId, V.ServicioId, V.Fecha,
                                    C.Nombre AS NombreCliente, S.Nombre AS NombreServicio
                                    FROM Visitas V
                                    INNER JOIN Clientes C ON V.ClienteId = C.Id
                                    INNER JOIN Servicios S ON V.ServicioId = S.Id
                                    ORDER BY V.Fecha DESC";

                SqlCommand comando = new SqlCommand(consulta, conexion);

                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Visita visita = new Visita
                    {
                        Id = reader.GetInt32(0),
                        ClienteId = reader.GetInt32(1),
                        ServicioId = reader.GetInt32(2),
                        Fecha = reader.GetDateTime(3),
                        NombreCliente = reader.GetString(4),
                        NombreServicio = reader.GetString(5)
                    };

                    visitas.Add(visita);
                }
            }

            return visitas;
        }

        // Metodo para obtener las visitas de un cliente especifico
        public List<Visita> ObtenerPorCliente(int clienteId)
        {
            List<Visita> visitas = new List<Visita>();

            using (SqlConnection conexion = new SqlConnection(ConexionDB.CadenaConexion))
            {
                string consulta = @"SELECT V.Id, V.ClienteId, V.ServicioId, V.Fecha,
                                    C.Nombre AS NombreCliente, S.Nombre AS NombreServicio
                                    FROM Visitas V
                                    INNER JOIN Clientes C ON V.ClienteId = C.Id
                                    INNER JOIN Servicios S ON V.ServicioId = S.Id
                                    WHERE V.ClienteId = @ClienteId
                                    ORDER BY V.Fecha DESC";

                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@ClienteId", clienteId);

                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Visita visita = new Visita
                    {
                        Id = reader.GetInt32(0),
                        ClienteId = reader.GetInt32(1),
                        ServicioId = reader.GetInt32(2),
                        Fecha = reader.GetDateTime(3),
                        NombreCliente = reader.GetString(4),
                        NombreServicio = reader.GetString(5)
                    };

                    visitas.Add(visita);
                }
            }

            return visitas;
        }

        // Metodo para eliminar una visita
        public void Eliminar(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ConexionDB.CadenaConexion))
            {
                string consulta = "DELETE FROM Visitas WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", id);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
    }
}

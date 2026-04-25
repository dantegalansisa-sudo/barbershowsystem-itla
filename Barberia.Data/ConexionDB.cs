namespace Barberia.Data
{
    // Clase que guarda el string de conexion a la base de datos
    // Aqui se cambia el nombre del servidor si es necesario
    public static class ConexionDB
    {
        // String de conexion a SQL Server
        // IMPORTANTE: Cambiar "NOMBRE_SERVIDOR" por el nombre de tu servidor local
        // Ejemplo: "DESKTOP-ABC123" o "localhost" o ".\SQLEXPRESS"
        public static string CadenaConexion =
            "Server=localhost;Database=BarberiaDB;Trusted_Connection=True;TrustServerCertificate=True;";
    }
}

namespace Barberia.Domain
{
    // Clase que representa una visita de un cliente a la barberia
    // Relaciona un cliente con un servicio en una fecha especifica
    public class Visita
    {
        // Identificador unico de la visita
        public int Id { get; set; }

        // Id del cliente que hizo la visita
        public int ClienteId { get; set; }

        // Id del servicio que se le realizo
        public int ServicioId { get; set; }

        // Fecha en que se realizo la visita
        public DateTime Fecha { get; set; }

        // Nombre del cliente (para mostrar en consultas)
        public string? NombreCliente { get; set; }

        // Nombre del servicio (para mostrar en consultas)
        public string? NombreServicio { get; set; }
    }
}

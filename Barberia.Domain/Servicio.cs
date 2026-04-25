namespace Barberia.Domain
{
    // Clase que representa un servicio que ofrece la barberia
    // Por ejemplo: corte de cabello, afeitado, etc.
    public class Servicio
    {
        // Identificador unico del servicio
        public int Id { get; set; }

        // Nombre del servicio (ej: "Corte clasico")
        public string Nombre { get; set; } = string.Empty;

        // Precio del servicio en pesos
        public decimal Precio { get; set; }
    }
}

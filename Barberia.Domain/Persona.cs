namespace Barberia.Domain
{
    // Clase abstracta que representa a una persona en general
    // No se puede instanciar directamente, solo se usa como base
    public abstract class Persona
    {
        // Identificador unico de la persona
        public int Id { get; set; }

        // Nombre completo de la persona
        public string Nombre { get; set; } = string.Empty;

        // Numero de telefono de la persona
        public string Telefono { get; set; } = string.Empty;
    }
}

namespace Barberia.Domain
{
    // Clase Cliente que hereda de Persona
    // Representa a un cliente de la barberia
    public class Cliente : Persona
    {
        // Correo electronico del cliente
        public string Email { get; set; } = string.Empty;
    }
}

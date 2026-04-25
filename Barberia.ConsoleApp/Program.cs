using Barberia.Domain;
using Barberia.Data;

namespace Barberia.ConsoleApp
{
    // Clase principal del programa
    // Aqui esta el menu de la aplicacion de consola
    class Program
    {
        // Creamos los repositorios que vamos a usar en todo el programa
        static ClienteRepositorio clienteRepo = new ClienteRepositorio();
        static ServicioRepositorio servicioRepo = new ServicioRepositorio();
        static VisitaRepositorio visitaRepo = new VisitaRepositorio();

        static void Main(string[] args)
        {
            // Variable para controlar el ciclo del menu
            bool ejecutando = true;

            Console.WriteLine("=========================================");
            Console.WriteLine("   SISTEMA DE GESTION - BARBERIA APP");
            Console.WriteLine("=========================================");
            Console.WriteLine();

            while (ejecutando)
            {
                // Mostramos el menu principal
                MostrarMenu();

                // Leemos la opcion que eligio el usuario
                string? opcion = Console.ReadLine();

                // Ejecutamos la opcion elegida
                switch (opcion)
                {
                    case "1":
                        RegistrarCliente();
                        break;
                    case "2":
                        VerClientes();
                        break;
                    case "3":
                        BuscarClientePorNombre();
                        break;
                    case "4":
                        RegistrarVisita();
                        break;
                    case "5":
                        VerVisitas();
                        break;
                    case "6":
                        ejecutando = false;
                        Console.WriteLine("\nGracias por usar Barberia App. Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("\nOpcion no valida. Intente de nuevo.");
                        break;
                }

                Console.WriteLine();
            }
        }

        // Metodo para mostrar el menu principal
        static void MostrarMenu()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("            MENU PRINCIPAL");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("  1) Registrar cliente");
            Console.WriteLine("  2) Ver clientes");
            Console.WriteLine("  3) Buscar cliente por nombre");
            Console.WriteLine("  4) Registrar visita");
            Console.WriteLine("  5) Ver visitas");
            Console.WriteLine("  6) Salir");
            Console.WriteLine("-----------------------------------------");
            Console.Write("Seleccione una opcion: ");
        }

        // Metodo para registrar un nuevo cliente
        static void RegistrarCliente()
        {
            Console.WriteLine("\n--- REGISTRAR NUEVO CLIENTE ---\n");

            // Pedimos los datos del cliente
            Console.Write("Nombre completo: ");
            string nombre = Console.ReadLine() ?? "";

            Console.Write("Telefono: ");
            string telefono = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            // Creamos el objeto cliente con los datos ingresados
            Cliente nuevoCliente = new Cliente
            {
                Nombre = nombre,
                Telefono = telefono,
                Email = email
            };

            try
            {
                // Guardamos el cliente en la base de datos
                clienteRepo.Agregar(nuevoCliente);
                Console.WriteLine("\nCliente registrado exitosamente!");
            }
            catch (Exception ex)
            {
                // Si hay un error lo mostramos
                Console.WriteLine($"\nError al registrar el cliente: {ex.Message}");
            }
        }

        // Metodo para ver todos los clientes
        static void VerClientes()
        {
            Console.WriteLine("\n--- LISTA DE CLIENTES ---\n");

            try
            {
                List<Cliente> clientes = clienteRepo.ObtenerTodos();

                if (clientes.Count == 0)
                {
                    Console.WriteLine("No hay clientes registrados.");
                    return;
                }

                // Mostramos los clientes en formato de tabla
                Console.WriteLine($"{"ID",-5} {"NOMBRE",-25} {"TELEFONO",-15} {"EMAIL",-30}");
                Console.WriteLine(new string('-', 75));

                foreach (Cliente c in clientes)
                {
                    Console.WriteLine($"{c.Id,-5} {c.Nombre,-25} {c.Telefono,-15} {c.Email,-30}");
                }

                Console.WriteLine($"\nTotal de clientes: {clientes.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError al obtener los clientes: {ex.Message}");
            }
        }

        // Metodo para buscar clientes por nombre
        static void BuscarClientePorNombre()
        {
            Console.WriteLine("\n--- BUSCAR CLIENTE POR NOMBRE ---\n");

            Console.Write("Ingrese el nombre a buscar: ");
            string nombre = Console.ReadLine() ?? "";

            try
            {
                List<Cliente> clientes = clienteRepo.BuscarPorNombre(nombre);

                if (clientes.Count == 0)
                {
                    Console.WriteLine($"\nNo se encontraron clientes con el nombre '{nombre}'.");
                    return;
                }

                Console.WriteLine($"\nSe encontraron {clientes.Count} resultado(s):\n");
                Console.WriteLine($"{"ID",-5} {"NOMBRE",-25} {"TELEFONO",-15} {"EMAIL",-30}");
                Console.WriteLine(new string('-', 75));

                foreach (Cliente c in clientes)
                {
                    Console.WriteLine($"{c.Id,-5} {c.Nombre,-25} {c.Telefono,-15} {c.Email,-30}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError al buscar clientes: {ex.Message}");
            }
        }

        // Metodo para registrar una visita
        static void RegistrarVisita()
        {
            Console.WriteLine("\n--- REGISTRAR NUEVA VISITA ---\n");

            try
            {
                // Primero mostramos los clientes disponibles
                List<Cliente> clientes = clienteRepo.ObtenerTodos();

                if (clientes.Count == 0)
                {
                    Console.WriteLine("No hay clientes registrados. Primero registre un cliente.");
                    return;
                }

                Console.WriteLine("Clientes disponibles:");
                foreach (Cliente c in clientes)
                {
                    Console.WriteLine($"  {c.Id} - {c.Nombre}");
                }

                Console.Write("\nIngrese el ID del cliente: ");
                if (!int.TryParse(Console.ReadLine(), out int clienteId))
                {
                    Console.WriteLine("ID no valido.");
                    return;
                }

                // Ahora mostramos los servicios disponibles
                List<Servicio> servicios = servicioRepo.ObtenerTodos();

                if (servicios.Count == 0)
                {
                    Console.WriteLine("No hay servicios registrados. Primero ejecute el script SQL con los datos iniciales.");
                    return;
                }

                Console.WriteLine("\nServicios disponibles:");
                foreach (Servicio s in servicios)
                {
                    Console.WriteLine($"  {s.Id} - {s.Nombre} (${s.Precio})");
                }

                Console.Write("\nIngrese el ID del servicio: ");
                if (!int.TryParse(Console.ReadLine(), out int servicioId))
                {
                    Console.WriteLine("ID no valido.");
                    return;
                }

                // Creamos la visita con la fecha actual
                Visita nuevaVisita = new Visita
                {
                    ClienteId = clienteId,
                    ServicioId = servicioId,
                    Fecha = DateTime.Now
                };

                visitaRepo.Agregar(nuevaVisita);
                Console.WriteLine("\nVisita registrada exitosamente!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError al registrar la visita: {ex.Message}");
            }
        }

        // Metodo para ver todas las visitas
        static void VerVisitas()
        {
            Console.WriteLine("\n--- HISTORIAL DE VISITAS ---\n");

            try
            {
                List<Visita> visitas = visitaRepo.ObtenerTodas();

                if (visitas.Count == 0)
                {
                    Console.WriteLine("No hay visitas registradas.");
                    return;
                }

                // Mostramos las visitas en formato de tabla
                Console.WriteLine($"{"ID",-5} {"CLIENTE",-25} {"SERVICIO",-20} {"FECHA",-20}");
                Console.WriteLine(new string('-', 70));

                foreach (Visita v in visitas)
                {
                    Console.WriteLine($"{v.Id,-5} {v.NombreCliente,-25} {v.NombreServicio,-20} {v.Fecha.ToString("dd/MM/yyyy HH:mm"),-20}");
                }

                Console.WriteLine($"\nTotal de visitas: {visitas.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError al obtener las visitas: {ex.Message}");
            }
        }
    }
}

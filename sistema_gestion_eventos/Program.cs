using System;
using System.Data.SQLite;

class Program
{
    static void Main()
    {
        string connectionString = "Data Source=events.db;Version=3;";
        var dbManager = new DatabaseManager(connectionString);
        dbManager.InitializeDatabase();
        Console.Clear();
        Console.WriteLine("===========================================");
        Console.WriteLine("         SISTEMA DE GESTIÓN DE EVENTOS");
        Console.WriteLine("===========================================");
        Console.WriteLine();
        bool running = true;
        while (running)
        {

            Console.WriteLine("1. Agregar Evento");
            Console.WriteLine("2. Agregar Participante");
            Console.WriteLine("3. Listar Eventos");
            Console.WriteLine("4. Listar Participantes");
            Console.WriteLine("5. Actualizar Evento");
            Console.WriteLine("6. Eliminar Evento");
            Console.WriteLine("7. Editar Participante"); 
            Console.WriteLine("8. Eliminar Participante");
            Console.WriteLine("9. Salir");
            Console.WriteLine("Eliga una opcion:");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    // Lógica para agregar un evento
                    Console.Write("Introduce el nombre del evento: ");
                    string name = Console.ReadLine();
                    Console.Write("Introduce la fecha del evento (yyyy-mm-dd): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
                    {
                        Console.WriteLine("Fecha inválida. Intenta de nuevo.");
                        break;
                    }
                    Console.Write("Introduce la ubicación del evento: ");
                    string location = Console.ReadLine();
                    Console.Write("Introduce una descripción del evento: ");
                    string description = Console.ReadLine();
                    var newEvent = new Event { Name = name, Date = date, Location = location, Description = description };
                    dbManager.AddEvent(newEvent);
                    Console.WriteLine("Evento agregado exitosamente.");
                    break;

                case "2":
                    // Lógica para agregar un participante
                    Console.Write("Introduce el ID del evento: ");
                    if (!int.TryParse(Console.ReadLine(), out int eventId))
                    {
                        Console.WriteLine("ID inválido. Intenta de nuevo.");
                        break;
                    }
                    Console.Write("Introduce el nombre del participante: ");
                    string participantName = Console.ReadLine();
                    Console.Write("Introduce el correo electrónico del participante: ");
                    string email = Console.ReadLine();
                    Console.Write("Introduce el teléfono del participante: ");
                    string phone = Console.ReadLine();
                    var participant = new Participant { EventId = eventId, Name = participantName, Email = email, Phone = phone };
                    dbManager.AddParticipant(participant);
                    Console.WriteLine("Participante agregado exitosamente.");
                    break;

                case "3":
                    // Listar eventos
                    Console.WriteLine("Eventos registrados:");
                    dbManager.ListEvents();
                    break;

                case "4":
                    // Listar participantes
                    Console.WriteLine("Participantes registrados:");
                    dbManager.ListParticipants();
                    break;

                case "5":
                    // Lógica para actualizar un evento
                    Console.Write("Introduce el ID del evento a actualizar: ");
                    if (!int.TryParse(Console.ReadLine(), out int updateId))
                    {
                        Console.WriteLine("ID inválido. Intenta de nuevo.");
                        break;
                    }
                    Console.Write("Introduce el nuevo nombre del evento: ");
                    string newName = Console.ReadLine();
                    Console.Write("Introduce la nueva fecha del evento (yyyy-mm-dd): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime newDate))
                    {
                        Console.WriteLine("Fecha inválida. Intenta de nuevo.");
                        break;
                    }
                    Console.Write("Introduce la nueva ubicación del evento: ");
                    string newLocation = Console.ReadLine();
                    Console.Write("Introduce la nueva descripción del evento: ");
                    string newDescription = Console.ReadLine();
                    var updatedEvent = new Event { Id = updateId, Name = newName, Date = newDate, Location = newLocation, Description = newDescription };
                    dbManager.UpdateEvent(updatedEvent);
                    Console.WriteLine("Evento actualizado exitosamente.");
                    break;

                case "6":
                    // Lógica para eliminar un evento
                    Console.Write("Introduce el ID del evento a eliminar: ");
                    if (!int.TryParse(Console.ReadLine(), out int deleteId))
                    {
                        Console.WriteLine("ID inválido. Intenta de nuevo.");
                        break;
                    }
                    dbManager.DeleteEvent(deleteId);
                    Console.WriteLine("Evento eliminado exitosamente.");
                    break;
                case "7":
                    // Lógica para editar un participante
                    Console.Write("Introduce el ID del participante a editar: ");
                    if (!int.TryParse(Console.ReadLine(), out int participantId))
                    {
                        Console.WriteLine("ID inválido. Intenta de nuevo.");
                        break;
                    }
                    Console.Write("Introduce el nuevo nombre del participante: ");
                    string newParticipantName = Console.ReadLine();
                    Console.Write("Introduce el nuevo correo electrónico del participante: ");
                    string newEmail = Console.ReadLine();
                    Console.Write("Introduce el nuevo teléfono del participante: ");
                    string newPhone = Console.ReadLine();
                    var updatedParticipant = new Participant { Id = participantId, Name = newParticipantName, Email = newEmail, Phone = newPhone };
                    dbManager.UpdateParticipant(updatedParticipant);
                    Console.WriteLine("Participante actualizado exitosamente.");
                    break;

                case "8":
                    // Lógica para eliminar un participante
                    Console.Write("Introduce el ID del participante a eliminar: ");
                    if (!int.TryParse(Console.ReadLine(), out int deleteParticipantId))
                    {
                        Console.WriteLine("ID inválido. Intenta de nuevo.");
                        break;
                    }
                    dbManager.DeleteParticipant(deleteParticipantId);
                    Console.WriteLine("Participante eliminado exitosamente.");
                    break;

                case "9":
                    running = false;
                    Console.WriteLine("Saliendo del programa...");
                    break;

                default:
                    Console.WriteLine("Opción inválida, por favor intenta de nuevo.");
                    break;
            }

            // Espera que el usuario presione una tecla antes de continuar
            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey();
        }
    }
}



























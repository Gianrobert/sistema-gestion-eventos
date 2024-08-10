using System.Data.SQLite;

public class DatabaseManager
{
    private readonly string _connectionString;

    public DatabaseManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void InitializeDatabase()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            // Crear tablas si no existen
            var createEventsTable = "CREATE TABLE IF NOT EXISTS Events (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Date TEXT, Location TEXT, Description TEXT)";
            var createParticipantsTable = "CREATE TABLE IF NOT EXISTS Participants (Id INTEGER PRIMARY KEY AUTOINCREMENT, EventId INTEGER, Name TEXT, Email TEXT, Phone TEXT)";

            using (var command = new SQLiteCommand(createEventsTable, connection))
            {
                command.ExecuteNonQuery();
            }
            using (var command = new SQLiteCommand(createParticipantsTable, connection))
            {
                command.ExecuteNonQuery();
            }

            // Reiniciar los IDs si las tablas están vacías
            ResetAutoIncrement("Events");
            ResetAutoIncrement("Participants");
        }
    }

    private void ResetAutoIncrement(string tableName)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            // Verificar si la tabla está vacía
            var checkEmptyQuery = $"SELECT COUNT(*) FROM {tableName}";
            using (var command = new SQLiteCommand(checkEmptyQuery, connection))
            {
                long count = (long)command.ExecuteScalar();

                if (count == 0)
                {
                    // Reiniciar el valor de AUTOINCREMENT en la tabla sqlite_sequence
                    var resetAutoIncrementQuery = $"DELETE FROM sqlite_sequence WHERE name='{tableName}'";
                    using (var resetCommand = new SQLiteCommand(resetAutoIncrementQuery, connection))
                    {
                        resetCommand.ExecuteNonQuery();
                    }
                }
            }
        }
    }


    public void AddEvent(Event ev)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var query = "INSERT INTO Events (Name, Date, Location, Description) VALUES (@Name, @Date, @Location, @Description)";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", ev.Name);
                command.Parameters.AddWithValue("@Date", ev.Date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@Location", ev.Location);
                command.Parameters.AddWithValue("@Description", ev.Description);
                command.ExecuteNonQuery();
            }
        }
    }

    public void AddParticipant(Participant participant)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var query = "INSERT INTO Participants (EventId, Name, Email, Phone) VALUES (@EventId, @Name, @Email, @Phone)";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@EventId", participant.EventId);
                command.Parameters.AddWithValue("@Name", participant.Name);
                command.Parameters.AddWithValue("@Email", participant.Email);
                command.Parameters.AddWithValue("@Phone", participant.Phone);
                command.ExecuteNonQuery();
            }
        }
    }

    public void ListEvents()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var query = "SELECT * FROM Events";
            using (var command = new SQLiteCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["Id"]}, Nombre: {reader["Name"]}, Fecha: {reader["Date"]}, Ubicación: {reader["Location"]}, Descripción: {reader["Description"]}");
                    }
                }
            }
        }
    }

    public void ListParticipants()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var query = "SELECT * FROM Participants";
            using (var command = new SQLiteCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["Id"]}, Evento ID: {reader["EventId"]}, Nombre: {reader["Name"]}, Correo: {reader["Email"]}, Teléfono: {reader["Phone"]}");
                    }
                }
            }
        }
    }

    public void UpdateEvent(Event ev)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var query = "UPDATE Events SET Name = @Name, Date = @Date, Location = @Location, Description = @Description WHERE Id = @Id";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nombre", ev.Name);
                command.Parameters.AddWithValue("@fecha", ev.Date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@Locacion", ev.Location);
                command.Parameters.AddWithValue("@Descripcion", ev.Description);
                command.Parameters.AddWithValue("@Id", ev.Id);
               
            }
        }
    }
    public void DeleteEvent(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var query = "DELETE FROM Events WHERE Id = @Id";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
 
    public void UpdateParticipant(Participant participant)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var query = "UPDATE Participants SET Name = @Name, Email = @Email, Phone = @Phone WHERE Id = @Id";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", participant.Name);
                command.Parameters.AddWithValue("@Email", participant.Email);
                command.Parameters.AddWithValue("@Phone", participant.Phone);
                command.Parameters.AddWithValue("@Id", participant.Id);
                command.ExecuteNonQuery();
            }
        }
    }
    public void DeleteParticipant(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var query = "DELETE FROM Participants WHERE Id = @Id";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }



}



using System.Data.SqlClient;

// BAD, Encrypt not specified
try
            {
                WeatherForecast item = null;
                using SqlConnection connection = new SqlConnection("Server=localhost;Database=Todo;User Id=sa;Password=Password123;");
 flows to this SQL connection and does not specify Encrypt=True.
CodeQL
                connection.OpenAsync();
                
                string selectCommand = "SELECT * FROM WeatherForecast WHERE id = " + id.ToString();

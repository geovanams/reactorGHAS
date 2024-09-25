using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace UnsecureApp.Controllers
{
    public class MyController
    {

        public string ReadFile(string userInput)
        {
            // Validate user input
            if (userInput.Contains("..") || userInput.Contains("/") || userInput.Contains("\\"))
            {
                throw new ArgumentException("Invalid path");
            }

            string safeDirectory = "/safe/directory"; // Define a safe directory
            string filePath = Path.GetFullPath(Path.Combine(safeDirectory, userInput));

            // Ensure the path stays within the safe directory
            if (!filePath.StartsWith(safeDirectory + Path.DirectorySeparatorChar))
            {
                throw new ArgumentException("Invalid path");
            }

            using (FileStream fs = File.Open(filePath, FileMode.Open))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);

                while (fs.Read(b, 0, b.Length) > 0)
                {
                    return temp.GetString(b);
                }
            }

            return null;
        }

        public int GetProduct(string productName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT ProductId FROM Products WHERE ProductName = @productName", connection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@productName", productName);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                return reader.GetInt32(0); 
            }
        }

        public void GetObject()
        {
            try
            {
                object o = null;
                o.ToString();
            }
            catch (Exception e)
            {
                this.Response.Write(e.ToString());
            }
        
        }

        private string connectionString = "";
    }
}
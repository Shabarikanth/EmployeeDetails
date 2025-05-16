namespace EmployeeDetails.Models
{
    public class DataTransactions
    {
        private readonly string _connectionString;
        public DataTransactions(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}

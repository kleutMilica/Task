public interface IEmployeeService
{
    Task<Dictionary<string, double>> GetEmployeeTableAsync();
}

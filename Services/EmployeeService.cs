public class EmployeeService : IEmployeeService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private IConfiguration _configuration;
    private readonly string _endpoint;

    public EmployeeService(IHttpClientFactory httpClientFactory,IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _endpoint = _configuration.GetValue<string>("MyConfigurations:Endpoint");
    }

    public async Task<Dictionary<string, double>> GetEmployeeTableAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync(_endpoint);

        var employeeTable = new Dictionary<string, double>();

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsAsync<List<Employee>>();

            foreach (var d in data)
            {
                var name = d.EmployeeName;
                var startTimeStr = d.StarTimeUtc;
                var endTimeStr = d.EndTimeUtc;

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(startTimeStr) && !string.IsNullOrEmpty(endTimeStr))
                {
                    var startTime = DateTime.Parse(startTimeStr);
                    var endTime = DateTime.Parse(endTimeStr);
                    var workedHours = Math.Abs((endTime - startTime).TotalHours);
                    
                    if(!string.IsNullOrEmpty(name)){

                        if (employeeTable.ContainsKey(name))
                        {
                            employeeTable[name] += workedHours;
                        }
                        else
                        {
                            employeeTable[name] = workedHours;
                        }

                    }
                }
            }

            employeeTable = employeeTable.OrderBy(e => e.Value).ToDictionary(k => k.Key, v => v.Value);
        }

        return employeeTable;
    }
}

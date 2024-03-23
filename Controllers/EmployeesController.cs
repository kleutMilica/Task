using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class EmployeesController : Controller
{
    private readonly IHttpClientFactory _httpClientFactroy;
    private readonly string endpoint = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";

    public EmployeesController(IHttpClientFactory httpClientFactroy){
        _httpClientFactroy=httpClientFactroy;
    }

    [HttpGet]
    public async Task<IActionResult> employeesTable()
    {
        var client = _httpClientFactroy.CreateClient();
        var response = await client.GetAsync(endpoint);

        if(response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsAsync<List<Employee>>();
            var employeeTable = new Dictionary<string,double>();
            
            foreach (var d in data)
            {
                var name = d.EmployeeName;
                var startTimeStr = d.StarTimeUtc;
                var endTimeStr = d.EndTimeUtc;
                
                if(!string.IsNullOrEmpty(startTimeStr) && !string.IsNullOrEmpty(endTimeStr)){

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

        var sortedEmployees = employeeTable.OrderBy( e => e.Value);
        
        return View(sortedEmployees);
        }
        else{
            return Content("Error fetching data from API");
        }

        
    }
}

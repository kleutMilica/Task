using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("/")]
public class EmployeesController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService){
        _employeeService=employeeService;
    }

    [HttpGet("table")]
    public async Task<IActionResult> employeesTable()
    {
        var sortedEmployees = await _employeeService.GetEmployeeTableAsync();
        if(sortedEmployees != null){
            return View(sortedEmployees);
        }
        else{
            return Content("Error fetching data from API");
        }
        
    }

    [HttpGet]
    public async Task<IActionResult> pieChart()
    {
        var sortedEmployees = await _employeeService.GetEmployeeTableAsync();
        if(sortedEmployees != null){
            return View(sortedEmployees);
        }
        else{
            return Content("Error fetching data from API");
        }
    }


}
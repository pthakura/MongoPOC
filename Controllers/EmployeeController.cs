using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Linq;
using System.Xml.Linq;
using TutorialMongo.Framewrok;
using TutorialMongo.Framewrok.Contract;
using TutorialMongo.Repository;

namespace TutorialMongo.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmployeeController : ControllerBase
  {

    private readonly IBaseMongoRepository _baseMongoRepository;
    public EmployeeController(IBaseMongoRepository baseMongoRepository) {
      _baseMongoRepository = baseMongoRepository;
    }

    [HttpGet]
    public List<EmployeeDetails> GetEmployees(string name) {

      var filterDefinition = Builders<EmployeeDetails>.Filter.Where(p => p.Name.Contains(name));
      return _baseMongoRepository.LoadData<EmployeeDetails>("EmployeeDetails", filterDefinition);

    }


    [HttpPost("InsertOneEmployee")]

    public void InsertEmployee(EmployeeDetails employee) {

      _baseMongoRepository.InsertData(employee, "EmployeeDetails");
    }



    [HttpPost("InsertManyEmployees")]
    public void InsertEmployees(List<EmployeeDetails> employees) {

      _baseMongoRepository.InsertManyData(employees, "EmployeeDetails");
    }


    [HttpPost("UpdateOneEmployee")]
    public void UpdateEmployee(EmployeeDetails employee) {
      var filterDefinition = Builders<EmployeeDetails>.Filter.Eq(p => p.EmployeeID, employee.EmployeeID);
      var updateDefinition = Builders<EmployeeDetails>.Update.Set(p => p.Address, employee.Address)
        .Set(p => p.Name, employee.Name)
        .Set(p => p.DateOfBirth, employee.DateOfBirth);
      _baseMongoRepository.FindOneAndUpdate("EmployeeDetails", filterDefinition, updateDefinition);
    }

    [HttpPost("UpdateManyEmployeesAddress")]
    public void UpdateManyEmployees(string  employeeName, string address) {
      var filterDefinition = Builders<EmployeeDetails>.Filter.Where(p => p.Name.Contains(employeeName));
      var updateDefinition = Builders<EmployeeDetails>.Update.Set(p => p.Address, address);
      _baseMongoRepository.UpdateData("EmployeeDetails", filterDefinition, updateDefinition);
    }

    [HttpPost("RemoveEmployee")]
    public void RemoveEmployee(EmployeeDetails employee) {
      var filterDefinition1 = Builders<EmployeeDetails>.Filter.Eq(p => p.Name, employee.Name);
      var filterDefinition2 = Builders<EmployeeDetails>.Filter.Eq(p => p.Address, employee.Address);

      _baseMongoRepository.RemoveData("EmployeeDetails", filterDefinition1 & filterDefinition2);
    }
  }
}

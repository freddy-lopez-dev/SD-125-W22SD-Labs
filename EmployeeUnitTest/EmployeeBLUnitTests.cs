using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Moq;
using MOQ_Ex_1.BLL;
using MOQ_Ex_1.Data;
using MOQ_Ex_1.DLL;
using MOQ_Ex_1.Models;

namespace EmployeeUnitTest
{
    [TestClass]
    public class EmployeeBLUnitTests
    {
        private EmployeeBusinessLogic BusinessLogic;
        public EmployeeBLUnitTests()
        {
            //DbSet
            var data = new List<Employee>
            {
                new Employee{Name = "Mock Employee Juan", Salary = 30000, HiringDate = DateTime.Parse("06/15/2019")},
                new Employee{Name = "Mock Employee Tu", Salary = 20000, HiringDate = DateTime.Parse("06/15/2018")},
                new Employee{Name = "Mock Employee Trie", Salary = 10000, HiringDate = DateTime.Parse("06/15/2015")},
                new Employee{Name = "Mock Employee Por", Salary = 50000, HiringDate = DateTime.Parse("06/15/2010")}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Employee>>();

            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<MOQ_Ex_1Context>();
            mockContext.Setup(m => m.Employee).Returns(mockDbSet.Object);

            BusinessLogic = new EmployeeBusinessLogic(new EmployeeRepository(mockContext.Object));
        }

        [DataRow(true, "true")]
        [TestMethod]
        public void UpdateTicketStatus(bool validInput, string InvalidOutput)
        {
            h
        }
    }
}
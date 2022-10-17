using Microsoft.EntityFrameworkCore;
using Moq;
using SD_125_W22SD_Lab_MVC.Models;
using System.Collections.Generic;

namespace ParkoMaticUnitTest
{
    [TestClass]
    public class ParkingHelperUnitTests
    {
        private ParkingContext _context;
        private ParkingHelper ParkingHelper;

        public ParkingHelperUnitTests()
        {
            var Data = new List<Pass>
            {
                new Pass{ID = 1},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Pass>>();

            mockDbSet.As<IQueryable<Pass>>().Setup(m => m.Provider).Returns(Data.Provider);
            mockDbSet.As<IQueryable<Pass>>().Setup(m => m.Expression).Returns(Data.Expression);
            mockDbSet.As<IQueryable<Pass>>().Setup(m => m.ElementType).Returns(Data.ElementType);
            mockDbSet.As<IQueryable<Pass>>().Setup(m => m.GetEnumerator()).Returns(Data.GetEnumerator());

            var mockContext = new Mock<ParkingContext>();
            mockContext.Setup(p => p.Passes).Returns(mockDbSet.Object);
            _context = mockContext.Object;
            ParkingHelper = new ParkingHelper(mockContext.Object);
        }

        [DataRow(1)]
        [TestMethod]
        public void CreatePass_ValidPassParam_GeneratesANewPass(int expectedPassCount)
        {
            ParkingHelper.CreatePass("test", true, 5);
            int actualPassCount = _context.Passes.Count();

            Assert.AreEqual(expectedPassCount, actualPassCount);
        }

        [DataRow("be", true, 5)]
        [DataRow("be", true, 2)]
        [DataRow("teascs", true, 0)]
        [TestMethod]
        public void CreatePass_InvalidInput_ThrowExceptionWhenPurchaserCharIsOutOfRange(string purchaser, bool prem, int capacity)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                ParkingHelper.CreatePass(purchaser, prem, capacity);
            });
        }
    }
}
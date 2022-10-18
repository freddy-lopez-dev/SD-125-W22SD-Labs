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
            // Pass Mock Data
            var passData = new List<Pass>
            {
                new Pass{ID = 1, Capacity = 5, Premium = false, Purchaser = "Jane"},
                new Pass{ID = 2, Capacity = 5, Premium = false, Purchaser = "Joz"},
                new Pass{ID = 3, Capacity = 5, Premium = false, Purchaser = "Joseph"},
                new Pass{ID = 4, Capacity = 5, Premium = false, Purchaser = "Jacob"},
                new Pass{ID = 5, Capacity = 5, Premium = false, Purchaser = "Julian"},
            }.AsQueryable();

            var passMockDbSet = new Mock<DbSet<Pass>>();

            passMockDbSet.As<IQueryable<Pass>>().Setup(m => m.Provider).Returns(passData.Provider);
            passMockDbSet.As<IQueryable<Pass>>().Setup(m => m.Expression).Returns(passData.Expression);
            passMockDbSet.As<IQueryable<Pass>>().Setup(m => m.ElementType).Returns(passData.ElementType);
            passMockDbSet.As<IQueryable<Pass>>().Setup(m => m.GetEnumerator()).Returns(passData.GetEnumerator());

            // Parking Spot Mock Data
            var PSData = new List<ParkingSpot>
            {
                //Reservation
                new ParkingSpot{ID = 1, Occupied = false},
                new ParkingSpot{ID = 2, Occupied = false},
                new ParkingSpot{ID = 3, Occupied = false},
                new ParkingSpot{ID = 4, Occupied = false},
                new ParkingSpot{ID = 5, Occupied = false},
            }.AsQueryable();

            var PSmockDbSet = new Mock<DbSet<ParkingSpot>>();

            PSmockDbSet.As<IQueryable<ParkingSpot>>().Setup(m => m.Provider).Returns(PSData.Provider);
            PSmockDbSet.As<IQueryable<ParkingSpot>>().Setup(m => m.Expression).Returns(PSData.Expression);
            PSmockDbSet.As<IQueryable<ParkingSpot>>().Setup(m => m.ElementType).Returns(PSData.ElementType);
            PSmockDbSet.As<IQueryable<ParkingSpot>>().Setup(m => m.GetEnumerator()).Returns(PSData.GetEnumerator());

            // Vehicle Mock Data
            var VehicleData = new List<Vehicle>
            {
                new Vehicle{ID = 1, Parked = false, Licence = "129-291"},
                new Vehicle{ID = 2, Parked = false, Licence = "129-231"},
                new Vehicle{ID = 3, Parked = false, Licence = "129-124"},
                new Vehicle{ID = 4, Parked = false, Licence = "129-242"},
                new Vehicle{ID = 5, Parked = false, Licence = "129-211"},
            }.AsQueryable();

            var VehicleMockDbSet = new Mock<DbSet<Vehicle>>();

            VehicleMockDbSet.As<IQueryable<Vehicle>>().Setup(m => m.Provider).Returns(VehicleData.Provider);
            VehicleMockDbSet.As<IQueryable<Vehicle>>().Setup(m => m.Expression).Returns(VehicleData.Expression);
            VehicleMockDbSet.As<IQueryable<Vehicle>>().Setup(m => m.ElementType).Returns(VehicleData.ElementType);
            VehicleMockDbSet.As<IQueryable<Vehicle>>().Setup(m => m.GetEnumerator()).Returns(VehicleData.GetEnumerator());

            // Reservation Mock Data
            var ReservationData = new List<Reservation>
            {
                new Reservation{ID = 1},
                new Reservation{ID = 2},
                new Reservation{ID = 3},
                new Reservation{ID = 4},
                new Reservation{ID = 5},
            }.AsQueryable();

            var ReservationMockDbSet = new Mock<DbSet<Reservation>>();

            ReservationMockDbSet.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(ReservationData.Provider);
            ReservationMockDbSet.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(ReservationData.Expression);
            ReservationMockDbSet.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(ReservationData.ElementType);
            ReservationMockDbSet.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(ReservationData.GetEnumerator());

            var mockContext = new Mock<ParkingContext>();
            mockContext.Setup(p => p.Passes).Returns(passMockDbSet.Object);
            mockContext.Setup(v => v.ParkingSpots).Returns(PSmockDbSet.Object);
            mockContext.Setup(ps => ps.Vehicles).Returns(VehicleMockDbSet.Object);
            mockContext.Setup(r => r.Reservations).Returns(ReservationMockDbSet.Object);
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

        [DataRow("InvalidPurchaser", "123-234")]
        [TestMethod]
        public void AddVehicleToPass_InvalidInput_ThrowExceptionWhenPassHolderOrVehicleNotFound(string passHolderName, string vehicleLicence)
        {
            Assert.ThrowsException<NullReferenceException>(() =>
            {
                ParkingHelper.AddVehicleToPass(passHolderName, vehicleLicence);
            });
        }

        [DataRow("Jane", "129-291")]
        [TestMethod]
        public void AddVehicleToPass_OutOfCapacity_ThrowExceptionWhenUserAddAVehicleForOutOfCapacityPass(string passHolderName, string vehicleLicence)
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                ParkingHelper.AddVehicleToPass(passHolderName, vehicleLicence);
            });
        }
    }
}
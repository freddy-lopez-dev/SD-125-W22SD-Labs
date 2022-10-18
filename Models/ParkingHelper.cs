namespace SD_125_W22SD_Lab_MVC.Models
{
    public class ParkingHelper
    {
        private ParkingContext parkingContext;

        public ParkingHelper(ParkingContext context)
        {
            parkingContext = context;
        }

        public Pass CreatePass(string purchaser, bool premium, int capacity)
        {
            if(purchaser.Count() <= 3 || purchaser.Count() >= 20 || capacity <= 0)
            {
                throw new ArgumentOutOfRangeException("Purchaser or capacity is out of range");
            } else
            {
                Pass newPass = new Pass();

                newPass.Purchaser = purchaser;

                newPass.Premium = premium;

                newPass.Capacity = capacity;

                parkingContext.Passes.Add(newPass);

                parkingContext.SaveChanges();

                return newPass;
            }
            

        }

        public ParkingSpot CreateParkingSpot()
        {

            ParkingSpot newSpot = new ParkingSpot();

            newSpot.Occupied = false;

            parkingContext.ParkingSpots.Add(newSpot);

            return newSpot;

        }

        public void AddVehicleToPass(string passHolderName, string vehicleLicence)
        {
            try
            {
                Pass currPass = parkingContext.Passes.First(p => p.Purchaser == passHolderName);
                Vehicle currVehicle = parkingContext.Vehicles.First(v => v.Licence == vehicleLicence);
                currPass.Vehicles.Add(currVehicle);
                if(currPass.Capacity >= currPass.Vehicles.Count)
                {
                    parkingContext.SaveChanges();
                } else
                {
                    throw new IndexOutOfRangeException("Unable to add vehicle as pass capacity is out of range");
                }
                
            } catch
            {
                throw new NullReferenceException("Pass or Vehicle not found");
            }

        }
    }
}


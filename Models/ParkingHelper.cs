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

    }
}


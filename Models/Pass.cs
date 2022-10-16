using System.ComponentModel.DataAnnotations;

namespace SD_125_W22SD_Lab_MVC.Models
{
    public class Pass
    {
        public int ID { get; set; }

        [MinLength(3)]
        [MaxLength(20)]
        public string Purchaser { get; set; }

        public bool Premium { get; set; }
        [MinLength(1)]
        public int Capacity { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}

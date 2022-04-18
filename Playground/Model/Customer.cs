using System.ComponentModel.DataAnnotations;

namespace Playground.Model
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MMM-yyyy}")]
        public DateTime DoB { get; set; }

        [Required]
        public string City { get; set; }


    }
}

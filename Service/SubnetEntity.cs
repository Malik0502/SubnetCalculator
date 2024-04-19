using System.ComponentModel.DataAnnotations;

namespace Service
{
    public class SubnetEntity{

        [Required]
        [MaxLength(15)]
        [MinLength(7)]
        public string? IPAdress {get; set;}

        [Required]
        [MaxLength(15)]
        [MinLength(7)]
        public string? SubnetMask {get; set;}

        [Required]
        public int SubnetAmount {get; set;}
    }
}
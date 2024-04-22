using System.ComponentModel.DataAnnotations;

namespace Service
{
    public class SubnetEntity{

        [Required]
        [StringLength(15, ErrorMessage ="Die Ip-Adresse war zu kurz/ zu lang")]
        public string? IPAdress {get; set;}

        [Required]
        [StringLength(15, ErrorMessage ="Die Subnetzmaske war zu kurz/ zu lang")]
        public string? SubnetMask {get; set;}

        [Required]
        public int SubnetAmount {get; set;}
    }
}
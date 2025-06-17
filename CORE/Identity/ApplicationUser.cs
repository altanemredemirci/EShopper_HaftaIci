using CORE.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CORE.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        [Column(Order=1)]
        public string Name { get; set; }

        [StringLength(50)]
        [Column(Order = 2)]
        public string Surname { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class ClientFavorite : BaseEntity<int>
    {
        public int UserId { get; set; }
        public virtual IdentityUser<int>? User { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}

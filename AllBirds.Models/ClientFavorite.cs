using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class ClientFavorite : BaseEntity<int>
    {
        public int ClientId { get; set; }
        public virtual CustomUser? Client { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}

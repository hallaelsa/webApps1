using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1theAteam.DBModels
{
    public class OrderItem
    {
        public int Id { get; set; }
        
        public virtual Order Order { get; set; }
        public virtual Movie Movie { get; set; }
    }
}

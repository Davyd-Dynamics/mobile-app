using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareWatch.Mobile.Models
{
    public class Patient
    {
        public Guid Id { get; set; }
        public Contact Contact { get; set; }
    }
}

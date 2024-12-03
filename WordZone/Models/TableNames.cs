using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordZone.Models
{
    [Keyless]
    public class TableNames
    {
        public string Name { get; set; }
    }
}

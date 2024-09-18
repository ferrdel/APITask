using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_DataAcccessLayer.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string State { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}

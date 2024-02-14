using Ef_Model.Entities.Abstarct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef_Model.Entities.Concret
{
    public class Car: BaseEntityClass
    {
        public string? Model { get; set; }
        public string? Marka { get; set; }
        public string? Year { get; set; }
        public string? Money { get; set; }
        public Image Image { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
       
    }
}

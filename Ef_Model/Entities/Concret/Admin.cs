using Ef_Model.Entities.Abstarct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ef_Model.Entities.Concret
{
    public class Admin: BaseEntityClass
    {
        
        public string? AdminName { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
       
        


    }
}

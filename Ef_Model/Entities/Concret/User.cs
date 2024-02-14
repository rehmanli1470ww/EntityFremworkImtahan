using Ef_Model.Entities.Abstarct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ef_Model.Entities.Concret
{
    public class User: BaseEntityClass
    {
        public string? UserName { get; set; }
        public string? Surname { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Birthday { get; set; }
        public string? Password { get; set; }
        public ICollection<Car> ?Cars { get; set; }
        

    }
}

using Ef_Model.Entities.Abstarct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef_Model.Entities.Concret
{
    public class Image:BaseEntityClass
    {
        public string FullPath { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime DeleteTime { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }

        public Image()
        {
            CreateTime = DateTime.Now;
        }

    }
}

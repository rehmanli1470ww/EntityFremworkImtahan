using Ef_Model.Entities.Concret;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef_DbAccess.Configuration
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasOne(e => e.Car).WithOne(x => x.Image).HasForeignKey<Image>(e=>e.CarId);
            
        }
    }
}

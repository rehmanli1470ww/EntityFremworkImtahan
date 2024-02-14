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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(e=>e.Cars).WithOne(e=>e.User).HasForeignKey(e=>e.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}

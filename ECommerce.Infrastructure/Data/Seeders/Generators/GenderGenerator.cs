using ECommerce.Infrastructure.Data.Seeders.SeederEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Data.Seeders.Generators
{
    public static class GenderGenerator
    {
        public static List<Gender> Generate()
        {
            return
            [
                new() { Id = Guid.NewGuid(), Name = "Male" },
                new() { Id = Guid.NewGuid(), Name = "Female" },
                new() { Id = Guid.NewGuid(), Name = "Other" }
            ];
        }
    }
}

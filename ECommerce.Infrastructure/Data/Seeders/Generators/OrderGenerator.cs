using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Data.Seeders.Generators
{
    public static class OrderGenerator
    {
        public static List<Guid> Generate(int orderCount)
        {
            var orders = new List<Guid>();
            for (int i = 0; i < orderCount; i++)
                orders.Add(Guid.NewGuid());

            return orders;
        }
    }
}
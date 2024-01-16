using Microsoft.EntityFrameworkCore;
using RentACar.Web.Models;

namespace RentACar.Web.Data
{
    public class RentACarDBContext(DbContextOptions options): DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }

    }
}


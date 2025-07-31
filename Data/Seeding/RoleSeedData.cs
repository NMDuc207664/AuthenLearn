using AuthenLearn.Model;
using Microsoft.EntityFrameworkCore;

namespace AuthenLearn.Data.Seeding
{
    public static class RoleSeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
              new Role
              {
                  Id = 1,
                  RoleName = "Admin"
              },

              new Role
              {
                  Id = 2,
                  RoleName = "User"
              }
            );
        }
    }
}
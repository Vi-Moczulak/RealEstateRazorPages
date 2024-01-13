using Microsoft.EntityFrameworkCore;
using RealEstate.Data;

namespace RealEstate.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RealEstateContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<RealEstateContext>>()))
            {
                if (context == null || context.Estate == null)
                {
                    throw new ArgumentNullException("Null RealEstateContext");
                }

              
                if (!context.Type.Any())
                {
                    context.Type.AddRange(

                        new Type
                        {
                            Name = "Apartment"
                        },
                        new Type
                        {
                            Name = "Beach House"
                        });
                }

                if (!context.Estate.Any())
                {
                    context.Estate.AddRange(
                    new Estate
                    {
                        Name = "Gdynia Beach House",
                        Price = 450000,
                        BedRooms = 2,
                        BathRooms = 1,
                        SquareFeet = 60

                    },

                    new Estate
                    {
                        Name = " Sea view Apartment",
                        Price = 850000,
                        BedRooms = 3,
                        BathRooms = 2,
                        SquareFeet = 60
                    }
                ); ;
                }

                if (!context.Estate.Any() && !context.Type.Any())
                {

                    var estate1 = new Estate
                    {
                        Name = "Gold44 top apartment",
                        Price = 1560000,
                        BedRooms = 3,
                        BathRooms = 3,
                        SquareFeet = 150
                    };

                    context.AddRange(estate1);


                    var type1 = new Type
                    {
                        Name = "Gdańsk",
                        Estates = new List<Estate> { estate1 }
                    };
                    context.AddRange(type1);

                }
                context.SaveChanges();
            }
        }
    }
}

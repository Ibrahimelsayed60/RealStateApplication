using RealState.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RealState.Infrastructure.Data
{
    public static class ApplicationDbContextSeed
    {

        public static async Task SeedAsync(ApplicationDbContext _dbContext)
        {
            if(_dbContext.Viilas.Count() == 0)
            {
                var villaData = File.ReadAllText("../RealState.Infrastructure/Data/DataSeed/villas.json");
                var villass = JsonSerializer.Deserialize<List<Villa>>(villaData);

                if (villass?.Count() > 0)
                {
                    foreach (var villa in villass)
                    {
                        _dbContext.Set<Villa>().Add(villa);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            if(_dbContext.VillaNumbers.Count() == 0)
            {
                var villaNumberData = File.ReadAllText("../RealState.Infrastructure/Data/DataSeed/villaNumbers.json");
                var villaNumbers = JsonSerializer.Deserialize<List<VillaNumber>>(villaNumberData);

                if(villaNumbers?.Count() > 0)
                {
                    foreach(var villaNumber in villaNumbers)
                    {
                        _dbContext.Set<VillaNumber>().Add(villaNumber);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            if(_dbContext.Amenities.Count() == 0)
            {
                var AmenityData = File.ReadAllText("../RealState.Infrastructure/Data/DataSeed/Amenity.json");
                var amenities = JsonSerializer.Deserialize<List<Amenity>>(AmenityData);

                if(amenities?.Count() > 0)
                {
                    foreach( var amenity in amenities)
                    {
                        _dbContext.Set<Amenity>().Add(amenity);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

    }
}

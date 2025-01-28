﻿using RealState.Domain.Entities;
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
        }

    }
}

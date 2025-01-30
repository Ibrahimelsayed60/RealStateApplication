﻿using RealState.Domain.Entities;
using RealState.Domain.Specifications.VillaNumberSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain.Services.Contract
{
    public interface IVillaNumberService
    {

        Task<IEnumerable<VillaNumber>> GetAllVillaNumbers();

        Task<IEnumerable<VillaNumber>> GetAllVillaNumbersWithVillaData();

        Task<VillaNumber?> GetVillaNumberByVilla_number(int villa_number);

        Task<VillaNumber?> GetVillaNumberWithSpecById(int id);

        int CreateVillaNumber(VillaNumber villaNumber);

        int UpdateVillaNumber(VillaNumber villaNumber);

        bool DeleteVillaNumber(VillaNumber villa_number);

        Task<bool> CheckVillaNumberExists(int villa_number);

    }
}

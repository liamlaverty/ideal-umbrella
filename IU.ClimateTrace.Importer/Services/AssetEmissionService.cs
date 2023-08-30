using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IU.ClimateTrace.Importer.Services
{
    internal class AssetEmissionService
    {
        private readonly IRepository<AssetEmission> _assetEmissionRepository;

        public AssetEmissionService(
            IRepository<AssetEmission> assetEmissionRepository
            
            ) 
        {
            _assetEmissionRepository = assetEmissionRepository;

        }

        public async Task UpdateAssetEmissionsFromCsvsAsync()
        {

        }



    }
}

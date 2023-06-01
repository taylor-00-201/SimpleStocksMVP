using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleStocks.Interfaces;
using System.Runtime.CompilerServices;
using SimpleStocks.Models;

namespace SimpleStocks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetRepository _assetRepository;

        public AssetsController(IAssetRepository assetRepository) 
        {
            _assetRepository = assetRepository;
        }

        [HttpPost("AddAsset")]
        public IActionResult AddAsset(Asset asset) 
        {
            _assetRepository.AddAsset(asset);
            return NoContent();
        }


        [HttpDelete("DeleteAsset")]
        public IActionResult RemoveAsset(int Id)
        {
            _assetRepository.RemoveAsset(Id);
            return NoContent();
        }

    }
}

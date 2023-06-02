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

        [HttpGet("AssetById")]
        public IActionResult GetAssetById(int AssetId)
        {
           var returnedAsset = _assetRepository.GetAssetById(AssetId);
            return Ok(returnedAsset);
        }

        [HttpGet("AssetByName")]
        public IActionResult GetAssetByName(string Name)
        {
            var returnedAsset = _assetRepository.GetAssetByName(Name);
            return Ok(returnedAsset);
        }

        [HttpGet("AssetBySymbol")]
        public IActionResult GetAssetBySymbol(string Symbol)
        {
            var returnedAsset = _assetRepository.GetAssetBySymbol(Symbol);
            return Ok(returnedAsset);
        }

        [HttpPost("AllAssets")]
        public IActionResult GetAllAssets()
        {
           var allAssets = _assetRepository.GetAllAssets();
            return Ok(allAssets);
        }


        [HttpDelete("DeleteAsset")]
        public IActionResult RemoveAsset(int Id)
        {
            _assetRepository.RemoveAsset(Id);
            return NoContent();
        }

    }
}

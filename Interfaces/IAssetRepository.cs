using SimpleStocks.Models;

namespace SimpleStocks.Interfaces
{
    public interface IAssetRepository
    {
        void AddAsset(Asset asset);
        List<Asset> GetAllAssets();
        Asset GetAssetById(int AssetId);
        Asset GetAssetByName(string Name);
        Asset GetAssetBySymbol(string Symbol);
        void RemoveAsset(int Id);
    }
}
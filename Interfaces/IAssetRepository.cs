using SimpleStocks.Models;

namespace SimpleStocks.Interfaces
{
    public interface IAssetRepository
    {
        void AddAsset(Asset asset);
        void RemoveAsset(int Id);
    }
}
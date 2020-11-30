using System.Threading.Tasks;

namespace Surveynetic.Client.Core.Interfaces.Services
{
    public interface ILocalStorageService
    {
        Task<T> GetItem<T>(string key);
        Task SetItem<T>(string key, T value);
        Task SetItem<T>(string key, T value, params object[] p);
        Task RemoveItem(string key);
    }
}

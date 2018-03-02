using System.Threading.Tasks;

namespace HatServer.Data
{
    public interface IDbInitializer
    {
        Task Initialize();
    }
}
using System.Threading.Tasks;

namespace YellowClone.Interfaces
{
    public interface IAsyncInitialization
    {
        Task Initialization { get; }
    }
}

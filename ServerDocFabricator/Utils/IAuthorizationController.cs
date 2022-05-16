
using ServerDocFabricator.BL;

namespace ServerDocFabricator.Utils
{
    public interface IAuthorizationController<T>
    {
        T Identity { get; set; }
        List<IBindable> Services { get; set; }
    }
}
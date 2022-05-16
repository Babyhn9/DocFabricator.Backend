using Microsoft.AspNetCore.Mvc.Filters;
using ServerDocFabricator.DAL.Entities;

namespace ServerDocFabricator.Utils.Attributes
{
    public class UseIdentityAttribute : Attribute, IActionFilter, IOrderedFilter
    {
        public int Order => 2;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as IAuthorizationController<UserEntity>;
            if (controller != null && controller.Identity == null)
                controller.Identity = context.HttpContext.Items["User"] as UserEntity;
        }
    }
}

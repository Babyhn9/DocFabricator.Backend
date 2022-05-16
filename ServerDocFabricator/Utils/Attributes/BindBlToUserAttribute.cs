using Microsoft.AspNetCore.Mvc.Filters;
using ServerDocFabricator.DAL.Entities;

namespace ServerDocFabricator.Utils.Attributes
{
    public class BindBlToUserAttribute : Attribute, IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = 5;

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as IAuthorizationController<UserEntity>;
            if (controller != null)
            {
                foreach (var service in controller.Services)
                    service.UserId = controller.Identity.Id;
            }

        }
    }
}
    
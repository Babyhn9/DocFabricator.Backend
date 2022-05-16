using Microsoft.AspNetCore.Mvc.Filters;

namespace ServerDocFabricator.Utils.Attributes
{
    public class RequireRoleAttribute : Attribute, IActionFilter, IOrderedFilter
    {

        public int Order { get; } = 2;
        
        public RequireRoleAttribute()
        {

        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
          
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //if (context.Controller is IAuthorizationController<UserEntity> controller)
            //    if (controller.Identity.Roles.All(el => el.Role != _reqRole))
            //        context.Result = new JsonResult(new {Message = "У вас нет прав"})
            //        {
            //            StatusCode = StatusCodes.Status405MethodNotAllowed
            //        };
        }

    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShoppingCart.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.ActionFilters
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                byte[] encoded = Convert.FromBase64String(context.ActionArguments["id"].ToString());
                var id = new Guid(System.Text.Encoding.UTF8.GetString(encoded));

                var currentLoggedInUser = context.HttpContext.User.Identity.Name;

                IStudentAssignmentsService saService = (IStudentAssignmentsService)context.HttpContext.RequestServices.GetService(typeof(IStudentAssignmentsService));
                var sa = saService.GetStudentAssignment(id);

                bool canView = false;

                if (sa.Student.Email == currentLoggedInUser)
                {
                    canView = true;
                }
                
                if (sa.Student.Teacher.Email == currentLoggedInUser) {
                    canView = true;
                }

                if (canView == false) {
                    context.Result = new UnauthorizedObjectResult("Access Denied");
                }
            }
            catch (Exception ex)
            {
                context.Result = new BadRequestObjectResult("Bad Request");
            }
        }
    }
}

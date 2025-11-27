using ECommerce.Service.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.presentation.Attribute
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInMin;
        public RedisCacheAttribute(int DurationInMin = 5)
        {
            _durationInMin = DurationInMin;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cachekey = CreateCachekey(context.HttpContext.Request);
            var cacheValue = await cacheService.GetAsync(cachekey);
            if (cacheValue is not null)
            {

                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK

                };

            }

            var ExecutedContext = await next.Invoke();

            if (ExecutedContext.Result is OkObjectResult result)
            {
                await cacheService.SetAsync(cachekey, result.Value!, TimeSpan.FromMinutes(5));

            }


            //return base.OnActionExecutionAsync(context, next);
        }





        private string CreateCachekey(HttpRequest request)
        {
            StringBuilder key = new StringBuilder();
            key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(X => X.Key))
            {

                key.Append($"|{item.Key}-{item.Value}");

            }
            return key.ToString();

        }






    }
}

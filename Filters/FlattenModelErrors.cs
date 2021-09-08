﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NDDR
{
    public class FlattenModelErrors : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();

                var responseObj = new
                {
                    //Message = errors
                    Message = errors[0]
                };

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = 400
                };
            }

            base.OnActionExecuting(context);
        }
    }
}
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSystem
{
    public class AddAuthorizeFiltersControllerConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.Actions.Any(a => a.ActionName.Contains("Index"))){
                controller.Filters.Add(new AuthorizeFilter("defaultpolicy"));
            }
            else
            {
                controller.Filters.Add(new AuthorizeFilter("apipolicy"));
            }
        }
    }
}

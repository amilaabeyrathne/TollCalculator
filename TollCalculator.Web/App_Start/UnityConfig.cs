using System.Web.Http;
using System.Web.Mvc;
using TollCalculator.Services;
using TollCalculator.Services.Interfaces;
using Unity;
using Unity.WebApi;

namespace TollCalculator.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IFeesService, FeesService>();
            container.RegisterType<IVehicleService, VehicleService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
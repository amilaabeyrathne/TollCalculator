using System.Web.Mvc;
using TollCalculator.Services;
using TollCalculator.Services.Interfaces;
using Unity;
using Unity.Mvc5;

namespace TollCalculator.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<IVehicleService, VehicleService>();
            container.RegisterType<IFeesService, FeesService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
﻿using LightInject;

using Zilon.Core.CommonServices.Dices;
using Zilon.Core.Schemes;
using Zilon.Core.WorldGeneration;
using Zilon.IoC;

namespace Zilon.Core.MassWorldGenerator
{
    public class Startup
    {
        private readonly string _schemeCatalog;

        public Startup(string schemeCatalog)
        {
            _schemeCatalog = schemeCatalog;
        }

        public void RegisterServices(IServiceRegistry serviceRegistry)
        {
            RegisterSchemeService(serviceRegistry);
            RegisterAuxServices(serviceRegistry);
            RegisterGlobeServices(serviceRegistry);
        }

        private void RegisterGlobeServices(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IWorldGenerator, WorldGenerator>();
        }

        private void RegisterSchemeService(IServiceRegistry container)
        {
            container.Register<ISchemeLocator>(factory =>
            {
                var schemeLocator = new FileSchemeLocator(_schemeCatalog);

                return schemeLocator;
            }, LightInjectWrapper.CreateSingleton());

            container.Register<ISchemeService, SchemeService>(LightInjectWrapper.CreateSingleton());

            container.Register<ISchemeServiceHandlerFactory, SchemeServiceHandlerFactory>(LightInjectWrapper.CreateSingleton());
        }

        /// <summary>
        /// Подготовка дополнительных сервисов
        /// </summary>
        private void RegisterAuxServices(IServiceRegistry container)
        {
            var dice = new LinearDice();
            container.Register<IDice>(factory => dice, LightInjectWrapper.CreateSingleton());
            
        }
    }
}

﻿using System;

using Microsoft.Extensions.DependencyInjection;

using Zilon.Bot.Players.NetCore;
using Zilon.Bot.Players.NetCore.DependencyInjectionExtensions;
using Zilon.Bot.Players.Strategies;
using Zilon.Emulation.Common;

namespace Zilon.Bot.Players.DevelopmentTests
{
    class Startup : InitializationBase
    {
        public override void ConfigureAux(IServiceProvider serviceProvider)
        {
            // Конфигурация дополнительных сервисов для коробочного источника команд не требуется.
        }

        protected override void RegisterBot(IServiceCollection container)
        {
            container.RegisterLogicState();
            container.AddScoped<ILogicStateFactory>(factory => new ContainerLogicStateFactory(factory));
            container.AddScoped<LogicStateTreePatterns>();

            container.AddScoped<HumanBotActorTaskSource>();
        }
    }
}

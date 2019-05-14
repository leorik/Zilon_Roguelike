﻿using LightInject;

using Zilon.Bot.Players.Logics;
using Zilon.Bot.Players.Triggers;

namespace Zilon.Bot.Players.DependencyInjection
{
    public static class LightInjectServiceContainerExtensions
    {
        public static void RegisterBot(this IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<DefeatTargetLogicState>();
            serviceRegistry.Register<IdleLogicState>();
            serviceRegistry.Register<RoamingLogicState>();

            serviceRegistry.Register<CounterOverTrigger>();
            serviceRegistry.Register<IntruderDetectedTrigger>();
            serviceRegistry.Register<LogicOverTrigger>();


        }
    }
}

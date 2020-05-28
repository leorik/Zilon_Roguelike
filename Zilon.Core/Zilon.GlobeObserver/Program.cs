﻿using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Zilon.Core.World;

namespace Zilon.GlobeObserver
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var serviceContainer = new ServiceCollection();
            var startUp = new StartUp();
            startUp.RegisterServices(serviceContainer);

            serviceContainer.AddSingleton<IGlobeInitializer, GlobeInitializer>();

            var serviceProvider = serviceContainer.BuildServiceProvider();

            // Create globe
            var globeInitializer = serviceProvider.GetRequiredService<IGlobeInitializer>();
            var globe = await globeInitializer.CreateGlobeAsync("intro");

            // Iterate globe
            var globeIterationCounter = 0L;
            do
            {
                Console.WriteLine("Iteratin count:");
                var iterationCount = int.Parse(Console.ReadLine());
                for (var i = 0; i < iterationCount; i++)
                {
                    await globe.UpdateAsync();

                    globeIterationCounter++;

                    var hasActors = globe.SectorNodes.SelectMany(x => x.Sector.ActorManager.Items).Any();
                    if (!hasActors)
                    {
                        break;
                    }
                }

                Console.WriteLine($"Globe Iteration: {globeIterationCounter}");
                PrintReport(globe);

            } while (true);
        }

        private static void PrintReport(IGlobe globe)
        {
            var sectorNodesDiscoveredCount = globe.SectorNodes.Count();
            Console.WriteLine($"Sector Nodes Discovered: {sectorNodesDiscoveredCount}");

            var actorCount = globe.SectorNodes.SelectMany(x => x.Sector.ActorManager.Items).Count();
            Console.WriteLine($"Actors: {actorCount}");
        }
    }
}
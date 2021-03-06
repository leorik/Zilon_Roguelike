﻿using System.Linq;

using Zilon.Core.PersonModules;
using Zilon.Core.Persons;
using Zilon.Core.Props;
using Zilon.Core.Schemes;
using Zilon.Core.Tactics;

namespace Zilon.Bot.Players.Triggers
{
    public class ThirstAndHasResourceTrigger : ILogicStateTrigger
    {
        public void Reset()
        {
            // Нет состояния.
        }

        public bool Test(IActor actor, ILogicState currentState, ILogicStrategyData strategyData)
        {
            if (actor is null)
            {
                throw new System.ArgumentNullException(nameof(actor));
            }

            if (currentState is null)
            {
                throw new System.ArgumentNullException(nameof(currentState));
            }

            if (strategyData is null)
            {
                throw new System.ArgumentNullException(nameof(strategyData));
            }

            var hazardEffect = actor.Person.GetModule<IEffectsModule>().Items.OfType<SurvivalStatHazardEffect>()
               .SingleOrDefault(x => x.Type == SurvivalStatType.Hydration);
            if (hazardEffect == null)
            {
                return false;
            }

            //

            var props = actor.Person.GetModule<IInventoryModule>().CalcActualItems();
            var resources = props.OfType<Resource>();
            var bestResource = ResourceFinder.FindBestConsumableResourceByRule(resources,
                ConsumeCommonRuleType.Thirst);

            if (bestResource == null)
            {
                return false;
            }

            return true;
        }

        public void Update()
        {
            // Нет состояния.
        }
    }
}

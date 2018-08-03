﻿using System;

using Zilon.Core.Persons;
using Zilon.Core.Tactics.Behaviour.Bots;
using Zilon.Core.Tactics.Spatial;

namespace Zilon.Core.Tactics
{
    public class TacticalActUsageService : ITacticalActUsageService
    {
        private readonly IDecisionSource _decisionSource;
        private readonly IPerkResolver _resolver;

        public TacticalActUsageService(IDecisionSource decisionSource, IPerkResolver resolver)
        {
            _decisionSource = decisionSource;
            _resolver = resolver;
        }

        public void UseOn(IActor actor, IAttackTarget target, ITacticalAct act)
        {
            //TODO реализовать возможность действовать на себя некоторыми скиллами.
            if (actor == target)
            {
                throw new ArgumentException("Актёр не может атаковать сам себя", nameof(target));
            }

            var currentCubePos = ((HexNode)actor.Node).CubeCoords;
            var targetCubePos = ((HexNode)target.Node).CubeCoords;

            var isInDistance = act.CheckDistance(currentCubePos, targetCubePos);
            if (!isInDistance)
            {
                throw new InvalidOperationException("Попытка атаковать цель, находящуюся за пределами атаки.");
            }

            var minEfficient = act.MinEfficient;
            var maxEfficient = act.MaxEfficient;
            var rolledEfficient = _decisionSource.SelectEfficient(minEfficient, maxEfficient);

            if (target is IActor targetActor)
            {
                var targetIsDeadLast = targetActor.IsDead;

                targetActor.TakeDamage(rolledEfficient);

                if (!targetIsDeadLast && targetActor.IsDead)
                {
                    var evolutionData = actor.Person.EvolutionData;
                    if (evolutionData != null)
                    {
                        var defeatProgress = new DefeatActorJobProgress(targetActor);
                        foreach (var perk in evolutionData.ActivePerks)
                        {
                            var isArchieved = _resolver.ApplyProgress(defeatProgress, perk);

                        }
                    }
                }
            }
            else
            {
                target.TakeDamage(rolledEfficient);
            }
        }
    }
}
﻿using System;
using System.Linq;

using Zilon.Core.Props;
using Zilon.Core.StaticObjectModules;

namespace Zilon.Core.Tactics.Behaviour
{
    public sealed class ToolMineDepositMethod : IMineDepositMethod
    {
        private readonly Equipment _tool;

        public ToolMineDepositMethod(Equipment tool)
        {
            _tool = tool ?? throw new ArgumentNullException(nameof(tool));
        }

        public IMineDepositResult TryMine(IPropDepositModule deposit)
        {
            if (deposit is null)
            {
                throw new ArgumentNullException(nameof(deposit));
            }

            var requiredToolTags = deposit.GetToolTags();

            if (_tool.Scheme.Tags.Intersect(requiredToolTags) == requiredToolTags)
            {
                throw new InvalidOperationException("Попытка выполнить добычу ресурса не подходящим инструментом.");
            }

            deposit.Mine();

            return new SuccessMineDepositResult();
        }
    }
}
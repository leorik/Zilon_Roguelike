﻿using System;

using JetBrains.Annotations;

using Zilon.Core.Schemes;
using Zilon.Core.Scoring;

namespace Zilon.Core.Persons
{
    /// <summary>
    /// Персонаж, находящийся под управлением игрока.
    /// </summary>
    public class HumanPerson : PersonBase
    {
        /// <inheritdoc/>
        public override int Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public IPersonScheme Scheme { get; }

        public IPlayerEventLogService PlayerEventLogService { get; set; }

        public override PhysicalSize PhysicalSize { get => PhysicalSize.Size1; }

        public HumanPerson([NotNull] IPersonScheme scheme)
        {
            Scheme = scheme ?? throw new ArgumentNullException(nameof(scheme));

            Name = scheme.Sid;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
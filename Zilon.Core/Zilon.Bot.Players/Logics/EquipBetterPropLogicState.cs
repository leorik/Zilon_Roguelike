﻿using System;
using System.Collections.Generic;
using System.Linq;
using Zilon.Core.Common;
using Zilon.Core.Components;
using Zilon.Core.Props;
using Zilon.Core.Schemes;
using Zilon.Core.Tactics;
using Zilon.Core.Tactics.Behaviour;

namespace Zilon.Bot.Players.Logics
{
    public sealed class EquipBetterPropLogicState : LogicStateBase
    {
        public override IActorTask GetTask(IActor actor, ILogicStrategyData strategyData)
        {
            var currentInventoryEquipments = actor.Person.Inventory.CalcActualItems().OfType<Equipment>();
            var emptyEquipmentSlots = new List<PersonSlotSubScheme>();

            for (int i = 0; i < actor.Person.EquipmentCarrier.Slots.Length; i++)
            {
                var targetSlotIndex = i;
                var slot = actor.Person.EquipmentCarrier.Slots[i];
                var equiped = actor.Person.EquipmentCarrier[i];

                var availableEquipments = currentInventoryEquipments
                        .Where(x => (x.Scheme.Equip.SlotTypes[0] & slot.Types) > 0)
                        .Where(x => x.Scheme.Tags?.Contains(PropTags.Equipment.Ranged) != true);

                if (equiped == null)
                {
                    if (availableEquipments.Any())
                    {
                        var targetEquipmentFromInventory = GetBestEquipmentForSlot(slot, availableEquipments);

                        

                        return new EquipTask(actor, targetEquipmentFromInventory, targetSlotIndex);
                    }
                }
                else
                {
                    if (slot.Types.HasFlag(EquipmentSlotTypes.Hand))
                    {
                        if (equiped.Scheme.Tags?.Contains(PropTags.Equipment.Weapon) == true)
                        {
                            var currentWeaponRating = GetWeaponDamageRating(equiped);
                            var targetEquipmentFromInventory = GetBestEquipmentForSlot(slot, availableEquipments);
                            var targetWeaponRating = GetWeaponDamageRating(targetEquipmentFromInventory);

                            if (targetWeaponRating > currentWeaponRating)
                            {
                                return new EquipTask(actor, targetEquipmentFromInventory, targetSlotIndex);
                            }
                        }
                    }
                }
            }

            Complete = true;
            return null;
        }

        private Equipment GetBestEquipmentForSlot(PersonSlotSubScheme slot, IEnumerable<Equipment> availableEquipments)
        {
            Equipment targetEquipmentFromInventory;
            if (slot.Types.HasFlag(EquipmentSlotTypes.Hand))
            {
                var weaponInventoryEquipments = availableEquipments
                    .Where(x => x.Scheme.Tags?.Contains(PropTags.Equipment.Weapon) == true)
                    .Where(x => x.Scheme.Tags?.Contains(PropTags.Equipment.Shield) != true);
                targetEquipmentFromInventory = OrderWeaponByQuality(weaponInventoryEquipments).First();
            }
            else
            {
                var weaponInventoryEquipments = availableEquipments
                    .Where(x => x.Scheme.Tags?.Contains(PropTags.Equipment.Weapon) != true);
                targetEquipmentFromInventory = OrderArmorByQuality(weaponInventoryEquipments).First();
            };
            return targetEquipmentFromInventory;
        }

        protected override void ResetData()
        {
            // Нет состояния.
        }

        private IEnumerable<Equipment> OrderWeaponByQuality(IEnumerable<Equipment> equipments)
        {
            return equipments.OrderByDescending(x => GetWeaponDamageRating(x));
        }

        private IEnumerable<Equipment> OrderArmorByQuality(IEnumerable<Equipment> equipments)
        {
            return equipments.OrderByDescending(x => GetArmorRating(x));
        }

        private int GetWeaponDamageRating(Equipment equipment)
        {
            var weaponEffecient = equipment.Acts[0].Stats.Efficient;
            var rating = weaponEffecient.Dice * weaponEffecient.Count * weaponEffecient.Count;
            return rating;
        }

        private int GetArmorRating(Equipment equipment)
        {
            var armors = equipment.Scheme.Equip.Armors;
            var rating = 0;

            foreach (var armor in armors)
            {
                rating = armor.ArmorRank * ((int)armor.AbsorbtionLevel + 1);
            }

            return rating;
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using Assets.Zilon.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Zilon.Core.Persons;
using Zilon.Core.Tactics;

public class PerksModalBody : MonoBehaviour, IModalWindowHandler {

	private IActor _actor;
	
	public Transform PerkItemsParent;
	public Transform EquipmentSlotsParent;
	public PerkItemViewModel PerkItemPrefab;
	public InventorySlotVm EquipmentSlotPrefab;
	
	[NotNull] [Inject] private DiContainer _diContainer;
	
	public void Start()
	{
		CreateSlots();
	}
	
	private void CreateSlots()
	{
		// TODO Это расположение брать из схемы или из профилей персонажа. Или из ресурсов.
		var positions = new[]
		{
			new Vector3(-50, 16), // Weapon
			new Vector3(50, 16), // Outhand
			new Vector3(0, 25), // Body
			new Vector3(0, 72), // Head
			new Vector3(0, -25), // Legs
			new Vector3(-50, 64), // Aux
			new Vector3(50, 64) // Aux
		};

		for (var i = 0; i < 7; i++)
		{
			var slotObject = _diContainer.InstantiatePrefab(EquipmentSlotPrefab, EquipmentSlotsParent);
			slotObject.transform.localPosition = positions[i];
			var slotVm = slotObject.GetComponent<InventorySlotVm>();
			slotVm.SlotIndex = i;
		}
	}
	
	public void Init(IActor actor)
	{
		_actor = actor;
		var evolutionData = _actor.Person.EvolutionData;
		UpdatePerksInner(PerkItemsParent, evolutionData.Perks);
	}

	private void UpdatePerksInner(Transform itemsParent, IPerk[] perks)
	{
		foreach (Transform itemTranform in itemsParent)
		{
			Destroy(itemTranform.gameObject);
		}

		foreach (var perk in perks)
		{
			var propItemVm = Instantiate(PerkItemPrefab, itemsParent);
			propItemVm.Init(perk);
		}
	}

	public void ApplyChanges()
	{
		
	}

	public void CancelChanges()
	{
		throw new System.NotImplementedException();
	}
}

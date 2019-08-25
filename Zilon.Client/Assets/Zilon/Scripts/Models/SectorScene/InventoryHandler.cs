﻿using System;
using System.Collections.Generic;
using System.Linq;

using Assets.Zilon.Scripts.Models;

using JetBrains.Annotations;

using UnityEngine;

using Zenject;

using Zilon.Core.Client;
using Zilon.Core.Commands;
using Zilon.Core.Props;
using Zilon.Core.Tactics;

public class InventoryHandler : MonoBehaviour
{
    private const string HISTORY_BOOK_SID = "history-book";

    private IActor _actor;
    private readonly List<PropItemVm> _propViewModels;

    public Transform InventoryItemsParent;
    public PropItemVm PropItemPrefab;
    public Transform EquipmentSlotsParent;
    public InventorySlotVm EquipmentSlotPrefab;
    public PropInfoPopup PropInfoPopup;
    public GameObject UseButton;
    public GameObject ReadButton;
    public GameObject UsePropDropArea;

    [NotNull] [Inject] private readonly DiContainer _diContainer;
    [NotNull] [Inject] private readonly ISectorUiState _playerState;
    [NotNull] [Inject] private readonly IInventoryState _inventoryState;
    [NotNull] [Inject] private readonly ICommandManager _commandManager;
    [NotNull] [Inject(Id = "use-self-command")] private readonly ICommand _useSelfCommand;
    [NotNull] [Inject(Id = "show-history-command")] private readonly ICommand _showHistoryCommand;

    public event EventHandler Closed;

    public InventoryHandler()
    {
        _propViewModels = new List<PropItemVm>();
    }

    public void Start()
    {
        CreateSlots();

        // Изначально скрываем все кнопки.
        // Потому что изначально никакой предмет не должен быть выбран.
        // Поэтому не ясно, какие действия доступны.
        UseButton.SetActive(false);
        ReadButton.SetActive(false);

        _actor = _playerState.ActiveActor.Actor;
        var inventory = _actor.Person.Inventory;
        UpdatePropsInner(InventoryItemsParent, inventory.CalcActualItems());

        inventory.Added += Inventory_Added;
        inventory.Removed += Inventory_Removed;
        inventory.Changed += Inventory_Changed;

        _inventoryState.SelectedPropChanged += InventoryState_SelectedPropChanged;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _inventoryState.SelectedProp = null;
        }
    }

    private void InventoryState_SelectedPropChanged(object sender, EventArgs e)
    {
        foreach (var propViewModel in _propViewModels)
        {
            var isSelected = ReferenceEquals(propViewModel, _inventoryState.SelectedProp);
            propViewModel.SetSelectedState(isSelected);
        }

        PropInfoPopup.SetPropViewModel(_inventoryState.SelectedProp as IPropViewModelDescription);

        UpdateUseControlsState(_inventoryState.SelectedProp as PropItemVm);
    }

    public void OnDestroy()
    {
        var inventory = _actor.Person.Inventory;

        inventory.Added -= Inventory_Added;
        inventory.Removed -= Inventory_Removed;
        inventory.Changed -= Inventory_Changed;

        _inventoryState.SelectedPropChanged -= InventoryState_SelectedPropChanged;
        _inventoryState.SelectedProp = null;
    }

    private void CreateSlots()
    {
        var actorViewModel = _playerState.ActiveActor;
        var slots = actorViewModel.Actor.Person.EquipmentCarrier.Slots;

        for (var i = 0; i < slots.Length; i++)
        {
            var slotObject = _diContainer.InstantiatePrefab(EquipmentSlotPrefab, EquipmentSlotsParent);
            var slotViewModel = slotObject.GetComponent<InventorySlotVm>();
            slotViewModel.Actor = actorViewModel.Actor;
            slotViewModel.SlotIndex = i;
            slotViewModel.SlotTypes = slots[i].Types;
            slotViewModel.Click += Slot_Click;
            slotViewModel.MouseEnter += SlotViewModel_MouseEnter;
            slotViewModel.MouseExit += SlotViewModel_MouseExit;
        }
    }

    private void SlotViewModel_MouseExit(object sender, EventArgs e)
    {
        PropInfoPopup.SetPropViewModel(null);
    }

    private void SlotViewModel_MouseEnter(object sender, EventArgs e)
    {
        var currentItemVm = (IPropViewModelDescription)sender;
        PropInfoPopup.SetPropViewModel(currentItemVm);
    }

    private void Slot_Click(object sender, EventArgs e)
    {
        var slotVm = sender as InventorySlotVm;
        if (slotVm == null)
        {
            throw new NotSupportedException();
        }

        slotVm.ApplyEquipment();
    }

    private void Inventory_Removed(object sender, PropStoreEventArgs e)
    {
        foreach (var removedProp in e.Props)
        {
            var propViewModel = _propViewModels.Single(x => x.Prop == removedProp);
            _propViewModels.Remove(propViewModel);
            Destroy(propViewModel.gameObject);

            var isRemovedPropWasSelected = ReferenceEquals(propViewModel, _inventoryState.SelectedProp);
            if (isRemovedPropWasSelected)
            {
                _inventoryState.SelectedProp = null;
                UseButton.SetActive(false);
            }
        }

        UpdateItemsParentObject();
    }

    private void Inventory_Changed(object sender, PropStoreEventArgs e)
    {
        foreach (var changedProp in e.Props)
        {
            var propViewModel = _propViewModels.Single(x => x.Prop == changedProp);
            propViewModel.UpdateProp();
        }
    }

    private void Inventory_Added(object sender, PropStoreEventArgs e)
    {
        foreach (var newProp in e.Props)
        {
            CreatePropObject(InventoryItemsParent, newProp);
        }

        UpdateItemsParentObject();
    }

    private void UpdateItemsParentObject()
    {
        var inventory = _actor.Person.Inventory;
        var inventoryProps = inventory.CalcActualItems();
        RecalcItemsObject(InventoryItemsParent, inventoryProps);
    }

    private void UpdatePropsInner(Transform itemsParent, IEnumerable<IProp> props)
    {
        foreach (Transform itemTranform in itemsParent)
        {
            Destroy(itemTranform.gameObject);
        }

        foreach (var prop in props)
        {
            CreatePropObject(itemsParent, prop);
        }

        RecalcItemsObject(itemsParent, props);
    }

    private static void RecalcItemsObject(Transform itemsParent, IEnumerable<IProp> props)
    {
        var parentRect = itemsParent.GetComponent<RectTransform>();
        var rowCount = (int)Math.Ceiling(props.Count() / 4f);
        parentRect.sizeDelta = new Vector2(parentRect.sizeDelta.x, (40 + 5) * rowCount);
    }

    private void CreatePropObject(Transform itemsParent, IProp prop)
    {
        var propItemViewModel = Instantiate(PropItemPrefab, itemsParent);
        propItemViewModel.Init(prop);
        propItemViewModel.Click += PropItem_Click;
        propItemViewModel.MouseEnter += PropItemViewModel_MouseEnter;
        propItemViewModel.MouseExit += PropItemViewModel_MouseExit;
        _propViewModels.Add(propItemViewModel);
    }

    private void PropItemViewModel_MouseExit(object sender, EventArgs e)
    {
        PropInfoPopup.SetPropViewModel(null);
    }

    private void PropItemViewModel_MouseEnter(object sender, EventArgs e)
    {
        var currentItemVm = (PropItemVm)sender;
        PropInfoPopup.SetPropViewModel(currentItemVm);
    }

    //TODO Дубликат с ContainerModalBody.PropItemOnClick
    private void PropItem_Click(object sender, EventArgs e)
    {
        var currentItemViewModel = (PropItemVm)sender;
        foreach (var propViewModel in _propViewModels)
        {
            var isSelected = propViewModel == currentItemViewModel;
            propViewModel.SetSelectedState(isSelected);
        }

        // этот фрагмент - не дубликат
        // Не понимаю назначение этого фрагмента.
        // Разобраться "зачем" и вставить развёрнутое пояснение.
        UpdateUseControlsState(currentItemViewModel);

        if (!ReferenceEquals(_inventoryState.SelectedProp, currentItemViewModel))
        {
            _inventoryState.SelectedProp = currentItemViewModel;
        }
        else
        {
            _inventoryState.SelectedProp = null;
        }

        // --- этот фрагмент - не дубликат
    }

    private void UpdateUseControlsState(PropItemVm currentItemViewModel)
    {
        if (currentItemViewModel?.Prop == null)
        {
            UseButton.SetActive(false);
            ReadButton.SetActive(false);
            return;
        }

        if (currentItemViewModel.SelectAsDrag)
        {
            UseButton.SetActive(false);
            ReadButton.SetActive(false);

            UsePropDropArea.SetActive(true);
        }
        else
        {
            UsePropDropArea.SetActive(false);

            var currentItem = currentItemViewModel.Prop;

            var canUseProp = currentItem.Scheme.Use != null;
            UseButton.SetActive(canUseProp);

            var canRead = currentItem.Scheme.Sid == HISTORY_BOOK_SID;
            ReadButton.SetActive(canRead);
        }
    }

    public void UseButton_Handler()
    {
        _commandManager.Push(_useSelfCommand);
    }

    public void ReadButton_Handler()
    {
        if (_inventoryState.SelectedProp.Prop.Scheme.Sid == HISTORY_BOOK_SID)
        {
            _commandManager.Push(_showHistoryCommand);
        }
    }
}

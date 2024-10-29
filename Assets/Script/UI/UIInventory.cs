using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    public GameObject invetoryWindow;
    public Transform slotPannel;

    [Header("Selet Item")]
    public TextMeshProUGUI selectedName;
    public TextMeshProUGUI selectedDescription;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;

    private PlayerController controller;
    private PlayerConditions conditions;

    ItemData selectedItem;
    int selectedIndex = 0;

    int curEquipIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        conditions = CharacterManager.Instance.Player.conditions;

        controller.inventory += Toggle;
        CharacterManager.Instance.Player.addItem += AddItem;

        invetoryWindow.SetActive(false);
        slots = new ItemSlot[slotPannel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPannel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }

        ClearSelectedItemWindow();
    }
    
    void ClearSelectedItemWindow()
    {
        selectedName.text = string.Empty;
        selectedDescription.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
    }

    public void Toggle()
    {
        if(IsOpen())
        {
            invetoryWindow.SetActive(false);
        }
        else
        {
            invetoryWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return invetoryWindow.activeInHierarchy;
    }

    void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;

        ItemSlot emptyslot = GetEmptySlot();
        if(emptyslot != null)
        {
            emptyslot.item = data;
            UpdatUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }

        //CharacterManager.Instance.Player.itemData = null;
    }

    void UpdatUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }
    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;

        selectedItem = slots[index].item;
        selectedIndex = index;

        selectedName.text = selectedItem.displayName;
        selectedDescription.text = selectedItem.description;

        useButton.SetActive(selectedItem.type == ItemType.Useable);
        equipButton.SetActive(selectedItem.type == ItemType.Equipable && !slots[index].equipped);
        unEquipButton.SetActive(selectedItem.type == ItemType.Equipable && slots[index].equipped);
    }

    public void OnUseButton()
    {
        if(selectedItem.type == ItemType.Useable)
        {
            RemoveSelectedItem();
        }
    }

    void RemoveSelectedItem()
    {
        selectedItem = null;
        slots[selectedIndex].item = null;
        selectedIndex -= 1;
        ClearSelectedItemWindow();

        UpdatUI();
    }

    public void OnEquipButton()
    {
        if(slots[curEquipIndex].equipped)
        {
            UnEquip(curEquipIndex);
        }

        slots[selectedIndex].equipped = true;
        curEquipIndex = selectedIndex;
        CharacterManager.Instance.Player.equip.EquipNew(selectedItem);
        UpdatUI();
        SelectItem(selectedIndex);
    }

    void UnEquip(int index)
    {
        slots[index].equipped = false;
        CharacterManager.Instance.Player.equip.UnEquip();
        UpdatUI();
        if(selectedIndex == index)
        {
            SelectItem(selectedIndex);
        }
    }

    public void OnUnEquipButton()
    {
        UnEquip(selectedIndex);
    }
}

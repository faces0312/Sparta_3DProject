using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Useable,//사용템
    Equipable,//장착
    Consumable//회복
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;//이름
    public string description;//설명
    public ItemType type;//타입
    public Sprite icon;//아이콘
    public GameObject dropPrefab;//프리팹

    [Header("Stacking")]
    public bool canStack;//중복획득 가능한지
    public int maxStackAmount;//최대 몇개까지 가능한지

    [Header("Heal")]
    public int heal;//얼마나 힐 되는지
    /*[Header("Equip")]
    public GameObject equipPrefab;*/
}

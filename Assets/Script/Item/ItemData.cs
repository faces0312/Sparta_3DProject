using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Useable,//�����
    Equipable,//����
    //Consumable//ȸ��
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;//�̸�
    public string description;//����
    public ItemType type;//Ÿ��
    public Sprite icon;//������

    [Header("Equip")]
    public GameObject equipPrefab;
}

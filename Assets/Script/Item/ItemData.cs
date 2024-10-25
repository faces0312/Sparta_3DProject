using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Useable,//�����
    Equipable,//����
    Consumable//ȸ��
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;//�̸�
    public string description;//����
    public ItemType type;//Ÿ��
    public Sprite icon;//������
    public GameObject dropPrefab;//������

    [Header("Stacking")]
    public bool canStack;//�ߺ�ȹ�� ��������
    public int maxStackAmount;//�ִ� ����� ��������

    [Header("Heal")]
    public int heal;//�󸶳� �� �Ǵ���
    /*[Header("Equip")]
    public GameObject equipPrefab;*/
}

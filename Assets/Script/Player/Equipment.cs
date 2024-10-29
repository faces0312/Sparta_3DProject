using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public GameObject curEquip;
    public Transform equipParent;

    private PlayerController controller;
    private PlayerConditions conditions;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        conditions = GetComponent<PlayerConditions>();
    }

    public void EquipNew(ItemData data)
    {
        UnEquip();
        curEquip = Instantiate(data.equipPrefab, equipParent);
    }

    public void UnEquip()
    {
        if(curEquip != null)
        {
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
}

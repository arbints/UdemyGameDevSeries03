using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] string AttachSlotTag;

    public string GetAttachSlotTag()
    {
        return AttachSlotTag;
    }
    public GameObject Owner
    {
        get;
        private set;
    }

    public void Init(GameObject owner)
    {
        Owner = owner;
        UnEquip();
    }

    public void Equip()
    {
        gameObject.SetActive(true);
    }

    public void UnEquip()
    {
        gameObject.SetActive(false);
    }
}

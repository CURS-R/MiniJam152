using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using UnityEngine;

public class RatController : MonoBehaviour
{
    [field: SerializeField] private Transform carryItemTransform;

    [HideInInspector] public bool HasItem
    {
        get
        {
            try
            {
                return carryItem != null;
            }
            catch (Exception e)
            {
                return false; // I don't want to talk about this
            }
        }
    }

    private Item carryItem = null;

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        
        if (other.CompareTag("Spray") || other.CompareTag("ToothPick"))
        {
            Debug.Log("Rathit");
            TryDropItem();
            Destroy(gameObject, 10);
            gameObject.SetActive(false);
        }

        if (item != null && !HasItem)
        {
            Debug.Log($"{gameObject.name} collided with Item");
            if (!item.IsPickedUp)
                TryPickUpItem(item);
        }
    }

    private void TryPickUpItem(Item item)
    {
        if (HasItem) return;
        carryItem = item;
        carryItem.PickUp(carryItemTransform);
        Debug.Log($"{gameObject.name} TryPickup item!");
    }

    private void TryDropItem()
    {
        if (!HasItem) return;
        carryItem.Drop();
        carryItem = null;
        Debug.Log("TryDrop item!");
    }
}

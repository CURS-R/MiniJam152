using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using UnityEngine;

public class RatController : MonoBehaviour
{
    [field: SerializeField] private Transform carryItemTransform;
    [field: SerializeField] private GameObject thingToDestroyWhenDead;

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
        
        // TODO: getting hit by a spray projectile?
        //if (other.CompareTag("Spray"))// || other.CompareTag("ToothPick"))

        if (item != null && !HasItem)
        {
            Debug.Log($"{gameObject.name} collided with Item");
            if (!item.IsPickedUp)
                TryPickUpItem(item);
        }
    }

    public void Die()
    {
        TryDropItem();
        Destroy(thingToDestroyWhenDead, 0.5f);
        //gameObject.SetActive(false);
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

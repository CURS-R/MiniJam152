using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using UnityEngine;

public class RatController : MonoBehaviour
{
    [field: SerializeField] private Transform carryItemTransform;
    [field: SerializeField] private GameObject thingToDestroyWhenDead;
    
    [field: SerializeField] private GameObject poofEffect;

    [HideInInspector] public bool HasItem { get; private set; }
    [HideInInspector] public bool IsDying { get; private set; }

    private Item carryItem = null;

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        var projectile = other.GetComponent<Projectile>();
        
        // TODO: getting hit by a spray projectile?
        //if (other.CompareTag("Spray"))// || other.CompareTag("ToothPick"))

        if (projectile)
        {
            Debug.Log($"{gameObject.name} collided with Projectile");
            projectile.Die();
            TryDie();
        }

        if (item && !HasItem && !IsDying)
        {
            Debug.Log($"{gameObject.name} collided with Item");
            if (!item.IsPickedUp)
                TryPickUpItem(item);
        }
    }

    public void TryDie()
    {
        if (IsDying)
            return;
        IsDying = true;
        TryDropItem();
        var poofGO = Instantiate(poofEffect, transform.position, Quaternion.identity);
        Destroy(thingToDestroyWhenDead);
    }

    private void TryPickUpItem(Item item)
    {
        if (HasItem) return;
        carryItem = item;
        carryItem.PickUp(carryItemTransform);
        Debug.Log($"{gameObject.name} TryPickup item!");
        HasItem = true;
    }

    private void TryDropItem()
    {
        if (!HasItem || carryItem == null) return;
        carryItem.Drop();
        carryItem = null;
        Debug.Log("TryDrop item!");
        HasItem = false;
        IsDying = true;
    }
}

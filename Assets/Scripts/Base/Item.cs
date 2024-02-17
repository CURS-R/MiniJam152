using UnityEngine;

namespace Base
{
    [RequireComponent(typeof(Rigidbody))]
    public class Item : MonoBehaviour
    {
        private Rigidbody RB => this.GetComponent<Rigidbody>();
        
        [HideInInspector] public bool IsPickedUp { get; private set; } = false;

        public void PickUp(Transform newParent)
        {
            if (newParent)
            {
                RB.isKinematic = true;
                transform.parent = newParent;
                transform.localPosition = Vector3.zero;
                Debug.Log($"{gameObject.name} should have new parent and rb disabled.");
            }
            IsPickedUp = true;
            Debug.Log($"{gameObject.name} getting PickUp, with newParent={newParent}. isPickedUp={IsPickedUp}");
        }

        public void Drop()
        {
            transform.parent = null;
            RB.isKinematic = false;
            IsPickedUp = false;
            Debug.Log($"{gameObject.name} getting Dropped.");
        }
    }
}
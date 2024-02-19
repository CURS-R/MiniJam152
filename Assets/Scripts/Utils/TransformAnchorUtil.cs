using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utils
{
    public class TransformAnchorUtil : MonoBehaviour
    {
        public Transform childTransform;
        public Transform parentTransform;
        [SerializeField] private Vector3 offset;
        [SerializeField] public bool matchPosition;
        [SerializeField] public bool matchRotation;
        [SerializeField] public bool matchYAxisOnly;
        [SerializeField] public bool lockRotationTo0;

        // Testing
        private float followSmoothTime = 12f;
        private Vector3 refVelocityForCameraFollow = Vector3.zero;
        
        #region Unity Methods
        private void LateUpdate()
        {
            Anchor();
        }
        #endregion
        
        #region Methods
        private void Anchor()
        {
            if (childTransform == null || parentTransform == null)
            {
                Debug.Log(this.gameObject.name + ": Child or parent object was null in TransformAnchor.");
            }
            if (matchPosition)
            {
                Vector3 newPos = Vector3.SmoothDamp(
                    childTransform.position,
                    parentTransform.position,
                    ref refVelocityForCameraFollow,
                    1/followSmoothTime,
                    Mathf.Infinity,
                    Time.deltaTime
                );
                childTransform.position = newPos;
                childTransform.position = parentTransform.position;
            }
            if (matchRotation)
            {
                childTransform.rotation = parentTransform.rotation;
            }
            if (matchYAxisOnly)
            {
                childTransform.rotation = Quaternion.Euler(0f, parentTransform.eulerAngles.y, 0f);
            }
            if (offset != Vector3.zero)
            {
                childTransform.Translate(offset, Space.Self);
            }
            if (lockRotationTo0)
            {
                childTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        #endregion
    }
}



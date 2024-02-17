using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReticle : MonoBehaviour
{
    private RectTransform reticle;


    public float RestingSize;
    public float MaxSize;
    public float Speed;
    private float currentSize;

    private void Start()
    {
        reticle = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           // currentSize = Mathf.Lerp(currentSize, MaxSize, Time.deltaTime * Speed);
            Debug.Log("clicked");
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, RestingSize, Time.deltaTime * Speed);
        }

        reticle.sizeDelta = new Vector2(currentSize, currentSize);
    }

    /// <summary>
    /// this function checks to see if the player is moving 
    /// </summary>
    bool isMoving
    {
        get
        {
            if (
                Input.GetAxis("Horizontal") != 0 ||
                Input.GetAxis("Vertical") != 0 ||
                Input.GetAxis("Mouse X") != 0 ||
                Input.GetAxis("Mouse Y") != 0
                )
                return true;
            else
                return false;

        }
    }
}

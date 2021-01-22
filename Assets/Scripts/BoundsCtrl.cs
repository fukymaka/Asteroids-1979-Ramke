using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCtrl : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float radius = 0.5f;
    public bool keepOnScreen = true;

    [Header("Set in script")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;

    [HideInInspector]
    public bool offRigth, offLeft, offUp, offDown;

    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }


    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        isOnScreen = true;
        offRigth = offLeft = offDown = offUp = false;

        if (pos.x > camWidth - radius)
        {
            pos.x = -camWidth + radius;
            offRigth = true;
        }

        if (pos.x < -camWidth + radius)
        {
            pos.x = camWidth - radius;
            offLeft = true;
        }

        if (pos.y > camHeight - radius)
        {
            pos.y = -camHeight + radius;
            offUp = true;
        }

        if (pos.y < -camHeight + radius)
        {
            pos.y = camHeight - radius;
            offDown = true;
        }

        isOnScreen = !(offRigth || offLeft || offUp || offDown);

        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
            offRigth = offLeft = offDown = offUp = false;
        }

        if (!keepOnScreen && !isOnScreen)
        {
            Destroy(this.gameObject);
        }        
    }
}

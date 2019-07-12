using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReloadEventHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float threshold = 80f;
    private bool isPress = false;
    private bool isReloaded = false;

    private Vector3 clickPoint;
    // Update is called once per frame
    void Update()
    {
        if (isPress && !isReloaded)
        {
            var end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(end, clickPoint) > threshold)
            {
                isReloaded = true;
                // todo: reload arrows.
                print("reloading!!");
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPress = true;
        clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
        isReloaded = false;
    }
}

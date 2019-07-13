using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BGEventHandler : MonoBehaviour, IPointerDownHandler
{
    public GameObject ArrowMiss;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GamePlayManager.Instance().isOver())
        {
            GamePlayManager.Instance().Fire();
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            var ob = GameObject.Instantiate(ArrowMiss, pos, Quaternion.identity, this.transform);
            ob.SetActive(true);
        }
    }
}

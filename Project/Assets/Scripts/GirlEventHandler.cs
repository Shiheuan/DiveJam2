using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;

public class GirlEventHandler : MonoBehaviour, IPointerDownHandler
{
    private bool isDone = false;

    public int pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        StartCoroutine(WaitSeconds());
    }

    IEnumerator WaitSeconds()
    {
        // todo: girl can't wait animation.
        yield return new WaitForSeconds(3f);

        if (!isDone)
        {
            // todo: girl "heng" animation and gameover.
            print("heng");
            Destroy(this);
            GameOver();
        }
    }

    void GameOver()
    {
        print("game over!");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("shot this girl.");
        isDone = true;
        // todo: move this to objects pool.
        
        this.gameObject.SetActive(false);
        GameManager.Instance().Remove(pos);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GirlEventHandler : MonoBehaviour, IPointerDownHandler
{
    private bool isDone = false;
    private GamePlayManager gm = null;
    public int pos;
    public int score;
    public bool isEndable = false;
    void Awake()
    {
        gm = GamePlayManager.Instance();
    }

    void OnEnable()
    {
        isDone = false;
        StartCoroutine(WaitSeconds());
    }

    IEnumerator WaitSeconds()
    {
        // todo: girl can't wait animation.
        yield return new WaitForSeconds(3f);

        if (!gm.isOver())
        {
            if (!this.isDone && this.isEndable)
            {
                // todo: girl "heng" animation.
                print("heng!");
                Destroy(this);
                gm.GameOver(this.pos);
            }
            Dispear();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gm.AmmoCheck() && !gm.isOver())
        {
            print("shot this girl.");
            gm.Fire();
            this.isDone = true;
            gm.AddMoney(score);
            StartCoroutine(Satisfied());

        }
    }

    IEnumerator Satisfied()
    {
        // todo: play the "heart" animation
        var im = this.gameObject.GetComponent<Image>();
        var c = im.color;
        c.a = 0.2f;
        im.color = c;
        
        yield return new WaitForSeconds(1f);

        Dispear();
    }

    private void Dispear()
    {
        // todo: move this to objects pool.
        this.gameObject.SetActive(false);
        gm.Remove(pos);
    }
}

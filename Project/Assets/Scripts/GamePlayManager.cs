using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GamePlayManager : MonoBehaviour
{
    private int boardCap = 16;
    /// <summary>
    /// readonly positions
    /// </summary>
    private List<Vector3> posList;
    private float dis_x = 160f;
    private float dis_y = 200f;
    [SerializeField]
    private List<GameObject> girlsOnBorad;

    [Serializable]
    private class GirlsPool
    {
        public int num_girl1 = 0;
        public int num_girl2 = 0;
        public int num_girl3 = 0;
        public List<GameObject> Pool;

        public GirlsPool()
        {
            Pool = new List<GameObject>(16);
        }
    }
    [SerializeField]
    private GirlsPool girlspool = new GirlsPool();

    [SerializeField]
    private List<bool> isOccupied;
    [SerializeField]
    private List<GameObject> girls;

    /// <summary>
    /// the number of the current arrows
    /// </summary>
    private int arrows = 5;

    private Text theArrNum;
    /// <summary>
    /// range: 0 to 5
    /// </summary>
    public int Arrows
    {
        get { return arrows; }
        set
        {
            if (value > 5)
            {
                arrows = 5;
            }
            else if (value < 0)
            {
                arrows = 0;
            }
            else
            {
                arrows = value;
            }

            theArrNum.text = arrows.ToString();
        }
    }

    /// <summary>
    /// the amount of money that the player have
    /// </summary>
    private int money = 0;

    private Text theMonNum; 

    private int Money
    {
        get { return money; }
        set
        {
            money = value;
            theMonNum.text = money.ToString();
        }
    }
    private bool isGameOn = false;
    private bool hasAmmo = true;
    private Coroutine gameflow = null;

    private static GamePlayManager instance = null;

    #region public method interface
    public static GamePlayManager Instance()
    {
        return instance;
    }

    public void Remove(int index)
    {
        isOccupied[index] = false;
        //var girl = girlsOnBorad[index];
        //girlsOnBorad.Remove(girl);
        //girlsPool.Add(girl);
        //girl.SetActive(false);
    }

    public bool isOver()
    {
        return !isGameOn;
    }

    public void GameOver(int index)
    {
        StopCoroutine(gameflow);
        FadeOthers(index);
        isGameOn = false;
    }

    public void Fire()
    {
        if (Arrows <= 0)
        {
            hasAmmo = false;
            print("need reload");
            // todo: show "low ammo" icon
        }
        Arrows--;
    }

    public void Reload()
    {
        // sweep the bow bag or "space" button
        hasAmmo = true;
        Arrows = 5;
    }

    public bool AmmoCheck()
    {
        return hasAmmo;
    }

    public void AddMoney(int val)
    {
        Money += val;
    }

    #endregion

    #region private

    void Awake()
    {
        theArrNum = GameObject.Find("GameCanvas/GuiArea/BowBag/Text").GetComponent<Text>();
        Arrows = 5;
        theMonNum = GameObject.Find("GameCanvas/GuiArea/Coin/Text").GetComponent<Text>();
        Money = 0;
        if (instance == null)
        {
            instance = this;
        }
        posList = new List<Vector3>(boardCap);
        isOccupied = new List<bool>(boardCap);

        var init = new Vector3(-240f, -100f, 0f);
        for (int i = 0; i < boardCap; i++)
        {
            isOccupied.Add(false);
            posList.Add(init);

            if (i == 3 || i == 7 || i == 11)
            {
                init.x = -240f;
                init.y += dis_y;
            }
            else
            {
                init.x += dis_x;
            }
        }

        isGameOn = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameflow = StartCoroutine(test());
    }

    IEnumerator test()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.8f);
            SpawnRandGirl();
        }
    }

    void SpawnRandGirl()
    {
        List<int> tmp = new List<int>();
        for (int i = 0; i < boardCap; i++)
        {
            if (isOccupied[i]) continue;
            tmp.Add(i);
        }

        if (tmp.Count == 0) return;
        var rp = Random.Range(0, tmp.Count);
        var rg = Random.Range((int)0, 3);

        var ob = Instantiate(girls[rg], posList[tmp[rp]], Quaternion.identity, this.transform);
        isOccupied[tmp[rp]] = true;
        ob.GetComponent<GirlEventHandler>().pos = tmp[rp];
        ob.gameObject.SetActive(true);
    }

    void FadeOthers(int index)
    {
        foreach (var VARIABLE in isOccupied)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isOver() && Input.GetKeyDown(KeyCode.Space))
            Reload();
            
    }

    #endregion

}

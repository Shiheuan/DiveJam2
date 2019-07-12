using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int boardCap = 16;
    [SerializeField]
    private List<Vector3> posList;

    private float dis_x = 160f;
    private float dis_y = 200f;
    [SerializeField]
    private List<bool> isOccupied;
    [SerializeField]
    private List<GameObject> girls;

    private static GameManager instance = null;

    public static GameManager Instance()
    {
            return instance;
    }

    public void Remove(int index)
    {
        isOccupied[index] = false;
    }

    void Awake()
    {
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
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(test());
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
            if (isOccupied[i])continue;
            tmp.Add(i);
        }

        if (tmp.Count == 0) return;
        var rp = Random.Range(0, tmp.Count);
        var rg = Random.Range((int) 0, 3);

        var ob = Instantiate(girls[rg], posList[tmp[rp]], Quaternion.identity, this.transform);
        isOccupied[tmp[rp]] = true;
        ob.GetComponent<GirlEventHandler>().pos = tmp[rp];
        ob.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

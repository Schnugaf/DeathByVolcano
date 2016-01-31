using UnityEngine;
using System.Collections;

public class CountDownGame : MonoBehaviour
{

    public float curST;
    public float endST;

    // Use this for initialization
    void Start()
    {
        CountDownStart();
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CountDownStart()
    {
        curST += Time.deltaTime;
        if (curST > endST)
        {
            Time.timeScale = 1f;
            CountDownRound();
        }
    }

    void CountDownRound()
    {

    }
}

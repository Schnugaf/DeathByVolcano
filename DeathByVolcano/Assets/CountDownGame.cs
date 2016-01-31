using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDownGame : MonoBehaviour
{

    public float curST;
    public float endST;

    public float curCDR;
    public float endCDR;

    float tCount;
    int tCountint;
    Text visTim;

    GameObject EndGUI;
    
    // Use this for initialization
    void Start()
    {
        curST = 0;

            
    }

    // Update is called once per frame
    void Update()
    {

        EndGUI = GameObject.Find("EndGUI");

        CountDownRound();

        visualTimer();
    }

    void CountDownRound()
    {
        curCDR += Time.deltaTime;
        if (curCDR > endCDR)
        {
            print("Time is Out");
            Time.timeScale = 0;
            EndGUI.SetActive(true);
            

        }
        print(curCDR);

    }

    void visualTimer()
    {
        tCount = endCDR - curCDR;
        tCountint = (int)tCount;
        
        visTim = GetComponent<Text>();
        visTim.text = tCountint.ToString();
    }
}

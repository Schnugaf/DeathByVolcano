using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PrintScores : MonoBehaviour
{
    public PointDistributionUnityKernelFinalBuild lava;
    Text txt;
    public string player = "P1";

	void Start ()
    {
        txt = GetComponent<Text>();
	}
	
	void Update ()
    {
        if (player == "P1")
        {
            txt.text = lava.PlayerOneScore.ToString();
        }
        else
        {
            txt.text = lava.PlayerTwoScore.ToString();
        }
	}
}

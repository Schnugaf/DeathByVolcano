using UnityEngine;
using System.Collections;

public class PointDistributionUnityKernelFinalBuild : MonoBehaviour
{

    public int lavaScoreValue;

    public int PlayerOneScore;
    public int PlayerTwoScore;
    public static PointDistributionUnityKernelFinalBuild InstanceScore;
    public static WhenTheManComesAroundDynamcs godInst;
    WhenTheManComesAroundDynamcs GodScript;
    GameObject God;

    // Use this for initialization
    void Start()
    {
        InstanceScore = this;

        
    }

    // Update is called once per frame
    void Update()
    {
        God = GameObject.Find("VolcanoGod");
        GodScript = God.GetComponent<WhenTheManComesAroundDynamcs>();


    }

    void lavaCollisionOrIncinerationIfYouWill(Collision2D lavaCollision)
    {
        if (lavaCollision.gameObject.tag == "Player1" || lavaCollision.gameObject.tag == "Player2")
        {
            if (lavaCollision.gameObject.tag == "Player1")
            {
                PlayerOneScore = PlayerOneScore + lavaScoreValue;
                Destroy(GameObject.FindWithTag("Player1"));
            }

            if (lavaCollision.gameObject.tag == "Player2")
            {
                PlayerTwoScore = PlayerTwoScore + lavaScoreValue;
                Destroy(GameObject.FindWithTag("Player2"));
            }
        }

    }


}

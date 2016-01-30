using UnityEngine;
using System.Collections;

public class PointDistributionUnityKernelFinalBuild : MonoBehaviour
{

    public int lavaScoreValue;

    public int PlayerOneScore;
    public int PlayerTwoScore;
    public static PointDistributionUnityKernelFinalBuild InstanceScore;

    // Use this for initialization
    void Start()
    {
        InstanceScore = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void lavaCollisionOrIncinerationIfYouWill(Collision2D lavaCollision)
    {
        if (lavaCollision.transform.tag == "Player1" || lavaCollision.transform.tag == "Player2")
        {
            if (tag == "Player1")
            {
                PlayerOneScore = PlayerOneScore + lavaScoreValue;
                Destroy(GameObject.FindWithTag("Player1"));
            }

            if (tag == "Player2")
            {
                PlayerTwoScore = PlayerTwoScore + lavaScoreValue;
                Destroy(GameObject.FindWithTag("Player2"));
            }
        }

    }

    void CheckScoreUpdate()
    {
        if (PlayerOneScore < WhenTheManComesAroundDynamcs.godClass.pOneScore)
        {
            PlayerOneScore = WhenTheManComesAroundDynamcs.godClass.pOneScore;
        }

        if (PlayerTwoScore < WhenTheManComesAroundDynamcs.godClass.pOneScore)
        {
            PlayerTwoScore = WhenTheManComesAroundDynamcs.godClass.PTwoScore;
        }
    }


}

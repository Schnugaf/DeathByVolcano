using UnityEngine;
using System.Collections;

public class PointDistributionUnityKernelFinalBuild : MonoBehaviour
{

    public int lavaScoreValue;

    public int PlayerOneScore;
    public int PlayerTwoScore;

    // Use this for initialization
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerEnter2D(Collider2D lavaCollision)
    {
        print("Collision");
        if (lavaCollision.gameObject.tag == "Player1" || lavaCollision.gameObject.tag == "Player2")
        {
            Debug.Log("Collision detected");
            if (lavaCollision.gameObject.tag == "Player1")
            {
                PlayerOneScore = PlayerOneScore + lavaScoreValue;
                Destroy(GameObject.FindWithTag("Player1"));
                Debug.Log("Player One Collided");
            }

            if (lavaCollision.gameObject.tag == "Player2")
            {
                PlayerTwoScore = PlayerTwoScore + lavaScoreValue;
                Destroy(GameObject.FindWithTag("Player2"));
            }
        }

    }


}

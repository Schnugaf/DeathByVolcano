using UnityEngine;
using System.Collections;

public class PointDistributionUnityKernelFinalBuild : MonoBehaviour
{

    public int lavaScoreValue;

    public int PlayerOneScore;
    public int PlayerTwoScore;

    public GameObject lavaBlobPrefab;

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
        if (lavaCollision.gameObject.tag == "Player1" || lavaCollision.gameObject.tag == "Player2")
        {
            Vector3 pos = new Vector3(lavaCollision.transform.position.x, lavaCollision.transform.position.y + 1.35f, 0f);
            Instantiate(lavaBlobPrefab, pos, Quaternion.identity);

            if (lavaCollision.gameObject.tag == "Player1")
            {
                PlayerOneScore = PlayerOneScore + lavaScoreValue;
                Destroy(lavaCollision.gameObject);
            }

            if (lavaCollision.gameObject.tag == "Player2")
            {
                PlayerTwoScore = PlayerTwoScore + lavaScoreValue;
                Destroy(lavaCollision.gameObject);
            }
        }

    }


}

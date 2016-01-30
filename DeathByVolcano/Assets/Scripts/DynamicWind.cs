using UnityEngine;
using System.Collections;

public class DynamicWind : MonoBehaviour
{
	float timer;
	float rndTime = 1f;
	public GameObject[] particles;
	public Transform[] particleSpawnPos;

    public float maxWind;
    public float minWind;
    public float windFloat;
    public float downWardGravity;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(WindChangeFreq());
    }

    // Update is called once per frame
    void Update()
    {
        GravityPushX();

		timer += Time.deltaTime;

		if (timer > rndTime)
		{
			Vector3 basePos = particleSpawnPos [Random.Range (0, particleSpawnPos.Length)].position;
			Vector3 pos = new Vector2 (basePos.x + Random.Range (-2f, 2f), basePos.y);
			Instantiate (particles [Random.Range (0, particles.Length)], pos, Quaternion.identity);
			timer = 0;
			rndTime = Random.Range (0.5f, 1.5f);
		}
    }

    void WindRandDir()
    {
        windFloat = Random.Range(minWind, maxWind);
    }

    IEnumerator WindChangeFreq()
    {
//        Debug.Log("Hello, I am in WindChangeFreq");
        int timeWait = Random.Range(5, 9);
        yield return new WaitForSeconds(timeWait);
//        Debug.Log("Hello, I have counted for " + timeWait);
        WindRandDir();
//        Debug.Log("Hello again, friend, I have changed the wind direction.");
        StartCoroutine(WindChangeFreq());
    }
    

    void GravityPushX()
    {
		Physics2D.gravity = new Vector2 (windFloat, downWardGravity);
		print (Physics2D.gravity.x);
    }
}

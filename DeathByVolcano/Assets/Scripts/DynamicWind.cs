using UnityEngine;
using System.Collections;

public class DynamicWind : MonoBehaviour
{


    public float MaxWind;
    public float MinWind;
    public float WindFloat;
    public float DownWardGravity;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(WindChangeFreq());
    }

    // Update is called once per frame
    void Update()
    {
        GravityPushX();
    }

    void WindRandDir()
    {
        WindFloat = Random.Range(MinWind, MaxWind);
    }

    IEnumerator WindChangeFreq()
    {
        Debug.Log("Hello, I am in WindChangeFreq");
        int timeWait = Random.Range(2, 5);
        yield return new WaitForSeconds(timeWait);
        Debug.Log("Hello, I have counted for " + timeWait);
        WindRandDir();
        Debug.Log("Hello again, friend, I have changed the wind direction.");
        StartCoroutine(WindChangeFreq());
    }
    


    void GravityPushX()
    {
        Physics.gravity = new Vector3(WindFloat, DownWardGravity, 0);
    }
}

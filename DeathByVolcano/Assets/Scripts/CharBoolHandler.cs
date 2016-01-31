using UnityEngine;
using System.Collections;

public class CharBoolHandler : MonoBehaviour {

    public bool charBool;

	// Use this for initialization
	void Start () 
    {
        GameObject.DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
}

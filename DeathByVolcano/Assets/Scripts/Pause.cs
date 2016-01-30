using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	public bool paused;

	public GameObject pCanvas;

	// Use this for initialization
	void Start () 
	{
		paused = false;
		pCanvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ((Input.GetKeyDown (KeyCode.Escape)) || (Input.GetKeyDown(KeyCode.P)))
		{
				paused = !paused;
		}

		if (paused) {
			pCanvas.SetActive(true);
			Time.timeScale = 0f;
		} 

		else 
		{
			pCanvas.SetActive(false);
			Time.timeScale = 1f;
		}
	}
}

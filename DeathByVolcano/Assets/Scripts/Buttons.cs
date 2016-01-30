using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	public void startClicked ()
	{
		SceneManager.LoadScene("MainGame");
	}

	public void tutorialClicked()
	{
		SceneManager.LoadScene ("Tutorial");
	}

	public void exitClicked()
	{
		print ("Shit Got Real");
		Application.Quit();

	}
}
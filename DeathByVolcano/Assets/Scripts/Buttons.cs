using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	public Pause pause;

	public void startClicked ()
	{
		SceneManager.LoadScene("TestScene");
	}

	public void tutorialClicked()
	{
		SceneManager.LoadScene ("VictoryScreen");
	}

	public void exitClicked()
	{
		print ("Shit Got Real");
		Application.Quit();
	}

	public void unPauseClicked()
	{
		pause.paused = false;
	}

	public void menuClicked ()
	{
		SceneManager.LoadScene ("StartMenu");
	}

	public void p1Right()
	{
		
	}
	public void p1Left()
	{

	}
	public void p2Right()
	{

	}
	public void p2Left()
	{

	}
}
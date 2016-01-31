using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	public Pause pause;
    public CharSelect charSelect;
    public SelectionStage selStage;

	public void startClicked ()
	{
		SceneManager.LoadScene("Selection");
	}

//	public void tutorialClicked()
//	{
//		SceneManager.LoadScene ("VictoryScreen");
//	}

	public void exitClicked()
	{
		print ("Shit Got Real");
		Application.Quit();
	}

	public void unPauseClicked()
	{
        if (pause)
        {
            pause.paused = false;
        }
	}

	public void menuClicked ()
	{
		SceneManager.LoadScene ("StartMenu");
	}
        
    public void Leftwards()
    {
        if (selStage.chosen == false)
        {
            charSelect.character--;
        }
    }
    public void Rightward()
    {
        if (selStage.chosen == false)
        {
            charSelect.character++;
        }
    }

    public void startButton()
    {
        SceneManager.LoadScene ("JTestScene");
    }

    public void backButton()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void backSelection()
    {
        SceneManager.LoadScene("Selection");
    }
}
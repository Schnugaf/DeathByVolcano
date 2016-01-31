using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour 
{
    public Text charText;
    public GameObject brutus;
    public GameObject lotti;
    public Characters character;

    public SelectionStage p1Chosen;
    public SelectionStage p2Chosen;
    public Button startButton;

    public enum Characters
    {
        brutus,
        lotti
    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        brutus.SetActive(character == Characters.brutus);
        lotti.SetActive(character == Characters.lotti);

        if (character > (Characters)1)
        {
            character = (Characters)0;
        }
        if (character < (Characters)0)
        {
            character = (Characters)1;
        }

        if (character == (Characters)0)
        {
            charText.text = "Brutus";
        }
        if (character == (Characters)1)
        {
            charText.text = "Lotti";
        }

        if (p1Chosen.chosen == true && p2Chosen.chosen == true)
        {
            startButton.gameObject.SetActive(true);
        }
        else
        {
            startButton.gameObject.SetActive(false);
        }
	}

//    public void Leftwards()
//    {
//        character--;
//    }
//    public void Rightward()
//    {
//        character++;
//    }
}

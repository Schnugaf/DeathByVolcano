using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectionStage : MonoBehaviour
{

	public bool chosen;


	private Button button;
	private ColorBlock theColor;
	Color color;

//	public GameObject brutus;
//	public GameObject lotti;
//	public Characters character;
//
//	public enum Characters
//	{
//		brutus,
//		lotti
//	}

	// Use this for initialization
	void Start ()
	{
		chosen = false;

		button = GetComponent<Button>();
		theColor = GetComponent<Button>().colors;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (chosen == false) 
		{
			theColor.highlightedColor = Color.green;
			theColor.normalColor = Color.red;
			color.a = 0.25f;
		} 
		else 
		{
			theColor.highlightedColor = Color.red;
			theColor.normalColor = Color.green;
			color.a = 0.25f;
		}
		button.colors = theColor;

//		brutus.SetActive(character == Characters.brutus);
//		lotti.SetActive(character == Characters.brutus);
//
//		if (character > (Characters)1)
//		{
//			character = (Characters)0;
//		}
//		if (character < (Characters)0)
//		{
//			character = (Characters)1;
//		}
	}

	public void Choose ()
	{
		if (chosen == true)
		{
			chosen = false;
		}
		else
		{
			chosen = true;
		}
	}

	
}

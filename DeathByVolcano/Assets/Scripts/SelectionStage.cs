using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectionStage : MonoBehaviour
{

	public bool chosen;

	private Button button;
	private ColorBlock theColor;
	Color color;

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

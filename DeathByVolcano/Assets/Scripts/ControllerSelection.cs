using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControllerSelection : MonoBehaviour {

    public CharSelect charSelect;
    public PlayerInput pInput;
    public SelectionStage selStage;
    public SelectionStage otherChosen;

    float selectorFloat;


	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (pInput.horizontal >= 1f)
        {
            selectorFloat += Time.deltaTime;
        }
        if (pInput.horizontal <= -1f)
        {
            selectorFloat -= Time.deltaTime;
        }
        if (selectorFloat > 0.5f)
        {
            if (selStage.chosen == false)
            {
                charSelect.character++;
            }
            selectorFloat = 0f;
        }
        if (selectorFloat < -0.5f)
        {
            if (selStage.chosen == false)
            {
                charSelect.character--;
            }

            selectorFloat = 0f;
        }
        if (pInput.submit)
        {
            selStage.chosen = !selStage.chosen;
        }
        if (pInput.cancel)
        {
            SceneManager.LoadScene("StartMenu");
        }
        if (pInput.start)
        {
            if (selStage.chosen == true && otherChosen.chosen == true)
            {
                SceneManager.LoadScene("TestScene");
            }
        }
	}
}

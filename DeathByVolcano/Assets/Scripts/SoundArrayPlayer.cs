using UnityEngine;
using System.Collections;

public class SoundArrayPlayer : MonoBehaviour {

    public AudioClip[] ManGrunt;
    public AudioClip[] FemGrunt;

    GameObject[] fem;
    GameObject[] man;

    PlayerThink femThink;
    PlayerThink manThink;
    PlayerThink manThinkTwo;
    PlayerThink femThinkTwo;


    // Use this for initialization
    void Start()
    {
        man = GameObject.FindGameObjectsWithTag("ManPlayer");
        fem = GameObject.FindGameObjectsWithTag("FemPlayer");

        manThink = man[0].GetComponent<PlayerThink>();
        if (man.Length > 0)
        {
            manThinkTwo = man[1].GetComponent<PlayerThink>();
        }
        femThink = fem[0].GetComponent<PlayerThink>();

        if (fem.Length > 0)
        {
            femThinkTwo = fem[1].GetComponent<PlayerThink>();
        }

    }
	
	// Update is called once per frame
	void Update ()
    {

        

	}
}

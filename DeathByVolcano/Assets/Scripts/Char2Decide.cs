﻿using UnityEngine;
using System.Collections;

public class Char2Decide : MonoBehaviour {

    public GameObject p2CharSel; 

    public GameObject brutus;
    public GameObject lotti;
	// Use this for initialization
	void Awake () 
    {
        p2CharSel = GameObject.FindGameObjectWithTag("CharSel2");

        if (p2CharSel.GetComponent<CharBoolHandler>().charBool == true)
        {
            brutus.SetActive(true);
            lotti.SetActive(false);
        }
        else
        {
            brutus.SetActive(false);
            lotti.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}

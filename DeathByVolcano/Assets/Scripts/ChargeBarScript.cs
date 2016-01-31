using UnityEngine;
using System.Collections;

public class ChargeBarScript : MonoBehaviour
{
    Transform trC;

    public float chargeConversion;
    float chargeScale;
    float chTimer;
    float chMaxTimer;
    public float depletionRate;


    public GameObject playerObj;

    // Use this for initialization
    void Start()
    {
        trC = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        chTimer = playerObj.GetComponent<PlayerThink>().chargeTimer;
        chMaxTimer = playerObj.GetComponent<PlayerThink>().maxChargeTime;

        if (chTimer > 0 && chTimer < chMaxTimer)
        {
            scaleFromCharge();
            chargeScale = chTimer * chargeConversion;
        }

        else if (chTimer > chMaxTimer)
        {
            chargeScale = chMaxTimer + chargeConversion;
        }

        else if (chTimer == 0 && transform.localScale.y > 0)
        {
            chargeScale = 0;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - depletionRate, transform.localScale.z);
            if( transform.localScale.y < 0.3)
            {
                transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
            }
        }


    }




    void scaleFromCharge()
    {
        print("Object is scaling");
        transform.localScale = new Vector3(transform.localScale.x, chargeScale, transform.localScale.z);

    }


}


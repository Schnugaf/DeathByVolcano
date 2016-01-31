using UnityEngine;

public class ObjectProperties : MonoBehaviour
{
    Rigidbody2D rb;

    public enum WeightClass
    {
        Light,
        Medium,
        Heavy
    }

    public WeightClass weight = WeightClass.Medium;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        if (weight == WeightClass.Light)
        {
            rb.mass = 1f;
        }
        else if (weight == WeightClass.Medium)
        {
            rb.mass = 2f;
        }
        else if (weight == WeightClass.Heavy)
        {
            rb.mass = 3f;
        }
	}
}

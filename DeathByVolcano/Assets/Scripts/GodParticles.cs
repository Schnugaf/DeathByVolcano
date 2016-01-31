using UnityEngine;

public class GodParticles : MonoBehaviour
{
    public Transform god;
	
	void Update ()
    {
        transform.position = god.position;
	}
}

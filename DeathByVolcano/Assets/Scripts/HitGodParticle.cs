using UnityEngine;

public class HitGodParticle : MonoBehaviour
{
    public float upSpeed = 10f;
    SpriteRenderer rend;
    
	void Start ()
    {
        rend = GetComponent<SpriteRenderer>();
	}
	
	void Update ()
    {
        transform.position += Vector3.up * Time.deltaTime * upSpeed;
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, rend.color.a - 0.005f);
	}
}

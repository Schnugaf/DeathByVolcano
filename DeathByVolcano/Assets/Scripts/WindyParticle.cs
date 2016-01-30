using UnityEngine;

public class WindyParticle : MonoBehaviour
{
	public float lifeTime = 5f;

	float timer;
	Rigidbody2D rb;
	public float forceY = 0.8f;
	public float forceModifierX = 0.2f;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		rb.AddForce (new Vector2 (Physics2D.gravity.x * forceModifierX, forceY));

		timer += Time.deltaTime;

		if (timer > lifeTime)
		{
			Destroy (gameObject);
		}
	}
}

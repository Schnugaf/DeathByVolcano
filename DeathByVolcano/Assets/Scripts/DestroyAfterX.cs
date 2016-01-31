using UnityEngine;

public class DestroyAfterX : MonoBehaviour
{
    public float lifeTime = 1f;
    float timer;
	
	void Update ()
    {
        timer += Time.deltaTime;

        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
	}
}

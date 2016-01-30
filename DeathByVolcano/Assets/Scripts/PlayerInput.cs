using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public string vertical;
	public string charging;
	[HideInInspector]public bool isCharging;
	[HideInInspector]public float verticalAxis;

	void Start ()
	{
		
	}

	void Update ()
	{
		verticalAxis = Input.GetAxis (vertical);
		isCharging = Input.GetButton (charging);
	}
}

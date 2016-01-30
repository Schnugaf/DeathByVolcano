using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public string inputVertical;
	public string inputCharging;
	[HideInInspector]public bool chargeDown;
	[HideInInspector]public bool charge;
	[HideInInspector]public float vertical;

	void Start ()
	{
		
	}

	void Update ()
	{
		vertical = Input.GetAxis (inputVertical);
		chargeDown = Input.GetButtonDown (inputCharging);
		charge = Input.GetButton (inputCharging);
	}
}

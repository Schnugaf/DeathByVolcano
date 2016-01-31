using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public string inputVertical;
	public string inputCharging;
    public string inputHorizontal;
    public string inputSubmit;
    public string inputCancel;
    public string inputStart;
	[HideInInspector]public bool chargeDown;
	[HideInInspector]public bool charge;
	[HideInInspector]public float vertical;
    [HideInInspector]public float horizontal;
    [HideInInspector]public bool submit;
    [HideInInspector]public bool cancel;
    [HideInInspector]public bool start;

	void Start ()
	{
		
	}

	void Update ()
	{
		vertical = Input.GetAxis (inputVertical);
		chargeDown = Input.GetButtonDown (inputCharging);
		charge = Input.GetButton (inputCharging);
        horizontal = Input.GetAxisRaw(inputHorizontal);
        submit = Input.GetButtonDown(inputSubmit);
        cancel = Input.GetButtonDown(inputCancel);
        start = Input.GetButtonDown(inputStart);
	}
}

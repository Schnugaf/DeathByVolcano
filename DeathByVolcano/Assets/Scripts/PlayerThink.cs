using UnityEngine;

public class PlayerThink : MonoBehaviour
{
	PlayerInput pi;
	Animator anim;
	public Transform upperBody;
	public Transform aimParent;
	Puppet2D_GlobalControl puppet;

	public bool isHoldingObject;
	public bool anim_pickingUpObject;
	public string side = "Left";

	public float aimSensitivity = 100f;
	public float maxAimAngle = 70f;
	public float oppositeAimAngle = 295f;

	public float maxChargeTime = 2f;
	float chargeTimer;
	bool startedCharge;

	public bool anim_throw;
	bool hasThrownObject;
	public int throwScale = 500;
	public float throwWaitTime = 1.5f;
	float throwWaitTimer;

	public GameObject[] projectiles;
	public Transform rightHand;
	GameObject heldObject;

	void Start ()
	{
		pi = GetComponent<PlayerInput> ();
		anim = GetComponent<Animator> ();
		puppet = GetComponent<Puppet2D_GlobalControl> ();
	}

	void Update ()
	{
		//If I don't have an object and click, play animation to pick it up.
		if (!isHoldingObject && !hasThrownObject)
		{
			if (pi.chargeDown)
			{
				anim.SetTrigger ("PickUp");
			}

			if (side == "Left")
			{
				puppet.flip = true;
			}
			else	//Right side character will turn around.
			{
				puppet.flip = false;
			}
			upperBody.localEulerAngles = Vector3.zero;


			//Animation will keyframe anim_pickingUpObject to be true.
			if (!heldObject && anim_pickingUpObject)
			{
				//Place random object in hand.
				heldObject = Instantiate(projectiles[Random.Range(0, projectiles.Length)], rightHand.position, rightHand.rotation) as GameObject;
				heldObject.transform.parent = rightHand;
				heldObject.GetComponent<Rigidbody2D> ().isKinematic = true;

				isHoldingObject = true;
				anim_pickingUpObject = false;
			}
		}

		//---------HAS PICKED UP OBJECT AT THIS POINT---------\\

		if (isHoldingObject)
		{
			if (side == "Left")
			{
				puppet.flip = false;
			}
			else	//Right side character will turn around.
			{
				puppet.flip = true;
			}

			//Aiming up/down
			upperBody.localEulerAngles += new Vector3(0f, 0f, pi.vertical * aimSensitivity * Time.deltaTime);

			if (upperBody.localEulerAngles.z > maxAimAngle)
			{
				if (upperBody.localEulerAngles.z < oppositeAimAngle && upperBody.localEulerAngles.z > oppositeAimAngle - 10f)
				{
					upperBody.localEulerAngles = new Vector3 (0f, 0f, oppositeAimAngle);
				}
				else if (upperBody.localEulerAngles.z < maxAimAngle + 10)
				{
					upperBody.localEulerAngles = new Vector3 (0f, 0f, maxAimAngle);
				}
			}

			//Charging a shot

			if (pi.chargeDown)
			{
				startedCharge = true;
				anim.SetBool("Charging", true);
			}

			if (startedCharge)
			{
				chargeTimer += Time.deltaTime;
			}

			//Release
			if (startedCharge && !pi.charge)
			{
				startedCharge = false;
				anim.SetBool("Charging", false);
			}

			//Send projectile
			if (anim_throw)
			{
				print ("Release");
				anim_throw = false;

				//Detach it
				heldObject.transform.parent = null;
				Vector3 angle = (aimParent.GetChild(0).position - aimParent.position).normalized;

				Rigidbody2D rb = heldObject.GetComponent<Rigidbody2D> ();
				isHoldingObject = false;

				rb.isKinematic = false;
				rb.AddForce (angle * (chargeTimer * throwScale));
				chargeTimer = 0;
				hasThrownObject = true;
				heldObject = null;
			}
		}

		if (hasThrownObject)
		{
			throwWaitTimer += Time.deltaTime;
		}
		if (throwWaitTimer > throwWaitTime)
		{
			hasThrownObject = false;
			throwWaitTimer = 0f;
		}

		chargeTimer = Mathf.Clamp (chargeTimer, 0f, maxChargeTime);

	}
}

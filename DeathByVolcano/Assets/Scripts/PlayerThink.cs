﻿using UnityEngine;

public class PlayerThink : MonoBehaviour
{
	PlayerInput pi;
	Animator anim;
	public Transform aimParent;
	Puppet2D_GlobalControl puppet;

	public bool inPickupAnim;

	public bool isHoldingObject;
	public bool anim_pickingUpObject;
	public string side = "Left";

	public float aimSensitivity = 10f;
	public float maxAimAngle = 70f;
	public float oppositeAimAngle = 295f;

	public float maxChargeTime = 2f;
	float chargeTimer;
	bool startedCharge;

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
		//If I don't have an object, play animation to pick it up.
		if (!isHoldingObject)
		{
			anim.SetTrigger ("PickUp");
			inPickupAnim = true;

			if (inPickupAnim)
			{
				if (side == "Left")
				{
					puppet.flip = true;
				}
				else	//Right side character will turn around.
				{
					puppet.flip = false;
				}
			}

			//Animation will keyframe anim_pickingUpObject to be true.
			if (!heldObject && anim_pickingUpObject)
			{
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
			aimParent.localEulerAngles += new Vector3(0f, 0f, pi.vertical * aimSensitivity * Time.deltaTime);

			if (aimParent.localEulerAngles.z > maxAimAngle)
			{
				if (aimParent.localEulerAngles.z < oppositeAimAngle && aimParent.localEulerAngles.z > oppositeAimAngle - 10f)
				{
					aimParent.localEulerAngles = new Vector3 (0f, 0f, oppositeAimAngle);
				}
				else if (aimParent.localEulerAngles.z < maxAimAngle + 10)
				{
					aimParent.localEulerAngles = new Vector3 (0f, 0f, maxAimAngle);
				}
			}

			//Charging a shot

			if (pi.chargeDown)
			{
				startedCharge = true;
			}

			if (startedCharge)
			{
				chargeTimer += Time.deltaTime;
			}

			//Release
			if (startedCharge && !pi.charge)
			{
				startedCharge = false;
				//Send projectile
			}
		}

		chargeTimer = Mathf.Clamp (chargeTimer, 0f, maxChargeTime);

		//--------------Anims--------------\\

//		anim.SetBool("Charging", startedCharge);
	}
}

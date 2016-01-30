using UnityEngine;

public class PlayerThink : MonoBehaviour
{
	PlayerInput pi;
	public Transform aimParent;

	public float aimSensitivity = 1f;
	public float maxAimAngle = 70f;
	public float oppositeAimAngle = 280f;

	public float maxChargeTime = 2f;
	float chargeTimer;
	bool startedCharge;

	void Start ()
	{
		pi = GetComponent<PlayerInput> ();
	}

	void Update ()
	{
		//Aiming up/down
		aimParent.localEulerAngles += new Vector3(0f, 0f, pi.verticalAxis * aimSensitivity);

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
		if (pi.isCharging)
		{
			startedCharge = true;
			chargeTimer += Time.deltaTime;
		}

		//Release
		if (startedCharge && !pi.isCharging)
		{
			startedCharge = false;
			//Send projectile
		}

		chargeTimer = Mathf.Clamp (chargeTimer, 0f, maxChargeTime);
	}
}

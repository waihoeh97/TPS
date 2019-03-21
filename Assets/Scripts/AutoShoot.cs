using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShoot : MonoBehaviour {

	public Camera cam;

	public float damage = 10f;
	public float range = 50f;
	public float impactForce = 30f;

	float shootDelay = 0.2f;
	float lastShoot;

	public GameObject impactEffect;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		RaycastHit hit;

		if (Time.time > shootDelay + lastShoot)
		{
			if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
			{
				Debug.Log (hit.transform.name);
				ZombieBehaviour zombie = hit.transform.GetComponent<ZombieBehaviour>();
				if (zombie != null)
				{
					zombie.TakeDamage(damage);
				}
				if (hit.rigidbody != null)
				{
					hit.rigidbody.AddForce(hit.normal * impactForce);
				}

				GameObject impactGO = Instantiate (impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
				Destroy(impactGO, 2f);
			}
			lastShoot = Time.time;
			SoundManagerScript.Instance.PlaySFX(SoundManagerScript.AudioClipID.SFX_SHOOT);
		}
	}
}

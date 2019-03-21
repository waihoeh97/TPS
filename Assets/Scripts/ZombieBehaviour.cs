using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : MonoBehaviour {

	public static int score;

	Transform player;

	float health = 50f;
	float rotateSpeed = 3.0f;
	float moveSpeed = 2.0f;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	public void TakeDamage (float damage)
	{
		health -= damage;
	}

	// Update is called once per frame
	void Update ()
	{
		// Look at player
		transform.rotation = Quaternion.Slerp (transform.rotation
				, Quaternion.LookRotation(player.position - transform.position)
				, rotateSpeed * Time.deltaTime);

		// Move to player
		transform.position += transform.forward * moveSpeed * Time.deltaTime;

		if (health <= 0f)
		{
			score += 2;
			SoundManagerScript.Instance.PlaySFX(SoundManagerScript.AudioClipID.SFX_DESTROY);
			Destroy(gameObject);
		}
	}
}

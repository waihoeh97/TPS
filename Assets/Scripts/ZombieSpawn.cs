using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour {

	public TPSCharacter player;

	public GameObject enemy;
	public bool canSpawn;

	// Use this for initialization
	void Start () 
	{
		canSpawn = true;
		InstantiateEnemy ();
	}

	void InstantiateEnemy ()
	{
		if (canSpawn)
		{
			GameObject current = (GameObject) Instantiate (enemy);
			current.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

			StartCoroutine ("waitForFewSeconds");
		}
	}

	IEnumerator waitForFewSeconds ()
	{
		yield return new WaitForSeconds (5.0f);
		InstantiateEnemy ();
	}

	// Update is called once per frame
	void Update () 
	{
		if (player.dead == true)
		{
			canSpawn = false;
		}
	}
}

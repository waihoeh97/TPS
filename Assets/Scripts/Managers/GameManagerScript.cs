using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour 
{
	private static GameManagerScript mInstance;

	public static GameManagerScript Instance
	{
		get
		{
			if(mInstance == null)
			{
				GameObject[] tempObjectList = GameObject.FindGameObjectsWithTag("GameManager");

				if(tempObjectList.Length > 1)
				{
					Debug.LogError("You have more than 1 Game Manager in the Scene");
				}
				else if(tempObjectList.Length == 0)
				{
					GameObject obj = new GameObject("_GameManager");
					mInstance = obj.AddComponent<GameManagerScript>();
					obj.tag = "GameManager";
				}
				else
				{
					if(tempObjectList[0] != null)
					{
						Debug.Log("Found a game manager");
						mInstance = tempObjectList[0].GetComponent<GameManagerScript>();
					}
				}
			}
			return mInstance;
		}
	}

	public TPSCharacter player;

	public GameObject endScene;
	public GameObject beginningScene;

	public Text text;

	float time;

	// Use this for initialization
	void Start () 
	{
		endScene.SetActive(false);
		beginningScene.SetActive(true);
		Scene curScene = SceneManager.GetActiveScene();
		Debug.Log(curScene.buildIndex);
		if(curScene.buildIndex == 0)
		{
			SoundManagerScript.Instance.PlayBGM(SoundManagerScript.AudioClipID.BGM_LEVEL);
		}
	}

	void Update () 
	{
		time = Time.time;

		if (time >= 1.0f) 
		{
			beginningScene.SetActive(false);
		}

		text.text = "" + ZombieBehaviour.score;

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		if (player.dead == true)
		{
			SoundManagerScript.Instance.StopBGM();
			endScene.SetActive(true);
		}
	}

}

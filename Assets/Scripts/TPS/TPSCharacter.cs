using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCharacter : MonoBehaviour {

	private Vector3 fp;   //First touch position
	private Vector3 lp;   //Last touch position
	private float dragDistance;

	Vector3 startPos, endPos, direction;
	float touchTimeStart, touchTimeFinish, timeInterval;

	public float throwForce = 0.3f;
	public float walkSpeed = 3f;

	public bool onGround;
	public bool dead;

	private Rigidbody rb;

	// Use this for initialization
	void Start ()
	{
		dragDistance = Screen.height * 15 / 100;
		onGround = true;
		dead = false;
		rb = GetComponent<Rigidbody> ();
	}

	public void Jumping ()
	{
		if (onGround)
		{
			rb.velocity = new Vector3 (0f, 5f, 0f);
			onGround = false;
		}
	}

	public void Move (Vector3 _direction)
	{
		this.transform.Translate (_direction * Time.deltaTime * walkSpeed, Space.Self);
	}

	void SwipeDash ()
	{
		if (Input.touchCount == 1) // user is touching the screen with a single touch
		{
			Touch touch = Input.GetTouch(0); // get the touch
			if (touch.phase == TouchPhase.Began) //check for the first touch
			{
				fp = touch.position;
				lp = touch.position;
			}
			else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
			{
				lp = touch.position;
			}
			else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
			{
				lp = touch.position;  //last touch position. Ommitted if you use list

				//Check if drag distance is greater than 20% of the screen height
				if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
				{//It's a drag
					//check if the drag is vertical or horizontal
					if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
					{   //If the horizontal movement is greater than the vertical movement...
						if ((lp.x > fp.x))  //If the movement was to the right)
						{   //Right swipe
							Debug.Log("Right Swipe");
							rb.AddForce(Vector3.right * 200);
						}
						else
						{   //Left swipe
							Debug.Log("Left Swipe");
							rb.AddForce(Vector3.left * 200);
						}
					}
					else
					{   //the vertical movement is greater than the horizontal movement
						if (lp.y > fp.y)  //If the movement was up
						{   //Up swipe
							Debug.Log("Up Swipe");
							rb.AddForce(Vector3.forward * 200);
						}
						else
						{   //Down swipe
							Debug.Log("Down Swipe");
							rb.AddForce(Vector3.back * 200);
						}
					}
				}
				else
				{   //It's a tap as the drag distance is less than 20% of the screen height
					Debug.Log("Tap");
				}
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{
		// Testing
		if (onGround) 
		{
			if (Input.GetKeyDown(KeyCode.Space)) 
			{
				rb.velocity = new Vector3 (0f, 5f, 0f);
				onGround = false;
			}
		}
		SwipeDash();
	}

	void OnCollisionEnter (Collision other) 
	{
		if (other.gameObject.CompareTag ("Ground")) 
		{
			onGround = true;
		}
		if (other.gameObject.CompareTag ("Enemy")) 
		{
			Debug.Log("Dead");
			dead = true;
		}
	}
}

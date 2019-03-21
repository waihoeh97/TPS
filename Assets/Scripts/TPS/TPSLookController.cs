using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TPSLookController : MonoBehaviour, IDragHandler, IEndDragHandler {

	RectTransform rect;
	Vector3 originPos;
	Vector3 direction;

	public Transform rootTransform;

	// Use this for initialization
	void Start () {
		rect = this.GetComponent<RectTransform>();
		originPos = rect.position;
	}

	public void OnDrag (PointerEventData eventData)
	{
		this.rect.position = eventData.position;
		direction = rect.position - originPos;
		direction.Normalize();
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		this.rect.position = originPos;
		//direction = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {

		float angles = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
		rootTransform.eulerAngles = new Vector3 (0f, -angles, 0f);
	}
}

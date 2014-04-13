using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {
	public bool left;
	public bool mid;
	public bool right;
	public bool ready;
	int SwipeID;
	Vector2 StartPos;
	int minMovement;
	float buttonCooler;
	void Start () {
		ready = true;
		buttonCooler = 1;
		minMovement = 0;
		left = false;
		mid = true;
		right = false;
	}
	void Swipe ()
	{
		foreach (var T in Input.touches) {
			var P = T.position;
			var C = T.tapCount;
			if (T.phase == TouchPhase.Began && SwipeID == -1) {
				SwipeID = T.fingerId;
				StartPos = P;
			} else if (T.fingerId == SwipeID) {
				var delta = P - StartPos;
				if (T.phase == TouchPhase.Moved && delta.magnitude > minMovement) {
					SwipeID = -1;
					if (Mathf.Abs (delta.x) > Mathf.Abs (delta.y)) {
						if (delta.x > 0 && !right) {
							if(mid && ready)
							{
								right = true;
								mid = false;
							}
							if(left && ready)
							{
								mid = true;
								left = false;
							}
						} else if (delta.x < 0 && !left) {
							if(mid && ready)
							{
								left = true;
								mid = false;
							}
							if(right && ready)
							{
								mid = true;
								right = false;
							}
						}
					} else {
						if (delta.y > 0) {
							
							//свайп вверх
							
						} else {
							//свайп вниз
							
						}
					}
				} else if (T.phase == TouchPhase.Canceled || T.phase == TouchPhase.Ended) {
					SwipeID = -1;
					if (buttonCooler > 0 && C >= 2) {
						//двойной тап
					} else {
						buttonCooler = 0.5f; 
						C += 1;
					}
				}
			} 
		} 
	}
	void Update () {
		if(gameObject.transform.position == new Vector3(0,gameObject.transform.position.y, gameObject.transform.position.z)
		   || gameObject.transform.position == new Vector3(2.5f,gameObject.transform.position.y, gameObject.transform.position.z) || 
		   gameObject.transform.position == new Vector3(-2.5f,gameObject.transform.position.y, gameObject.transform.position.z))
		{
			ready = true;
		}
		else
		{
			ready = false;
		}
		if(right)
		{
			if(gameObject.transform.position.x < 2.5f)
			{
				gameObject.transform.Translate(0.5f,0,0);
			}
		}
		if(left)
		{
			if(gameObject.transform.position.x > -2.5f)
			{
				gameObject.transform.Translate(-0.5f,0,0);
			}
		}
		if(mid)
		{
			if(!(gameObject.transform.position.x == 0))
			{
				if(gameObject.transform.position.x >= -2.5f && gameObject.transform.position.x < 0)
				{
					gameObject.transform.Translate(0.5f,0,0);
				}
				if(gameObject.transform.position.x <= 2.5f && gameObject.transform.position.x > 0)
				{
					gameObject.transform.Translate(-0.5f,0,0);
				}
			}
		}
		Swipe();
		gameObject.transform.Translate(0,0,0.1f);
	}
}

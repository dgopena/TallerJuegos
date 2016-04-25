﻿using UnityEngine;
using System.Collections;

public class Measurer : MonoBehaviour {

	public Camera camera;

	private Transform lineDot;
	private Transform lineLabel;

	private Vector3 lastPoint;
	private Vector3 newPoint;

	private bool touchReleased = true;
	private bool active = false;
	private bool buttonClick = true; //descuenta el primer click al boton
	// Use this for initialization
	void Start () {
		lineLabel = this.transform.FindChild ("RulerLabel");
		lineDot = this.transform.FindChild ("RulerLine");
		lastPoint = new Vector3(float.NaN, float.NaN, float.NaN);
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			if (Input.GetMouseButton (0) || Input.touchCount > 0) {
				if (touchReleased) {
					touchReleased = false;
					if (!buttonClick) {
						ClickOnScreen ();
					} else {
						buttonClick = false;
					}
				}
			} else if (!Input.GetMouseButton (0) || Input.touchCount == 0) {
				touchReleased = true;
			}
		}
	}

	void ClickOnScreen(){
		if (active) {
			if (float.IsNaN (lastPoint.x)) {
				lastPoint = camera.ScreenToWorldPoint (Input.mousePosition); //Input.GetTouch(0).position;
			} else {
				newPoint = camera.ScreenToWorldPoint (Input.mousePosition);

				float distance = (newPoint - lastPoint).magnitude;
				Vector3 direction = (newPoint - lastPoint);
				direction.Normalize ();
				Vector3 midPoint = lastPoint + (direction * (distance / 2f));
				lineDot.position = new Vector3(midPoint.x, midPoint.y, -1.2f);
				lineDot.localScale = new Vector3(distance*5f, 1f, 1f);

				if(direction.x == 0){
					lineDot.rotation = Quaternion.Euler(Vector3.zero);
				}
				else{
					float angle = Mathf.Atan((direction.y/direction.x));
					angle = (angle*180f)/Mathf.PI;
					lineDot.rotation = Quaternion.Euler(new Vector3(0f,0f,angle));
				}

				lineLabel.position = new Vector3(midPoint.x, midPoint.y, -1.3f);
				lineLabel.GetComponent<TextMesh>().text = "" + distance;

				lastPoint = newPoint;
			}
		}
	}

	public void showLine(){
		active = true;
	}

	public void hideLine(){
		lineLabel.position = new Vector3 (-20f, 0f, -1.5f);
		lineDot.localScale = new Vector3 (1f, 1f, 1f);
		lineDot.position = new Vector3 (-20f, 0f, -1.5f);
		active = false;
		buttonClick = true;
		lastPoint = new Vector3(float.NaN, float.NaN, float.NaN);
	}
}
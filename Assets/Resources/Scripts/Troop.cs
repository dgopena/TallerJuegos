using UnityEngine;
using System.Collections;

public class Troop : MonoBehaviour {


	private int troopID  = 0;
	private Vector3 goalPos;
	private bool goalSet = false;
	public float speedFactor = 2f;
	public float distanceFactor = 0.8f;
	public float randomEndFactor = 0.4f;

	private bool goingToGoal = false;
	private Vector3 goalDir = Vector3.zero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (goingToGoal) {
			float distance = (goalPos - transform.position).magnitude;
			if (distance < distanceFactor) {
				goingToGoal = false;
				this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}
		}
	}

	public void setID(int ID){ troopID = ID; }
	public int getID(){ return troopID; }
	public void setGoal(float X, float Y){ goalPos = new Vector3(X,Y,-2f); goalSet = true;}
	public Vector3 getGoal(){ return goalPos; }

	void OnMouseDown(){
		//Debug.Log (troopID);
		//Debug.Log("Goal: ("+goalPos.x+","+goalPos.y+")");
	}

	public void goToGoal(int k){
		goingToGoal = true;
		//randomizer de cercania
		if (k == -1) {
			goalPos = new Vector3 (Random.Range (goalPos.x - randomEndFactor, goalPos.x + randomEndFactor), Random.Range (goalPos.y - randomEndFactor, goalPos.y + randomEndFactor), goalPos.z);
		} else {
			goalPos += Vector3.right*k;
		}
		goalDir = goalPos - transform.position;
		goalDir.Normalize ();
		
		this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (goalDir.x, goalDir.y) * speedFactor;
	}
}

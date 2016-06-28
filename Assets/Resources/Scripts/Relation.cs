using UnityEngine;
using System.Collections;

public class Relation : MonoBehaviour {
	
	
	public Relation other;
	public Transform resultLabel;
	
	private bool locked = false;
	[HideInInspector]
	public int relationID = -1;
	[HideInInspector]
	public string relationName = "";

	//solo se soportan 3 relation boxes
	public float relation12 = 5f;
	public float relation23 = 2f;
	public float relation13 = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public bool getLock(){
		return locked;
	}

	public void setLock(bool set){
		locked = set;
	}

	void OnMouseDown(){
		int rID = PlayerPrefs.GetInt("lastRelClick");
		if (rID != other.relationID) {
			relationID = rID;
			relationName = PlayerPrefs.GetString ("lastRelName");
			TextMesh labb = transform.FindChild ("Label").GetComponent<TextMesh> ();
			labb.text = relationName;
			locked = true;

			
			TextMesh rL = resultLabel.GetComponent<TextMesh> ();
			if (transform.name == "Box1") {
				if (relationID == 1 && other.relationID == 2) {
					rL.text = "" + relation12;
				} else if (relationID == 1 && other.relationID == 3) {
					rL.text = "" + relation13;
				} else if (relationID == 2 && other.relationID == 3) {
					rL.text = "" + relation23;
				}
                Log_writer.addLine("Probo relacion entre " + relationID + " y " + other.relationID);
			}
			else if (transform.name == "Box2") {
				if (relationID == 2 && other.relationID == 1) {
					rL.text = "" + relation12;
				} else if (relationID == 3 && other.relationID == 1) {
					rL.text = "" + relation13;
				} else if (relationID == 3 && other.relationID == 2) {
					rL.text = "" + relation23;
				}
			}

		}
		if (transform.name == "Box1") { //se corrige orden
			if(locked && other.locked){
				if(relationID > other.relationID){
					TextMesh one = transform.FindChild("Label").GetComponent<TextMesh>();
					TextMesh two = other.transform.FindChild("Label").GetComponent<TextMesh>();
					one.text = other.relationName;
					two.text = relationName;
					int aux1 = other.relationID;
					string aux2 = other.relationName;
					other.relationName = relationName;
					other.relationID = relationID;
					relationName = aux2;
					relationID = aux1;
				}
			}
		}
	}
}

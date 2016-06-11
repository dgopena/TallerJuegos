using UnityEngine;
using System.Collections;

public class RelationButton : MonoBehaviour {

	public int relationID = 0;

	private string name;
	// Use this for initialization
	void Start () {
		name = transform.FindChild ("Label").GetComponent<TextMesh> ().text;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		PlayerPrefs.SetInt ("lastRelClick", relationID);
		PlayerPrefs.SetString ("lastRelName", name);
	}
}

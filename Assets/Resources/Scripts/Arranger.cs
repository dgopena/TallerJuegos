using UnityEngine;
using System.Collections;

public class Arranger : MonoBehaviour {

	public int amount = 10;
	public float minHor = -2f;
	public float maxHor = 2f;
	public float minVer = -2f;
	public float maxVer = 2f;

	private Object item;
	public string itemName = "Soldier";
	public Transform[] squad;

	// Use this for initialization
	void Start () {
		Object item = Resources.Load("Prefabs/"+itemName);
		squad = new Transform[amount];
		for (int i = 0; i<amount; i++) {
			GameObject spawn = (GameObject)Instantiate (item);
			Vector3 poss = new Vector3(Random.Range(minHor,maxHor), Random.Range(minVer,maxVer),-2f);
			spawn.transform.position = poss;
			squad[i] = spawn.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using UnityEngine;
using System.Collections;

public class Arranger : MonoBehaviour {

	public int amount = 10;
	public float minHor = -2f;
	public float maxHor = 2f;
	public float minVer = -2f;
	public float maxVer = 2f;

	public int groupAmount = 4; //temporal variable. cantidad de soldados en cada grupo

	private Object item;
	public string itemName = "Soldier";
	private Transform[] squad;

	// Use this for initialization
	void Start () {
		Object item = Resources.Load("Prefabs/"+itemName);
		squad = new Transform[amount];
		for (int i = 0; i<amount; i++) {
			GameObject spawn = (GameObject)Instantiate (item);
			Vector3 poss = new Vector3(Random.Range(minHor,maxHor), Random.Range(minVer,maxVer),-2f);
			spawn.transform.position = poss;
			spawn.GetComponent<Troop>().setID(i);
			spawn.transform.parent = this.transform;
			squad[i] = spawn.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void orderTroops(int groupNum){
		Debug.Log ("Arranging by " + groupNum);
		int groupTotal = (int) (amount / groupNum);
		if ((amount % groupNum) > 0f) {
			groupTotal++;
		}
		Debug.Log (groupTotal);
		for (int i = 0; i<groupTotal; i++) {
			Vector3 point = new Vector3(Random.Range(minHor,maxHor), Random.Range(minVer,maxVer),-2f);
			for(int k = 0; k<groupNum; k++){
				if((groupNum*i)+k < amount){
					Transform soldier = squad[(groupNum*i)+k];
					soldier.GetComponent<Troop>().setGoal(point.x, point.y);
					soldier.GetComponent<Troop>().goToGoal();
				}
			}
		}
	}
}

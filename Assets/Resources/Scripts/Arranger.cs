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
    private Object flaggy;
	public string itemName = "Soldier";
	private Transform[] squad;
    private GameObject[] flags;

	// Use this for initialization
	void Start () {
		Object item = Resources.Load("Prefabs/"+itemName);
        flaggy = Resources.Load("Prefabs/flaggy");
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
        //Debug.Log ("Arranging by " + groupNum);
        Log_writer.addLine("Ordeno items en grupos de " + groupNum);
		int groupTotal = (amount / groupNum);
		if ((amount % groupNum) > 0f) {
			groupTotal++;
		}

        //limpiamos flags
        if (flags != null)
        {
            for (int i = 0; i < flags.Length; i++)
            {
                Destroy(flags[i]);
            }
        }
        flags = new GameObject[groupTotal];

		//Debug.Log (groupTotal);
		for (int i = 0; i<groupTotal; i++) {
			//el resto mandarlo a la banca
			Vector3 point = new Vector3(Random.Range(minHor,maxHor), Random.Range(minVer,maxVer),-2f);
            flags[i] = (GameObject)Instantiate(flaggy, point, Quaternion.Euler(Vector3.zero)); //ponemos banderita
            flags[i].transform.position += 0.8f * Vector3.up;
			if(i == groupTotal - 1 && (amount % groupNum) != 0f){ //punto del resto
				point = new Vector3(-6f, -5f, -2f);
                Destroy(flags[i]);
            }
			for(int k = 0; k<groupNum; k++){
				if((groupNum*i)+k < amount){
					Transform soldier = squad[(groupNum*i)+k];
					soldier.GetComponent<Troop>().setGoal(point.x, point.y);
					if(i != groupTotal - 1){
						soldier.GetComponent<Troop>().goToGoal(-1);
					}
					else{
                        int rest = (amount - (groupNum * i));
                        if(rest == groupNum) { soldier.GetComponent<Troop>().goToGoal(-1); } //resto justo
                        else { soldier.GetComponent<Troop>().goToGoal(k); } //no justo
                        //Debug.Log("hubo resto: "+rest+"| groupNum: "+groupNum);
					}
				}
			}
		}
	}
}

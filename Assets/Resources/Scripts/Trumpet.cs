using UnityEngine;
using System.Collections;

public class Trumpet : MonoBehaviour {

	public Arranger areaObject;
	public int value = 0;

	void OnMouseDown(){
		areaObject.orderTroops (value);
	}
}

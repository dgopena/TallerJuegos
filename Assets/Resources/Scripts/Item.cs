using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	//acciones relevantes para el item que se coloca en grid
	// Use this for initialization

	private int matX = 0;
	private int matY = 0;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int[] getCoods(){
		return new int[2]{matX, matY};
	}

	public void setCoods(int X, int Y){
		matX = X;
		matY = Y;
	}

	void OnMouseDown(){
		ObjectGrid par = transform.parent.GetComponent<ObjectGrid> ();
		par.childClicked (matX, matY);
		if (par.getToolSelected () == 2) {
			Destroy(this.gameObject);
		}
	}
}

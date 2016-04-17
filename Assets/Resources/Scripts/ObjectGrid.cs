using UnityEngine;
using System.Collections;

public class ObjectGrid : MonoBehaviour {

	public int minX = -4;
	public int minY = -5;

	public float horizontalOffset = 1f;
	public float verticalOffset = 3f;

	public int lenX = 6;
	public int lenY = 8;

	private int[,] occupationMatrix;

	public Camera camera;

	private Sprite[] objectGraphic; 
	private Object item;
	public bool randomObject = true;
	public int objectNum = 9;
	public string itemName = "Ball";

	private int toolSelected = 0; //default with add object

	// Use this for initialization
	void Start () {
		occupationMatrix = new int[lenY,lenX];
		for (int j = 0; j<lenY; j++) {
			for(int i = 0; i<lenX; i++){
				occupationMatrix[j,i] = 0;
			}
		}
		item = Resources.Load("Prefabs/"+itemName);
		//modificar esto para flexibilidad en entregas futuras
		objectGraphic = new Sprite[objectNum];
		for (int i = 0; i<objectNum; i++) {
			objectGraphic[i] = Resources.Load<Sprite>("Sprites/ball" + i) as Sprite;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//ver en mas detencion como escalar
	private void add(float X, float Y){
		Vector2 pos = new Vector2 (float.NaN, float.NaN);

		int coodX = (int) Mathf.Floor (X);
		int coodY = (int) Mathf.Floor (Y);
		int matY =(int) (coodX - minY);
		int matX =(int) (coodY - minX);

		if ((matX >= 0) && (matY >= 0)) {
			if ((matY < lenX) && (matX < lenY)) {
				if(occupationMatrix[matX,matY] == 0){
					pos = new Vector2 (coodX + 0.5f, coodY + 0.5f);
					occupationMatrix[matX,matY] = 1;
				}
			} 
		}

		if (!float.IsNaN (pos.x)) {
			GameObject spawn = (GameObject)Instantiate (item);
			spawn.transform.position = new Vector3 (pos.x, pos.y, 0f);
			if (randomObject) {
				int r = (int)Random.Range (0, objectNum);
				spawn.GetComponent<SpriteRenderer> ().sprite = objectGraphic [r];
			}
		}

	}

	//temp
	void OnMouseDown(){
		if (toolSelected == 0) {
			Vector3 poss = camera.ScreenToWorldPoint (Input.mousePosition);
			add (poss.x, poss.y);
		}
	}

	public void changeSelection(int type){
		toolSelected = type;
	}
}

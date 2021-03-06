﻿using UnityEngine;
using System.Collections;

public class ObjectGrid : MonoBehaviour {

	public int originVertical = -4;
	public int originHorizontal = -5;

	public int lenHor = 6;
	public int lenVer = 8;

	private int[,] occupationMatrix;

	public Camera camera;

    private TextMesh dbg;

	private Sprite[] objectGraphic; 
	private Object item;
	public bool randomObject = true;
	public int objectNum = 9;
	public string itemName = "Ball";

	private int toolSelected = 0; //default with add object
	//0 = add
	//1 = ruler
	//2 = erase

	public Measurer ruler;

    private Log_writer lg;

    // Use this for initialization
    void Start () {

        occupationMatrix = new int[lenVer,lenHor];
		for (int j = 0; j<lenVer; j++) {
			for(int i = 0; i<lenHor; i++){
				occupationMatrix[j,i] = 0;
			}
		}
		item = Resources.Load("Prefabs/"+itemName);
		//modificar esto para flexibilidad en entregas futuras
		objectGraphic = new Sprite[objectNum];
		for (int i = 0; i<objectNum; i++) {
			objectGraphic[i] = Resources.Load<Sprite>("Sprites/"+itemName + i) as Sprite;
		}

        dbg = findDebugger();
        lg = findLogger();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//ver en mas detencion como escalar
	private void add(float X, float Y){
		Vector2 pos = new Vector2 (float.NaN, float.NaN);

		int coodX = (int) Mathf.Floor (X);
		int coodY = (int) Mathf.Floor (Y);
		int matY =(int) (coodX - originHorizontal);
		int matX =(int) (coodY - originVertical);

		if ((matX >= 0) && (matY >= 0)) {
			if ((matY < lenHor) && (matX < lenVer)) {
				if(occupationMatrix[matX,matY] == 0){
					pos = new Vector2 (coodX + 0.5f, coodY + 0.5f);
					occupationMatrix[matX,matY] = 1;
				}
			} 
		}

		if (!float.IsNaN (pos.x)) {
			GameObject spawn = (GameObject)Instantiate (item);
			spawn.transform.position = new Vector3 (pos.x, pos.y, -1f);
			if (randomObject) {
				int r = (int)Random.Range (0, objectNum);
				spawn.GetComponent<SpriteRenderer> ().sprite = objectGraphic [r];
			}
			spawn.GetComponent<Item>().setCoods(matX,matY);
			spawn.transform.parent = this.transform;

            //log guarda accion
            Log_writer.addLine("Coloco objeto en : "+matY + " , " + matX);

        }
    }

	//temp
	void OnMouseDown(){
		if (toolSelected == 0) {
			Vector3 poss = camera.ScreenToWorldPoint (Input.mousePosition);
			if(Input.touchCount > 0){
				poss = camera.ScreenToWorldPoint(Input.GetTouch(0).position);
			}
			add (poss.x, poss.y);
		}
	}

	public void changeSelection(int type){
		toolSelected = type;
	}

	public int getToolSelected(){
		return toolSelected;
	}

	public void childClicked(int X, int Y){
		if (toolSelected == 2) {
            //log guarda accion
            occupationMatrix[X,Y] = 0;
            Log_writer.addLine("Borro pelota en : " + Y + " , " + X);
        }
	}

	public void hideToMeasurer(){
		ruler.hideLine ();
	}

	public void showToMeasurer(){
		ruler.showLine ();
	}

    public static TextMesh findDebugger()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Debug");
        GameObject debugger = null;
        TextMesh ret = null;
        foreach (GameObject g in targets)
        {
            if (g.name == "Debugger")
            {
                Debug.Log("Found debugger");
                debugger = g;
                break;
            }
        }
        if (debugger != null)
        {
            ret = debugger.transform.FindChild("Label").GetComponent<TextMesh>();
            ret.text = "Debugger linked";
        }
        return ret;
    }

    public static Log_writer findLogger()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Logger");
        GameObject logger = null;
        Log_writer ret = null;
        foreach (GameObject g in targets)
        {
            if (g.name == "Log")
            {
                Debug.Log("Found logger");
                logger = g;
                break;
            }
        }
        if (logger != null)
        {
            ret = logger.transform.GetComponent<Log_writer>();
        }
        return ret;
    }
}

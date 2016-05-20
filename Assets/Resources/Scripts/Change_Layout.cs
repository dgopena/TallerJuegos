using UnityEngine;
using System.Collections;

public class Change_Layout : MonoBehaviour {

    public string layout_name = "";
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        Application.LoadLevel(layout_name);
    }

    public void move()
    {
        this.transform.localPosition = new Vector3((float) -1.7, (float)-2.1, 0);
    }
}

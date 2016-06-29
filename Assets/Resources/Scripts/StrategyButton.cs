using UnityEngine;
using System.Collections;

public class StrategyButton : MonoBehaviour {
    public string nextLayout = "";


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        string alt = transform.GetComponent<TextMesh>().text;
        Log_writer.addLine("Estrategia elegida en item anterior:\n" + alt);
        Application.LoadLevel(nextLayout);
    }
}

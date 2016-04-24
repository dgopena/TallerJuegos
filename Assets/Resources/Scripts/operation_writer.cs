using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Operation_writer : MonoBehaviour {

    private Text operation;
    
    // Use this for initialization
	void Start () {
        operation = GetComponent<Text>();       
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void write(string operacin)
    {
        operation.text = operacin;
    }
}

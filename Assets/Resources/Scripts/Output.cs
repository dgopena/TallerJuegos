using UnityEngine;
using System.Collections;

public class Output : MonoBehaviour {

	private Transform goButton;
	private Transform cancelButton;
	private Transform outPanel;
	private Transform outLabel;

	private bool active;

	public float targetResult = 0;
    public float result = 0;
    public string nt = "";
	// Use this for initialization
	void Start () {
		goButton = transform.FindChild("Out");
		outLabel = transform.FindChild("outputLabel");
		cancelButton = transform.FindChild("Cancel");
		outPanel = transform.FindChild("Panel");

		active = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void buttonCall(int buttonType){
		if (buttonType == 2) {
			outPanel.position += Vector3.down * 8f;
			active = true;
		} else if (buttonType == 1) {
			outPanel.position += Vector3.up * 8f;
			active = false;
			result = 0;
			nt = "";
			outLabel.GetComponent<TextMesh> ().text = nt;
		} else if (buttonType == 0) {
			if(result == targetResult){
				Debug.Log("yes very gud");
				//colocar evento de congratz y pasar de layout
			}
			else{
				Debug.Log("nope");
				//same
			}
		}
	}

	public void receiveNumbers(string numType){
		if (active) {
            nt = numType;
            outLabel.GetComponent<TextMesh>().text = numType;
		}
	}
}

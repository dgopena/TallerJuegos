using UnityEngine;
using System.Collections;

public class cronometer_button : MonoBehaviour {

	public Sprite ButtonUnpressed;
	public Sprite ButtonPressed;
	private SpriteRenderer rend;
	public bool is_stop = false;
	private cronometertime ct;

	// Use this for initialization
	void Start () {
		ct = this.transform.parent.GetComponent<cronometertime> ();
		rend = this.GetComponent<SpriteRenderer>();
		rend.sprite = this.ButtonUnpressed;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		rend.sprite = this.ButtonPressed;
		if (is_stop) {
			ct.stop ();
		} else {
			ct.toggle_pause_start ();
		}
	}

	void OnMouseUp(){
		rend.sprite = this.ButtonUnpressed;
	}
}

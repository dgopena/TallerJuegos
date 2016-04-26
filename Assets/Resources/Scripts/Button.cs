using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	public int buttonType = 0; //0 = add object, 1 = measure, 2 = erase
	private int numButtons = 3;
	private Sprite[] buttonGraphic;
	private Sprite[] outputGraphic;
	private bool buttonActive = false; //starts inactive

	public bool isOutputType = false; //2 = eliminate output panel, 0 = submit, 1 = cancel
	
	private SpriteRenderer rend;
	// Use this for initialization
	void Start () {
		rend = this.GetComponent<SpriteRenderer>();

		buttonGraphic = new Sprite[numButtons * 2];
		outputGraphic = new Sprite[4];

		for (int i = 0; i<numButtons; i++) {
			buttonGraphic[2*i] = Resources.Load<Sprite>("Sprites/button"+i+"_0") as Sprite; //def
			buttonGraphic[(2*i)+1] = Resources.Load<Sprite>("Sprites/button"+i+"_1") as Sprite; //pressed
		}

		outputGraphic[0] = Resources.Load<Sprite>("Sprites/buttonOut0") as Sprite;
		outputGraphic[1] = Resources.Load<Sprite>("Sprites/buttonOut1") as Sprite;
		outputGraphic[2] = Resources.Load<Sprite>("Sprites/outCancel0") as Sprite;
		outputGraphic[3] = Resources.Load<Sprite>("Sprites/outCancel1") as Sprite;



		if (buttonType == 0 && !isOutputType) {
			this.activate();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if (!isOutputType) {
			if (!buttonActive) {
				rend.sprite = buttonGraphic [buttonType * 2 + 1];
				buttonActive = true;
				if (buttonType == 1) {
					this.transform.parent.GetComponent<ObjectGrid> ().showToMeasurer ();
				}
				disableBrothers ();
				alertParent ();
			}
		} else {
			if(buttonType != 2){
				rend.sprite = outputGraphic [buttonType * 2 + 1];
			}
			transform.parent.GetComponent<Output>().buttonCall(buttonType);
		}
	}

	void OnMouseUp(){
		if (isOutputType && buttonType!= 2) {
			rend.sprite = outputGraphic [buttonType * 2];
		}
	}

	private void disableBrothers(){
		Transform buttonBar = transform.parent;
		for (int i = 0; i<buttonBar.childCount; i++) {
			Transform brother = buttonBar.GetChild(i);
			if(brother.GetInstanceID() != this.transform.GetInstanceID()){ //no es el mismo objeto que llama al metodo
				Button ent = brother.GetComponent<Button>();
				if(ent != null){ ent.deactivate(); }
			}
		}
	}

	public void deactivate(){
		rend.sprite = buttonGraphic[buttonType*2];
		buttonActive = false;
		if (buttonType == 1) {
			this.transform.parent.GetComponent<ObjectGrid>().hideToMeasurer();
		}
	}

	public void activate(){
		rend.sprite = buttonGraphic[buttonType*2 + 1];
		buttonActive = true;
		alertParent ();
		if (buttonType == 1) {
			this.transform.parent.GetComponent<ObjectGrid>().showToMeasurer();
		}
	}

	private void alertParent(){
		ObjectGrid par = transform.parent.GetComponent<ObjectGrid> ();
		par.changeSelection (buttonType);
	}
}

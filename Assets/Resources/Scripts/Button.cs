using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	public int buttonType = 0; //0 = add object, 1 = measure
	private int numButtons = 2;
	private Sprite[] buttonGraphic;
	private bool buttonActive = false; //starts inactive

	
	private SpriteRenderer rend;
	// Use this for initialization
	void Start () {
		rend = this.GetComponent<SpriteRenderer>();

		buttonGraphic = new Sprite[numButtons * 2];

		//esto quedo desordenado porque hice la spritesheet al lote. lo arreglare luego
		buttonGraphic[0] = Resources.Load<Sprite>("Sprites/button0_0") as Sprite;
		buttonGraphic[1] = Resources.Load<Sprite>("Sprites/button0_1") as Sprite;
		buttonGraphic[2] = Resources.Load<Sprite>("Sprites/button1_0") as Sprite;
		buttonGraphic[3] = Resources.Load<Sprite>("Sprites/button1_1") as Sprite;

		if (buttonType == 0) {
			this.activate();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if (!buttonActive) {
			rend.sprite = buttonGraphic[buttonType*2 + 1];
			buttonActive = true;
			disableBrothers();
			alertParent();
		}
	}

	private void disableBrothers(){
		Transform buttonBar = transform.parent;
		for (int i = 0; i<buttonBar.childCount; i++) {
			Transform brother = buttonBar.GetChild(i);
			if(brother.GetInstanceID() != this.transform.GetInstanceID()){ //no es el mismo objeto que llama al metodo
				brother.GetComponent<Button>().deactivate();
			}
		}
	}

	public void deactivate(){
		rend.sprite = buttonGraphic[buttonType*2];
		buttonActive = false;
	}

	public void activate(){
		rend.sprite = buttonGraphic[buttonType*2 + 1];
		buttonActive = true;
		alertParent ();
	}

	private void alertParent(){
		ObjectGrid par = transform.parent.GetComponent<ObjectGrid> ();
		par.changeSelection (buttonType);
	}
}

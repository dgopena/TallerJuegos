﻿using UnityEngine;
using System.Collections;

public class calculator_button : MonoBehaviour {

    public bool is_calculator;
	public Sprite ButtonUnpressed;
	public Sprite ButtonPressed;
	public bool ButtonActive = false; //starts inactive
	private SpriteRenderer rend;
    public int number;
    public int action_type;// 0 - coma decimal, 1 - suma, 2 - resta, 3 - multiplicación, 4 - división,
                           //5 - borrar, 6 - borrar todo, 7 - resultado, 8 - arriba, 9 - abajo
    public bool is_action;
    private Calculator_function calc;
    private Output opt;

    // Use this for initialization
    void Start () {
		rend = this.GetComponent<SpriteRenderer>();
        rend.sprite = this.ButtonUnpressed;
        if (is_calculator)
        {
            calc = transform.parent.GetComponent<Calculator_function>();
        }
        else
        {
            opt = transform.parent.GetComponent<Output>();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if (!ButtonActive) {
            this.activate();
        }
	}

    void OnMouseUp()
    {
        this.deactivate();
        
    }

    public void deactivate(){
		rend.sprite = this.ButtonUnpressed;
		ButtonActive = false;
	}

	public void activate(){
		rend.sprite = this.ButtonPressed;
        if (is_calculator)
        {
            if (!is_action)
            { //es número
                calc.write_number(number);
            }
            else
            {//es_acción
                if (action_type == 0)
                {
                    calc.write_decimal_point();
                }
                if (action_type >= 1 && action_type <= 4)
                {
                    calc.write_operation(action_type);
                }
                if (action_type == 5)
                {
                    calc.erease();
                }
                if (action_type == 6)
                {
                    calc.clear_operation();
                }
                if (action_type == 7)
                {
                    calc.solve();
                }
                if (action_type == 8)
                {
                    calc.up_history();
                }
                if (action_type == 9)
                {
                    calc.down_history();
                }
            }
        }
        else
        {
            if (!is_action)
            { //es número
                opt.write_number(number);
            }
            else
            {//es_acción
                if (action_type == 0)
                {
                    opt.write_decimal_point();
                }
                if (action_type == 5)
                {
                    opt.erease();
                }
            }
        }
		ButtonActive = true;
		//alertParent ();
	}

}


using UnityEngine;
using System.Collections;
using System;

public class Output : MonoBehaviour {

	private Transform goButton;
	private Transform cancelButton;
	private Transform outPanel;
	private Transform outLabel;

	private bool active;

	public float targetResult = 0;
    public float result = 0;
    public string nt = "";
    //public string right_answer = "";
    //public string wrong_answer = "";

    private int spaces;
    private bool dp;
    private int dn;

    private float[] state_result;
    private bool[] state_dp;
    private int[] state_dn;
    private string[] state_string;

    // Use this for initialization

    public GameObject right_answer;
    public GameObject wrong_answer;
    private Change_Layout ra;
    private Change_Layout wa;

    public GameObject log;
    private Log_writer lg;

    void Start () {
        lg = log.GetComponent<Log_writer>();

        ra = right_answer.transform.GetComponent<Change_Layout>();
        wa = wrong_answer.transform.GetComponent<Change_Layout>();

        goButton = transform.FindChild("Out");
		outLabel = transform.FindChild("outputLabel");
		cancelButton = transform.FindChild("Cancel");
		outPanel = transform.FindChild("Panel");

		active = false;

        outLabel.GetComponent<TextMesh>().text = "";

        spaces = 0;
        dn = 0;
        dp = false;

        state_result = new float[12];
        state_dp = new bool[12];
        state_dn = new int[12];
        state_string = new string[13];

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

                //log guarda accion
                lg.write_event("Se ingreso " + result + " como respuesta. La respuesta correcta es: " + targetResult);
                lg.write_event("----------------------------------------------------------------");
                //Application.LoadLevel(right_answer);
                ra.move();
			}
			else{
				Debug.Log("nope");

                //log guarda accion
                lg.write_event("Se ingreso " + result + " como respuesta. La respuesta correcta es: " + targetResult);
                lg.write_event("----------------------------------------------------------------");
                //Application.LoadLevel(wrong_answer);
                wa.move();
            }
		}

	}

	public void receiveNumbers(string numType){
		if (active) {
            nt = numType;
            outLabel.GetComponent<TextMesh>().text = numType;
		}
	}

    public void write_decimal_point()
    {
        if (spaces <= 13)
        {
                        
            if (!dp)
            {
                spaces++;
                nt = nt + ".";
            }
            dp = true;
            state_string[spaces] = nt;
            state_dp[spaces] = dp;
        }
        outLabel.GetComponent<TextMesh>().text = nt;
    }

    public void erease()
    {
        if (spaces > 0)
        {
            spaces--;

            result = state_result[spaces];
            dp = state_dp[spaces];
            dn = state_dn[spaces];
            nt = state_string[spaces];

            outLabel.GetComponent<TextMesh>().text = nt;
        }
        
    }

    public void write_number(int num)
    {
        if (spaces <= 12)
        {
            if (!dp)
            {
                result = result * 10 + num;
                nt = nt + num.ToString();
                spaces++;
                state_result[spaces] = result;
                state_dp[spaces] = dp;
                state_dn[spaces] = dn;
                state_string[spaces] = nt;
            }
            else
            {
                dn++;
                result = result + num / Mathf.Pow(10, dn);
                nt = nt + num.ToString();
                spaces++;
                state_result[spaces] = result;
                state_dp[spaces] = dp;
                state_dn[spaces] = dn;
                state_string[spaces] = nt;
            }
            outLabel.GetComponent<TextMesh>().text = nt;
        }
    }
}

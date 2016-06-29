using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public TextMesh showcase;
    public bool button = false;
    public int buttonID = 0;

    private Camera cam;
    private float startTextY;
    private bool clicked;
    private float lastY;
    private int clickCount;

	// Use this for initialization
	void Start () {
        if (!button)
        {
            showcase.text = PlayerPrefs.GetString("Log");
        }
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        startTextY = showcase.transform.position.y;
        clicked = false;
        lastY = startTextY;
        clickCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (clicked)
        {
            Vector3 pos = Vector3.zero;
            if ((Application.platform == RuntimePlatform.Android) || (Application.platform == RuntimePlatform.IPhonePlayer))
            {
                Vector2 posi = Input.GetTouch(0).position;
                pos = new Vector3(posi.x, posi.y, 0f);
            }
            else
            {
                pos = Input.mousePosition;
            }
            pos = cam.ScreenToWorldPoint(pos);

            if (clickCount > 0)
            {
                float diffy = pos.y - lastY;
                showcase.transform.position += new Vector3(0f, diffy, 0f);
            }
            lastY = pos.y;
            clickCount++;
        }
	}

    void OnMouseDown()
    {
        if (!button)
        {
            clicked = true;
        }
        else
        {
            if(buttonID == 0)
            {
                //clear log button
                if (clickCount > 0)
                {
                    Log_writer.clearLog();
                    Log_writer.addLine("########### Start of new Log ###########");
                    showcase.text = PlayerPrefs.GetString("Log");
                    transform.FindChild("Label").GetComponent<TextMesh>().text = "Clear Log";
                    clickCount = 0;
                }
                else
                {
                    transform.FindChild("Label").GetComponent<TextMesh>().text = "Seguro?";
                }
                clickCount++;

            }
            else if (buttonID == 1)
            {
                Application.LoadLevel("Menu.2");
            }
            else if(buttonID == 2)
            {
                Application.LoadLevel("Menu");
            }
            else if(buttonID == 3) //setear respuestas
            {
                GameObject[] answers = GameObject.FindGameObjectsWithTag("Finish");
                for(int i=0; i<answers.Length; i++)
                {
                    float res = answers[i].GetComponent<Output>().result;
                    PlayerPrefs.SetFloat(answers[i].name, res);
                }

                Application.LoadLevel("Menu.1");
            }
            else if(buttonID == 4)
            {
                Application.LoadLevel("Menu.1");
            }
            else if (buttonID == 5)
            {
                Application.LoadLevel("Menu.3");
            }
        }
    }

    void OnMouseUp()
    {
        if (!button)
        {
            clicked = false;
            clickCount = 0;
        }
    }
}

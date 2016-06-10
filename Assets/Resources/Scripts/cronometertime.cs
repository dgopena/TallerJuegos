using UnityEngine;
using System.Collections;

public class cronometertime : MonoBehaviour {


	public double time_in_seconds;
	public string time_display = "";
	public int seconds;
	public int min;
	public int sec;
	public bool running = false;

	public bool start_again = true;
	public double start_time;
	public double pause_time;
	public double waiting_time;

	private TextMesh time_display_label_ms;
	private TextMesh time_display_label_s;
	private TextMesh time_display_label_m;
	// Use this for initialization
	void Start () {
		time_display_label_ms = transform.FindChild ("Panel").FindChild ("milisec").GetComponent<TextMesh> ();
		time_display_label_s = transform.FindChild ("Panel").FindChild ("sec").GetComponent<TextMesh> ();
		time_display_label_m = transform.FindChild ("Panel").FindChild ("min").GetComponent<TextMesh> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		time_in_seconds = Time.timeSinceLevelLoad;
		if (running) {
			set_time_display (time_in_seconds);
		}
	
	}

	void set_time_display(double tm){
		tm = tm - start_time - waiting_time;
		string aux = "" + tm;
		if (aux.Length > 10) {
			int pointposition = aux.IndexOf ('.');
			string aux1 = aux.Substring (pointposition + 1, 2);//miliseconds
			string aux2 = "00";//seconds
			string aux3 = "00";//minutes

			seconds = (int)tm;
			min = seconds / 60;
			sec = seconds % 60;

			if (sec <= 9) {
				aux2 = "0" + sec;
			} else {
				aux2 = "" + sec;
			}
			if (min <= 9) {
				aux3 = "0" + min;
			} else {
				aux3 = "" + min;
			}
			time_display = aux3 + ":" + aux2 + ":" + aux1;
			time_display_label_ms.text = aux1;
			time_display_label_s.text = aux2;
			time_display_label_m.text = aux3 + ":";

		}
	}

	void start(){
		running = true;
		if (start_again) {
			start_time = time_in_seconds;
			pause_time = time_in_seconds;
			waiting_time = 0;
		}
		start_again = false;
		waiting_time += time_in_seconds - pause_time;
	}

	void pause(){
		running = false;
		pause_time = time_in_seconds;

	}

	public void toggle_pause_start(){
		if (!running) {
			start ();
		} else {
			pause ();
		}
	}

	public void stop(){
		running = false;
		start_again = true;
		time_display_label_ms.text = "00";
		time_display_label_s.text = "00";
		time_display_label_m.text = "00:";
	}
}

using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using System;

public class Log_writer : MonoBehaviour {

    public string txtdirection = "Test.txt";
    public string time_in_minutes = "";
    public bool continuar_log = false;
    public int wrt = 0;

    private StreamWriter sw;
    public double time_in_seconds = 0;
    public string ua = "";
    public string name_activity = "";

    public bool pc = false;

    //mobile log data
    private int nowID = 0;

	// Use this for initialization
	void Start () {

        if (pc)
        {
            sw = new StreamWriter(txtdirection, continuar_log, Encoding.ASCII);
            write_event("Inicio de la actividad: " + name_activity);
        }
        else
        {
            /*
            int lastID = PlayerPrefs.GetInt("lastID");
            nowID = lastID + 1;
            PlayerPrefs.SetInt("lastID", nowID);
            string log = PlayerPrefs.GetString("Log");
            log += "\n Log #" + nowID;
            PlayerPrefs.SetString("Log", log);
            */
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void write_event(string e)
    {
        ua = convert_time() + " --> " + e;

        sw.WriteLine("");
        sw.WriteLine(ua);
        wrt++;
        sw.Flush();
    }

    private string convert_time()
    {
        time_in_seconds = (double) Time.realtimeSinceStartup;
        time_in_minutes = "";
        int hours = (int) Math.Floor(time_in_seconds / 3600);
        if (hours < 10) { time_in_minutes = time_in_minutes + "0" + hours; }
        else { time_in_minutes = time_in_minutes + hours; }
        int minutes = (int)Math.Floor((time_in_seconds-hours*3600) / 60);
        if (minutes < 10) { time_in_minutes = time_in_minutes + ":0" + minutes; }
        else { time_in_minutes = time_in_minutes + ":" + minutes; }
        int seconds = (int)Math.Floor(time_in_seconds - hours*3600 - minutes*60);
        if (seconds < 10) { time_in_minutes = time_in_minutes + ":0" + seconds; }
        else { time_in_minutes = time_in_minutes + ":" + seconds; }
        return time_in_minutes;
    }

    public static void clearLog() //solo gatillarlo si se pretende borrar todo el log
    {
        PlayerPrefs.SetString("Log", "");
    }

    public static void addLine(string addition)
    {
        string log = PlayerPrefs.GetString("Log");
        log += "\n" + addition;
        PlayerPrefs.SetString("Log", log);
    }

    public static void showLog()
    {
        string log = PlayerPrefs.GetString("Log");
        Debug.Log(log);
    }
}

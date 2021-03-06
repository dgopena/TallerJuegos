﻿using UnityEngine;
using System.Collections;

public class TimeScript : MonoBehaviour {

    public SpriteRenderer secondState;
    public float timeSkip = 10f;
    public int objectLimit = 30;
    public Object item;

    private SpriteRenderer firstState;
    private float ssCooldown = 0f;
    private float ssRate = 1f;

    private float timeCooldown = 0f;
    private int objectCount = 0;

    private bool started = false;

	// Use this for initialization
	void Start () {
        timeCooldown = timeSkip;
        firstState = transform.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (started)
        {
            if (ssCooldown > 0f)
            {
                ssCooldown -= Time.deltaTime;
            }
            else
            {
                secondState.enabled = false;
                firstState.enabled = true;
            }

            if (timeCooldown < 0f && objectCount < objectLimit)
            {
                GameObject ne = (GameObject)Instantiate(item);
                ne.transform.position = transform.position + 1.2f * Vector3.down;
                ne.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                ssCooldown = ssRate;
                secondState.enabled = true;
                firstState.enabled = false;
                timeCooldown = timeSkip;
                objectCount++;
                if(objectCount == objectLimit)
                {
                    Log_writer.addLine("Se llego al limite de objetos en este juego de tiempo. (item 3)");
                }
            }
            else
            {
                timeCooldown -= Time.deltaTime;
            }
        }
        else
        {
            cronometertime tim = GameObject.FindGameObjectWithTag("Timer").GetComponent<cronometertime>();
            if (tim.running) { started = true; }
        }
	}
}

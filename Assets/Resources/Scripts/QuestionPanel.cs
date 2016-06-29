using UnityEngine;
using System.Collections;

public class QuestionPanel : MonoBehaviour {

    public float moveFactor = 6f;
    public float moveRate = 2f;

    private bool open = true;
    private bool changed = false;
    private float startX;
    private float finishX;

	// Use this for initialization
	void Start () {
        startX = transform.position.x;
        finishX = startX - moveFactor;
	}
	
	// Update is called once per frame
	void Update () {
        if (changed)
        {
            if (!open)
            {
                transform.position += moveRate * Vector3.left;

                if (transform.position.x <= startX - moveFactor)
                {
                    changed = false;
                }
            }
            else
            {
                transform.position += moveRate * Vector3.right;

                if (transform.position.x >= finishX + moveFactor)
                {
                    changed = false;
                }
            }
        }
	}

    void OnMouseDown()
    {
        if (open)
        {
            open = false;
        }
        else
        {
            open = true;
        }
        changed = true;
    }
}

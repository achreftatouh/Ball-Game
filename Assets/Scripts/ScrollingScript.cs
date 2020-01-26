using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingScript : MonoBehaviour
{
    public static float scrollSpeed = 0.064f;
    public static float scrollSpeedSlow = 0.044f;
    public float destroyPos = 250;

    public static ScrollingScript scrollSCript;


    
    // Start is called before the first frame update
    void Start()
    {
        scrollSCript = this;
    }

    // Update is called once per frame
    void Update()
    {
        bool alive = BallControl.script.alive;
        if (alive)
        {
            if (!BallControl.isSlow)
            {
                GetComponent<Transform>().Translate(new Vector3(0, 0, -scrollSpeed));
            }
            else
            {
                GetComponent<Transform>().Translate(new Vector3(0, 0, -scrollSpeedSlow));
            }

            if (gameObject.transform.position.z < -destroyPos)
                Destroy(gameObject);
        }

    }
}

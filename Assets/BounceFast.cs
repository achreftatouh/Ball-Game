using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceFast : MonoBehaviour
{
    
    
    private Boolean isCoroutineExecuting;
    private Boolean isResting;

    private Boolean touched;
    private Boolean fastBounce;

    public Boolean isSlow;
    public float time;

    private float oldFall;
    private float oldBounce;



    public float fallSpeed = 200f;
    public float bounceSpeed = 1800f;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (touched)
        {
            oldFall = BallControl.script.fall;
            oldBounce = BallControl.script.bounce;
            
            fastBounce = true;
            StartCoroutine(ExecuteAfterTime(time, () =>
            {
                fastBounce = false;
                BallControl.script.bounce = 800;
                BallControl.script.fall = 30;
            }));
            GetComponent<Collider>().enabled = false;
            touched = false;
        }

        if (fastBounce)
        {
            if (!isSlow)
            {
                BallControl.script.fall= fallSpeed;
                BallControl.script.bounce= bounceSpeed;
            }
            else
            {
                BallControl.script.fall= fallSpeed;
            }
        }


    }
    
    private void OnTriggerEnter(Collider other)
    {
        touched = true;
    }


    IEnumerator ExecuteAfterTime(float time, Action task)
    {
        if (isCoroutineExecuting)
            yield break;
        isCoroutineExecuting = true;
        yield return new WaitForSeconds(time);
        task();
        isCoroutineExecuting = false;
    }

}

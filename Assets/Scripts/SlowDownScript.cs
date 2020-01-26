using System;
using UnityEngine;

public class SlowDownScript : MonoBehaviour
{

    public float time = 5.0f;
    
    private float timer;

    public Boolean isSlow;
    
    
    // Start is called before the first frame update
    void Start()
    {
        ScrollingScript.scrollSpeedSlow = 0.024f;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSlow)
        {
            timer += Time.deltaTime;
            var oldval = GameController.script.offsetZ;
            GameController.script.offsetZ=BallControl.script.scrollSpeedAdd;
            if (timer > time)
            {
                BallControl.isSlow = false;
                isSlow = false;
                timer = 0;
            //    GameController.script.offsetZ=oldval+GameController.script.offsetZ;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        BallControl.isSlow = true;
        isSlow = true;
    }
}

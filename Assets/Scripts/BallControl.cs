using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class BallControl : MonoBehaviour
{
    
    public static BallControl script;
    public float bounce;
    public float fall;
    public float restartDelay = 1f;
    public bool GameHasEnded = false;
    public float speed = 10F;
    Rigidbody rb;

    private Renderer rend;
    public Material[] materials;

    public int bounceCounter;

    public int health;

    public bool alive = true;

    private int score;

    public float scrollSpeedAdd = 0.05f;
    public float fallAdd = 5f;
    
    public int addLevel=120;

    public static Boolean isSlow;

    public GameObject TextScore;
    public GameObject TextHealth;
    
    public GameObject[] powerUps;

    public int addPowerUp = 20;
    private Boolean isCoroutineExecuting;

    public float time=1f;
    
    void Start()
    {
        
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        script = this;
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        health = 3;
        score = 0;
        alive = true;
        
        
        TextHealth.GetComponent<TextMeshPro>().text = health + "";
    }
 
    void FixedUpdate ()
    {
        if(GameHasEnded ==true)
        {
            return;
        }
        var clampPosX=0f;

        if (GameController.script.level < 3)
        {
            clampPosX = 3.0f;
        }
        else
        {
            clampPosX = 3.55f;
        }
        
        var pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, -clampPosX, clampPosX);
        transform.position = pos;

        if (alive)
        {
            rb.AddForce(new Vector3(0, -fall, 0), ForceMode.Force);
        
            Vector3 acc = Input.acceleration;
     //       rb.AddForce(acc.x * speed, 0,0);
            transform.Translate(new Vector3(acc.x * speed, 0,0)*Time.deltaTime);

            if (transform.position.x >= clampPosX )
            {
                rb.velocity = new Vector3(0,rb.velocity.y,rb.velocity.z);
            }
            if (transform.position.x <= -clampPosX )
            {
                rb.velocity = new Vector3(0,rb.velocity.y,rb.velocity.z);
            }
            StartCoroutine(ExecuteAfterTime(time, () =>
            {
                score++;
                TextScore.GetComponent<TextMeshPro>().text = score + "";
            }));
            
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, -1, 0);
        }
        
        
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
    
    
        Debug.Log("col"+collision.transform.gameObject.name);
    
        if (alive)
        {
            rb.velocity = new Vector3(rb.velocity.x, bounce * Time.deltaTime, 0);

            if (collision.transform.tag == "ColorPlane" || collision.transform.tag == "ShortColorPlane" || collision.transform.tag == "NormalPlane")
            {
                    
            }
            
            else if (collision.transform.tag != gameObject.tag)
            {
                health--;
                //           score--;
                Debug.Log("h: " + health);

                if (health < 1)
                {
                    alive = false;
                    Invoke("restart", restartDelay);
                    GameHasEnded = true;
                }
            }
             
             
                //    score++;
                

                TextHealth.GetComponent<TextMeshPro>().text = health + "";
            
            bounceCounter++;
            if (bounceCounter % 4 == 0)
                changeColor();

            if (bounceCounter < 120)
            {
                if (bounceCounter % 10 == 0)
                {
                    ScrollingScript.scrollSpeed = ScrollingScript.scrollSpeed+  scrollSpeedAdd/3;
                    fall = fall + fallAdd/3;

                    GameController.script.offsetZ = GameController.script.offsetZ + scrollSpeedAdd/3;
                }
            }

  /*          if (bounceCounter % addPowerUp == 0)
            {
                
                int i = Random.Range(0, powerUps.Length);
             
                Instantiate(powerUps[i],new Vector3(0,1.6f,transform.position.z+50f),transform.rotation);

            }*/

            if (bounceCounter>addLevel)
            {
                addLevel = addLevel+bounceCounter;
                GameController.script.level++;
            //    Debug.Log("level +");

                if (GameController.script.level == 3)
                {
                    GameController.script.narrow = true;
                }               
            }
        }
    }
    
      
    public void changeColor()
    {

    //    bounceCounter = 0;
        int r = Random.Range(0, 3);
        rend.material = materials[r];
        if (r == 0)
            gameObject.tag = "Red";
        else if (r == 1)
            gameObject.tag = "Green";
        else 
            gameObject.tag = "Blue";
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

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
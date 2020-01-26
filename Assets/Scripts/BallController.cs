using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    Vector3 velocity;
    
    public float speed;
    public float fall;

    private bool start;

    float h, v;
    bool left1, left2, right1, right2;


    private Renderer rend;

   // public Material[] materials;

    public int newScore;

    
    private float current = 0;


    private GameObject Pic;
    private Coroutine r;
    private Coroutine l;
    
    void Awake()
    {
        
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        var pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, -3.0f, 3.0f);
        transform.position = pos;

        rb.AddForce(new Vector3(0, -fall, 0), ForceMode.Force);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 relativeLocation = new Vector3(-3, 0, 0);
            ClickedL(relativeLocation);
          
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 relativeLocation = new Vector3(3, 0, 0);
            ClickedR(relativeLocation);
         
        }
    }

  

    private void OnCollisionEnter(Collision collision)
    {
        string ch1, ch2;
        ch1 = gameObject.tag;
        ch2 = collision.gameObject.tag;
       
          /*  if (ch1.Equals(ch2))
                Controller.instanceController.SubScore(newScore);
            else
            {
                if (!ch2.Equals("blanc"))
                    Controller.instanceController.AddScore(newScore);
            }
*/
            rb.velocity = new Vector3(rb.velocity.x, speed * Time.deltaTime, 0);

       

    }

   /* public void changeColor()
    {
        r = Random.Range(0, 3);
        rend.material = materials[r];
        if (r == 0)
            gameObject.tag = "bleu";
        if (r == 1)
            gameObject.tag = "rouge";
        if (r == 2)
            gameObject.tag = "jaune";
    }*/
    
    
    
    
    void ClickedL(Vector3 relativeLocation)
    {
        if (r!=null)
        {
            
            StopCoroutine(r);
        }
        // Get the target position
        Vector3 targetLocation = gameObject.transform.position + relativeLocation;
        float timeDelta = 0.05f;
 
        // Start your coroutine
        l = StartCoroutine(SmoothMove(targetLocation, timeDelta));
    }
    
    void ClickedR(Vector3 relativeLocation)
    {
        if (l!=null)
        {
            
            StopCoroutine(l);
        }
        Vector3 targetLocation = gameObject.transform.position + relativeLocation;
        float timeDelta = 0.05f;
 
        // Start your coroutine
        r = StartCoroutine(SmoothMove(targetLocation, timeDelta));
        
    }
 
    IEnumerator SmoothMove(Vector3 target, float delta)
    {
        float closeEnough = 0.2f;
        float distance = (gameObject.transform.position - target).magnitude;
 
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
 
        while(distance >= closeEnough)
        {
            // Confirm that it's moving
            Debug.Log("Executing Movement");
 
            transform.position = Vector3.Slerp(gameObject.transform.position, target, delta);
            yield return wait;
 
            distance = (gameObject.transform.position - target).magnitude;
        }
 
        gameObject.transform.position = target;
 
        Debug.Log ("Movement Complete");
    }

}
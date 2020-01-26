using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGlowEffect : MonoBehaviour
{
    public GameObject effect;

    private GameObject go;
    private GameObject goo;

    public float dist = 1.5f;
    
    void Start()
    {
        go = Instantiate(effect,new Vector3(transform.position.x+dist,0,transform.position.z),transform.rotation);
        goo = Instantiate(effect, new Vector3(transform.position.x - dist, 0, transform.position.z), transform.rotation);
        
        var quaternion = go.transform.rotation;
        quaternion.x = go.transform.rotation.x + 90;
        var transformRotation = goo.transform.rotation;
        transformRotation.x = goo.transform.rotation.x + 90; 
        
        
        go.transform.parent=transform;
        goo.transform.parent=transform;


        /*   go.transform.position = new Vector3(go.transform.position.x,go.transform.position.y,0);
           goo.transform.position = new Vector3(goo.transform.position.x,goo.transform.position.y,0);*/


    }
}

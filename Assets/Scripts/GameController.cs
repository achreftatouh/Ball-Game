using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public GameObject[] narrowPlane;
    
    public GameObject[] nPlanes;
    public GameObject[] nShortColoredPlanes;
    public GameObject[] nColoredPlanes;
    
    public GameObject[] bigPlanes;
    public GameObject[] bigShortColoredPlanes;
    public GameObject[] bigColoredPlanes;
    

    public GameObject narrowPlan;
    
   
    public GameObject lastPlane;

    public float posIns= 10;

    public float offsetZ = 0.1f;

    GameObject gameobj;

    public static GameController script;

    public int level = 1;

    public Boolean narrow;
    
    void Start()
    {
        script = this;

    }

    void Update()
    {
        GameObject[] planes=null;
        GameObject[] shortColoredPlanes=null;
        GameObject[] ColoredPlanes=null;

        int choice;

        if (level < 3)
        {
            planes = nPlanes;
            shortColoredPlanes = nShortColoredPlanes;
            ColoredPlanes = nColoredPlanes;
        }
        else
        {
            planes = bigPlanes;
            shortColoredPlanes = bigShortColoredPlanes;
            ColoredPlanes = bigColoredPlanes;
        }


        if (lastPlane.transform.position.z < posIns)
        {

            if (narrow)
            {
                GeneratePlane(narrowPlane);
                narrow = false;
            }
            else
            {
                choice = Random.Range(1, 4);
                switch (choice)
                {
                    case 1:
                        GeneratePlane(planes);
                        break;
                    case 2:
                        GeneratePlane(ColoredPlanes);
                        break;
                    case 3:
                        GeneratePlane(shortColoredPlanes);
                        break;
                }
            }
        }
    }

    void GeneratePlane(GameObject[] objs)
    {
        int i = Random.Range(0, objs.Length);
        float intPoZ = 0f;
        
        gameobj = Instantiate(objs[i],new Vector3(0,0,0),lastPlane.transform.rotation);
  
        float sizeObj = (gameobj.transform.GetChild(0).localScale.x/2) * gameobj.transform.localScale.z;

        float sizeLObj = (lastPlane.transform.GetChild(0).localScale.x / 2) * lastPlane.transform.localScale.z;
        
    //    Debug.Log(" pos: "+sizeLObj );
        
        float posZ = lastPlane.transform.position.z+  sizeLObj + sizeObj - offsetZ;
        
        gameobj.transform.position = new Vector3(0,0,posZ);

        lastPlane = gameobj;

        if (gameobj.tag=="NormalPlane")
        {
            Debug.Log("bormal plane");
            if (BallControl.script.bounceCounter % BallControl.script.addPowerUp == 0)
            {
                
                int num = Random.Range(0, BallControl.script.powerUps.Length);
             
                Instantiate(BallControl.script.powerUps[num],new Vector3(0,1.6f,gameobj.transform.position.z+5f),transform.rotation);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTexture : MonoBehaviour
{

    public float scrollSpeed = 0.08f;
    private Renderer rend;
    
    
    void Start() {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (BallControl.script.alive)
        {
            float offset = Time.time * scrollSpeed;
            rend.material.SetTextureOffset("_MainTex", new Vector2(0, -offset));
        }
    }
}

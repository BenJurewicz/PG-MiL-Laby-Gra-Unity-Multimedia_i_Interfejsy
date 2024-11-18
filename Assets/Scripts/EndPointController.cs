using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointController : MonoBehaviour
{
    // Starr is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("Gameover");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Gameover");
    }
}

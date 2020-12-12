using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    //[SerializeField] private GameObject Player;
    public float TpTo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = new Vector2(collision.transform.position.x,collision.transform.position.y+TpTo) ;

    }
}

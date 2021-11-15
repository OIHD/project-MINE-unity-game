using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisible : MonoBehaviour
{
    private SpriteRenderer gorunmezRender ;
    // Start is called before the first frame update
    void Start()
    {
        gorunmezRender = gameObject.GetComponent<SpriteRenderer>();
        gorunmezRender.enabled = false ;
    }

    public void OnTriggerEnter2D(Collider2D GuzerindekiBLOK) //2D collider bulunan için 
    { 
        switch (GuzerindekiBLOK.tag)
        {
            case "gorunmez":
            gorunmezRender.enabled = true ;
                break;
            default:
                break;
        }  
            
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

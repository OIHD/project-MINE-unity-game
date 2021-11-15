using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    
    // Play Global
    private static music instance = null ;
    public static music Instance 
    {
        get { return instance;}
    }

    void Awake() {
    if (instance != null && instance != this )
    {
        Destroy(this.gameObject);
        return;
    }    
    else
    {
        instance = this ;
    }
    DontDestroyOnLoad(this.gameObject);
    }
    //Play global end

    
    void Start()
    {
                

    
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    

}
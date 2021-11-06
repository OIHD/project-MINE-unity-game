using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzik : MonoBehaviour
{
    
    // Play Global
    private static Muzik instance = null ;
    public static Muzik Instance 
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
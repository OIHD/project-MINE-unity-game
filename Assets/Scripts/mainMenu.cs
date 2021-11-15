using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public AudioSource baslamaSES;
    public void OyunuBaslat ()
    {
        baslamaSES.Play();
        StartCoroutine(oyunubaslat());
    }

        IEnumerator oyunubaslat()
    {
        yield return new WaitForSeconds(1);
        string SonrakiSeviye = "bolum1" ;
        SceneManager.LoadScene(SonrakiSeviye.ToString());
    }

}

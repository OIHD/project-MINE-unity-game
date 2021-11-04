using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KarakterKontrolcusu : MonoBehaviour
{
    private float yurumeHizi = 1f ;
    public Transform gidilecekNokta;
    public LayerMask NeBeniDurduracak;
    public LayerMask NeBeniGebertecek;
    public Animator Karakter;   
    private bool deaktif = false ;
    private SpriteRenderer KarakterRender ;
    private bool KarakterYasiyor ;
    private Scene Degiseceklevel ;
    public Text bombaSayaci ;

    public string bombaSayaciTEXT ;
    public GameObject[] YoketBIR;
    public GameObject[] YoketIKI;
    public GameObject[] YoketUC;
    public SceneManager SonrakiSeviye;
    public int buraKacinciSeviye;

    public void KONSOLAYAZDIR(String BurayaMetinGelecek = "METIN GIRINIZ."){Debug.Log(BurayaMetinGelecek);}

    public void hareketET()
    {
        transform.position = Vector3.MoveTowards(transform.position, gidilecekNokta.position, yurumeHizi * Time.deltaTime);
    }

    public void KarakterRenderSwitch ()
    {
            KarakterRender.enabled = false ;
    }
    void Start()
    {
        KarakterRender = gameObject.GetComponent<SpriteRenderer>();
        gidilecekNokta.parent = null; 
        KarakterYasiyor = true ;
        Degiseceklevel = SceneManager.GetActiveScene();
    }

    public void OnTriggerEnter2D(Collider2D uzerindekiBLOK) //2D collider bulunan için 
    { 
        switch (uzerindekiBLOK.tag)
        {
            case "Finish":
            string SonrakiSeviye = "bolum"+((buraKacinciSeviye+1).ToString()) ;
                    SceneManager.LoadScene(SonrakiSeviye.ToString());
                break;
            case "Gebertici":
                    StartCoroutine(Ogecikme());
                    KarakterYasiyor = false ;
                break;
            case "bombaUyarici":
                    bombaSayaciTEXT = "1";
                    bombaSayaci.text = bombaSayaciTEXT;
                break;
            case "bombaUyarici2":
                    bombaSayaciTEXT = "2";
                    bombaSayaci.text = bombaSayaciTEXT;
                break;
            case "bombaUyarici3":
                    bombaSayaciTEXT = "3";
                    bombaSayaci.text = bombaSayaciTEXT;
                break;
            case "buttonZemin":
            for (var i = 0 ; i < YoketBIR.Length ; i++)
            {
                    Destroy(YoketBIR[i]);
            }
                break;
            case "buttonZemin2":
            for (var i = 0 ; i < YoketIKI.Length ; i++)
            {
                    Destroy(YoketIKI[i]);
            }
                break;
            case "buttonZemin3":
            for (var i = 0 ; i < YoketUC.Length ; i++)
            {
                    Destroy(YoketUC[i]);
            }
                break;
            default:
            bombaSayaciTEXT = "0";
            bombaSayaci.text = bombaSayaciTEXT;
                break;
        }  
    }
    public void OnTriggerExit2D(Collider2D uzerindekiBLOKcikis) //2D collider bulunan için 
    { 
        bombaSayaciTEXT = "0";
        bombaSayaci.text = bombaSayaciTEXT;
        /*
        switch (uzerindekiBLOKcikis.tag)
        {
            case "bombaUyarici":
                    bombaSayaciTEXT = "0";
                    bombaSayaci.text = bombaSayaciTEXT;
                break;
            case "bombaUyarici2":
                    bombaSayaciTEXT = "0";
                    bombaSayaci.text = bombaSayaciTEXT;
                break;
            case "bombaUyarici3":
                    bombaSayaciTEXT = "0";
                    bombaSayaci.text = bombaSayaciTEXT;
                break;
            default:
                break;
        }
        */
    }

 public void KarakterYolCiz ()
    {
            if (Vector3.Distance(transform.position, gidilecekNokta.position) <= .05f)
        {
            if ((Math.Abs(Input.GetAxisRaw("Horizontal")) == 1f ) && !Physics2D.OverlapCircle(gidilecekNokta.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, NeBeniDurduracak))
            {
                    StartCoroutine(Xgecikme()); //HAREKET X
            }
            else if ((Math.Abs(Input.GetAxisRaw("Vertical")) == 1f) && (!Physics2D.OverlapCircle(gidilecekNokta.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, NeBeniDurduracak)))
            {
                    StartCoroutine(Ygecikme()); //HAREKET Y
            }
        }
    }

    private void Update()
    {
        if (KarakterYasiyor == true)
        {
            hareketET();
            KarakterYolCiz();
        }
    }

    IEnumerator Xgecikme()
    {
        if (deaktif == false)
        {
            deaktif = true;
            VEKTORdegistir("Horizontal","Xhareket");
            yield return new WaitForSeconds(1);
            idlehareketinegec ();
            deaktif = false;
        }
    }

    IEnumerator Ygecikme()
    {
        if (deaktif == false)
        {
            deaktif = true;
            VEKTORdegistir("Vertical","Yhareket");
            yield return new WaitForSeconds(1);
            idlehareketinegec ();
            deaktif = false;
        }
    }

    IEnumerator Ogecikme()
    {
           // KONSOLAYAZDIR("GEBERDIN")
            deaktif = true;
            Karakter.SetBool("Geberdi", true);
            idlehareketinegec ();
            yield return new WaitForSeconds(1);
            KarakterRenderSwitch ();
            SceneManager.LoadScene(Degiseceklevel.name);
            deaktif = false;
    }

    public void idlehareketinegec ()
    {
            Karakter.SetFloat("Xhareket", 0);
            Karakter.SetFloat("Yhareket", 0);
    }

    public void VEKTORdegistir (string YonBELIRT , string HareketBELIRT)
    {
            //KONSOLAYAZDIR(HareketBELIRT + "ekseninde hareket edildi");
            switch (YonBELIRT)
            {
                case "Vertical":
            gidilecekNokta.position += new Vector3(0f, Input.GetAxisRaw(YonBELIRT), 0f);
                    break;
                case "Horizontal":
            gidilecekNokta.position += new Vector3(Input.GetAxisRaw(YonBELIRT), 0f, 0f);
                    break;
                default:
                    break;
            }
            Karakter.SetFloat(HareketBELIRT, Input.GetAxisRaw(YonBELIRT));
    }

    public static int distanceCount(int bombDistanceCount){  
        return bombDistanceCount;
    }

}

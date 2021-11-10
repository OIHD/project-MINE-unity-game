using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KarakterKontrolcusu : MonoBehaviour
{
    private float yurumeHizi = 4f ;
    public Transform gidilecekNokta;
    public LayerMask NeBeniDurduracak;
    public LayerMask NeBeniGebertecek;
    public Animator Karakter;   
    public Animator Dron;
    private bool deaktif = false ;
    private SpriteRenderer KarakterRender ;
    private bool KarakterYasiyor ;
    private Scene Degiseceklevel ;
    public Text bombaSayaci ;

    public string bombaSayaciTEXT ;
    public GameObject[] YoketBIR;
    public GameObject[] YoketIKI;
    public GameObject[] YoketUC;
    public GameObject[] UIsayilar;
    public SceneManager SonrakiSeviye;
    public int buraKacinciSeviye;
    public Transform karakterKONUM;
    public int Xyaz;
    public int Yyaz;
    public AudioSource yurumeSES;
    public AudioSource olmeSES;
    //public AudioSource KazanmaSES;


    public void KONSOLAYAZDIR(String BurayaMetinGelecek = "METIN GIRINIZ."){Debug.Log(BurayaMetinGelecek);}

    public void hareketET()
    {
        transform.position = Vector3.MoveTowards(transform.position, gidilecekNokta.position, yurumeHizi * Time.deltaTime);
    }

    public void DokunmaALGILA()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 DokunmaYERI = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(DokunmaYERI.x >= 1 && Math.Abs(DokunmaYERI.x) > Math.Abs(DokunmaYERI.y))
            {
                Xyaz = 1 ;
                Yyaz = 0 ;
                
            }
            else if (DokunmaYERI.x <= -1 && Math.Abs(DokunmaYERI.x) > Math.Abs(DokunmaYERI.y))
            {
                Xyaz = -1 ;
                Yyaz = 0 ;
            }
            else if (DokunmaYERI.y >= 1 && Math.Abs(DokunmaYERI.y) > Math.Abs(DokunmaYERI.x))
            {
                Xyaz = 0 ;
                Yyaz = 1 ;
            }
            else if (DokunmaYERI.y <= -1 && Math.Abs(DokunmaYERI.y) > Math.Abs(DokunmaYERI.x))
            {
                Xyaz = 0 ;
                Yyaz = -1 ;
            }
        }
    }

    public void KarakterRenderSwitch ()
    {
            KarakterRender.enabled = false ;
    }
    void Start()
    {
        Xyaz = 0 ;
        Yyaz = 0 ;
        KarakterRender = gameObject.GetComponent<SpriteRenderer>();
        gidilecekNokta.parent = null; 
        KarakterYasiyor = true ;
        Degiseceklevel = SceneManager.GetActiveScene();
        UITemizle(0);
    }

    void YurumeSESoynat()
    {
        yurumeSES.Play();
    }

    public void OnTriggerEnter2D(Collider2D uzerindekiBLOK) //2D collider bulunan için 
    { 
        switch (uzerindekiBLOK.tag)
        {
            case "Finish":
            //KazanmaSES.Play();
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
                    Dron.SetBool("dronr", true);
                    UITemizle(1);
                break;
            case "bombaUyarici2":
                    bombaSayaciTEXT = "2";
                    bombaSayaci.text = bombaSayaciTEXT;
                    Dron.SetBool("dronr", true);
                    UITemizle(2);
                break;
            case "bombaUyarici3":
                    bombaSayaciTEXT = "3";
                    bombaSayaci.text = bombaSayaciTEXT;
                    Dron.SetBool("dronr", true);
                    UITemizle(3);
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
            Dron.SetBool("dronr", false);
            UITemizle(0);
                break;
        }  
    }
    public void OnTriggerExit2D(Collider2D uzerindekiBLOKcikis) //2D collider bulunan için 
    { 
        bombaSayaciTEXT = "0";
        bombaSayaci.text = bombaSayaciTEXT;
        UITemizle(0);
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

     public void KarakterYolCizMOBIL ()
    {
            if (Vector3.Distance(transform.position, gidilecekNokta.position) <= .05f)
        {
            if ((Math.Abs(Xyaz) == 1f ) && !Physics2D.OverlapCircle(gidilecekNokta.position + new Vector3(Xyaz, 0f, 0f), .2f, NeBeniDurduracak))
            {
                    StartCoroutine(MXgecikme()); //HAREKET X
            }
            else if ((Math.Abs(Yyaz) == 1f) && (!Physics2D.OverlapCircle(gidilecekNokta.position + new Vector3(0f, Yyaz, 0f), .2f, NeBeniDurduracak)))
            {
                    StartCoroutine(MYgecikme()); //HAREKET Y
            }
        }
    }

    public void UITemizle(int i)
    {
    UIsayilar[0].SetActive(false);
    UIsayilar[1].SetActive(false);
    UIsayilar[2].SetActive(false);
    UIsayilar[3].SetActive(false);
    UIsayilar[i].SetActive(true);
    }

    private void Update()
    {

        if (KarakterYasiyor == true)
        {
            DokunmaALGILA();
            hareketET();
            KarakterYolCizMOBIL();
            KarakterYolCiz();
        }
    }

    IEnumerator Xgecikme()
    {
        if (deaktif == false)
        {
            deaktif = true;
            VEKTORdegistir("Horizontal","Xhareket");
            yield return new WaitForSeconds(0.25f);
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
            yield return new WaitForSeconds(0.25f);
            idlehareketinegec ();
            deaktif = false;
        }
    }

        IEnumerator MXgecikme()
    {
        if (deaktif == false)
        {
            deaktif = true;
            MVEKTORdegistir("Horizontal","Xhareket");
            yield return new WaitForSeconds(0.25f);
            idlehareketinegec ();
            deaktif = false;
        }
    }

    IEnumerator MYgecikme()
    {
        if (deaktif == false)
        {
            deaktif = true;
            MVEKTORdegistir("Vertical","Yhareket");
            yield return new WaitForSeconds(0.25f);
            idlehareketinegec ();
            deaktif = false;
        }
    }

    IEnumerator Ogecikme()
    {
           // KONSOLAYAZDIR("GEBERDIN")
            deaktif = true;
            olmeSES.Play();
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
            Xyaz = 0;
            Yyaz = 0;
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

    public void MVEKTORdegistir (string YonBELIRT , string HareketBELIRT)
    {
        int geciciyon = 0 ; 
        
            //KONSOLAYAZDIR(HareketBELIRT + "ekseninde hareket edildi");
            switch (YonBELIRT)
            {
                case "Vertical":
            geciciyon = Yyaz;
            gidilecekNokta.position += new Vector3(0f, Yyaz, 0f);
                    break;
                case "Horizontal":
            geciciyon = Xyaz ;
            gidilecekNokta.position += new Vector3(Xyaz, 0f, 0f);
                    break;
                default:
                    break;
            }
            Karakter.SetFloat(HareketBELIRT, geciciyon);
    }

    public static int distanceCount(int bombDistanceCount){  
        return bombDistanceCount;
    }

}

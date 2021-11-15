
/*

    MERHABA , Ben OIHD ( Göktuğ )

    Proje dosyamızı indirdiğin için teşekkürler. 
    Eğer türkçe kodlar üzerinden ilerlemek istiyorsan ;
    game.cs dosyasını silip game-tr.cs nin ismini game.cs olarak değiştir ve game.cs nin önceki olduğu konuma at . .


*/


using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro ; 

public class KarakterKontrolcusu : MonoBehaviour
{
    public int buraKacinciSeviye , MetinINDEX,Xyaz,Yyaz,bombaADET,bombaHACKoncesi;
    public int AlternatifBolumSAYISI = 1 ;
    private float yurumeHizi = 4f ;
    public float MetinYAZMAHIZI;
    public LayerMask NeBeniDurduracak,NeBeniGebertecek;
    public Animator Karakter,Dron;
    private bool deaktif = false;
    private SpriteRenderer KarakterRender ;
    public bool KarakterYasiyor ,bombaHACK;
    public Scene Degiseceklevel ;
    public Text bombaSayaci ;

    public string bombaSayaciTEXT ;
    public GameObject[] YoketBIR,YoketIKI,YoketUC,UIsayilar,YOKEDILEN;
    public SceneManager SonrakiSeviye;

    public Transform karakterKONUM,gidilecekNokta;
    public AudioSource yurumeSES,olmeSES;
    public TextMeshProUGUI Metin ;
    public string[] satirlar ;
    public SpriteRenderer[] OgreticiRENDER;

    public void KONSOLAYAZDIR(String BurayaMetinGelecek = "METIN GIRINIZ."){Debug.Log(BurayaMetinGelecek);}

    public void hareketET()
    {
        transform.position = Vector3.MoveTowards(transform.position, gidilecekNokta.position, yurumeHizi * Time.deltaTime);
    }

    public void DiyalogBASLAT()
    {
        MetinINDEX = 0;
        StartCoroutine(MetinYAZ());

    }

    IEnumerator MetinYAZ()
    {
        foreach (char harf in satirlar[MetinINDEX].ToCharArray())
        {
            Metin.text += harf ;
            yield return new WaitForSeconds(MetinYAZMAHIZI);
        }
                        KarakterYasiyor = true ;
                        for (var i = 0; i < YOKEDILEN.Length ; i++)
                        {
                            if (YOKEDILEN[i] != null)
                            {
                                OgreticiRENDER[i].enabled = false;
                            }
                        }

    }

    public void DokunmaALGILA()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 DokunmaYERI = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(DokunmaYERI.x >= 1 && Math.Abs(DokunmaYERI.x) > Math.Abs(DokunmaYERI.y))
            {
                clearIDLE();
                Xyaz = 1 ;
                Yyaz = 0 ;
                
            }
            else if (DokunmaYERI.x <= -1 && Math.Abs(DokunmaYERI.x) > Math.Abs(DokunmaYERI.y))
            {
                clearIDLE();
                Xyaz = -1 ;
                Yyaz = 0 ;
            }
            else if (DokunmaYERI.y >= 1 && Math.Abs(DokunmaYERI.y) > Math.Abs(DokunmaYERI.x))
            {
                clearIDLE();
                Xyaz = 0 ;
                Yyaz = 1 ;
            }
            else if (DokunmaYERI.y <= -1 && Math.Abs(DokunmaYERI.y) > Math.Abs(DokunmaYERI.x))
            {
                clearIDLE();
                Xyaz = 0 ;
                Yyaz = -1 ;
            }
        }
    }

        public void clearIDLE ()
    {
        Karakter.SetBool("IdUP", false);
        Karakter.SetBool("IdDOWN", false);
        Karakter.SetBool("IdLEFT", false);
        Karakter.SetBool("IdRIGHT", false);
    }

    public void KarakterRenderSwitch ()
    {
            KarakterRender.enabled = false ;
    }
    void Start()
    {
        OgreticiRENDER[0].enabled = false;
        OgreticiRENDER[1].enabled = false;
        Metin.text = string.Empty;
        bombaADET = 0 ;
        Xyaz = 0 ;
        Yyaz = 0 ;
        KarakterRender = gameObject.GetComponent<SpriteRenderer>();
        gidilecekNokta.parent = null; 
        KarakterYasiyor = true ;
        Degiseceklevel = SceneManager.GetActiveScene();
        UITemizle(0);
        UIsayilar[9].SetActive(false);
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
            case "DEAD":
                    StartCoroutine(Ogecikme());
                    KarakterYasiyor = false ;
                break;
            case "bombWARN":
            if (bombaHACK == false)
            {
                bombaADET = bombaADET + 1 ;
                bombaSayaciTEXT = Convert.ToString(bombaADET) ;
                bombaSayaci.text = bombaSayaciTEXT;
                    if (bombaADET == 0)
                    {
                        Dron.SetBool("dronr", false);
                    }
                    else if (bombaADET != 0)
                    {
                        Dron.SetBool("dronr", true);
                    }

                    if (bombaHACK == false)
                    {
                    UITemizle(bombaADET);
                    }
            }
                    else if (bombaHACK == true)
            {
                bombaADET = bombaADET + 1 ;
            }
                break;
            case "bombHACK":
                bombaHACK = true ;
                bombaHACKoncesi = bombaADET ;
                UIsayilar[0].SetActive(false);
                UIsayilar[1].SetActive(false);
                UIsayilar[2].SetActive(false);
                UIsayilar[3].SetActive(false);
                UIsayilar[9].SetActive(true);
                break;
            case "button":
            for (var i = 0 ; i < YoketBIR.Length ; i++)
            {
                    Destroy(YoketBIR[i]);
            }
                break;
            case "button2":
            for (var i = 0 ; i < YoketIKI.Length ; i++)
            {
                    Destroy(YoketIKI[i]);
            }
                break;
            case "button3":
            for (var i = 0 ; i < YoketUC.Length ; i++)
            {
                    Destroy(YoketUC[i]);
            }
                break;
            case "TUTORIAL":
                KarakterYasiyor = false ;
                OgreticiRENDER[0].enabled = true;
                OgreticiRENDER[1].enabled = true;
                DiyalogBASLAT();
                break;
            case "TUTORIALRESET":
                Metin.text = string.Empty;
                break;
            default:
                break;
        }  
    }
    public void OnTriggerExit2D(Collider2D uzerindekiBLOKcikis) //2D collider bulunan için 
    { 
        switch (uzerindekiBLOKcikis.tag)
        {
            case "bombWARN":
            if (bombaHACK == false)
            {
                bombaADET = bombaADET - 1 ;
            bombaSayaciTEXT = Convert.ToString(bombaADET) ;
            bombaSayaci.text = bombaSayaciTEXT;
                if (bombaADET == 0)
                {
                    Dron.SetBool("dronr", false);
                }
                else if (bombaADET != 0)
                {
                    Dron.SetBool("dronr", true);
                }
            UITemizle(bombaADET);
            }
            else if (bombaHACK == true)
            {
                bombaADET = bombaADET - 1;
            }
            break;
            case "bombHACK":
                bombaHACK = false ;
                UIsayilar[9].SetActive(false);
                bombaSayaciTEXT = Convert.ToString(bombaADET) ;
                bombaSayaci.text = bombaSayaciTEXT;
                UITemizle(bombaADET);
                if (bombaADET == 0)
                {
                    Dron.SetBool("dronr", false);
                }
                else if (bombaADET != 0)
                {
                    Dron.SetBool("dronr", true);
                }
                break;
            default:
            break;
        }

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
            VEKTORdegistir("Horizontal","Xsize");
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
            VEKTORdegistir("Vertical","Ysize");
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
            MVEKTORdegistir("Horizontal","Xsize");
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
            MVEKTORdegistir("Vertical","Ysize");
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
            Karakter.SetBool("cdead", true);
            idlehareketinegec ();
            yield return new WaitForSeconds(1);
            KarakterRenderSwitch ();
            int raskeleBolumNumarasi = UnityEngine.Random.Range(1,AlternatifBolumSAYISI+1);
            string SonrakiSeviye = "bolum"+((buraKacinciSeviye).ToString())+"-"+((raskeleBolumNumarasi).ToString()) ;
            SceneManager.LoadScene(SonrakiSeviye.ToString());
            deaktif = false;
    }

    public void idlehareketinegec ()
    {
        switch (Karakter.GetFloat("Xsize"))
        {
            case 1:
            Karakter.SetBool("IdRIGHT", true);
        break;
            case -1:
            Karakter.SetBool("IdLEFT", true);
        break;
            default:
            break;
        }
                switch (Karakter.GetFloat("Ysize"))
        {
            case 1:
            Karakter.SetBool("IdUP", true);
        break;
            case -1:
            Karakter.SetBool("IdDOWN", true);
        break;
            default:
            break;
        }
            Karakter.SetFloat("Xsize", 0);
            Karakter.SetFloat("Ysize", 0);
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

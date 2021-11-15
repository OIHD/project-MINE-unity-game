using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro ; 

public class game : MonoBehaviour
{
    public int whichLevel , textINDEX,Xset,Yset,bombTOTAL,bombHACKearly,AlternativeLEVELS ;
    private float walkSPEED = 4f ;
    public float textWriteSPEED;
    public LayerMask whatStopsMe,whatKillsMe;
    public Animator gameCharacter,drone;
    private bool deactive = false;
    private SpriteRenderer characterRender ;
    public bool ischaracterDEAD ,bombHACK;
    public Scene levelActive ;
    public Text bombCalculator ;

    public string bombCalculatorTEXT ;
    public GameObject[] destroyONE,destroyTWO,destroyTHREE,UInumbers,destroyNEXT;
    public SceneManager nextLevel;

    public Transform characterTRANSFORM,targetPoint;
    public AudioSource walkSOUND,deadSOUND;
    public TextMeshProUGUI chatText ;
    public string[] chatLines ;
    public SpriteRenderer[] tutorialRENDER;


    public void startMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, walkSPEED * Time.deltaTime);
    }

    public void startChat()
    {
        textINDEX = 0;
        StartCoroutine(chatTextWRITE());

    }

    IEnumerator chatTextWRITE()
    {
        foreach (char harf in chatLines[textINDEX].ToCharArray())
        {
            chatText.text += harf ;
            yield return new WaitForSeconds(textWriteSPEED);
        }
                        ischaracterDEAD = true ;
                        for (var i = 0; i < destroyNEXT.Length ; i++)
                        {
                            if (destroyNEXT[i] != null)
                            {
                                tutorialRENDER[i].enabled = false;
                            }
                        }

    }

    public void touchM()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(touchPoint.x >= 1 && Math.Abs(touchPoint.x) > Math.Abs(touchPoint.y))
            {
                clearIDLE();
                Xset = 1 ;
                Yset = 0 ;
            }
            else if (touchPoint.x <= -1 && Math.Abs(touchPoint.x) > Math.Abs(touchPoint.y))
            {
                clearIDLE();
                Xset = -1 ;
                Yset = 0 ;
            }
            else if (touchPoint.y >= 1 && Math.Abs(touchPoint.y) > Math.Abs(touchPoint.x))
            {
                clearIDLE();
                Xset = 0 ;
                Yset = 1 ;
            }
            else if (touchPoint.y <= -1 && Math.Abs(touchPoint.y) > Math.Abs(touchPoint.x))
            {
                clearIDLE();
                Xset = 0 ;
                Yset = -1 ;
            }
        }
    }

    public void clearIDLE ()
    {
        gameCharacter.SetBool("IdUP", false);
        gameCharacter.SetBool("IdDOWN", false);
        gameCharacter.SetBool("IdLEFT", false);
        gameCharacter.SetBool("IdRIGHT", false);
    }
    public void characterRenderSwitch ()
    {
            characterRender.enabled = false ;
    }
    void Start()
    {
        tutorialRENDER[0].enabled = false;
        tutorialRENDER[1].enabled = false;
        chatText.text = string.Empty;
        bombTOTAL = 0 ;
        Xset = 0 ;
        Yset = 0 ;
        characterRender = gameObject.GetComponent<SpriteRenderer>();
        targetPoint.parent = null; 
        ischaracterDEAD = true ;
        levelActive = SceneManager.GetActiveScene();
        UIclean(0);
        UInumbers[9].SetActive(false);
    }

    void walkSOUNDstart()
    {
        walkSOUND.Play();
    }

    public void OnTriggerEnter2D(Collider2D enterCol)
    { 
        switch (enterCol.tag)
        {
            case "Finish":
            string nextLevel = "bolum"+((whichLevel+1).ToString()) ;
                    SceneManager.LoadScene(nextLevel.ToString());
                break;
            case "DEAD":
                    StartCoroutine(Odead());
                    ischaracterDEAD = false ;
                break;
            case "bombWARN":
            if (bombHACK == false)
            {
                bombTOTAL = bombTOTAL + 1 ;
                bombCalculatorTEXT = Convert.ToString(bombTOTAL) ;
                bombCalculator.text = bombCalculatorTEXT;
                    if (bombTOTAL == 0)
                    {
                        drone.SetBool("dronr", false);
                    }
                    else if (bombTOTAL != 0)
                    {
                        drone.SetBool("dronr", true);
                    }

                    if (bombHACK == false)
                    {
                    UIclean(bombTOTAL);
                    }
            }
                    else if (bombHACK == true)
            {
                bombTOTAL = bombTOTAL + 1 ;
            }
                break;
            case "bombHACK":
                bombHACK = true ;
                bombHACKearly = bombTOTAL ;
                UInumbers[0].SetActive(false);
                UInumbers[1].SetActive(false);
                UInumbers[2].SetActive(false);
                UInumbers[3].SetActive(false);
                UInumbers[9].SetActive(true);
                break;
            case "button":
            for (var i = 0 ; i < destroyONE.Length ; i++)
            {
                    Destroy(destroyONE[i]);
            }
                break;
            case "button2":
            for (var i = 0 ; i < destroyTWO.Length ; i++)
            {
                    Destroy(destroyTWO[i]);
            }
                break;
            case "button3":
            for (var i = 0 ; i < destroyTHREE.Length ; i++)
            {
                    Destroy(destroyTHREE[i]);
            }
                break;
            case "TUTORIAL":
                ischaracterDEAD = false ;
                tutorialRENDER[0].enabled = true;
                tutorialRENDER[1].enabled = true;
                startChat();
                break;
            case "TUTORIALRESET":
                chatText.text = string.Empty;
                break;
            default:
                break;
        }  
    }
    public void OnTriggerExit2D(Collider2D extCol) 
    { 
        switch (extCol.tag)
        {
            case "bombWARN":
            if (bombHACK == false)
            {
                bombTOTAL = bombTOTAL - 1 ;
            bombCalculatorTEXT = Convert.ToString(bombTOTAL) ;
            bombCalculator.text = bombCalculatorTEXT;
                if (bombTOTAL == 0)
                {
                    drone.SetBool("dronr", false);
                }
                else if (bombTOTAL != 0)
                {
                    drone.SetBool("dronr", true);
                }
            UIclean(bombTOTAL);
            }
            else if (bombHACK == true)
            {
                bombTOTAL = bombTOTAL - 1;
            }
            break;
            case "bombHACK":
                bombHACK = false ;
                UInumbers[9].SetActive(false);
                bombCalculatorTEXT = Convert.ToString(bombTOTAL) ;
                bombCalculator.text = bombCalculatorTEXT;
                UIclean(bombTOTAL);
                if (bombTOTAL == 0)
                {
                    drone.SetBool("dronr", false);
                }
                else if (bombTOTAL != 0)
                {
                    drone.SetBool("dronr", true);
                }
                break;
            default:
            break;
        }

    }

public void characterWalkLine ()
    {
            if (Vector3.Distance(transform.position, targetPoint.position) <= .05f)
        {
            if ((Math.Abs(Input.GetAxisRaw("Horizontal")) == 1f ) && !Physics2D.OverlapCircle(targetPoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMe))
            {
                clearIDLE();
                    StartCoroutine(Xaxis()); // X
            }
            else if ((Math.Abs(Input.GetAxisRaw("Vertical")) == 1f) && (!Physics2D.OverlapCircle(targetPoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMe)))
            {
                clearIDLE();
                    StartCoroutine(Yaxis()); // Y
            }
        }
    }

    public void characterWalkLineMOBILE ()
    {
            if (Vector3.Distance(transform.position, targetPoint.position) <= .05f)
        {
            if ((Math.Abs(Xset) == 1f ) && !Physics2D.OverlapCircle(targetPoint.position + new Vector3(Xset, 0f, 0f), .2f, whatStopsMe))
            {
                    StartCoroutine(MXaxis()); // X
            }
            else if ((Math.Abs(Yset) == 1f) && (!Physics2D.OverlapCircle(targetPoint.position + new Vector3(0f, Yset, 0f), .2f, whatStopsMe)))
            {
                    StartCoroutine(MYaxis()); // Y
            }
        }
    }

    public void UIclean(int i)
    {
    UInumbers[0].SetActive(false);
    UInumbers[1].SetActive(false);
    UInumbers[2].SetActive(false);
    UInumbers[3].SetActive(false);
    UInumbers[i].SetActive(true);
    }

    private void Update()
    {

        if (ischaracterDEAD == true)
        {
            touchM();
            startMove();
            characterWalkLineMOBILE();
            characterWalkLine();
        }
    }

    IEnumerator Xaxis()
    {
        if (deactive == false)
        {
            deactive = true;
            changeL("Horizontal","Xsize");
            yield return new WaitForSeconds(0.25f);
            setIdle ();
            deactive = false;
        }
    }

    IEnumerator Yaxis()
    {
        if (deactive == false)
        {
            deactive = true;
            changeL("Vertical","Ysize");
            yield return new WaitForSeconds(0.25f);
            setIdle ();
            deactive = false;
        }
    }

        IEnumerator MXaxis()
    {
        if (deactive == false)
        {
            deactive = true;
            MchangeL("Horizontal","Xsize");
            yield return new WaitForSeconds(0.25f);
            setIdle ();
            deactive = false;
        }
    }

    IEnumerator MYaxis()
    {
        if (deactive == false)
        {
            deactive = true;
            MchangeL("Vertical","Ysize");
            yield return new WaitForSeconds(0.25f);
            setIdle ();
            deactive = false;
        }
    }

    IEnumerator Odead()
    {
            deactive = true;
            deadSOUND.Play();
            gameCharacter.SetBool("cdead", true);
            setIdle ();
            yield return new WaitForSeconds(1);
            characterRenderSwitch ();
            if (AlternativeLEVELS == 0)
            {
            int randomLevelNumber = UnityEngine.Random.Range(1,AlternativeLEVELS+1);
            string nextLevel = "bolum"+((whichLevel).ToString()) ;
            SceneManager.LoadScene(nextLevel.ToString());
            deactive = false;
            }
            else
            {
            int randomLevelNumber = UnityEngine.Random.Range(1,AlternativeLEVELS+1);
            string nextLevel = "bolum"+((whichLevel).ToString())+"-"+((randomLevelNumber).ToString()) ;
            SceneManager.LoadScene(nextLevel.ToString());
            deactive = false;
            }
    }

    public void setIdle ()
    {
        switch (gameCharacter.GetFloat("Xsize"))
        {
            case 1:
            gameCharacter.SetBool("IdRIGHT", true);
        break;
            case -1:
            gameCharacter.SetBool("IdLEFT", true);
        break;
            default:
            break;
        }
                switch (gameCharacter.GetFloat("Ysize"))
        {
            case 1:
            gameCharacter.SetBool("IdUP", true);
        break;
            case -1:
            gameCharacter.SetBool("IdDOWN", true);
        break;
            default:
            break;
        }
            gameCharacter.SetFloat("Xsize", 0);
            gameCharacter.SetFloat("Ysize", 0);
            Xset = 0;
            Yset = 0;
    }

    public void changeL (string horizontalOrVertical , string XorYsize)
    {
            switch (horizontalOrVertical)
            {
                case "Vertical":
            targetPoint.position += new Vector3(0f, Input.GetAxisRaw(horizontalOrVertical), 0f);
                    break;
                case "Horizontal":
            targetPoint.position += new Vector3(Input.GetAxisRaw(horizontalOrVertical), 0f, 0f);
                    break;
                default:
                    break;
            }
            gameCharacter.SetFloat(XorYsize, Input.GetAxisRaw(horizontalOrVertical));
    }

    public void MchangeL (string horizontalOrVertical , string XorYsize)
    {
        int geciciyon = 0 ; 
            switch (horizontalOrVertical)
            {
                case "Vertical":
            geciciyon = Yset;
            targetPoint.position += new Vector3(0f, Yset, 0f);
                    break;
                case "Horizontal":
            geciciyon = Xset ;
            targetPoint.position += new Vector3(Xset, 0f, 0f);
                    break;
                default:
                    break;
            }
            gameCharacter.SetFloat(XorYsize, geciciyon);
    }

    public static int distanceCount(int bombDistanceCount){  
        return bombDistanceCount;
    }

}

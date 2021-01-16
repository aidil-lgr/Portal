using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// ----------------------------------------------------------------------------------
/// DESCRIPCIÓ
///         Script utilitzat per tot el que té a veure amb l'sprite del protagonista de joc, el fantasma. Controla tots els seus colliders, el seu moviment,
///         les vides, totes les accions que fa (com ara agafar pickups)... També, entre altres coses, controla els canvas de guanyar, de game over i de fade
///         out i moviment entre escenes del joc i nivells. 
///AUTOR:  Lídia García Romero
/// DATA:   06/01/2021
/// VERSIÓ: 1.0
/// CONTROL DE VERSIONS
///         1.0: primera versió.
/// ----------------------------------------------------------------------------------
/// </summary>

public class ScrPlayer : MonoBehaviour
{
    Rigidbody2D fantasma;
    float x, y;
    Vector3 posAnterior, direccio;
    float angle;
    public static int vides = 2; //inicialment el jugador tindrà 2/3 vides
    public static bool onPuertaMansion = false;
    public static bool onPuertaBosque = false;
    public static bool onPortal = false;
    static bool onCollider = false;
    static bool entrant = false;

    [SerializeField]
    float force = 1;

    [SerializeField] GameObject negreCanvas, overCanvas, winnerCanvas;
    [SerializeField] Image negreOut;
    [SerializeField] Text finalText;

    void Start()
    {
        fantasma = GetComponent<Rigidbody2D>();
        posAnterior = transform.position;

        //Preparem la imatge per fer el fade out de l'escena.....................................
        negreOut.color = Color.clear;
        negreOut.enabled = false;
        //.......................................................................................
    }

    void Update()
    {
        //Rebre imput perque el player es mogui..................................................
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        //.......................................................................................

        //Fer que el fantasma "miri" cap a la seva direcció de moviment..........................
        direccio = transform.position - posAnterior;

        if (direccio != Vector3.zero && !onCollider)
        {
            angle = Mathf.Atan2(direccio.y, direccio.x) * Mathf.Rad2Deg;
            posAnterior = transform.position;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        //.......................................................................................

        //Si el fantasma es queda sense vida, game over..........................................
        if (vides == 0)
        {
            GameOver();
        }
        //.......................................................................................

        //Si el fantasma arriba al portal amb tot lo necessari, winner i acaba el joc............
        if (onPortal && ScrControlGame.esperits == 3)
        {
            Winner();
        }
        //.......................................................................................
    }

    private void FixedUpdate()
    {
        //Fer que el fantasma es mogui amb físiques..............................................
        fantasma.AddForce(new Vector2(x * force, y * force));
        //.......................................................................................
    }

    private void OnTriggerEnter2D(Collider2D collision) //gestiona tots els triggers de l'escena
    {
        //Primer ens encarreguem del so dels triggers que trobi................................. 
        ReproduirSons(collision);
        //.......................................................................................

        //Fer que, quan el fantasma toqui la llum, es resti una vida.............................
        RestarVida(collision);
        //.......................................................................................

        //Fer que, quan el fantasma toqui la clau, desaparegui i la "guardi".....................
        AgafarClau(collision);
        //.......................................................................................

        //Fer que, quan el fantasma arribi a una porta, comprovi si té una clau i entri..........
        Entrar(collision);
        //.......................................................................................

        //Fer que, quan el fantasma agafi una poció, se li sumi una vida.........................
        SumarVida(collision);
        //.......................................................................................

        //Fer que el fantasma agafi un esperit i se'l "guardi....................................
        AgafarEsperit(collision);
        //.......................................................................................

        //Fer que el fantasma agafi diamants i sumi punts........................................
        AgafarDiamant(collision);
        //.......................................................................................

        //Fer que el fantasma accioni interruptors i obri les portes del nivell 3................
        AccionarInterruptor(collision);
        //.......................................................................................
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        fantasma.constraints = RigidbodyConstraints2D.FreezeRotation;
        onCollider = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        fantasma.constraints = RigidbodyConstraints2D.None;
        onCollider = false;
    }

    private void OnTriggerStay2D(Collider2D collision) //per poder entrar a les portes
    {
        if (collision.tag == "puertaMansion" && ScrControlGame.llaveMansion && ScrControlGame.esperits == 1 && Input.GetKeyDown(KeyCode.E))
        {
            onPuertaMansion = false;
            entrant = true;
            
        }

        if (collision.tag == "puertaBosque" && ScrControlGame.llaveBosque && ScrControlGame.esperits == 2 && Input.GetKeyDown(KeyCode.E))
        {
            onPuertaBosque = false;
            entrant = true;
        }

        fadeOut(collision.tag);
    }

    private void OnTriggerExit2D(Collider2D collision) //Per resetejar els valors quan surti dels triggers de les portes
    {
        if (collision.tag == "puertaMansion")
        {
            onPuertaMansion = false;
        }

        if (collision.tag == "puertaBosque")
        {
            onPuertaBosque = false;
        }

        if (collision.tag == "portal")
        {
            onPortal = false;
        }
    }

    void RestarVida(Collider2D collision)
    {
        if (collision.tag == "luz" && vides >= 0)
        {
            vides--;
        }

        if (collision.tag == "luz_int" && vides >= 0)
        {
            SpriteRenderer llum;
            llum = collision.GetComponent<SpriteRenderer>();

            if (llum.color == new Color(1f, 1f, 1f, 1f)) //que només resti vida si la llum està encesa
            {
                vides--;
            }
        }

        if (vides == 0)
        {
            //Fer sonar la música de game over.......................................................
            overCanvas.SetActive(true);
            AudioSource soGameOver = overCanvas.GetComponent<AudioSource>();
            soGameOver.Play();
            //.......................................................................................
        }
    }

    void AgafarClau(Collider2D collision)
    {
        if (collision.tag == "llaveMansion")
        {
            ScrControlGame.llaveMansion = true;
            Destroy(collision.gameObject);
        }

        if (collision.tag == "llaveBosque")
        {
            ScrControlGame.llaveBosque = true;
            Destroy(collision.gameObject);
        }
    }

    void Entrar(Collider2D collision)
    {
        //Per a la porta de la mansió............................................................
        if (collision.tag == "puertaMansion")
        {
            onPuertaMansion = true;
        }
        //.......................................................................................

        //Per a la porta del bosc................................................................
        if (collision.tag == "puertaBosque")
        {
            onPuertaBosque = true;
        }
        //.......................................................................................

        //Per al portal final....................................................................
        if (collision.tag == "portal")
        {
            onPortal = true;
        }
        //.......................................................................................
    }

    void SumarVida(Collider2D collision)
    {
        if (collision.tag == "pocion")
        {
            if (vides == 3) //En aquest cas, en comptes de sumar-li una vida (que ja és màxima) li suma 500 punts.
            {
                ScrControlGame.points += 250;
                Destroy(collision.gameObject);
            }
            
            else 
            {
                vides++;
                Destroy(collision.gameObject);
            }
        }
    }

    void AgafarEsperit(Collider2D collision)
    {
        if (collision.tag == "esperit")
        {
            ScrControlGame.esperits++;
            Destroy(collision.gameObject);
        }
    }

    void AgafarDiamant(Collider2D collision)
    {
        if (collision.tag == "gem")
        {
            ScrControlGame.points += 50;
            Destroy(collision.gameObject);
        }

        if (collision.tag == "superGem")
        {
            ScrControlGame.points += 100;
            Destroy(collision.gameObject);
        }
    }

    void ReproduirSons(Collider2D collision)
    {
        AudioSource soPickup;
        soPickup = collision.GetComponent<AudioSource>();

        SpriteRenderer llum;
        llum = collision.GetComponent<SpriteRenderer>();

        if (soPickup)
        {
            if (collision.tag == "luz_int") //que només soni si la llum està encesa
            {
                if (llum.color == new Color(1f, 1f, 1f, 1f))
                {
                    AudioSource.PlayClipAtPoint(soPickup.clip, Camera.main.transform.position);
                }                
            }
            else
            {
                AudioSource.PlayClipAtPoint(soPickup.clip, Camera.main.transform.position);
            }
        }
    }

    void fadeOut(string tag)
    {
        if (entrant)
        {
            negreOut.enabled = true;
            negreOut.color = Color.Lerp(negreOut.color, Color.black, 2f * Time.deltaTime);

            if (negreOut.color.a >= 0.95f)
            {
                if (tag == "puertaMansion")
                {
                    fantasma.constraints = RigidbodyConstraints2D.None;
                    SceneManager.LoadScene("Nivell2"); //carreguem el nivell de la mansió
                    entrant = false;
                }

                if (tag == "puertaBosque")
                {
                    fantasma.constraints = RigidbodyConstraints2D.None;
                    SceneManager.LoadScene("Nivell3"); //carreguem el nivell del bosc                    
                }
            }
        }        
    }

    void AccionarInterruptor(Collider2D collision)
    {
        if (collision.tag == "boto")
        {
            if (ScrPortaVermella.vermellaOberta)
            {
                ScrPortaVermella.vermellaOberta = false;
            }
            else
            {
                ScrPortaVermella.vermellaOberta = true;
            }
        }
    }

    void GameOver()
    {
        overCanvas.SetActive(true);
        Time.timeScale = 0;

        if (Input.GetKeyDown(KeyCode.S)) //sortir del joc
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.T)) //tornar al menu
        {

            ScrControlGame.enJoc = false;
            overCanvas.SetActive(false);
            ScrControlGame.resetJoc();
            ScrControlMusica.somusica.Stop();
            SceneManager.LoadScene("Menu");
        }
    }

    void Winner()
    {
        winnerCanvas.SetActive(true);
        finalText.text = "Felicitats! Has guanyat amb " + ScrControlGame.points + " punts i en " + ScrControlGame.temps.ToString("0") + " segons!";
        Time.timeScale = 0;

        if (Input.GetKeyDown(KeyCode.S)) //sortir del joc
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.T)) //tornar al menu
        {

            ScrControlGame.enJoc = false;
            overCanvas.SetActive(false);
            ScrControlGame.resetJoc();
            ScrControlMusica.somusica.Stop();
            onPortal = false;
            SceneManager.LoadScene("Menu");
        }
    }
}

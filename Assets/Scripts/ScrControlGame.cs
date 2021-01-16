using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ----------------------------------------------------------------------------------
/// DESCRIPCIÓ
///         Script utilitzat per controlar generalment el joc, amb les variables principals i amb les entrades de teclat que faci l'usuari.
/// AUTOR:  Lídia García Romero
/// DATA:   06/01/2021
/// VERSIÓ: 1.0
/// CONTROL DE VERSIONS
///         1.0: primera versió.
/// ----------------------------------------------------------------------------------
/// </summary>

public class ScrControlGame : MonoBehaviour
{
    public static bool llaveMansion, llaveBosque;
    public static int esperits = 0;
    public static int points = 0;
    [SerializeField] GameObject guiaCanvas, pausaCanvas;
    bool pausa = false;
    [SerializeField] Image reanudarBoto, menuBoto, sortirBoto;
    public static bool enJoc = false;
    public static float temps;

    void Start()
    {
        enJoc = true;

        //Al començament del joc apareix una guia de la UI amb el joc pausat.....................
        if (guiaCanvas.active == true)
        {
            Time.timeScale = 0;
        }
        //.......................................................................................

        //Controlem les claus que el jugador ha agafat...........................................
        llaveMansion = false;
        llaveBosque = false;
        //.......................................................................................
    }

    void Update()
    {
        temps += Time.deltaTime;

        //Al començament del joc l'usuari ha de pressionar ENTER per començar....................
        if (Input.GetKeyDown(KeyCode.Return))
        {
            guiaCanvas.SetActive(false);
            Time.timeScale = 1;
        }
        //.......................................................................................

        //Controlem totes les entrades de teclat general de l'usuari.............................
        controlTeclat();
        //.......................................................................................
    }

    void controlTeclat()
    {
        //Pausa i opcions dins del menú de pausa.....................................
        if (Input.GetKeyDown(KeyCode.P) && pausa == false) //obrir pausa
        {
            pausa = true;
            Time.timeScale = 0;
            pausaCanvas.SetActive(true);
            AudioSource soBoto = pausaCanvas.GetComponent<AudioSource>();
            soBoto.Play();
        }

        if (Input.GetKeyDown(KeyCode.R) && pausa == true)//reanudar partida
        {
            pausa = false;
            pausaCanvas.SetActive(false);
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.T) && pausa == true)//tornar al menú principal i resetejar el joc
        {
            enJoc = false;
            resetJoc();
            ScrControlMusica.somusica.Stop();
            SceneManager.LoadScene("Menu");
        }

        if (Input.GetKeyDown(KeyCode.S) && pausa == true)//tornar al menú principal i resetejar el joc
        {
            Application.Quit();
        }
        //.......................................................................................

        //Mutejar tota la música i sons del joc..................................................
        if (Input.GetKeyDown(KeyCode.M))
        {
            mute();
        }
        //.......................................................................................

        //Pujar i baixar el bolum general del joc................................................
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            ScrControlMusica.somusica.volume -= 0.05f;
            AudioListener.volume -= 0.05f;
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus)) 
        {
            ScrControlMusica.somusica.volume += 0.05f;
            AudioListener.volume += 0.05f;
        }

        AudioListener.volume = Mathf.Clamp(AudioListener.volume, 0, 1); //evitem que tingui valors negatius

        //Sortir del joc directament.............................................................
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        //.......................................................................................
    }

    public static void resetJoc()
    {
        temps = 0f;
        llaveMansion = false;
        llaveBosque = false;
        points = 0;
        esperits = 0;
        ScrPlayer.vides = 2;
        ScrPlayer.onPuertaMansion = false;
        ScrPlayer.onPuertaBosque = false;
        ScrPlayer.onPortal = false;
    }

    void mute()
    {
        if (ScrControlMusica.pausaMusica == false)
        {
            ScrControlMusica.somusica.Pause();
            ScrControlMusica.pausaMusica = true;
        }

        else
        {
            ScrControlMusica.somusica.Play();
            ScrControlMusica.pausaMusica = false;
        }
        
        AudioListener.pause = !AudioListener.pause;
    }
}
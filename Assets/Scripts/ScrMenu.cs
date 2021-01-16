using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// ----------------------------------------------------------------------------------
/// DESCRIPCIÓ
///         Script utilitzat per controlar el menú principal del joc i totes les seves opcions
/// AUTOR:  Lídia García Romero
/// DATA:   06/01/2021
/// VERSIÓ: 1.0
/// CONTROL DE VERSIONS
///         1.0: primera versió. 
/// ----------------------------------------------------------------------------------
/// </summary>


public class ScrMenu : MonoBehaviour
{
    [SerializeField] Image jugar, controls, web, sortir, back;
    [SerializeField] GameObject controlCanvas;
    AudioSource soBoto;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) //començar a jugar
        {
            jugar.color = Color.magenta;
            soBoto = jugar.GetComponent<AudioSource>();
            DontDestroyOnLoad(soBoto);
            soBoto.Play();
            SceneManager.LoadScene("Nivell1");
        }

        if (Input.GetKeyDown(KeyCode.C)) //obrir la pantalla de controls 
        {
            soBoto = controls.GetComponent<AudioSource>();
            DontDestroyOnLoad(soBoto);
            soBoto.Play();
            controls.color = Color.magenta;
            controlCanvas.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.B)) //tancar la pantalla de controls i tornar al menú
        {
            soBoto = back.GetComponent<AudioSource>();
            DontDestroyOnLoad(soBoto);
            soBoto.Play();
            controlCanvas.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.W)) //visitar la web
        {
            soBoto = web.GetComponent<AudioSource>();
            DontDestroyOnLoad(soBoto);
            soBoto.Play();
            web.color = Color.magenta;
            Application.OpenURL("https://www.emav.com/");
        }

        if (Input.GetKeyDown(KeyCode.S)) //sortir del joc
        {
            soBoto = sortir.GetComponent<AudioSource>();
            DontDestroyOnLoad(soBoto);
            soBoto.Play();
            sortir.color = Color.magenta;
            Application.Quit();
        }
    }
}

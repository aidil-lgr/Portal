using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrControlMusica : MonoBehaviour
{


    /// <summary>
    /// ----------------------------------------------------------------------------------
    /// DESCRIPCIÓ
    ///         Script utilitzat per controlar la música de fons del joc durant totes les escenes.
    /// AUTOR:  Lídia García Romero
    /// DATA:   07/01/2021
    /// VERSIÓ: 1.0
    /// CONTROL DE VERSIONS
    ///         1.0: primera versió.
    /// ----------------------------------------------------------------------------------
    /// </summary>

    public static AudioSource somusica;
    public static bool pausaMusica = false;

    void Start()
    {
        //Preparem la música de fons.............................................................
        somusica = GetComponent<AudioSource>();
        DontDestroyOnLoad(somusica);
        somusica.Play();
        somusica.ignoreListenerPause = true;
        //.......................................................................................
    }

    void Update()
    {
        
    }
}

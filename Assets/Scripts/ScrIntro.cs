using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ----------------------------------------------------------------------------------
/// DESCRIPCIÓ
///         Script utilitzat pel primer nivell de tots en iniciar el joc, la intro, on només s'ha de clicar ENTER per començar.
/// AUTOR:  Lídia García Romero
/// DATA:   06/01/2021
/// VERSIÓ: 1.0
/// CONTROL DE VERSIONS
///         1.0: primera versió.
/// ----------------------------------------------------------------------------------
/// </summary>

public class ScrIntro : MonoBehaviour
{
    AudioSource soBoto;

    void Start()
    {
        soBoto = GetComponent<AudioSource>();
        DontDestroyOnLoad(soBoto);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && ScrControlGame.enJoc == false)
        {
            soBoto.Play();
            SceneManager.LoadScene("Menu");
        }
    }
}

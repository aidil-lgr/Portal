using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ----------------------------------------------------------------------------------
/// DESCRIPCIÓ
///         Script utilitzat per controlar les portes blaves del tercer nivell i per "animar-les".
/// AUTOR:  Lídia García Romero
/// DATA:   06/01/2021
/// VERSIÓ: 1.0
/// CONTROL DE VERSIONS
///         1.0: primera versió.
/// ----------------------------------------------------------------------------------
/// </summary>


public class ScrPortaBlava : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer porta;

    Vector3 escOberta, escTancada;

    void Start()
    {
        //Especifiquem el tamany màxim de la porta oberta i el mínim.............................
        escOberta = new Vector3(0.15f, 1f, 1f);
        escTancada = porta.transform.localScale;
        //.......................................................................................
    }

    void Update()
    {
        if (!ScrPortaVermella.vermellaOberta) Obrir();
        else Tancar();
    }

    void Obrir()
    {
        if (porta.transform.localScale.x >= escOberta.x)
        {
            porta.transform.localScale -= new Vector3(0.01f, 0f, 0f);
        }
        ScrPortaVermella.vermellaOberta = false;
    }

    void Tancar()
    {
        if (porta.transform.localScale.x <= escTancada.x)
        {
            porta.transform.localScale += new Vector3(0.01f, 0f, 0f);
        }
        ScrPortaVermella.vermellaOberta = true;
    }
}

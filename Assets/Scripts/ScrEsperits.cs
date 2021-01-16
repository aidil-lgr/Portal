using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ----------------------------------------------------------------------------------
/// DESCRIPCIÓ
///         Script utilitzat per a la petita animació que tenen els esperits.
/// AUTOR:  Lídia García Romero
/// DATA:   06/01/2021
/// VERSIÓ: 1.0
/// CONTROL DE VERSIONS
///         1.0: primera versió. 
/// ----------------------------------------------------------------------------------
/// </summary>

public class ScrEsperits : MonoBehaviour
{
    SpriteRenderer esperit;
    float desplazamiento = 1f;
    [SerializeField]
    float velocitat = 0.01f;
    [SerializeField]
    float max = 0.01f;

    void Start()
    {
        esperit = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Fer que l'esperit es mogui subtilment..................................................
        esperit.transform.Translate(0, -velocitat*Time.deltaTime, 0);
        desplazamiento += max;

        if (desplazamiento > 2)
        {
            velocitat *= -1;
            desplazamiento = 0;
        }
        //.......................................................................................
    }
}

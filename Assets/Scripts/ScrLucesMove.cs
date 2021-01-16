using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ----------------------------------------------------------------------------------
/// DESCRIPCIÓ
///         Script utilitzat per controlar les llums que es mouen i les "anima".
///AUTOR:  Lídia García Romero
/// DATA:   06/01/2021
/// VERSIÓ: 1.0
/// CONTROL DE VERSIONS
///         1.0: primera versió. 
/// ----------------------------------------------------------------------------------
/// </summary>

public class ScrLucesMove : MonoBehaviour
{
    SpriteRenderer luz;
    float desplazamiento = 1f;
    [SerializeField]
    float velocitat = 0.01f;
    [SerializeField]
    float max = 0.01f;

    void Start()
    {
        luz = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Fer que la llum, si és de tipus "move", es balancegi...................................
        luz.transform.Translate(-velocitat*Time.deltaTime, 0, 0);
        desplazamiento += max;

        if (desplazamiento > 2)
        {
            velocitat *= -1;
            desplazamiento = 0;
        }
        //.......................................................................................
    }
}

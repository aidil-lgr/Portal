using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


/// <summary>
/// ----------------------------------------------------------------------------------
/// DESCRIPCIÓ
///         Script utilitzat per controlas les llums que són intermitents. 
/// AUTOR:  Lídia García Romero
/// DATA:   06/01/2021
/// VERSIÓ: 1.0
/// CONTROL DE VERSIONS
///         1.0: primera versió.
/// ----------------------------------------------------------------------------------
/// </summary>


public class ScrLucesInt : MonoBehaviour
{
    SpriteRenderer luz;
    float alpha;

    void Start()
    {
        luz = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Fer que la llum, si és de tipus intermitent, faci "pampallugues".......................
        alpha = 1 + Mathf.Cos(Time.time);

        if (alpha >= 1)
        {
            luz.color = new UnityEngine.Color(1f, 1f, 1f, 1f);
        }
        else
        {
            luz.color = new UnityEngine.Color(1f, 1f, 1f, 0f);
        }
        //.......................................................................................
    }
}

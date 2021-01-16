using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ----------------------------------------------------------------------------------
/// DESCRIPCIÓ
///         Script utilitzat per controlar la UI que es mostra durant tot el joc. 
/// AUTOR:  Lídia García Romero
/// DATA:   06/01/2021
/// VERSIÓ: 1.0
/// CONTROL DE VERSIONS
///         1.0: primera versió.
/// ----------------------------------------------------------------------------------
/// </summary>

public class ScrUI : MonoBehaviour
{
    [SerializeField]
    Text esperitsText, puntsText, tempsText, misatgesText;
    [SerializeField]
    Image cors, claus;
    [SerializeField]
    Sprite zeroCors, unCor, dosCors, tresCors, unaClau, dosClaus;

    void Start()
    {

    }

    void Update()
    {
        //Establim el contingut de les caixes de text............................................
        esperitsText.text = "x" + ScrControlGame.esperits;

        puntsText.text = "" + ScrControlGame.points;

        tempsText.text = ScrControlGame.temps.ToString("0");
        //.......................................................................................

        //Comprovar les vides del jugador per mostrar-les amb diferents sprites..................
        comprovarVides();
        //.......................................................................................

        //Comprovar les claus del jugador per mostrar-les amb diferents sprites..................
        comprovarClaus();
        //.......................................................................................

        //Mostrar els misatges corresponents a sota de la pantalla quan calguin..................
        mostrarMisatges();
        //.......................................................................................
    }

    void comprovarVides()
    {
        switch (ScrPlayer.vides)
        {
            case 0:
                cors.sprite = zeroCors;
                break;
            case 1:
                cors.sprite = unCor;
                break;
            case 2:
                cors.sprite = dosCors;
                break;
            case 3:
                cors.sprite = tresCors;
                break;
        }
    }

    void comprovarClaus()
    {
        if (ScrControlGame.llaveMansion)
        {
            claus.sprite = unaClau;
        }

        if (ScrControlGame.llaveBosque)
        {
            claus.sprite = dosClaus;
        }
    }

    void mostrarMisatges()
    {
        if (ScrPlayer.onPuertaMansion)
        {
            if (ScrControlGame.llaveMansion && ScrControlGame.esperits == 1)
            {
                misatgesText.text = "Pressiona [E] per entrar";
            }
            else
            {
                misatgesText.text = "Encara no tens la clau o l'esperit!";
            }
            
        }
        else if (ScrPlayer.onPuertaBosque)
        {
            if (ScrControlGame.llaveBosque && ScrControlGame.esperits == 2)
            {
                misatgesText.text = "Pressiona [E] per entrar";
            }
            else
            {
                misatgesText.text = "Encara no tens la clau o l'esperit!";
            }
        }
        else if (ScrPlayer.onPortal)
        {
            if (ScrControlGame.esperits != 3)
            {
                misatgesText.text = "Encara no tens els tres esperits!";
            }
        }        

        else
        {
            misatgesText.text = "";
        }
    }
}

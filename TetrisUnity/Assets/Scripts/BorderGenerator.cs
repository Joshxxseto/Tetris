using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderGenerator : MonoBehaviour
{
    public GameObject BlockPrefab;
    Vector2 Origin;
    void Awake()
    {
        GenerateBorder();
    }

    private void GenerateBorder()
    {
        //Se generará un borde automatico para contener 10x20 bloques en el
        //Es decir el borde será de 12x22 sin Techo

        //Mediante un ciclo for de 12 instancias crearemos los bloques de abajo
        Origin = new Vector2(-1,-1);
        for (int i=0;i<12;i++)
        {
            Origin.x = i-1;
            Instantiate(BlockPrefab,this.transform).transform.position=Origin;
        }
        Origin = new Vector2(-1,-1);
        //Esta vez Crearemos los bloques para la torre Izquiera y derecha
        for (int i=0;i<21;i++)
        {
            Origin.y = i - 1;
            Origin.x = -1;//Izq
            Instantiate(BlockPrefab,this.transform).transform.position = Origin;
            Origin.x = 10;//Der
            Instantiate(BlockPrefab, this.transform).transform.position = Origin;
        }
    }
}

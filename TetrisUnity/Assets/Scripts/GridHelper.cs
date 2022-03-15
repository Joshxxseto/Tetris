using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHelper : MonoBehaviour
{
    public static int width = 10, height = 20+4;
    public static Transform[,] grid = new Transform[width,height];

    public static Vector2 RoundVector(Vector2 v)
    {
       return new Vector2(Mathf.Round(v.x),Mathf.Round(v.y));
    }
    public static bool IsInsideBorders(Vector2 pos)
    {
        return (pos.x >= 0 && pos.y >= 0 && pos.x < width);
    }
    public static void DeleteRow(int y)
    {
        //Recibiendo una Y de la fila que va a ELIMINARSE
        //Para cada elemento en la fila..
        for (int x=0;x<width;x++)
        {
            //Destruiremos el GameObject que contenga (Graficamente)
            Destroy(grid[x, y].gameObject);
            //Vaciaremos el Slot (Logico)
            grid[x, y] = null;
        }
    }
    public static void PushRow(int y)
    {
        //Recibiendo una Y de la fila que va a BAJAR
        //Para cada elemento en la fila..
        for (int x=0; x<width; x++)
        {
            //Que no sea nulo es decir que exista
            if (grid[x,y]!=null)
            {
                //Vamos a ir un espacio abajo (el eliminado) y le asignaremos este elemento existente
                //Es decir "bajará"
                grid[x, y - 1] = grid[x, y];
                //Y dejará su lugar original como Null
                grid[x, y] = null;
                //De modo que hay que repintar el bloque mas abajo
                grid[x, y - 1].position += new Vector3(0,-1,0);
            }
        }
    }

    public static void PushRowAbove(int y)
    {
        //Por cada fila restante desde y hasta Height será empujada
        for (int i = y;i<height;i++)
        {
            PushRow(i);
        }
    }

    public static bool IsRowFull(int y)
    {
        //Por cada elemento en la fila y..
        for (int x =0; x<width;x++)
        {
            //Validar si existe un [x,y] que sea nulo
            if (!grid[x,y])
            {
                //de ser así aún no esta completa
                return false;
            }
        }
        //De otro modo es que esta fila esta completa
        return true;
    }

    public static void deleteAllFullRows()
    {
        //Por cada fila..
        for (int y=0; y<height;y++)
        {
            //Validar si esta fila esta completa
            if (IsRowFull(y))
            {
                //De ser así, se borrará
                DeleteRow(y);
                //La fila de arriba será empujada
                PushRowAbove(y+1);
                //hay que comprobar que la filaempuejada no este completa
                //Esto lo hacemos restando el valor de la Y en el for
                y--;
            }
        }
    }

    public static void CleanPieces()
    {
        foreach (GameObject piece in GameObject.FindGameObjectsWithTag("Piece"))
        {
            if (piece.transform.childCount <= 0)
            {
                Destroy(piece);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    float lastFall = 0.0f;

    private void Start()
    {
        if (!IsValidPiecePosition())
        {
            Debug.Log("GameOver");
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Muevo la pieza a la izquierda
            MovePieceHorizontally(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Muevo la pieza a la Derecha
            MovePieceHorizontally(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Rotar Ficha
            this.transform.Rotate(new Vector3(0, 0, -90));
            if (IsValidPiecePosition())
            {
                updateGrid();
            }
            else
            {
                this.transform.Rotate(new Vector3(0, 0, 90));
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)||Time.time-lastFall>1.0f)
        {
            this.transform.position += new Vector3(0, -1, 0);
            //comprobar si el movimiento fue legal
            if (IsValidPiecePosition())
            {
                //Actualizar parrilla
                updateGrid();
            }
            else
            {
                //Si no, revertir movimiento
                this.transform.position += new Vector3(0, 1, 0);
                //Revisar si hay que elominar filas si no puede bajar
                //Tambien debemos seder la prioridad/Control a la otra ficha
                GridHelper.deleteAllFullRows();
                FindObjectOfType<PieceSpawner>().SpawnPiece();
                //Deshabilitar este script
                this.enabled = false;
            }
            lastFall = Time.time;
        }
    }

    private void MovePieceHorizontally(int direction)
    {
        this.transform.position += new Vector3(direction, 0, 0);
        //comprobar si el movimiento fue legal
        if (IsValidPiecePosition())
        {
            //Actualizar parrilla
            updateGrid();
        }
        else
        {
            //Si no, revertir movimiento
            this.transform.position += new Vector3(-direction, 0, 0);
        }
    }

    private bool IsValidPiecePosition()
    {
        foreach (Transform block in this.transform)
        {
            //Redondear la psicion del bloque
            Vector2 pos = GridHelper.RoundVector(block.position);

            if (!GridHelper.IsInsideBorders(pos))
            {
                //Si esta fuera de los limites entonces no es una posicion valida
                return false;
            }
            //Si ya hay otro bloque en esa posicion entoces esa tampoco es valida
            Transform possibleObject = GridHelper.grid[(int)pos.x, (int)pos.y];
            if (possibleObject!=null && possibleObject.parent!=this.transform)
            {
                return false;
            }
        }
        return true;
    }

    private void updateGrid()
    {
        for (int y=0;y<GridHelper.height;y++)
        {
            for (int x=0;x<GridHelper.width;x++)
            {
                //El padre del bloque es la pieza del script
                if (GridHelper.grid[x,y]!=null && GridHelper.grid[x, y].parent == this.transform)
                {
                    GridHelper.grid[x, y] = null;
                }
            }
        }

        foreach (Transform block in this.transform)
        {
            Vector2 pos = GridHelper.RoundVector(block.position);

            GridHelper.grid[(int)pos.x,(int)pos.y]=block;
        }
    }
}

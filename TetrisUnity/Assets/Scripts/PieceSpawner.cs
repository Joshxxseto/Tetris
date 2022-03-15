using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Pieces;
    [SerializeField] Transform nextPiecePos;
    public GameObject currentPiece, nextPiece;
    private void Start()
    {
        int i = Random.Range(0, Pieces.Length);
        nextPiece = Instantiate(Pieces[i], this.transform.position, Quaternion.identity);
        SpawnPiece();
    }
    public void SpawnPiece()
    {
        currentPiece = nextPiece;
        currentPiece.GetComponent<Piece>().enabled = true;
        currentPiece.transform.position = this.transform.position;
        //Preparar Siguiente pieza
        int i = Random.Range(0,Pieces.Length);
        nextPiece=Instantiate(Pieces[i],nextPiecePos.position,Quaternion.identity);
        nextPiece.GetComponent<Piece>().enabled = false;
        
        GridHelper.CleanPieces();
    }
}

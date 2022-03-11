using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Pieces;
    private void Start()
    {
        SpawnPiece();
    }
    void SpawnPiece()
    {
        int i = Random.Range(0,Pieces.Length-1);
        Instantiate(Pieces[i],this.transform.position,Quaternion.identity);
    }
}

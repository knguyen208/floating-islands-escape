using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2f;

    private void Update()
    {
        CheckForTreasureInteraction();
    }

    private void CheckForTreasureInteraction()
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("AircraftPiece");

        foreach (GameObject piece in pieces)
        {
            float distanceToPiece = Vector3.Distance(transform.position, piece.transform.position);

            if (distanceToPiece <= interactionRange && Input.GetKeyDown(KeyCode.E))
            {
                CollectPiece(piece.GetComponent<AircraftPiece>());
            }
        }
    }

    private void CollectPiece(AircraftPiece piece)
    {
        if (piece != null)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.pieceCollectSound);
            piece.NotifyEnemies();
            GameManager.Instance.CollectPiece();
            Destroy(piece.gameObject);
        }
    }
}
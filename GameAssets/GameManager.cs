using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text winText;
    public GameObject[] floatingCubeGroups;
    public GameObject finalObject;
    public GameObject[] pieceImages;

    private int totalPieces = 3;
    public int collectedPieces = 0;

    void Start()
    {
        winText.gameObject.SetActive(false);

        foreach (GameObject group in floatingCubeGroups)
        {
            group.SetActive(false);
        }

        if (finalObject != null)
        {
            finalObject.SetActive(false);
        }

        foreach (GameObject image in pieceImages)
        {
            image.SetActive(false);
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectPiece()
    {
        if (collectedPieces < floatingCubeGroups.Length)
        {
            floatingCubeGroups[collectedPieces].SetActive(true);
        }

        if (collectedPieces < pieceImages.Length)
        {
            pieceImages[collectedPieces].SetActive(true);
        }

        collectedPieces++;
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (collectedPieces >= totalPieces)
        {
            if (finalObject != null)
            {
                finalObject.SetActive(true);
            }

            winText.gameObject.SetActive(true);
            Debug.Log("You win!");
        }
    }
}
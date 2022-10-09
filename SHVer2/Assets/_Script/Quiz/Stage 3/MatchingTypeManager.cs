using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchingTypeManager : MonoBehaviour
{
    [SerializeField] private GameObject choicePrefab;

    //Reference quiz
    [SerializeField] private Stg3_Quiz quiz;
    [SerializeField] private Transform parentTransform, pieceParent;
    private List<int> choicePositionIndex;

    private void OnEnable()
    {
        RandomizePiecesPosition();
        spawnChoices();
    }

    private void OnDisable()
    {
        DestroyAllChoices();
    }

    private void spawnChoices()
    {
        for (int i = 0; i < 4; i++)
        {
            var spawnedPiece = Instantiate(choicePrefab, pieceParent.GetChild(choicePositionIndex[i]).position, Quaternion.identity, parentTransform);

            if (i == 0)
            {
                spawnedPiece.GetComponent<ChoicePiece>().correctAnswer = true;
                Debug.Log("Correct answer: picture " + choicePositionIndex[i]);
            }

            spawnedPiece.GetComponent<ChoicePiece>().image.sprite = quiz.questionBank.Qbank[quiz.questionIndex].choices[i];

        }
    }

    private void RandomizePiecesPosition()
    {
        List<int> values = new List<int>() { 0, 1, 2, 3 };

        System.Random rand = new System.Random();

        choicePositionIndex = values.OrderBy(_ => rand.Next()).ToList();
    }

    private void DestroyAllChoices()
    {
        foreach (Transform item in parentTransform)
        {
            GameObject.Destroy(item.gameObject);
        }
    }
}

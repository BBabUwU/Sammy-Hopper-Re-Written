using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    //Where the questions will be obtained
    [NonReorderable]
    [SerializeField]
    public List<Chapter1QnATemplate> easyQuestions = new List<Chapter1QnATemplate>();
    public List<Chapter1QnATemplate> hardQuestions = new List<Chapter1QnATemplate>();

    private List<Chapter1QnATemplate> GetQnA()
    {
        return easyQuestions;
    }
}

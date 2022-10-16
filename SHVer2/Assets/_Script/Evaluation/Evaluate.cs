using UnityEngine;
using TMPro;

public class Evaluate : MonoBehaviour
{
    //S = Yellow
    //A = RED
    //B = Blue
    //C = GREEN
    //D = BROWN

    [SerializeField] private TextMeshProUGUI hitsText;
    [SerializeField] private TextMeshProUGUI mistakesText;
    [SerializeField] private TextMeshProUGUI hardQText;
    [SerializeField] private TextMeshProUGUI result;

    public int hitsTaken;
    public int mistakes;
    public int hardQ;
    private int overallGrade = 0;

    private void Increase_Counter(string type)
    {
        if (type == "hit") hitsTaken++;
        else if (type == "mistake") mistakes++;
        else if (type == "hard") hardQ++;
        else Debug.Log("Evaluation does not exist");
    }

    private void SetDisplay()
    {
        hitsText.text += " " + hitsTaken;
        mistakesText.text += " " + mistakes;

        if (hardQText != null)
        {
            hardQText.text += " " + hardQ;
        }

        //10 = S
        //15 = A
        //20 = B
        //25 = C
        //30 = D

        overallGrade = hitsTaken + mistakes - hardQ;

        if (overallGrade <= 10)
        {
            result.color = Color.yellow;
            result.text = "S";
        }
        else if (overallGrade <= 15)
        {
            result.color = Color.red;
            result.text = "A";
        }
        else if (overallGrade <= 20)
        {
            result.color = Color.blue;
            result.text = "B";
        }
        else if (overallGrade <= 25)
        {
            result.color = Color.green;
            result.text = "C";
        }

        else if (overallGrade <= 30)
        {
            result.color = Color.grey;
            result.text = "D";
        }
    }


    private void OnEnable()
    {
        Actions.addEveluation += Increase_Counter;
        Actions.bossDefeated += SetDisplay;
    }

    private void OnDisable()
    {
        Actions.addEveluation -= Increase_Counter;
        Actions.bossDefeated -= SetDisplay;
    }
}

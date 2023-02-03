using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswersUIController : MonoBehaviour
{
    [SerializeField]
    AnswersManager answersManager;

    public delegate void UpdateAnswersUI(Answer[] answers);
    public UpdateAnswersUI OnUpdateAnswersUI;

    private void OnEnable()
    {
        answersManager.OnUpdateAnswers += ChangeAnswersUI;
    }

    private void OnDisable()
    {
        answersManager.OnUpdateAnswers -= ChangeAnswersUI;
    }

    private void ChangeAnswersUI(Answer[] answers)
    {
        OnUpdateAnswersUI?.Invoke(answers);
    }

    public void SubmitAnswer(int answerIndex)
    {
        answersManager.AnswerSelected (answerIndex);
    }
}

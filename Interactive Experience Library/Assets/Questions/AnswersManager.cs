using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswersManager : MonoBehaviour
{
    [SerializeField]
    QuestionsManager questionsManager;

    public delegate void UpdateAnswers(Answer[] answers);
    public UpdateAnswers OnUpdateAnswers;
    public delegate void OnAnswerSelected(Answer answer);
    public OnAnswerSelected D_OnAnswerSelected;

    private Answer[] currentAnswers;

    public void ChangeAnswers(Question question)
    {
        OnUpdateAnswers?.Invoke(question.answers);
        currentAnswers = question.answers;
    }

    private void OnEnable()
    {
        questionsManager.OnUpdateQuestion += ChangeAnswers;
    }

    private void OnDisable()
    {
        questionsManager.OnUpdateQuestion -= ChangeAnswers;
    }

    public void AnswerSelected(Answer answer)
    {        
        if (answer.nextQuestion)
        {
            questionsManager.ChangeQuestion(answer.nextQuestion);
        }
        else 
        {
            Debug.LogWarning("Selected answer has no next question specified");
        }
        answer.SelectAnswer();
        D_OnAnswerSelected?.Invoke(answer);
    }

    public void AnswerSelected(int answerIndex)
    {
        AnswerSelected(currentAnswers[answerIndex]);
    }
    
}

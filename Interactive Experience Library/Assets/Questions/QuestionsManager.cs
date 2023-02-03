using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsManager : MonoBehaviour
{
    [SerializeField] Question startingQuestion;

    private Question currentQuestion;



    public delegate void UpdateQuestion(Question question);
    public UpdateQuestion OnUpdateQuestion;
    public void ChangeQuestion(Question question)
    {
        if (currentQuestion)
        {
            currentQuestion.QuestionExit();
        }
        OnUpdateQuestion?.Invoke(question);        
        question.QuestionEntry();
        currentQuestion = question;
    }

    private void Start() {
        if (!startingQuestion)
        {
            Debug.LogWarning("No starting question specified");
        }
        else
        {
            ChangeQuestion(startingQuestion); 
        }
    }
    
}

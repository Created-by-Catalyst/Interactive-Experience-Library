using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Answer", menuName = "Conversations/Answers/Answer", order = 1)]
public class Answer : ScriptableObject {
    public string answerText;
    public Question nextQuestion;

    public delegate void AnswerSelected();
    public AnswerSelected OnAnswerSelected;
    public void SelectAnswer()
    {
        OnAnswerSelected?.Invoke();
    } 
}

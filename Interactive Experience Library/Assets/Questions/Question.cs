using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Conversations/Questions/Question", order = 0)]
public class Question : ScriptableObject {
    public string questionText;
    public Answer[] answers;

    public delegate void QuestionEntered();
    public QuestionEntered OnQuestionEntered;
    public void QuestionEntry()
    {
        OnQuestionEntered?.Invoke();
    }

    public delegate void QuestionExited();
    public QuestionExited OnQuestionExited;
    public void QuestionExit()
    {
        OnQuestionExited?.Invoke();
    }
}

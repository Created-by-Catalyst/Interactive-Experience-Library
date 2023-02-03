using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionsUIController : MonoBehaviour
{
    [SerializeField] QuestionsManager questionsManager;
    [SerializeField] TextMeshProUGUI questionText;

    private void OnEnable() {
        questionsManager.OnUpdateQuestion += UpdateQuestionText;
    }

    private void OnDisable() {
        questionsManager.OnUpdateQuestion -= UpdateQuestionText;
    }

    private void UpdateQuestionText(Question question)
    {
        questionText.text = question.questionText;
    }
}

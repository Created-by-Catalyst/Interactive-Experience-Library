using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static AnswersManager;
using static AnswersUIController;

public class QuestionHistory : MonoBehaviour
{
    [SerializeField]
    QuestionsManager questionsManager;


    List<Question> previousQuestions = new List<Question>();


    [SerializeField]
    AnswerObject[] answerObjects;


    private void OnEnable()
    {
        questionsManager.OnUpdateQuestion += AddNewPreviousQuestion;
    }

    private void OnDisable()
    {
        questionsManager.OnUpdateQuestion -= AddNewPreviousQuestion;
    }


    void AddNewPreviousQuestion(Question question)
    {
        previousQuestions.Add(question);
    }


    public void Back()
    {


        if (previousQuestions.Count > 1)
        {
            questionsManager.ChangeQuestion(previousQuestions[previousQuestions.Count - 2]);

            UpdateAnswerOptions(previousQuestions[previousQuestions.Count - 3]);

            previousQuestions.Remove(previousQuestions.Last<Question>());
            previousQuestions.Remove(previousQuestions[previousQuestions.Count - 2]);
        }

    }

    public void UpdateAnswerOptions(Question currentQuestion)
    {

        for (int i = 0; i < answerObjects.Length; i++)
        {
            answerObjects[i].aObj.SetActive(false);

            if (answerObjects[i].name == currentQuestion.questionText) answerObjects[i].aObj.SetActive(true);
        }

    }


    int index = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            foreach (var item in previousQuestions)
            {
                print(index + " " + item);
            }
        }
    }

    [Serializable]
    public class AnswerObject
    {
        public string name;
        public GameObject aObj;
    }


}

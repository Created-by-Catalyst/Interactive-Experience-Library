using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISequencer : MonoBehaviour
{


    [SerializeField]
    GameObject[] uiScreens;


    [SerializeField]
    Animator uiAnimatior;

    int currentScreen;

    private void Start()
    {
        DisplayNextScreen(0);
    }

    void FadeIn()
    {
        if(uiAnimatior != null)
        uiAnimatior.SetTrigger("OpenScreen");
    }

    void FadeOut()
    {
        if (uiAnimatior != null)
        uiAnimatior.SetTrigger("CloseScreen");
    }



    public void DisplayNextScreen(int screenID)
    {

        currentScreen = screenID;

        switch (screenID)
        {
            case 0:
                StartCoroutine(ExampleScreen1());
                break;
            case 1:
                StartCoroutine(ExampleScreen2());
                break;
            case 2:
                StartCoroutine(ExampleScreen3());
                break;

            default:
                break;
        }

    }

    IEnumerator ExampleScreen1()
    {
        yield return StartCoroutine(BasicScreen(5f));


        DisplayNextScreen(1);
    }
    IEnumerator ExampleScreen2()
    {
        yield return StartCoroutine(InputScreen());


        DisplayNextScreen(2);
    }
    IEnumerator ExampleScreen3()
    {
        yield return StartCoroutine(BasicScreen(10f));


        uiScreens[currentScreen].SetActive(false);
    }



    IEnumerator BasicScreen(float displayTime)
    {
        uiScreens[currentScreen].SetActive(true);
        FadeIn();
        yield return new WaitForSeconds(displayTime);
        FadeOut();
        yield return new WaitForSeconds(2f);
        uiScreens[currentScreen].SetActive(false);
    }

    public static bool inputFinished = false;

    public static bool inInputScreen = false;


    IEnumerator InputScreen()
    {
        uiScreens[currentScreen].SetActive(true);
        FadeIn();


        inInputScreen = true;

        while (!inputFinished)
        {
            yield return null;
        }
        inputFinished = false;

        inInputScreen = false;

        FadeOut();
        yield return new WaitForSeconds(2f);
        uiScreens[currentScreen].SetActive(false);
    }


    public void SetInput(bool input)
    {
        inputFinished = input;
    }




}

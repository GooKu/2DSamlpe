using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
//[SerializeField]
    [SerializeField]
    private Text countdownText;
    [SerializeField]
    private Text resultText;
    [SerializeField]
    private GameObject resultUI;
    [SerializeField]
    private float levelTime = 10;

    private float time;

    private Coroutine countDownCoroutine;

    void Start()
    {
        GameObject.FindObjectOfType<PlayerController>().ArrivedGoalEvent += arrivedGoal;

        resultUI.SetActive(false);

        time = levelTime;

        countDownCoroutine = StartCoroutine(countDown());
    }

    private IEnumerator countDown()
    {
        do
        {
            yield return  null;
            time -= Time.deltaTime;

            if (time <= 0)
            {
                time = 0;
                gameEnd(false);
            }
            countdownText.text = time.ToString("0.00");

        } while (true);
    }

    private void arrivedGoal()
    {
        StopCoroutine(countDownCoroutine);
        gameEnd(true);
    }

    private void gameEnd(bool isPass)
    {
        GameObject.FindObjectOfType<PlayerController>().enabled = false;
        resultUI.SetActive(true);

        if (isPass)
        {
            resultText.text = "Pass";
        }
        else
        {
            resultText.text = "Loss";
        }
    }

    public void Again()
    {
        SceneManager.LoadScene(0);
    }
}

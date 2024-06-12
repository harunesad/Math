using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI firstQuestion, secondQuestion, proccess, timeText, countdownText;
    [SerializeField] TMP_Dropdown time;
    public TMP_Dropdown min, max;
    [SerializeField] GameStates gameStates;
    Button aBtn, bBtn, cBtn, dBtn;
    int firstNumber, secondNumber, questionNumber = 1;
    int answer = -1, answerFirst = -1, answerSecond = -1, answerThird = -1, answerId;
    float timeCount, countdown;
    bool countdownStart;
    [SerializeField] List<TextMeshProUGUI> answersText, copyAnswersText;
    private void Awake()
    {
        aBtn = answersText[0].GetComponentInParent<Button>();
        bBtn = answersText[1].GetComponentInParent<Button>();
        cBtn = answersText[2].GetComponentInParent<Button>();
        dBtn = answersText[3].GetComponentInParent<Button>();
        copyAnswersText.AddRange(answersText);
    }
    void Start()
    {
        //QuestionAndAnswers();
    }
    void Update()
    {
        if (countdown > 0 && countdownStart)
        {
            countdown -= Time.deltaTime;
            countdownText.text = ((int)countdown).ToString();
        }
        else if (countdown <= 0 && countdownText.transform.parent.gameObject.activeSelf)
        {
            countdown = 0;
            countdownText.transform.parent.gameObject.SetActive(false);
        }
        if (countdown <= 0 && timeCount > 0)
        {
            timeCount -= Time.deltaTime;
            timeText.text = ((int)timeCount).ToString();
        }
        if (timeCount <= 0)
        {
            timeCount = 0;
            timeText.text = ((int)timeCount).ToString();
        }
    }
    public void TimeStart(bool input)
    {
        if (!input)
        {
            timeCount = 100;
            timeText.text = timeCount.ToString();
        }
        else
        {
            timeCount = (time.value + 1) * 50;
            timeText.text = timeCount.ToString();
        }
    }
    public void Countdown()
    {
        countdown = 3;
        countdownText.text = ((int)countdown).ToString();
        countdownText.transform.parent.gameObject.SetActive(true);
        StartCoroutine(WaitCountdownStart());
    }
    IEnumerator WaitCountdownStart()
    {
        yield return new WaitForSeconds(2);
        countdownStart = true;
    }
    public void QuestionAndAnswers()
    {
        switch (gameStates.randomState)
        {
            case GameStates.RandomState.Random:
                switch (gameStates.gameState)
                {
                    case GameStates.GameState.Plus:
                        QuestionPlus(0);
                        Answers();
                        break;
                    case GameStates.GameState.Minus:
                        QuestionMinus(0);
                        Answers();
                        break;
                    case GameStates.GameState.Multiplication:
                        QusetionMultiplication(0);
                        Answers();
                        break;
                    case GameStates.GameState.Division:
                        QuestionDivision(0);
                        Answers();
                        break;
                    case GameStates.GameState.Mixed:
                        int proccessId = Random.Range(0, 4);
                        switch (proccessId)
                        {
                            case 0:
                                QuestionPlus(0);
                                Answers();
                                break;
                            case 1:
                                QuestionMinus(0);
                                Answers();
                                break;
                            case 2:
                                QusetionMultiplication(0);
                                Answers();
                                break;
                            case 3:
                                QuestionDivision(0);
                                Answers();
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                break;
            case GameStates.RandomState.Input:
                switch (gameStates.gameState)
                {
                    case GameStates.GameState.Plus:
                        QuestionPlus(100 * min.value);
                        //InputQuestionPlus();
                        Answers();
                        break;
                    case GameStates.GameState.Minus:
                        QuestionMinus(100 * min.value);
                        Answers();
                        break;
                    case GameStates.GameState.Multiplication:
                        QusetionMultiplication(100 * min.value);
                        Answers();
                        break;
                    case GameStates.GameState.Division:
                        QuestionDivision(100 * min.value);
                        Answers();
                        break;
                    case GameStates.GameState.Mixed:
                        int proccessId = Random.Range(0, 4);
                        switch (proccessId)
                        {
                            case 0:
                                QuestionPlus(100 * min.value);
                                Answers();
                                break;
                            case 1:
                                QuestionMinus(100 * min.value);
                                Answers();
                                break;
                            case 2:
                                QusetionMultiplication(100 * min.value);
                                Answers();
                                break;
                            case 3:
                                QuestionDivision(100 * min.value);
                                Answers();
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
    void QuestionPlus(int input)
    {
        proccess.text = "+";
        answer = Random.Range(((questionNumber - 1) * 10) + 1 + input, (questionNumber * 10) + 1 + input);
        firstNumber = Random.Range(0, answer);
        firstQuestion.text = firstNumber.ToString();
        secondNumber = answer - firstNumber;
        secondQuestion.text = secondNumber.ToString();
    }
    void QuestionMinus(int input)
    {
        proccess.text = "-";
        answer = Random.Range(((questionNumber - 1) * 10) + 1 + input, (questionNumber * 10) + 1 + input);
        firstNumber = Random.Range(answer, answer + 100);
        firstQuestion.text = firstNumber.ToString();
        secondNumber = firstNumber - answer;
        secondQuestion.text = secondNumber.ToString();
    }
    void QusetionMultiplication(int input)
    {
        proccess.text = "x";
        answer = Random.Range(((questionNumber - 1) * 10) + 1 + input, (questionNumber * 10) + 1 + input);
        List<int> multi = new List<int>();
        for (int i = 1; i < answer + 1; i++)
        {
            if (answer % i == 0)
            {
                multi.Add(i);
            }
        }
        int multiRandom = Random.Range(0, multi.Count);
        firstNumber = multi[multiRandom];
        firstQuestion.text = firstNumber.ToString();
        secondNumber = answer / firstNumber;
        secondQuestion.text = secondNumber.ToString();
    }
    void QuestionDivision(int input)
    {
        proccess.text = "/";
        answer = Random.Range(((questionNumber - 1) * 10) + 1 + input, (questionNumber * 10) + 1 + input);
        List<int> division = new List<int>();
        for (int i = 1; i < answer + 1; i++)
        {
            division.Add(i * answer);
        }
        int divisionRandom = Random.Range(0, division.Count);
        firstNumber = division[divisionRandom];
        firstQuestion.text = firstNumber.ToString();
        secondNumber = firstNumber / answer;
        secondQuestion.text = secondNumber.ToString();
    }
    void Answers()
    {
        answerId = Random.Range(0, answersText.Count);
        answersText[answerId].text = answer.ToString();
        answersText.RemoveAt(answerId);
        answerFirst = Random.Range(answer - 10, answer + 10);
        while (answerFirst == answer || answerFirst == answerSecond || answerFirst == answerThird || answerFirst < 0)
        {
            answerFirst = Random.Range(answer - 10, answer + 10);
        }
        answersText[0].text = answerFirst.ToString();
        answerSecond = Random.Range(answer - 10, answer + 10);
        while (answerSecond == answer || answerSecond == answerFirst || answerSecond == answerThird || answerSecond < 0)
        {
            answerSecond = Random.Range(answer - 10, answer + 10);
        }
        answersText[1].text = answerSecond.ToString();
        answerThird = Random.Range(answer - 10, answer + 10);
        while (answerThird == answer || answerThird == answerFirst || answerThird == answerSecond || answerThird < 0)
        {
            answerThird = Random.Range(answer - 10, answer + 10);
        }
        answersText[2].text = answerThird.ToString();
        answersText.Clear();
        answersText.AddRange(copyAnswersText);
    }
}

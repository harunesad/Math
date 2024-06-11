using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI firstQuestion, secondQuestion, proccess;
    [SerializeField] TMP_Dropdown time;
    public TMP_InputField min, max;
    [SerializeField] GameStates gameStates;
    Button aBtn, bBtn, cBtn, dBtn;
    int firstNumber, secondNumber, questionNumber = 1;
    int answer = -1, answerFirst = -1, answerSecond = -1, answerThird = -1, answerId;
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

    }
    public void QuestionAndAnswers()
    {
        switch (gameStates.randomState)
        {
            case GameStates.RandomState.Random:
                switch (gameStates.gameState)
                {
                    case GameStates.GameState.Plus:
                        QuestionPlus();
                        Answers();
                        break;
                    case GameStates.GameState.Minus:
                        QuestionMinus();
                        Answers();
                        break;
                    case GameStates.GameState.Multiplication:
                        QusetionMultiplication();
                        Answers();
                        break;
                    case GameStates.GameState.Division:
                        QuestionDivision();
                        Answers();
                        break;
                    case GameStates.GameState.Mixed:
                        int proccessId = Random.Range(0, 4);
                        switch (proccessId)
                        {
                            case 0:
                                proccess.text = "+";
                                QuestionPlus();
                                Answers();
                                break;
                            case 1:
                                proccess.text = "-";
                                QuestionMinus();
                                Answers();
                                break;
                            case 2:
                                proccess.text = "x";
                                QusetionMultiplication();
                                Answers();
                                break;
                            case 3:
                                proccess.text = "/";
                                QuestionDivision();
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
                        break;
                    case GameStates.GameState.Minus:
                        break;
                    case GameStates.GameState.Multiplication:
                        break;
                    case GameStates.GameState.Division:
                        break;
                    case GameStates.GameState.Mixed:
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
    void QuestionPlus()
    {
        proccess.text = "+";
        answer = Random.Range(((questionNumber - 1) * 10) + 1, (questionNumber * 10) + 1);
        firstNumber = Random.Range(0, answer);
        firstQuestion.text = firstNumber.ToString();
        secondNumber = answer - firstNumber;
        secondQuestion.text = secondNumber.ToString();
    }
    void QuestionMinus()
    {
        proccess.text = "-";
        answer = Random.Range(((questionNumber - 1) * 10) + 1, (questionNumber * 10) + 1);
        firstNumber = Random.Range(answer, answer + 100);
        firstQuestion.text = firstNumber.ToString();
        secondNumber = firstNumber - answer;
        secondQuestion.text = secondNumber.ToString();
    }
    void QusetionMultiplication()
    {
        proccess.text = "x";
        answer = Random.Range(((questionNumber - 1) * 10) + 1, (questionNumber * 10) + 1);
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
    void QuestionDivision()
    {
        proccess.text = "/";
        answer = Random.Range(((questionNumber - 1) * 10) + 1, (questionNumber * 10) + 1);
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

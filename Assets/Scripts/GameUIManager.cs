using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI firstQuestion, secondQuestion, proccess;
    [SerializeField] TMP_InputField min, max;
    [SerializeField] TMP_Dropdown time;
    [SerializeField] GameStates gameStates;
    Button aBtn, bBtn, cBtn, dBtn;
    int firstNumber, secondNumber, questionNumber = 1;
    int answer, answerId;
    bool minor;
    [SerializeField] List<TextMeshProUGUI> answersText;
    List<TextMeshProUGUI> copyAnswersText;
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
        
    }
    void Update()
    {
        
    }
    void QuestionAndAnswers()
    {
        switch (gameStates.randomState)
        {
            case GameStates.RandomState.Random:
                switch (gameStates.gameState)
                {
                    case GameStates.GameState.Plus:
                        firstNumber = Random.Range(0, questionNumber * 10);
                        firstQuestion.text = firstNumber.ToString();
                        secondNumber = Random.Range(0, questionNumber * 10);
                        secondQuestion.text = secondNumber.ToString();
                        answerId = Random.Range(0, answersText.Count);
                        answer = firstNumber + secondNumber;
                        answersText[answerId].text = answer.ToString();
                        answersText.RemoveAt(answerId);
                        minor = Random.Range(0, 2) == 0;
                        if (minor)
                        {
                            answer = Random.Range(0, firstNumber + secondNumber);
                        }
                        else
                        {
                            answer = Random.Range(firstNumber + secondNumber, questionNumber * 20);
                        }
                        answersText[0].text = answer.ToString();
                        minor = Random.Range(0, 2) == 0;
                        if (minor)
                        {
                            answer = Random.Range(0, firstNumber + secondNumber);
                        }
                        else
                        {
                            answer = Random.Range(firstNumber + secondNumber, questionNumber * 20);
                        }
                        answersText[1].text = answer.ToString();
                        minor = Random.Range(0, 2) == 0;
                        if (minor)
                        {
                            answer = Random.Range(0, firstNumber + secondNumber);
                        }
                        else
                        {
                            answer = Random.Range(firstNumber + secondNumber, questionNumber * 20);
                        }
                        answersText[2].text = answer.ToString();
                        answersText.Clear();
                        answersText.AddRange(copyAnswersText);
                        break;
                    case GameStates.GameState.Minus:
                        break;
                    case GameStates.GameState.Multiplication:
                        break;
                    case GameStates.GameState.Division:
                        break;
                    case GameStates.GameState.Exponent:
                        break;
                    case GameStates.GameState.SquareRoot:
                        break;
                    case GameStates.GameState.Mixed:
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
                    case GameStates.GameState.Exponent:
                        break;
                    case GameStates.GameState.SquareRoot:
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
}

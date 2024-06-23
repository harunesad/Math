using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI firstQuestion, secondQuestion, proccess, timeText, countdownText;
    public TextMeshProUGUI coinText;
    [SerializeField] TMP_Dropdown time;
    public TMP_Dropdown min, max;
    [SerializeField] GameStates gameStates;
    public GameObject finishPanel;
    Button aBtn, bBtn, cBtn, dBtn, trueAnswerBtn, answerBtn;
    int firstNumber, secondNumber, questionNumber = 1;
    int answer = -1, answerFirst = -1, answerSecond = -1, answerThird = -1, answerId;
    float timeCount, countdown, coin, superCoin;
    bool countdownStart, click;
    [SerializeField] List<TextMeshProUGUI> answersText, copyAnswersText;
    private void Awake()
    {
        aBtn = answersText[0].GetComponentInParent<Button>();
        bBtn = answersText[1].GetComponentInParent<Button>();
        cBtn = answersText[2].GetComponentInParent<Button>();
        dBtn = answersText[3].GetComponentInParent<Button>();
        aBtn.onClick.AddListener(delegate { TrueOrFalse(aBtn); });
        bBtn.onClick.AddListener(delegate { TrueOrFalse(bBtn); });
        cBtn.onClick.AddListener(delegate { TrueOrFalse(cBtn); });
        dBtn.onClick.AddListener(delegate { TrueOrFalse(dBtn); });
        copyAnswersText.AddRange(answersText);
    }
    void Start()
    {
        //coinText.text = gameCoin.ToString();
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
            countdownStart = false;
            countdownText.transform.parent.gameObject.SetActive(false);
        }
        if (countdown <= 0 && timeCount > 0)
        {
            timeCount -= Time.deltaTime;
            timeText.text = ((int)timeCount).ToString();
        }
        if (timeCount <= 0 || (gameStates.randomState == GameStates.RandomState.Input && ((questionNumber - 1) * 10) + 1 + 100 * min.value >= 100 * (max.value + 1)))
        {
            finishPanel.SetActive(true);
            click = true;
            timeCount = 0;
            timeText.text = ((int)timeCount).ToString();
        }
    }
    public void TimeStart(bool input)
    {
        if (!input)
        {
            timeCount = 10 + JsonSave.jsonSave.sv.extraTime;
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
    void TrueOrFalse(Button button)
    {
        if (click)
        {
            return;
        }
        answerBtn = button;
        string options = proccess.text;
        switch (options)
        {
            case "+":
                Proccesses(firstNumber + secondNumber == int.Parse(button.GetComponentInChildren<TextMeshProUGUI>().text), button);
                break;
            case "-":
                Proccesses(firstNumber - secondNumber == int.Parse(button.GetComponentInChildren<TextMeshProUGUI>().text), button);
                break;
            case "x":
                Proccesses(firstNumber * secondNumber == int.Parse(button.GetComponentInChildren<TextMeshProUGUI>().text), button);
                break;
            case "/":
                Proccesses(firstNumber / secondNumber == int.Parse(button.GetComponentInChildren<TextMeshProUGUI>().text), button);
                break;
            default:
                break;
        }
        click = true;
        questionNumber++;
        if (timeCount <= 0 || (gameStates.randomState == GameStates.RandomState.Input && ((questionNumber - 1) * 10) + 1 + 100 * min.value >= 100 * (max.value + 1)))
        {
            return;
        }
        StartCoroutine(NewQuestion());
    }
    void Proccesses(bool state, Button button)
    {
        if (state)
        {
            button.GetComponent<Image>().DOColor(Color.green, .75f);
            if (gameStates.randomState == GameStates.RandomState.Random)
            {
                superCoin++;
                if (superCoin == 4)
                {
                    superCoin = 0;
                    coin += 20;
                }
                else
                {
                    coin += 10;
                }
                coinText.text = (coin + JsonSave.jsonSave.sv.gameCoin).ToString();
            }
        }
        else
        {
            button.GetComponent<Image>().DOColor(Color.red, .75f);
            trueAnswerBtn.GetComponent<Image>().DOColor(Color.green, .75f);
            superCoin = 0;
        }
    }
    IEnumerator NewQuestion()
    {
        yield return new WaitForSeconds(1);
        QuestionAndAnswers();
    }
    public void QuestionAndAnswers()
    {
        click = false;
        if (trueAnswerBtn && answerBtn)
        {
            trueAnswerBtn.GetComponent<Image>().color = Color.white;
            answerBtn.GetComponent<Image>().color = Color.white;
        }
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
        trueAnswerBtn = answersText[answerId].GetComponentInParent<Button>();
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
    public bool CoinReset()
    {
        Debug.Log(coin);
        if (timeCount == 0)
        {
            JsonSave.jsonSave.ResultSave(coin);
            superCoin = 0;
            //gameCoin += coin;
            coin = 0;
            //coinText.text = gameCoin.ToString();
            JsonSave.jsonSave.GameCoinShowing(coinText);
            questionNumber = 1;
            return false;
        }
        else if (gameStates.randomState == GameStates.RandomState.Input && ((questionNumber - 1) * 10) + 1 + 100 * min.value >= 100 * (max.value + 1))
        {
            //superCoin = 0;
            //gameCoin += coin;
            //coin = 0;
            //coinText.text = gameCoin.ToString();
            questionNumber = 1;
            return false;
        }
        superCoin = 0;
        coin = 0;
        //coinText.text = gameCoin.ToString();
        JsonSave.jsonSave.GameCoinShowing(coinText);
        questionNumber = 1;
        return true;
    }
}

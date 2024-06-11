using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public RandomState randomState;
    public enum RandomState
    {
        Random,
        Input
    }
    public GameState gameState;
    public enum GameState
    {
        Plus,
        Minus,
        Multiplication,
        Division,
        Mixed
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

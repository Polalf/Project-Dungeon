using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager: MonoBehaviour
{
    public static TurnManager turnManagerInstance;
    // Enums
    public enum Turn { Player, Enemy }

    // Events
    public static event System.Action<Turn> OnTurnChange;

    // Properties
    private static Turn turn;

    // Methods
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Awake()
    {
        if(turnManagerInstance == null)
        {
            turnManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (turnManagerInstance != this) Destroy(gameObject);
        }
    }
    private void Start()
    {
        turn = Turn.Player;
        OnTurnChange?.Invoke(turn);
    }

    /// <summary>
    /// End the turn of the game.
    /// </summary>
    public static void EndTurn()
    {
        turn = turn == Turn.Player ? Turn.Enemy : Turn.Player;
        OnTurnChange?.Invoke(turn);
    }
}

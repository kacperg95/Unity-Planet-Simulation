using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasa określająca stan w jakim znajduje się gra
/// </summary>
public static class Game {


    public static GameState State //Stan gry
    {
        get
        {
            return state;
        }
    }

    private static GameState state;

    /// <summary>
    /// Ustawienie stanu gry
    /// </summary>
    /// <param name="gameState"></param>
    public static void SetState(GameState gameState)
    {
        state = gameState;
    }

}

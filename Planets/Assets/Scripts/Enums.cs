using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Tryby menu zależne od tego jaką czynność wykonujemy w danej chwili
/// </summary>
public enum MenuState
{
    Free,
    PlanetEdit,
    Distance,
    Simulation,
}

public enum CameraState
{
    Free,
    Focusing,
    Unfocusing,
    Focused,
}

public enum GameState
{
    Creative,
    Simulation,
    Paused,
    Spaceship
}

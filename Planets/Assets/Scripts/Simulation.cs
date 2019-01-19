using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Simulation {

    /// <summary>
    /// Rozpoczyna symulacje
    /// </summary>
    public static void Start()
    {
        Game.SetState(GameState.Simulation);
        PlanetColliders.Enable(true);
        PlanetForce.PushPlanets();
        MenuSwitcher.Switch(MenuState.Simulation);
    }

    /// <summary>
    /// Zatrzymuje symulacje
    /// </summary>
    public static void Stop()
    {
        PlanetPositioner.ResetPlanetsPosition();
        PlanetForce.StopPlanets();
        Game.SetState(GameState.Creative);
        PlanetColliders.Enable(false);
        MenuSwitcher.Switch(MenuState.Free);
    }

    /// <summary>
    /// Pauzuje lub wznawia symulację planet
    /// </summary>
    public static void ResumeOrPause()
    {
        if (Game.State == GameState.Simulation)
        {
            Game.SetState(GameState.Paused); ;
            PlanetData.SaveVelocities();
            PlanetForce.StopPlanets();
        }

        else if (Game.State == GameState.Paused)
        {
            Game.SetState(GameState.Simulation);
            PlanetForce.ResumePlanets();
        }

    }
}

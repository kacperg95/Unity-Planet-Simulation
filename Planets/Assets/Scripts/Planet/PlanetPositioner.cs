using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Klasa ustawiająca planety na konkretnych pozycjach
/// </summary>
public static class PlanetPositioner
{

    /// <summary>
    /// Ustawienie rotacji planety
    /// </summary>
    /// <param name="planet">Planeta</param>
    /// <param name="rotation">Rotacja</param>
    public static void SetRotation(IMovingPlanet planet, Quaternion rotation)
    {
        planet.transform.rotation = rotation;
    }

    /// <summary>
    /// Ustawienie pozycji planety
    /// </summary>
    /// <param name="planet"></param>
    /// <param name="pos"></param>
    public static void SetPosition(IPlanet planet, float pos)
    {
        planet.transform.position = new Vector3(pos, 0, 0);
    }

    /// <summary>
    /// Resetuje pozycje wszystkich planet ustawiając je na układzie słonecznym
    /// </summary>
    public static void ResetPlanetsPosition()
    {
        foreach(IPlanet planet in PlanetData.Planets)
            SetPosition(planet, planet.SunDistance);
    }



    /// <summary>
    /// Resetuje rotację planety do poprzedniej
    /// </summary>
    public static void ResetRotation(IMovingPlanet planet)
    {
        planet.transform.rotation = PlanetData.previousPlanetRotation;
    }


}

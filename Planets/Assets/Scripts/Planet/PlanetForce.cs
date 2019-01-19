using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Odpowiada za wprawianie planet w ruch
/// </summary>
public static class PlanetForce  {

    /// <summary>
    /// Popycha planetę o zadany wektor
    /// </summary>
    /// <param name="planet"></param>
    /// <param name="force"></param>
    public static void PushPlanet(IPlanet planet, Vector3 force)
    {
        planet.GetComponent<Rigidbody>().AddForce(force);
    }

    /// <summary>
    /// Wprawia wszystkie planety w ruch po rozpoczęciu symulacji
    /// </summary>
    public static void PushPlanets()
    {
        foreach (IPlanet planet in PlanetData.Planets)
        {
            if (planet is IMovingPlanet)
                planet.gameObject.GetComponent<Rigidbody>().AddForce(0, 0, planet.Speed, ForceMode.VelocityChange);
        }
    }

    /// <summary>
    /// Zatrzymuje planety w momencie gdy symulacja zostaje zatrzymana
    /// </summary>
    public static void StopPlanets()
    {
        foreach (IPlanet planet in PlanetData.Planets)
        {
            planet.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            planet.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

    }

    /// <summary>
    /// Wznawia ruch wszystkich planet
    /// </summary>
    public static void ResumePlanets()
    {
        foreach (IPlanet planet in PlanetData.Planets)
            planet.gameObject.GetComponent<Rigidbody>().AddForce(planet.PreviousPlanetVelocity, ForceMode.VelocityChange);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasa odpowiedzialna za przyciąganie grawitacyjne planet
/// </summary>
public static class PlanetAttractor {

    /// <summary>
    /// Metoda odpowiedzialna za przyciąganie planet do siebie
    /// </summary>
    /// <param name="callingPlanet">Planeta z poziomu której wywoływana jest ta metoda</param>
    public static void AttractPlanets(IPlanet callingPlanet)
    {
        foreach(IPlanet planet in PlanetData.Planets)
        {
            if(planet != callingPlanet)
            {
                Vector3 direction = GetPlanetDirection(callingPlanet, planet);
                float distance = GetDistanceBetweenPlanets(direction);

                if (distance == 0) return;

                float forceMagnitude = GetForceMagnitude(callingPlanet, planet, distance);
                Vector3 force = GetForceVector(direction, forceMagnitude);

                PlanetForce.PushPlanet(planet, force);
            }
        }
    }

    #region Metody prywatne

    private static Vector3 GetPlanetDirection(IPlanet p1, IPlanet p2)
    {
        Vector3 direction = p1.transform.position - p2.transform.position;
        return direction;
    }

    private static float GetDistanceBetweenPlanets(Vector3 direction)
    {
        return direction.magnitude;
    }

    private static float GetForceMagnitude(IPlanet p1, IPlanet p2, float distance)
    {
        return (p1.Mass * p2.Mass) / Mathf.Pow(distance, 2);
    }

    private static Vector3 GetForceVector(Vector3 direction, float forceMagnitude)
    {
        Vector3 force = direction.normalized * forceMagnitude;
        return force;
    }

    #endregion


}

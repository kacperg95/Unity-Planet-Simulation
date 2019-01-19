using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Klasa zarządzająca colliderami planet
/// </summary>
public static class PlanetColliders {

    /// <summary>
    /// Włącza/wygłącza collidery we wszystkich planetach na scenie
    /// </summary>
    /// <param name="input"></param>
    public static void Enable(bool input)
    {
        foreach (IPlanet planet in PlanetData.Planets)
        {
            planet.GetComponent<SphereCollider>().isTrigger = !input;
        }
    }

}

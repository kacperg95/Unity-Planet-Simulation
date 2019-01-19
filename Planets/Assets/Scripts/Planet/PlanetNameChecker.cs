using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class PlanetNameChecker {


    private static System.Random rand = new System.Random();

    /// <summary>
    /// Sprawdza czy nazwa planety już nie istnieje. Jeżeli tak, zmienia nazwę
    /// </summary>
    /// <param name="name"></param>
    public static string Check(string name)
    {
        if (PlanetData.Planets.Count(x => x.PlanetName.Trim() == name.Trim()) > 1)
        {
            return string.Format("{0} ({1}{2}{3})", name, rand.Next(10), rand.Next(10), rand.Next(10));
        }

        return name;
    }

}



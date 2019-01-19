using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Zarządzanie teksturami planet
/// </summary>
public static class PlanetMaterial
{
    public static void Set(IMovingPlanet planet, string materialName)
    {
        MeshRenderer mesh = planet.gameObject.GetComponent<MeshRenderer>();
        string materialPath = string.Format("PlanetMaterials\\{0}", materialName);
        mesh.material = Resources.Load(materialPath) as Material;
    }
}

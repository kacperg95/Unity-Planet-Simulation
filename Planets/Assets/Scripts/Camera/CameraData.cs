using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stałe informacje dla kamery
/// </summary>
public static class CameraData  {

    //Prędkość kamery w trybie lotu
    public static float MovingSpeed = 7f;
    public static float RotationSpeed = 7f;

    //Prędkość kamery w trybie poruszania
    public static float Sensitivity = 100f;
    public static float Speed = 5.0f;

    public static float MarginOfError = 0.03f; //Margines błędu określający jak blisko targetu musi dotrzeć kamera
                                               //Bez tego kamera leci zdecydowanie za długo

    //Współrzędne kamery na układ słoneczny
    public static Vector3 SolarSystemPosition = new Vector3(0, 160f, 0);
    public static Quaternion SolarSystemRotation = Quaternion.Euler(90, 0, 0);

    //Współrzędne kamery na ustawiania planet
    public static Vector3 PlanetsViewPosition = new Vector3(100f, 102.3852f, 0);
    public static Quaternion PlanetsViewRotation = Quaternion.Euler(90, 0, 0);


    //Współrzędne kamery na przycisk reset
    public static Vector3 ResetPosition = new Vector3(2.1f, 2.6f, -31.2f);
    public static Quaternion ResetRotation = Quaternion.Euler(10.271f, 37.358f, 0f);
}

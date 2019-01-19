using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Odpowiedzialna za skupianie kamery na konkretnym obiekcie lub w konkretnym miejscu
/// </summary>
public static class CameraFocus
{
    private static CameraScript camera = Factory.GetCamera();

    private static Quaternion lookAtDirection; //Wektor patrzący w kierunku, w którym powinna patrzyć kamera
    private static Vector3 newCameraPosition; //Nowa pozycja dla kamery
    private static Vector3 lookAtPoint; //Punkt w który ma się patrzeć kamera

    private static Vector3 previousCameraPosition; //Poprzednia pozycja kamery
    private static Quaternion previousCameraRotation; //Poprzednia rotacja kamery


    /// <summary>
    /// Lot swobodny kamery kiedy jest focusowana
    /// </summary>
    public static void Focusing()
    {
        //Interpolacyjna zmiana rotacji kamery w kierunku obiektu
        camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, lookAtDirection, CameraData.RotationSpeed * Time.deltaTime);

        //Interpolacyjna zmiana pozycji kamery w kierunku obiektu
        camera.transform.position = Vector3.Slerp(camera.transform.position, newCameraPosition, CameraData.MovingSpeed * Time.deltaTime);

        //Jeżeli kamera dotarła do celu, ustawiamy zmienną na false
        if (camera.CameraReachedTarget(camera.transform.position, newCameraPosition, camera.transform.rotation, lookAtDirection))
        {
            if (camera.State == CameraState.Focusing)
                camera.State = CameraState.Focused;
            else if (camera.State == CameraState.Unfocusing)
                camera.State = CameraState.Free;
        }
    }


    /// <summary>
    /// Spojrzenie kamery na planetę
    /// </summary>
    /// <param name="transform">Wybrany obiekt</param>
    /// <param name="saveCameraPosition">Określa czy zapisać pozycję kamery, którą później będzie można wykorzystać do powrotu</param>
    public static void OnPlanet(IMovingPlanet planet, bool saveCameraPosition)
    {
        //if (state != CameraState.Free)
        //     return;

        PlanetData.SelectedPlanet = (Planet)planet;

        camera.State = CameraState.Focusing;

        //Zapisanie poprzedniej pozycji i rotacji kamery
        if (saveCameraPosition)
        {
            previousCameraPosition = camera.transform.position;
            previousCameraRotation = camera.transform.rotation;
        }

        //Zapisanie rotacji planety
        PlanetData.SaveRotation(planet);

        //Ustawienie planety w taki sposób, aby "patrzyła się" na kamerę. Dzięki temu łatwo poprowadzić od niej potrzebne wektory
        planet.transform.LookAt(camera.transform);

        //Określenie wielkośći planety
        float planetSize = planet.transform.localScale.x;

        //Punkt do którego kamera ma dotrzeć
        newCameraPosition = planet.transform.position + (planet.transform.forward * planetSize * 2);

        //Punkt na który kamera ma spojrzeć
        lookAtPoint = planet.transform.position - (planet.transform.right * planetSize);

        //Rotacja kamery obliczona na podstawie wektora między punktem dotarcia a punktem patrzenia
        lookAtDirection = Quaternion.LookRotation(lookAtPoint - newCameraPosition);

        //Przywrócenie poprzedniej rotacji planety
        PlanetPositioner.ResetRotation(planet);

        //Wyświetl menu edycji planety
        MenuSwitcher.Switch(MenuState.PlanetEdit);
    }


    /// <summary>
    /// Funkcja ustawia widok na układ planet
    /// </summary>
    public static void OnPlanetsView()
    {
        camera.State = CameraState.Focusing;
        newCameraPosition = CameraData.PlanetsViewPosition;
        lookAtDirection = CameraData.PlanetsViewRotation;
    }

    /// <summary>
    /// Funkcja ustawia widok kamery na układ słoneczny
    /// </summary>
    public static void OnSolarSystem()
    {
        camera.transform.position = CameraData.SolarSystemPosition;
        camera.transform.rotation = CameraData.SolarSystemRotation;
    }

    /// <summary>
    /// Funkcja odfocusowuje kamere po tym jak zostałą skupiona na planecie
    /// </summary>
    public static void Unfocus()
    {
        camera.State = CameraState.Unfocusing;

        newCameraPosition = previousCameraPosition;
        lookAtDirection = previousCameraRotation;

        Factory.GetUIController().DeselectPlanetDropDown();
        MenuSwitcher.Switch(MenuState.Free);

        //Jeżeli planeta dopiero co zostala stworzona i nie została ustawiona na układzie słonecznym, to zostaje usunięta
        if (PlanetData.SelectedPlanet != null && PlanetData.SelectedPlanet.SunDistance == 220f)
            PlanetManager.Destroy(PlanetData.SelectedPlanet);

    }

    /// <summary>
    /// Funkcja resetuje widok kamery
    /// </summary>
    public static void Reset()
    {
        camera.transform.position = CameraData.ResetPosition;
        camera.transform.rotation = CameraData.ResetRotation;
    }

}

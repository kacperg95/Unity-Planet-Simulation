using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasa do przełączania menu
/// </summary>
public static class MenuSwitcher {

    private static UIController UI = Factory.GetUIController();

    /// <summary>
    /// Przełączanie menu
    /// </summary>
    /// <param name="menuState"></param>
    public static void Switch(MenuState state)
    {
        UIController.menuState = state;
        switch (UIController.menuState)
        {
            case MenuState.Free:
                ShowMainUI();
                ShowStartStopMenu();
                ShowSaveMenu();
                HidePlanetMenu();
                HideDistanceMenu();
                UI.RefreshSelectPlanetDropdown();
                break;

            case MenuState.PlanetEdit:
                HideShowStartMenu();
                HideMainUI();
                HideSaveMenu();
                ShowPlanetMenu();
                break;

            case MenuState.Distance:
                HidePlanetMenu();
                ShowDistanceMenu();
                HideMainUI();
                break;

            case MenuState.Simulation:
                ShowStartStopMenu();
                HideMainUI();
                HideSaveMenu();
                break;

        }

    }

    #region Włączanie/wyłączanie menu

    private static void ShowMainUI()
    {
        UI.MainUI.enabled = true;
    }

    private static void HideMainUI()
    {
        UI.MainUI.enabled = false;
    }

    private static void ShowStartStopMenu()
    {
        UI.StartStopMenu.enabled = true;
    }

    private static void HideShowStartMenu()
    {
        UI.StartStopMenu.enabled = false;
    }

    private static void ShowPlanetMenu()
    {

        UI.PlanetMenuNameInput.text = PlanetData.SelectedPlanet.PlanetName;
        UI.PlanetMenuMassSlider.value = PlanetData.SelectedPlanet.Mass;
        UI.PlanetMenuSpeedSlider.value = PlanetData.SelectedPlanet.Speed;
        UI.PlanetMenuModelDropdown.value = 0;

        UI.PlanetMenu.enabled = true;
    }

    private static void HidePlanetMenu()
    {
        UI.PlanetMenu.enabled = false;
        UI.RefreshSelectPlanetDropdown();
    }

    private static void ShowDistanceMenu()
    {
        UI.DistanceMenu.enabled = true;
        UI.DistanceMenuDistanceSlider.enabled = true;
        UI.DistanceMenuSizeSlider.enabled = true;
        UI.DistanceMenuDistanceSlider.value = PlanetData.SelectedPlanet.SunDistance;
        UI.DistanceMenuSizeSlider.value = PlanetData.SelectedPlanet.Size;
    }

    private static void HideDistanceMenu()
    {
        UI.DistanceMenu.enabled = false;
        UI.DistanceMenuDistanceSlider.enabled = false;
        UI.DistanceMenuSizeSlider.enabled = false;
    }

    private static void ShowSaveMenu()
    {
        UI.SaveMenu.enabled = true;
    }

    private static void HideSaveMenu()
    {
        UI.SaveMenu.enabled = false;
    }

    #endregion

}

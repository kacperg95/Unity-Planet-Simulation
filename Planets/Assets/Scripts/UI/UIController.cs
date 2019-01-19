using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIController : MonoBehaviour {

    #region Zmienne

    //Stan menu
    public static MenuState menuState;

    #region Canvasy
    public Canvas MainUI;
    public Canvas PlanetMenu;
    public Canvas DistanceMenu;
    public Canvas StartStopMenu;
    public Canvas SaveMenu;
    #endregion

    #region MainUI
    public Button AddPlanetButton;
    public Button SolarViewButton;
    public Button ResetViewButton;
    public Dropdown SelectPlanetDropdown;
    private List<string> selectPlanetList = new List<string>();
    #endregion

    #region StopStartMenu
    public Button StartButton;
    public Button PauseButton;
    #endregion

    #region PlanetMenu
    public InputField PlanetMenuNameInput;
    public Slider PlanetMenuMassSlider;
    public Slider PlanetMenuSpeedSlider;
    public Dropdown PlanetMenuModelDropdown;
    public Button PlanetMenuSaveButton;
    public Button PlanetMenuDeleteButton;
    #endregion
    
    #region DistanceMenu
    public Slider DistanceMenuDistanceSlider;
    public Slider DistanceMenuSizeSlider;
    public Button DistanceMenuAddButton;
    #endregion

    #region SaveMenu
    public InputField SaveMenuInput;
    public Button SaveMenuSaveButton;
    public Button SaveMenuLoadButton;
    #endregion

    #endregion

    // Use this for initialization
    void Start () {


        #region Listenery

        AddPlanetButton.onClick.AddListener(AddPlanetButtonClick);
        SelectPlanetDropdown.onValueChanged.AddListener(SelectPlanetDropdownValueChange);
        SolarViewButton.onClick.AddListener(SolarViewButtonClick);
        ResetViewButton.onClick.AddListener(ResetViewButtonClick);

        PlanetMenuSaveButton.onClick.AddListener(PlanetMenuSaveButtonClick);
        PlanetMenuDeleteButton.onClick.AddListener(PlanetMenuDeleteButtonClick);
        PlanetMenuModelDropdown.onValueChanged.AddListener(PlanetMenuModelDropDownValueChanged);

        StartButton.onClick.AddListener(StartButtonClick);
        PauseButton.onClick.AddListener(PauseButtonClick);

        DistanceMenuAddButton.onClick.AddListener(DistanceMenuAddButtonClick);
        DistanceMenuDistanceSlider.onValueChanged.AddListener(OnDistanceSliderValueChanged);
        DistanceMenuSizeSlider.onValueChanged.AddListener(OnSizeSliderValueChanged);

        SaveMenuSaveButton.onClick.AddListener(SaveMenuSaveButtonClick);
        SaveMenuLoadButton.onClick.AddListener(SaveMenuLoadButtonClick);

        #endregion

        //Model dropdown i nazwy modeli
        LoadModelDropdownTextures();

    }


    #region Buttony i slidery

    /// <summary>
    /// Dodawanie planety do układu słonecznego
    /// </summary>
    private void AddPlanetButtonClick()
    {
        IMovingPlanet planet = (Planet)PlanetManager.Create();
        CameraFocus.OnPlanet(planet, true);    
    }

    /// <summary>
    /// Startowanie i kończenie symulacji
    /// </summary>
    public void StartButtonClick()
    {
        if (Game.State == GameState.Creative)
        {
            Simulation.Start();
            StartButton.GetComponentInChildren<Text>().text = "Stop";
        }
        else if (Game.State == GameState.Simulation || Game.State == GameState.Paused)
        {
            Simulation.Stop();
            StartButton.GetComponentInChildren<Text>().text = "Start";

            //Trzeba uwzględnić możliwość zatrzymania symulacji w trakcie pauzy
            if (Game.State == GameState.Paused)
                PauseButton.GetComponentInChildren<Text>().text = "Pause";

        }

    }

    /// <summary>
    /// Zatrzymywanie i wznawianie sumulacji
    /// </summary>
    public void PauseButtonClick()
    {
        Simulation.ResumeOrPause();

        if (Game.State == GameState.Simulation)
            PauseButton.GetComponentInChildren<Text>().text = "Pause";
        else if (Game.State == GameState.Paused)
            PauseButton.GetComponentInChildren<Text>().text = "Resume";
    }

    /// <summary>
    /// Przejście na widok całego układu słonecznego
    /// </summary>
    public void SolarViewButtonClick()
    {
        if (Factory.GetCamera().State != CameraState.Free)
            return;
        CameraFocus.OnSolarSystem();
    }

    /// <summary>
    /// Zresetowanie widoku
    /// </summary>
    public void ResetViewButtonClick()
    {
        if (Factory.GetCamera().State != CameraState.Free)
            return;
        CameraFocus.Reset();
    }


    /// <summary>
    /// Zapisanie właściwości planety
    /// </summary>
    private void PlanetMenuSaveButtonClick()
    {
        PlanetData.SelectedPlanet.PlanetName = PlanetNameChecker.Check(PlanetMenuNameInput.text);
        PlanetData.SelectedPlanet.Mass = PlanetMenuMassSlider.value;
        PlanetData.SelectedPlanet.Speed = PlanetMenuSpeedSlider.value;
        PlanetPositioner.ResetRotation(PlanetData.SelectedPlanet);

        MenuSwitcher.Switch(MenuState.Distance);
        CameraFocus.OnPlanetsView();
    }

    /// <summary>
    /// Usunięcie planety z układu słonecznego
    /// </summary>
    private void PlanetMenuDeleteButtonClick()
    {
        PlanetManager.Destroy(PlanetData.SelectedPlanet);
        MenuSwitcher.Switch(MenuState.Free);
        CameraFocus.Unfocus();
        RefreshSelectPlanetDropdown();
    }

    /// <summary>
    /// Zapisanie pozycji i wielkości planety
    /// </summary>
    private void DistanceMenuAddButtonClick()
    {
        MenuSwitcher.Switch(MenuState.Free);
        CameraFocus.Unfocus();
    }

    private void OnSizeSliderValueChanged(float value)
    {
        PlanetData.SelectedPlanet.Size = value;
    }

    private void OnDistanceSliderValueChanged(float value)
    {
        PlanetData.SelectedPlanet.SunDistance = value;
    }


    /// <summary>
    /// Zapis układu planetarnego
    /// </summary>
    private void SaveMenuSaveButtonClick()
    {
        if (Game.State == GameState.Creative)
            SaveLoad.Save(SaveMenuInput.text);
    }

    /// <summary>
    /// Wczytanie układu planetarnego
    /// </summary>
    private void SaveMenuLoadButtonClick()
    {
        if (Game.State == GameState.Creative)
            SaveLoad.Load(SaveMenuInput.text);
    }

    #endregion

    #region PlanetDropdown

    private void SelectPlanetDropdownValueChange(int index)
    {
        //Odznaczenie planet
        if(index == 0)
        {
            CameraFocus.Unfocus();
            return;
        }

        //Zaznaczenie planety
        foreach (IMovingPlanet planet in PlanetData.Planets)
            if (planet.PlanetName == selectPlanetList[index])
            {
                CameraFocus.OnPlanet(planet, true);
                MenuSwitcher.Switch(MenuState.PlanetEdit);
                break;
            }
    }

    /// <summary>
    /// Odświeżenie dropdownu z nazwami planet
    /// </summary>
    public void RefreshSelectPlanetDropdown()
    {
        selectPlanetList = new List<string>();

        //Opcja w której nic nie zostało zaznaczone
        selectPlanetList.Add("None");

        foreach (var planet in PlanetData.Planets)
            selectPlanetList.Add(planet.PlanetName);

        SelectPlanetDropdown.ClearOptions();
        SelectPlanetDropdown.AddOptions(selectPlanetList);
    }

    /// <summary>
    /// Odznaczenie planety na dropdownie
    /// </summary>
    public void DeselectPlanetDropDown()
    {
        SelectPlanetDropdown.value = 0;
    }

    #endregion

    #region ModelDropdown
    
    /// <summary>
    /// Wczytywanie nazw tekstur do dropdownu
    /// </summary>
    private void LoadModelDropdownTextures()
    {
        PlanetMenuModelDropdown.AddOptions(GetMaterialNames());
    }

    private List<string> GetMaterialNames()
    {
        List<string> materialNames = new List<string>();
        Material[] materials = Resources.LoadAll("PlanetMaterials", typeof(Material)).Cast<Material>().ToArray();
        materialNames.Add(" "); //Jedno puste pole na początku
        foreach (Material m in materials)
            materialNames.Add(m.name);
        return materialNames;
    }

    private void PlanetMenuModelDropDownValueChanged(int index)
    {
        if (index == 0) return;

        PlanetData.SelectedPlanet.Material = PlanetMenuModelDropdown.options[index].text;
    }

    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Factory
{
    public static CameraScript GetCamera()
    {
        return GameObject.FindObjectOfType<Camera>().GetComponent<CameraScript>();
    }

    public static UIController GetUIController()
    {
        return GameObject.Find("EventSystem").GetComponent<UIController>();
    }


}

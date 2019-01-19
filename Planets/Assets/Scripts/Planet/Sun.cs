using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : BasePlanet, ISun {


    void Start()
    {
        Mass = 1000f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}

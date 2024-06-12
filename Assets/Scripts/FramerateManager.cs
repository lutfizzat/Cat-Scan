using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.Threading;

public class FrameRateManager : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
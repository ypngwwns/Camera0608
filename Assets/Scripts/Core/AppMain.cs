using System;
using UnityEngine;

public class AppMain : MonoBehaviour
{
     void Awake()
    {
        Debug.Log("AppMain.Awake");
        ActionCenter.Instance.Init();
    }
}
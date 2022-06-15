using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;


public abstract class ActionBase : MonoBehaviour 
{
    public abstract void Init();
    public abstract void RegisterAction();
    public abstract void RemoveAction();
   
}

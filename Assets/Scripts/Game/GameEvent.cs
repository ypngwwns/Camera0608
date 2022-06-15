using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvent
{
    //相机控制
    public static UnityAction StartCameraControl;
    public static UnityAction StopCameraControl;
    public static UnityAction<SceneType> SwitchScene;           //切换镜头
    public static UnityAction<Vector3> SwitchView;              //切换视角
    //UI页面控制
    public static UnityAction<PageType> OpenPage;
    public static UnityAction<PageType> ClosePage;
    public static UnityAction RefreshPage;

}

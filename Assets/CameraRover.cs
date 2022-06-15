using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CameraRover : ActionBase
{
    private bool isLookAt = false;                      //视角是否朝向目标
    private Vector3 targetPos = Vector3.zero;           //视角坐标

    private Vector3 viewPos = Vector3.zero;

    private string currentAnim = "";
    //private bool isRover = false;


    public override void Init()
    {
        currentAnim = "";
        isLookAt = false;
        //isRover = false;

    }

    public override void RegisterAction()
    {
        GameEvent.SwitchScene += SwitchScene;
        GameEvent.SwitchView += SwitchView;
    }

    public override void RemoveAction()
    {
        GameEvent.SwitchScene -= SwitchScene;
        GameEvent.SwitchView -= SwitchView;
    }

    void Start()
    {
        // if (GameCache.screenType == ScreenType.Screen_1)
        // {
        //     transform.position = new Vector3(20f, 180f, 136f);
        //     transform.eulerAngles = new Vector3(75f, 180f, 0f);
        // }
        // else if (GameCache.screenType == ScreenType.Screen_2)
        // {
        //     transform.position = new Vector3(-100f, 180f, -9f);
        //     transform.eulerAngles = new Vector3(78f, 265f, 0f);
        // }
        // else if (GameCache.screenType == ScreenType.Total)
        // {
        //     transform.position = new Vector3(-90f, 320f, 10f);
        //     transform.eulerAngles = new Vector3(90f, 180f, 0f);
        // }
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        //{
        //    if (isRover)
        //    {
        //        StopAutoRover();
        //    }
            
        //    CancelInvoke("StartAutoRover");
        //    Invoke("StartAutoRover", 15f);
        //}
    }

    void LateUpdate()
    {
        if (isLookAt)
        {
            transform.LookAt(targetPos);
        }
    }

    //切换镜头
    void SwitchScene(SceneType type)
    {
        //if (type == GameCache.currentScene)
        //{
        //    return;
        //}
        //离开当前场景
        // StopCoroutine(currentAnim);
        // isLookAt = false;
        // transform.DOKill();
        //切换镜头
        // GameCache.currentScene = type;
        // switch (type)
        // {
        //     case SceneType.Main:
        //         StartCoroutine("SwitchScene_Main");
        //         break;
        // }
    }

    //切换视角
    void SwitchView(Vector3 pos)
    {
        Debug.Log("SwitchView------" + pos);
        //离开视角
        isLookAt = false;
        transform.DOKill();
        //注视目标
        viewPos = pos;
        StopCoroutine("SwitchViewToPos");
        StartCoroutine("SwitchViewToPos");
    }

    //开始自动漫游
    //void StartAutoRover()
    //{
    //    StartCoroutine("SwitchScene_Main");
    //}

    //停止自动漫游
    //void StopAutoRover()
    //{
    //    isLookAt = false;
    //    isRover = false;
    //    transform.DOKill();
    //    StopCoroutine(currentAnim);
    //    ActionCenter.Instance.DoAction(GameEvent.StartCameraControl);
    //}




    //切换视角
    IEnumerator SwitchViewToPos()
    {
        yield return new WaitForEndOfFrame();
        ActionCenter.Instance.DoAction(GameEvent.StopCameraControl);
        transform.DOLookAt(viewPos, 1f);
        yield return new WaitForSeconds(1f);
        targetPos = viewPos;
        isLookAt = true;
        
        Vector3 dir = transform.position - viewPos;
        dir.y = 0;
        Vector3 cameraPos = viewPos + dir.normalized * 20f + new Vector3(0f, 12f, 0f);
        transform.DOMove(cameraPos, 1.5f);
        yield return new WaitForSeconds(1.5f);
        isLookAt = false;
        ActionCenter.Instance.DoAction(GameEvent.StartCameraControl);
    }

    //主场景
    // IEnumerator SwitchScene_Main()
    // {
    //     currentAnim = "SwitchScene_Main";
    //     ActionCenter.Instance.DoAction(GameEvent.StopCameraControl);
    //     yield return new WaitForEndOfFrame();
    //     if (GameCache.screenType == ScreenType.Screen_1)
    //     {
    //         transform.DOMove(new Vector3(5f, 75f, 195f), 2f);
    //         transform.DORotate(new Vector3(45f, 180f, 0f), 2f);
    //         yield return new WaitForSeconds(2f);
    //     }
    //     else if (GameCache.screenType == ScreenType.Screen_2)
    //     {
    //         transform.DOMove(new Vector3(-30f, 80f, 0f), 2f);
    //         transform.DORotate(new Vector3(45f, 265f, 0f), 2f);
    //         yield return new WaitForSeconds(2f);
    //     }
    //     else if (GameCache.screenType == ScreenType.Total)
    //     {
    //         transform.DOMove(new Vector3(-110f, 100f, 200f), 2f);
    //         transform.DORotate(new Vector3(36f, 160f, 0f), 2f);
    //         yield return new WaitForSeconds(2f);
    //     }
    //     yield return new WaitForEndOfFrame();
    //     ActionCenter.Instance.DoAction(GameEvent.OpenPage, PageType.Main);
    //     ActionCenter.Instance.DoAction(GameEvent.StartCameraControl);
    // }


    //漫游场景
    //IEnumerator SwitchScene_Main()
    //{
    //    isRover = true;
    //    currentAnim = "SwitchScene_Main";
    //    ActionCenter.Instance.DoAction(GameEvent.StopCameraControl);
    //    yield return new WaitForEndOfFrame();

    //    if (!isLookAt)
    //    {
    //        if (GameCache.screenType == ScreenType.Screen_1)
    //        {
    //            targetPos = new Vector3(32f, 0.7f, 85f);
    //        }
    //        else
    //        {
    //            targetPos = new Vector3(-145f, 0.7f, -15f);
    //        }
    //        transform.DOLookAt(targetPos, 1f);
    //        yield return new WaitForSeconds(1f);
    //        isLookAt = true;
    //    }


    //    float speed = 20f;
    //    if (GameCache.screenType == ScreenType.Screen_1)
    //    {
    //        float dis = (GameCache.path1[0] - transform.position).magnitude;
    //        float timer = dis / (speed * 3);
    //        transform.DOMove(GameCache.path1[0], timer);
    //        yield return new WaitForSeconds(timer);
    //        float pathDis = GetPathDis(GameCache.path1);
    //        float pathTimer = pathDis / speed;
    //        transform.DOPath(GameCache.path1, pathTimer, PathType.CatmullRom, PathMode.Full3D).SetEase(Ease.Linear);
    //        yield return new WaitForSeconds(pathTimer);
    //    }
    //    else if (GameCache.screenType == ScreenType.Screen_2)
    //    {
    //        float dis = (GameCache.path2[0] - transform.position).magnitude;
    //        float timer = dis / (speed * 3);
    //        transform.DOMove(GameCache.path2[0], timer);
    //        yield return new WaitForSeconds(timer);
    //        float pathDis = GetPathDis(GameCache.path2);
    //        float pathTimer = pathDis / speed;
    //        transform.DOPath(GameCache.path2, pathTimer, PathType.CatmullRom, PathMode.Full3D).SetEase(Ease.Linear);
    //        yield return new WaitForSeconds(pathTimer);
    //    }
    //    yield return new WaitForEndOfFrame();
    //    ActionCenter.Instance.DoAction(GameEvent.StartCameraControl);
    //    //递归
    //    StartCoroutine("SwitchScene_Main");
    //}



    //float GetPathDis(Vector3[] path)
    //{
    //    float dis = 0f;
    //    if (path.Length > 0)
    //    {
    //        for (int i = 1; i < path.Length; i++)
    //        {
    //            dis += Vector3.Distance(path[i], path[i - 1]);
    //        }
    //        dis += Vector3.Distance(path[0], path[path.Length - 1]);
    //    }
    //    return dis;
    //}

}


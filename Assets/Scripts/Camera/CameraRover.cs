using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CameraRover : ActionBase
{
    private bool isLookAt = false; //视角是否朝向目标
    private Vector3 targetPos = Vector3.zero; //视角坐标

    private Vector3 viewPos = Vector3.zero;

    private string currentAnim = "";


    public override void Init()
    {
        currentAnim = "";
        isLookAt = false;
    }

    public override void RegisterAction()
    {
        GameEvent.SwitchView += SwitchView;
    }

    public override void RemoveAction()
    {
        GameEvent.SwitchView -= SwitchView;
    }

    void LateUpdate()
    {
        if (isLookAt)
        {
            transform.LookAt(targetPos);
        }
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
        dir.y = viewPos.y;
        Vector3 cameraPos = viewPos + dir.normalized * 20f + new Vector3(0f, 12f, 0f);
        transform.DOMove(cameraPos, 1.5f);
        yield return new WaitForSeconds(1.5f);
        isLookAt = false;
        ActionCenter.Instance.DoAction(GameEvent.StartCameraControl, targetPos);
    }
}
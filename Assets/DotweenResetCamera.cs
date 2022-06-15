using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DotweenResetCamera : MonoBehaviour
{
    public Camera camera;
    public Transform 位置1;
    public Transform 位置2;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void 位置1视角()
    {
        // StartCoroutine(SwitchPos(位置1));
        // camera.transform.DOLookAt(位置1.position, 2);
        ActionCenter.Instance.DoAction(GameEvent.SwitchView, 位置1.position);
        // ActionCenter.Instance.DoAction();
    }

    public void 位置2视角()
    {
        ActionCenter.Instance.DoAction(GameEvent.SwitchView, 位置2.position);
        // StartCoroutine(SwitchPos(位置2));
    }

    private IEnumerator SwitchPos(Transform target)
    {
        yield return new WaitForEndOfFrame();
        // ActionCenter.Instance.DoAction(GameEvent.StopCameraControl);
        camera.transform.DOLookAt(target.position, 1f);
        yield return new WaitForSeconds(1f);
        // targetPos = viewPos;
        // isLookAt = true;

        Vector3 dir =  camera.transform.position - target.position;
        Vector3 cameraPos = target.position + dir.normalized * 5f;
        camera.transform.DOMove(cameraPos, 1.5f);
        yield return new WaitForSeconds(1.5f);
        // isLookAt = false;
        // ActionCenter.Instance.DoAction(GameEvent.StartCameraControl);
        // camera.transform.DOLookAt(位置2.position, 2);
    }
}

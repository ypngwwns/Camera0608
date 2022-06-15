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
        camera.transform.DOLookAt(位置1.position, 2);
    }

    public void 位置2视角()
    {
        camera.transform.DOLookAt(位置2.position, 2);
    }
}

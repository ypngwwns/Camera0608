using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class ResetMaxCamera : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera1;
    public CinemachineVirtualCamera cinemachineVirtualCamera2;
    
    
    // Start is called before the first frame update
    void Start()
    {
        位置1视角();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void 位置1视角()
    {
        cinemachineVirtualCamera1.Priority = 100;
        cinemachineVirtualCamera2.Priority = 10;
    }

    public void 位置2视角()
    {
        cinemachineVirtualCamera1.Priority = 10;
        cinemachineVirtualCamera2.Priority = 100;
    }
}

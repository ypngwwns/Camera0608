using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class ResetMaxCamera : MonoBehaviour
{
    public GameObject cinemachineVirtualCamera1;
    public GameObject cinemachineVirtualCamera2;
    
    
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
        cinemachineVirtualCamera1.GetComponent<CinemachineVirtualCamera>().Priority = 100;
        cinemachineVirtualCamera1.GetComponent<MaxCamera>().enabled = true;
        cinemachineVirtualCamera2.GetComponent<CinemachineVirtualCamera>().Priority = 10;
        cinemachineVirtualCamera2.GetComponent<MaxCamera>().enabled = false;
    }

    public void 位置2视角()
    {
        cinemachineVirtualCamera1.GetComponent<CinemachineVirtualCamera>().Priority = 100;
        cinemachineVirtualCamera1.GetComponent<MaxCamera>().enabled = false;
        cinemachineVirtualCamera2.GetComponent<CinemachineVirtualCamera>().Priority = 100;
        cinemachineVirtualCamera2.GetComponent<MaxCamera>().enabled = true;
    }
}

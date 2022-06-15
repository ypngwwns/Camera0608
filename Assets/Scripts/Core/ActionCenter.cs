using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class ActionCenter : MonoBehaviour
{
    private static ActionCenter _instance;
    public static ActionCenter Instance
    {
        get
        {
            if (_instance == null)
            {
                string name = "ActionCenter";
                GameObject obj = GameObject.Find(name);
                if (obj == null)
                {
                    obj = new GameObject(name);
                    _instance = obj.AddComponent<ActionCenter>();
                }
                else
                {
                    _instance = obj.GetComponent<ActionCenter>();
                    if (_instance == null)
                    {
                        _instance = obj.AddComponent<ActionCenter>();
                    }
                }
            }
            return _instance;
        }
    }

    private List<ActionBase> actionList = new List<ActionBase>();

    //初始化，场景加载完成后调用
    public void Init()
    {
        ActionBase[] actionArr = Resources.FindObjectsOfTypeAll<ActionBase>();
        Debug.Log("actionArr.Length：" + actionArr.Length);
        for (int i = 0; i < actionArr.Length; i++)
        { 
            if (actionArr[i].gameObject.scene.buildIndex >= 0)
            {
                actionArr[i].Init();
                actionArr[i].RegisterAction();
                actionList.Add(actionArr[i]);
            }
        }
    }

    public void AddAction(ActionBase action)
    {
        actionList.Add(action);
        action.Init();
        action.RegisterAction();
    }

    public void RemoveAction(ActionBase action)
    {
        if (actionList.Contains(action))
        {
            actionList.Remove(action);
        }
    }

    void OnDestroy()
    {
        for (int i = 0; i < actionList.Count; i++)
        {
            actionList[i].RemoveAction();
        }
    }

    /// <summary>
    /// 执行事件
    /// </summary>
    public void DoAction(UnityAction action)
    {
        if (action != null)
        {
            action();
        }
    }

    /// <summary>
    /// 执行事件
    /// </summary>
    public void DoAction<T>(UnityAction<T> action, T t)
    {
        if (action != null)
        {
            action.Invoke(t);
        }
    }



    /// <summary>
    /// delay秒后，执行事件
    /// </summary>
    public void DoActionDelay(UnityAction action, float delay)
    {
        StartCoroutine(DoActionDelayCor(action, delay));
    }

    IEnumerator DoActionDelayCor(UnityAction action, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (action != null)
        {
            action();
        }
    }



    /// <summary>
    /// delay秒后，执行事件
    /// </summary>
    public void DoActionDelay<T>(UnityAction<T> action, float delay, T t)
    {
        StartCoroutine(DoActionDelayCor(action, delay, t));
    }

    IEnumerator DoActionDelayCor<T>(UnityAction<T> action, float delay, T t)
    {
        yield return new WaitForSeconds(delay);
        if (action != null)
        {
            action(t);
        }
    }

    
}

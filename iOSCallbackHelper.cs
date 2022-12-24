using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iOSCallbackHelper : MonoBehaviour {
    static iOSCallbackHelper instance;
    static public iOSCallbackHelper INSTANCE
    {
        get
        {
            if(!instance) instance = FindObjectOfType<iOSCallbackHelper>() ?? 
                    new GameObject("iOSCallbackHelper").AddComponent<iOSCallbackHelper>();
            return instance;
        }
    }

    Action onShareCloseCallback;

    public void SetShareCloseCallback(Action callback)
    {
        onShareCloseCallback = callback;
    }

    //This method will be called from iOSNative Objective-C codes!!!
    public void OnShareCloseCallback(string msg)
    {
        if(msg.Equals("Closed") && onShareCloseCallback != null)
        {
            onShareCloseCallback.Invoke();
        }
    }

}

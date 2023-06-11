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
            if(!instance) {
                instance = FindObjectOfType<iOSCallbackHelper>() ?? 
                    new GameObject("iOSCallbackHelper").AddComponent<iOSCallbackHelper>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    event Action OnShareClose;

    public void SetShareCloseCallback(Action callback)
    {
        OnShareClose = callback;
    }

    //This method will be called from iOSNative Objective-C codes!!!
    public void OnShareCloseCallback(string msg)
    {
        if(msg.Equals("Closed") && OnShareClose != null)
        {
            OnShareClose.Invoke();
            OnShareClose = null;
        }
    }
    
    event Action OnFileSaved;
    public void SetSaveFileCallback(Action callback)
    {
        OnFileSaved = callback;
    }

    public void OnFileSaveCallback(string msg)
    {
        if (msg.Equals("True") && OnFileSaved != null)
        {
            OnFileSaved.Invoke();
        }
        else
        {
            OnFileSaved = null;
        }
    }
    
    event Action<string> OnFileSelected;
    event Action OnFileSelectFailed;
    public void SetFileSelectedCallback(Action<string> callback)
    {
        OnFileSelected = callback;
    }
    public void SetFileSelectedFailedCallback(Action callback)
    {
        OnFileSelectFailed = callback;
    }
    public void OnFileSelectedCallback(string msg)
    {
        if (OnFileSelected != null)
        {
            OnFileSelected.Invoke(msg);
            OnFileSelected = null;
        }

    }
    public void OnFileSelectedFailedCallback(string msg)
    {
        if (OnFileSelectFailed != null)
        {
            OnFileSelectFailed.Invoke();
            OnFileSelectFailed = null;
        }
        
    }
}

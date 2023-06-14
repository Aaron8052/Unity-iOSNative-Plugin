using System;
using UnityEngine;

namespace iOSNativePlugin
{
    internal class iOSCallbackHelper : MonoBehaviour 
    {
        static iOSCallbackHelper _instance;
        internal static iOSCallbackHelper Instance
        {
            get
            {
                if(!_instance) {
                    _instance = FindObjectOfType<iOSCallbackHelper>() ?? 
                               new GameObject("iOSCallbackHelper").AddComponent<iOSCallbackHelper>();
                    DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }

        event Action OnShareClose;

        internal void SetShareCloseCallback(Action callback)
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
        internal void SetSaveFileCallback(Action callback)
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
        internal void SetFileSelectedCallback(Action<string> callback)
        {
            OnFileSelected = callback;
        }
        internal void SetFileSelectedFailedCallback(Action callback)
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
}


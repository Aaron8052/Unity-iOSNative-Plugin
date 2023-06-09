using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class iOSNative
{
    public static class iCloudKeyValueStore
        {
#if UNITY_IOS && !UNITY_EDITOR
            [DllImport("__Internal")]
            private static extern bool _Synchronize();

            [DllImport("__Internal")]
            private static extern bool _IsICloudAvailable();

            [DllImport("__Internal")]
            private static extern bool _ClearICloudSave();

            [DllImport("__Internal")]
            private static extern string _iCloudGetString(string key, string defaultValue);

            [DllImport("__Internal")]
            private static extern bool _iCloudSaveString(string key, string value);

            [DllImport("__Internal")]
            private static extern int _iCloudGetInt(string key, int defaultValue);

            [DllImport("__Internal")]
            private static extern bool _iCloudSaveInt(string key, int value);

            [DllImport("__Internal")]
            private static extern float _iCloudGetFloat(string key, float defaultValue);

            [DllImport("__Internal")]
            private static extern bool _iCloudSaveFloat(string key, float value);

            [DllImport("__Internal")]
            private static extern bool _iCloudGetBool(string key, bool defaultValue);

            [DllImport("__Internal")]
            private static extern bool _iCloudSaveBool(string key, bool value);
#endif
            public static bool IsICloudAvailable()
            {
#if UNITY_IOS && !UNITY_EDITOR
                return _IsICloudAvailable();
#else
                return false;
#endif
            }
            public static bool Synchronize()
            {
#if UNITY_IOS && !UNITY_EDITOR
                return _Synchronize();
#else
                return false;
#endif
            }
            public static bool ClearICloudSave()
            {
#if UNITY_IOS && !UNITY_EDITOR
                return _ClearICloudSave();
#else
                return default(bool);
#endif
            }

            public static string iCloudGetStringValue(string key, string defaultValue)
            {
#if UNITY_IOS && !UNITY_EDITOR
                return _iCloudGetString(key, defaultValue);
#else
                return string.Empty;
#endif
            }

            public static bool iCloudSaveStringValue(string key, string value)
            {
#if UNITY_IOS && !UNITY_EDITOR
                return _iCloudSaveString(key, value);
#else
                return false;
#endif
            }

            public static int iCloudGetIntValue(string key, int defaultValue)
            {
#if UNITY_IOS && !UNITY_EDITOR
                return _iCloudGetInt(key, defaultValue);
#else
                return 0;
#endif
            }

            public static bool iCloudSaveIntValue(string key, int value)
            {
#if UNITY_IOS && !UNITY_EDITOR
                return _iCloudSaveInt(key, value);
#else
                return false;
#endif
            }

            public static float iCloudGetFloatValue(string key, float defaultValue)
            {
#if UNITY_IOS && !UNITY_EDITOR
                return _iCloudGetFloat(key, defaultValue);
#else
                return 0;
#endif
            }

            public static bool iCloudSaveFloatValue(string key, float value)
            {
#if UNITY_IOS && !UNITY_EDITOR
                return _iCloudSaveFloat(key, value);
#else
                return false;
#endif
            }

            public static bool iCloudGetBoolValue(string key, bool defaultValue)
            {
#if UNITY_IOS && !UNITY_EDITOR
                return _iCloudGetBool(key, defaultValue);
#else
                return false;
#endif
            }

            public static bool iCloudSaveBoolValue(string key, bool value)
            {
#if UNITY_IOS && !UNITY_EDITOR
                return _iCloudSaveBool(key, value);
#else
                return false;
#endif
            }
        }
    public static class iOSNotification
        {
#if UNITY_IOS && !UNITY_EDITOR
            [DllImport("__Internal")]
            private static extern void _PushNotification(string msg, string title, string identifier, int delay);

            [DllImport("__Internal")]
            private static extern void _RemovePendingNotifications(string identifier);

            [DllImport("__Internal")]
            private static extern void _RemoveAllPendingNotifications();
#endif
            public static void PushNotification(string msg, string title, string identifier, int delay)
            {
#if UNITY_IOS && !UNITY_EDITOR
                _PushNotification(msg, title, identifier, delay);
#endif
            }
            public static void RemovePendingNotifications(string identifier)
            {
#if UNITY_IOS && !UNITY_EDITOR
                _RemovePendingNotifications(identifier);
#endif
            }
            public static void RemoveAllPendingNotifications()
            {
#if UNITY_IOS && !UNITY_EDITOR
                _RemoveAllPendingNotifications();
#endif
            }
        }
    public static class iOSUIView
        {
#if UNITY_IOS && !UNITY_EDITOR
            [DllImport("__Internal")]
            private static extern void _ShowTempAlert(string alertString, int duration = 5);

            [DllImport("__Internal")]
            static extern void _SetStatusBarHidden(bool hidden);

            [DllImport("__Internal")]
            static extern void _SetStatusBarStyle(int style);
#endif


            public static void SetStatusBarHidden(bool hidden)
            {
#if UNITY_IOS && !UNITY_EDITOR
                _SetStatusBarHidden(hidden);
#endif
            }

            public enum UIStatusBarStyle
            {
                Default = 0, // Automatically chooses light or dark content based on the user interface style
                LightContent = 1, // Light content, for use on dark backgrounds
                DarkContent = 3, // Dark content, for use on light backgrounds
                [Obsolete("Use UIStatusBarStyle.LightContent instead.", true)]
                BlackTranslucent = 1,
                [Obsolete("Use UIStatusBarStyle.LightContent instead.", true)]
                BlackOpaque  = 2,
            }
            public static void SetStatusBarStyle(UIStatusBarStyle style)
            {
#if UNITY_IOS && !UNITY_EDITOR
                _SetStatusBarStyle((int)style);
#endif
            }
            
            public static void ShowTempAlert(string alertString, int duration = 5)
            {
#if UNITY_IOS && !UNITY_EDITOR
            _ShowTempAlert(alertString, duration);
#endif
            }
        }
    public static class iOSDevice
        {
#if UNITY_IOS && !UNITY_EDITOR
            [DllImport("__Internal")]
            static extern void _PlayHaptics(int style, float intensity);
            [DllImport("__Internal")]
            static extern string _GetCountryCode();
#endif
            public enum UIImpactFeedbackStyle
            {
                UIImpactFeedbackStyleLight,
                UIImpactFeedbackStyleMedium,
                UIImpactFeedbackStyleHeavy,
                UIImpactFeedbackStyleSoft,
                UIImpactFeedbackStyleRigid
            }
            /// <summary>
            /// PlayHaptics
            /// </summary>
            /// <param name="style">iOSNative.iOSDevice.UIImpactFeedbackStyle</param>
            /// <param name="intensity">Intensity 0.0 - 1.0</param>
            public static void PlayHaptics(UIImpactFeedbackStyle style, float intensity)
            {
#if UNITY_IOS && !UNITY_EDITOR
                _PlayHaptics((int)style, intensity);
#endif
            }

            public static string GetCountryCode()
            {
#if UNITY_IOS && !UNITY_EDITOR
                return _GetCountryCode();
#endif
                return string.Empty;
            }
        }
    public static class iOSShare
    {
#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void _Share(string message, string url, string imagePath);
        [DllImport("__Internal")]
        private static extern void _SaveFileDialog(string content, string fileName);
        [DllImport("__Internal")]
        private static extern bool _SelectFileDialog(string ext);
#endif
        public static void Share(string message, string url = "", string imagePath = "", Action closeCallback = null)
        {
#if UNITY_IOS && !UNITY_EDITOR
            _Share(message, url, imagePath);
            iOSCallbackHelper.INSTANCE.SetShareCloseCallback(closeCallback);
#endif
        }
        public static bool SaveFileDialog(string content, string fileName, Action callback = null)
        {
#if UNITY_IOS && !UNITY_EDITOR
            _SaveFileDialog(content, fileName);
            iOSCallbackHelper.INSTANCE.SetSaveFileCallback(callback);
            return true;
#else
            return false;
#endif
        }
        public static bool SelectFileDialog(string ext, Action<string> callback = null, Action failedCallback = null)
        {
#if UNITY_IOS && !UNITY_EDITOR
            iOSCallbackHelper.INSTANCE.SetFileSelectedCallback(callback);
            iOSCallbackHelper.INSTANCE.SetFileSelectedFailedCallback(failedCallback);
            return _SelectFileDialog(ext);
#else
            return false;
#endif
        }
   }



#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void _initialize();
#endif
    public static void Initialize(){
#if UNITY_IOS && !UNITY_EDITOR
        _initialize();
#endif
    } 
}


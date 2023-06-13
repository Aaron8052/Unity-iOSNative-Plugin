using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace iOSNativePlugin
{
    public static class iOSNative
    {
        [DllImport("__Internal")]
        private static extern void _initialize();
        public static void Initialize(){
            _initialize();
        } 
        
        public static class iCloudKeyValueStore
        {
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
            
            public static bool IsICloudAvailable()
            {
                return _IsICloudAvailable();
            }
            public static bool Synchronize()
            {
                return _Synchronize();
            }
            public static bool ClearICloudSave()
            {
                return _ClearICloudSave();
            }

            public static string iCloudGetStringValue(string key, string defaultValue)
            {
                return _iCloudGetString(key, defaultValue);
            }

            public static bool iCloudSaveStringValue(string key, string value)
            {
                return _iCloudSaveString(key, value);
            }

            public static int iCloudGetIntValue(string key, int defaultValue)
            {
                return _iCloudGetInt(key, defaultValue);
            }

            public static bool iCloudSaveIntValue(string key, int value)
            {
                return _iCloudSaveInt(key, value);
            }

            public static float iCloudGetFloatValue(string key, float defaultValue)
            {
                return _iCloudGetFloat(key, defaultValue);
            }

            public static bool iCloudSaveFloatValue(string key, float value)
            {
                return _iCloudSaveFloat(key, value);
            }

            public static bool iCloudGetBoolValue(string key, bool defaultValue)
            {
                return _iCloudGetBool(key, defaultValue);
            }

            public static bool iCloudSaveBoolValue(string key, bool value)
            {
                return _iCloudSaveBool(key, value);
            }
        }
        public static class Notification
        {
            [DllImport("__Internal")]
            private static extern void _PushNotification(string msg, string title, string identifier, int delay);

            [DllImport("__Internal")]
            private static extern void _RemovePendingNotifications(string identifier);

            [DllImport("__Internal")]
            private static extern void _RemoveAllPendingNotifications();
            public static void PushNotification(string msg, string title, string identifier, int delay)
            {
                _PushNotification(msg, title, identifier, delay);
            }
            public static void RemovePendingNotifications(string identifier)
            {
                _RemovePendingNotifications(identifier);
            }
            public static void RemoveAllPendingNotifications()
            {
                _RemoveAllPendingNotifications();
            }
        }
        public static class NativeUI
        {
            [DllImport("__Internal")]
            static extern bool _IsStatusBarHidden();
                
            [DllImport("__Internal")]
            private static extern void _ShowTempAlert(string alertString, int duration = 5);

            [DllImport("__Internal")]
            static extern void _SetStatusBarHidden(bool hidden, int withAnimation);

            [DllImport("__Internal")]
            static extern void _SetStatusBarStyle(int style, bool animated);

            public static bool IsStatusBarHidden()
            {
                return _IsStatusBarHidden();
            }

            public enum UIStatusBarAnimation
            {
                None,
                Fade,
                Slide,
            }
            public static void SetStatusBarHidden(bool hidden, UIStatusBarAnimation withAnimation = UIStatusBarAnimation.Fade)
            {
                _SetStatusBarHidden(hidden, (int)withAnimation);
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
            public static void SetStatusBarStyle(UIStatusBarStyle style, bool animated = false)
            {
                _SetStatusBarStyle((int)style, animated);
            }
                
            public static void ShowTempAlert(string alertString, int duration = 5)
            {
                _ShowTempAlert(alertString, duration);
            }
        }
        public static class Device
        {
            [DllImport("__Internal")]
            static extern bool _IsSuperUser();
            
            [DllImport("__Internal")]
            static extern void _SetAudioExclusive(bool exclusive);
            
            [DllImport("__Internal")]
            static extern void _PlayHaptics(int style, float intensity);
                
            [DllImport("__Internal")]
            static extern string _GetCountryCode();

            public static bool IsSuperUser()
            {
                return _IsSuperUser();
            }
            public static void SetAudioExclusive(bool exclusive)
            {
                _SetAudioExclusive(exclusive);
            }
            
            
            public enum UIImpactFeedbackStyle
            {
                Light,
                Medium,
                Heavy,
                Soft,
                Rigid
            }
            /// <summary>
            /// PlayHaptics
            /// </summary>
            /// <param name="style">iOSNative.iOSDevice.UIImpactFeedbackStyle</param>
            /// <param name="intensity">Intensity 0.0 - 1.0</param>
            public static void PlayHaptics(UIImpactFeedbackStyle style, float intensity)
            {
                _PlayHaptics((int)style, intensity);
            }

            public static string GetCountryCode()
            {
                return _GetCountryCode();
            }
        }
        public static class NativeShare
        {
            [DllImport("__Internal")]
            private static extern void _Share(string message, string url, string imagePath);
            [DllImport("__Internal")]
            private static extern void _SaveFileDialog(string content, string fileName);
            [DllImport("__Internal")]
            private static extern bool _SelectFileDialog(string ext);
            public static void Share(string message, string url = "", string imagePath = "", Action closeCallback = null)
            {
                _Share(message, url, imagePath);
                iOSCallbackHelper.INSTANCE.SetShareCloseCallback(closeCallback);
            }
            public static bool SaveFileDialog(string content, string fileName, Action callback = null)
            {
                _SaveFileDialog(content, fileName);
                iOSCallbackHelper.INSTANCE.SetSaveFileCallback(callback);
                return true; }
            public static bool SelectFileDialog(string ext, Action<string> callback = null, Action failedCallback = null)
            {
                iOSCallbackHelper.INSTANCE.SetFileSelectedCallback(callback);
                iOSCallbackHelper.INSTANCE.SetFileSelectedFailedCallback(failedCallback);
                return _SelectFileDialog(ext); 
            }
       }



        
    }
}



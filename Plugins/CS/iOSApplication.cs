using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace iOSNativePlugin
{
    public static class iOSApplication
    {
        [DllImport("__Internal")]
        static extern string _GetBundleIdentifier();
        
        [DllImport("__Internal")]
        static extern string _GetVersion();
        
        [DllImport("__Internal")]
        static extern string _GetBundleVersion();
        
        [DllImport("__Internal")]
        static extern void _OpenAppSettings();
        
        [DllImport("__Internal")]
        static extern void _SetUserSettingsBool(string identifier, bool value);
        
        [DllImport("__Internal")]
        static extern bool _GetUserSettingsBool(string identifier);
        
        [DllImport("__Internal")]
        static extern void _SetUserSettingsString(string identifier, string value);
        
        [DllImport("__Internal")]
        static extern string _GetUserSettingsString(string identifier);
        
        [DllImport("__Internal")]
        static extern void _SetUserSettingsFloat(string identifier, float value);
        
        [DllImport("__Internal")]
        static extern float _GetUserSettingsFloat(string identifier);
        
        [DllImport("__Internal")]
        static extern void _SetUserSettingsInt(string identifier, long value);
        
        [DllImport("__Internal")]
        static extern long _GetUserSettingsInt(string identifier);

        [DllImport("__Internal")]
        static extern void _RegisterUserSettingsChangeCallback(UserSettingsChangeCallback callback);
        
        [DllImport("__Internal")]
        static extern void _UnregisterUserSettingsChangeCallback();
        
        
        /// <summary>
        /// 获取当前应用的Bundle Identifier
        /// </summary>
        /// <returns>Bundle Identifier (与<c>Application.identifier</c>一致)</returns>
        public static string GetBundleIdentifier()
        {
            return _GetBundleIdentifier();
        }
        
        /// <summary>
        /// 获取应用版本号
        /// </summary>
        /// <returns></returns>
        public static string GetVersion()
        {
            return _GetVersion();
        }
        
        /// <summary>
        /// 获取应用构建号
        /// </summary>
        /// <returns></returns>
        public static string GetBundleVersion()
        {
            return _GetBundleVersion();
        }

        /// <summary>
        /// 打开本App的系统设置界面
        /// </summary>
        public static void OpenAppSettings()
        {
            _OpenAppSettings();
        }

        /// <summary>
        /// 设置用户设置中的布尔值（Switch）
        /// </summary>
        /// <param name="identifier">设置项的标识符</param>
        /// <param name="value">设置的值</param>
        public static void SetUserSettingsBool(string identifier, bool value)
        {
            _SetUserSettingsBool(identifier, value);
        }
        /// <summary>
        /// 获取用户设置中的布尔值（Switch）
        /// </summary>
        /// <param name="identifier">设置项的标识符</param>
        /// <returns>设置的值</returns>
        public static bool GetUserSettingsBool(string identifier)
        {
            return _GetUserSettingsBool(identifier);
        }
        
        /// <summary>
        /// 设置用户设置中的字符串值（TextArea）
        /// </summary>
        /// <param name="identifier">设置项的标识符</param>
        /// <param name="value">设置的值</param>
        public static void SetUserSettingsString(string identifier, string value)
        {
            _SetUserSettingsString(identifier, value);
        }
        
        /// <summary>
        /// 获取用户设置中的字符串值（TextArea）
        /// </summary>
        /// <param name="identifier">设置项的标识符</param>
        /// <returns>设置的值</returns>
        public static string GetUserSettingsString(string identifier)
        {
            return _GetUserSettingsString(identifier);
        }
        
        /// <summary>
        /// 设置用户设置中的浮点值（Slider）
        /// </summary>
        /// <param name="identifier">设置项的标识符</param>
        /// <param name="value">设置的值</param>
        public static void SetUserSettingsFloat(string identifier, float value)
        {
            _SetUserSettingsFloat(identifier, value);
        }
        
        /// <summary>
        /// 获取用户设置中的浮点值（Slider）
        /// </summary>
        /// <param name="identifier">设置项的标识符</param>
        /// <returns>设置的值</returns>
        public static float GetUserSettingsFloat(string identifier)
        {
            return _GetUserSettingsFloat(identifier);
        }
        
        /// <summary>
        /// 设置用户设置中的整数值（Slider）
        /// </summary>
        /// <param name="identifier">设置项的标识符</param>
        /// <param name="value">设置的值</param>
        public static void SetUserSettingsInt(string identifier, long value)
        {
            _SetUserSettingsInt(identifier, value);
        }
        
        /// <summary>
        /// 获取用户设置中的整数值（Slider）
        /// </summary>
        /// <param name="identifier">设置项的标识符</param>
        /// <returns>设置的值</returns>
        public static long GetUserSettingsInt(string identifier)
        {
            return _GetUserSettingsInt(identifier);
        }
        
        static Action _onUserSettingsChanged;
            
        /// <summary>
        /// !!该事件目前会导致游戏崩溃，不要调用
        /// </summary>
        public static event Action OnUserSettingsChanged
        {
            add
            {
                _onUserSettingsChanged += value;
                _RegisterUserSettingsChangeCallback(OnStatusBarOrientationChangeCallback);
            }
            remove
            {
                _onUserSettingsChanged -= value;
                    
                if(_onUserSettingsChanged == null)
                    _UnregisterUserSettingsChangeCallback();
            }
        }
        
        [MonoPInvokeCallback(typeof(UserSettingsChangeCallback))]
        static void OnStatusBarOrientationChangeCallback()
        {
            if (_onUserSettingsChanged != null)
                _onUserSettingsChanged();
        }
    }
}

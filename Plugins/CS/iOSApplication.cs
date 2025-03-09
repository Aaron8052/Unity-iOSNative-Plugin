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
        [DllImport("__Internal")] static extern string iOSApplication_GetBundleIdentifier();
        [DllImport("__Internal")] static extern string iOSApplication_GetVersion();
        [DllImport("__Internal")] static extern string iOSApplication_GetBundleVersion();
        [DllImport("__Internal")] static extern void iOSApplication_OpenAppSettings();
        [DllImport("__Internal")] static extern void iOSApplication_SetAlternateIconName(string iconName);
        [DllImport("__Internal")] static extern string iOSApplication_GetAlternateIconName();
        [DllImport("__Internal")] static extern void iOSApplication_SetUserSettingsBool(string identifier, bool value);
        [DllImport("__Internal")] static extern bool iOSApplication_GetUserSettingsBool(string identifier);
        [DllImport("__Internal")] static extern void iOSApplication_SetUserSettingsString(string identifier, string value);
        [DllImport("__Internal")] static extern string iOSApplication_GetUserSettingsString(string identifier);
        [DllImport("__Internal")] static extern void iOSApplication_SetUserSettingsFloat(string identifier, float value);
        [DllImport("__Internal")] static extern float iOSApplication_GetUserSettingsFloat(string identifier);
        [DllImport("__Internal")] static extern void iOSApplication_SetUserSettingsInt(string identifier, long value);
        [DllImport("__Internal")] static extern long iOSApplication_GetUserSettingsInt(string identifier);
        [DllImport("__Internal")] static extern void iOSApplication_RegisterUserSettingsChangeCallback(UserSettingsChangeCallback callback);
        [DllImport("__Internal")] static extern void iOSApplication_UnregisterUserSettingsChangeCallback();

        public static string GetAlternateIconName()
        {
            return iOSApplication_GetAlternateIconName();
        }

        public static void SetAlternateIconName(string iconName)
        {
            iOSApplication_SetAlternateIconName(iconName);
        }
        /// <summary>
        /// 获取当前应用的Bundle Identifier
        /// </summary>
        /// <returns>Bundle Identifier (与<c>Application.identifier</c>一致)</returns>
        public static string GetBundleIdentifier() => iOSApplication_GetBundleIdentifier();

        /// <summary>
        /// 获取应用版本号
        /// </summary>
        /// <returns></returns>
        public static string GetVersion() => iOSApplication_GetVersion();

        /// <summary>
        /// 获取应用构建号
        /// </summary>
        /// <returns></returns>
        public static string GetBundleVersion() => iOSApplication_GetBundleVersion();

        /// <summary>
        /// 打开本App的系统设置界面
        /// </summary>
        public static void OpenAppSettings() => iOSApplication_OpenAppSettings();

        /// <summary>
        /// 设置用户设置中的布尔值（Switch）
        /// </summary>
        /// <param name="identifier">设置项的标识符</param>
        /// <param name="value">设置的值</param>
        public static void SetUserSettingsBool(string identifier, bool value)
            => iOSApplication_SetUserSettingsBool(identifier, value);

        /// <summary>
        /// 获取用户设置中的布尔值（Switch）
        /// </summary>
        /// <param name="identifier">设置项的标识符</param>
        /// <returns>设置的值</returns>
        public static bool GetUserSettingsBool(string identifier)
            => iOSApplication_GetUserSettingsBool(identifier);

        /// <summary>
        /// 设置用户设置中的字符串值（TextArea）
        /// </summary>
        /// <param name="identifier">设置项的标识符</param>
        /// <param name="value">设置的值</param>
        public static void SetUserSettingsString(string identifier, string value)
            => iOSApplication_SetUserSettingsString(identifier, value);

        /// <summary>
        /// 获取用户设置中的字符串值（TextArea）
        /// </summary>
        /// <param name="identifier">设置项的标识符</param>
        /// <returns>设置的值</returns>
        public static string GetUserSettingsString(string identifier)
            => iOSApplication_GetUserSettingsString(identifier);

        /// <summary>
        /// 设置用户设置中的浮点值（Slider）
        /// </summary>
        /// <param name="identifier">设置项的标识符</param>
        /// <param name="value">设置的值</param>
        public static void SetUserSettingsFloat(string identifier, float value)
            => iOSApplication_SetUserSettingsFloat(identifier, value);

        /// <summary>
        /// 获取用户设置中的浮点值（Slider）
        /// </summary>
        /// <param name="identifier">设置项的标识符</param>
        /// <returns>设置的值</returns>
        public static float GetUserSettingsFloat(string identifier)
            => iOSApplication_GetUserSettingsFloat(identifier);

        /// <summary>
        /// 设置用户设置中的整数值（Slider）
        /// </summary>
        /// <param name="identifier">设置项的标识符</param>
        /// <param name="value">设置的值</param>
        public static void SetUserSettingsInt(string identifier, long value)
            => iOSApplication_SetUserSettingsInt(identifier, value);

        /// <summary>
        /// 获取用户设置中的整数值（Slider）
        /// </summary>
        /// <param name="identifier">设置项的标识符</param>
        /// <returns>设置的值</returns>
        public static long GetUserSettingsInt(string identifier)
            => iOSApplication_GetUserSettingsInt(identifier);

        static Action _onUserSettingsChanged;
            
        /// <summary>
        /// !!该事件目前会导致游戏崩溃，不要调用
        /// </summary>
        public static event Action OnUserSettingsChanged
        {
            add
            {
                _onUserSettingsChanged += value;
                iOSApplication_RegisterUserSettingsChangeCallback(OnStatusBarOrientationChangeCallback);
            }
            remove
            {
                _onUserSettingsChanged -= value;
                    
                if(_onUserSettingsChanged == null)
                    iOSApplication_UnregisterUserSettingsChangeCallback();
            }
        }
        
        [MonoPInvokeCallback(typeof(UserSettingsChangeCallback))]
        static void OnStatusBarOrientationChangeCallback() => _onUserSettingsChanged?.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
        static extern bool _GetUserSettingsBool(string identifier);
        
        [DllImport("__Internal")]
        static extern string _GetUserSettingsString(string identifier);
        
        [DllImport("__Internal")]
        static extern float _GetUserSettingsFloat(string identifier);
        
        [DllImport("__Internal")]
        static extern long _GetUserSettingsInt(string identifier);
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
        /// 获取iOS settings bundle的Toggle Switch值
        /// </summary>
        /// <param name="identifier">identifier</param>
        /// <returns></returns>
        public static bool GetUserSettingsBool(string identifier)
        {
            return _GetUserSettingsBool(identifier);
        }
        
        /// <summary>
        /// 获取iOS settings bundle的TextArea值
        /// </summary>
        /// <param name="identifier">identifier</param>
        /// <returns></returns>
        public static string GetUserSettingsString(string identifier)
        {
            return _GetUserSettingsString(identifier);
        }
        
        /// <summary>
        /// 获取iOS settings bundle的Slider Float值
        /// </summary>
        /// <param name="identifier">identifier</param>
        /// <returns></returns>
        public static float GetUserSettingsFloat(string identifier)
        {
            return _GetUserSettingsFloat(identifier);
        }
        
        /// <summary>
        /// 获取iOS settings bundle的Slider Long值
        /// </summary>
        /// <param name="identifier">identifier</param>
        /// <returns></returns>
        public static long GetUserSettingsInt(string identifier)
        {
            return _GetUserSettingsInt(identifier);
        }
    }
}

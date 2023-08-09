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
    }
}

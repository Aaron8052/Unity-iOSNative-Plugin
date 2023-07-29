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
        
        
        /// <summary>
        /// 获取当前应用的Bundle Identifier
        /// </summary>
        /// <returns>Bundle Identifier (与<c>Application.identifier</c>一致)</returns>
        public static string GetBundleIdentifier()
        {
            return _GetBundleIdentifier();
        }
    }
}

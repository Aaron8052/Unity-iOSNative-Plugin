
using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace iOSNativePlugin
{
    public static class Device
    {
        [DllImport("__Internal")]
        static extern int _GetDeviceOrientation();
            
        [DllImport("__Internal")]
        static extern bool _IsBluetoothHeadphonesConnected();
            
        [DllImport("__Internal")]
        static extern bool _IsMacCatalyst();
            
        [DllImport("__Internal")]
        static extern bool _IsSuperuser();
            
        [DllImport("__Internal")]
        static extern void _SetAudioExclusive(bool exclusive);
            
        [DllImport("__Internal")]
        static extern void _PlayHaptics(int style, float intensity);
                
        [DllImport("__Internal")]
        static extern string _GetCountryCode();

        /// <summary>
        /// 获取当前设备的物理朝向
        /// </summary>
        /// <returns></returns>
        public static UIDeviceOrientation GetDeviceOrientation()
        {
            return (UIDeviceOrientation)_GetDeviceOrientation();
        }
            
        /// <summary>
        /// 判断玩家当前是否连接了蓝牙耳机
        /// </summary>
        /// <returns></returns>
        public static bool IsBluetoothHeadphonesConnected()
        {
            return _IsBluetoothHeadphonesConnected();
        }
            
        /// <summary>
        /// 判断当前app是否运行在Mac Catalyst环境下
        /// </summary>
        /// <returns></returns>
        [Obsolete("Use IsRunningOnMac instead. (UnityUpgradable) -> IsRunningOnMac", true)]
        public static bool IsMacCatalyst()
        {
            return _IsMacCatalyst();
        }

        /// <summary>
        /// 判断当前app是否运行在Mac环境下
        /// </summary>
        /// <returns></returns>
        public static bool IsRunningOnMac()
        {
            return _IsMacCatalyst() || UnityEngine.iOS.Device.iosAppOnMac;
        }
        /// <summary>
        /// 判断当前设备是否越狱
        /// </summary>
        /// <returns><c>true</c> - 已越狱 <para><c>false</c> - 未越狱</para></returns>
        public static bool IsSuperuser()
        {
            return _IsSuperuser();
        }
            
        /// <summary>
        /// 调用此方法可静音/暂停设备后台正在播放的音频
        /// </summary>
        /// <param name="exclusive">音频独占</param>
        public static void SetAudioExclusive(bool exclusive)
        {
            _SetAudioExclusive(exclusive);
        }
            
            
           
        /// <summary>
        /// 播放短震动
        /// <para><b>> 此方法可用性未经测试</b></para>
        /// </summary>
        /// <param name="style">震动风格（轻、重、软...）</param>
        /// <param name="intensity">强度（0-1）</param>
        public static void PlayHaptics(UIImpactFeedbackStyle style, float intensity)
        {
            _PlayHaptics((int)style, intensity);
        }

        /// <summary>
        /// 获取当前设备的ISO地区码（ISO 3166-1 alpha-2）
        /// </summary>
        /// <returns>ISO 3166-1 alpha-2</returns>
        public static string GetCountryCode()
        {
            return _GetCountryCode();
        }
    }
}


using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace iOSNativePlugin
{
    public static class Device
    {
        [DllImport("__Internal")] static extern bool Device_IsIPhoneNotchScreen();
        [DllImport("__Internal")] static extern bool Device_IsIPad();
        [DllImport("__Internal")] static extern int Device_GetDeviceOrientation();
        [DllImport("__Internal")] static extern bool Audio_IsBluetoothHeadphonesConnected();
        [DllImport("__Internal")] static extern bool Device_IsMacCatalyst();
        [DllImport("__Internal")] static extern bool Device_IsSuperuser();
        [DllImport("__Internal")] static extern void Audio_SetAudioExclusive(bool exclusive);
        [DllImport("__Internal")] static extern void Device_PlayHaptics(int style, float intensity);
        [DllImport("__Internal")] static extern string Device_GetLocaleISOCode();
        [DllImport("__Internal")] static extern string Device_GetLanguageISOCode();

        /// <summary>
        /// 获取系统版本
        /// </summary>
        /// <returns>系统版本，获取失败返回 Version(0, 0) </returns>
        public static Version GetIOSVersion()
        {
            if(Version.TryParse(UnityEngine.iOS.Device.systemVersion, out var iOSVersion))
                return iOSVersion;
            return new Version(0, 0);
        }

        /// <summary>
        /// 获取系统版本
        /// </summary>
        /// <param name="iOSVersion">out 输出 Version 对象，失败则为 null</param>
        /// <returns>是否成功获取系统版本</returns>
        public static bool TryGetIOSVersion(out Version iOSVersion)
        {
            if (Version.TryParse(UnityEngine.iOS.Device.systemVersion, out iOSVersion))
                return true;
            iOSVersion = null;
            return false;
        }

        /// <summary>
        /// 是否是 iPhone 刘海屏
        /// </summary>
        /// <returns></returns>
        public static bool IsIPhoneNotchScreen() => Device_IsIPhoneNotchScreen();


        /// <summary>
        /// 获取当前设备的物理朝向
        /// </summary>
        /// <returns></returns>
        public static UIDeviceOrientation GetDeviceOrientation()
            => (UIDeviceOrientation)Device_GetDeviceOrientation();

        /// <summary>
        /// 判断玩家当前是否连接了蓝牙耳机
        /// </summary>
        /// <returns></returns>
        [Obsolete("未来将会移动到Audio类中")]
        public static bool IsBluetoothHeadphonesConnected()
            => Audio_IsBluetoothHeadphonesConnected();

        /// <summary>
        /// 判断当前 app 是否运行在 Mac Catalyst 环境下
        /// </summary>
        /// <returns></returns>
        [Obsolete("Use IsRunningOnMac instead.")]
        public static bool IsMacCatalyst() => Device_IsMacCatalyst();

        /// <summary>
        /// 判断当前 app 是否运行在 Mac 环境下
        /// </summary>
        /// <returns></returns>
        public static bool IsRunningOnMac()
            => Device_IsMacCatalyst() || UnityEngine.iOS.Device.iosAppOnMac;

        /// <summary>
        /// 判断当前 app 是否运行在 iPad 环境下
        /// </summary>
        /// <returns></returns>
        public static bool IsRunningOnIpad()
            => !IsRunningOnMac() && Device_IsIPad();

        /// <summary>
        /// 判断当前设备是否越狱
        /// </summary>
        /// <returns>是否越狱</returns>
        public static bool IsSuperuser() => Device_IsSuperuser();

        /// <summary>
        /// 调用此方法可静音/暂停设备后台正在播放的音频
        /// </summary>
        /// <param name="exclusive">音频独占</param>
        [Obsolete("未来将会移动到Audio类中")]
        public static void SetAudioExclusive(bool exclusive) => Audio_SetAudioExclusive(exclusive);


        /// <summary>
        /// 播放短震动
        /// <para><b>> 此方法可用性未经测试</b></para>
        /// </summary>
        /// <param name="style">震动风格（轻、重、软...）</param>
        /// <param name="intensity">强度（0-1）</param>
        public static void PlayHaptics(UIImpactFeedbackStyle style, float intensity)
            => Device_PlayHaptics((int)style, intensity);

        /// <summary>
        /// 获取当前设备的ISO地区码（ISO 3166-1 alpha-2）
        /// </summary>
        /// <returns>ISO 3166-1 alpha-2</returns>
        [Obsolete("Use Device.GetLocaleISOCode() instead")]
        public static string GetCountryCode() => GetLocaleISOCode();

        /// <summary>
        /// 获取当前设备的ISO地区码（ISO 3166-1 alpha-2）
        /// </summary>
        /// <returns>ISO 3166-1 alpha-2</returns>
        public static string GetLocaleISOCode() => Device_GetLocaleISOCode();

        /// <summary>
        /// 获取当前设备语言的ISO码（例：zh-Hans-CN）
        /// </summary>
        /// <returns>语言ISO码</returns>
        public static string GetLanguageISOCode() => Device_GetLanguageISOCode();
    }
}

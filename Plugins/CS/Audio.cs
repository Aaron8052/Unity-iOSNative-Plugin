
using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace iOSNativePlugin
{
    public static class Audio
    {
        [DllImport("__Internal")] static extern float Audio_SystemVolume();
        [DllImport("__Internal")] static extern double Audio_InputLatency();
        [DllImport("__Internal")] static extern double Audio_OutputLatency();
        [DllImport("__Internal")] static extern double Audio_SampleRate();
        [DllImport("__Internal")] static extern bool Audio_IsBluetoothHeadphonesConnected();
        [DllImport("__Internal")] static extern void Audio_SetAudioExclusive(bool exclusive);

        /// <summary>
        /// 当前系统音量
        /// <para>https://developer.apple.com/documentation/avfaudio/avaudiosession/1616533-outputvolume?language=objc</para>
        /// </summary>
        /// <returns>音量（0-1）</returns>
        public static float SystemVolume() => Audio_SystemVolume();

        
        /// <summary>
        /// 音频输入延迟
        /// <para>https://developer.apple.com/documentation/avfaudio/avaudiosession/1616537-inputlatency?language=objc</para>
        /// </summary>
        /// <returns>延迟（秒）</returns>
        public static double InputLatency() => Audio_InputLatency();
        
        /// <summary>
        /// 音频输出延迟
        /// <para>https://developer.apple.com/documentation/avfaudio/avaudiosession/1616500-outputlatency?language=objc</para>
        /// </summary>
        /// <returns>延迟（秒）</returns>
        public static double OutputLatency() =>  Audio_OutputLatency();
        
        /// <summary>
        /// 音频采样率
        /// <para>https://developer.apple.com/documentation/avfaudio/avaudiosession/1616499-samplerate?language=objc</para>
        /// </summary>
        /// <returns>采样率（Hertz）</returns>
        public static double SampleRate() => Audio_SampleRate();
        
        /// <summary>
        /// 判断玩家当前是否连接了蓝牙耳机
        /// </summary>
        /// <returns></returns>
        public static bool IsBluetoothHeadphonesConnected() => Audio_IsBluetoothHeadphonesConnected();
        
        /// <summary>
        /// 调用此方法可静音/暂停设备后台正在播放的音频
        /// </summary>
        /// <param name="exclusive">音频独占</param>
        public static void SetAudioExclusive(bool exclusive) =>  Audio_SetAudioExclusive(exclusive);
        
    }
}

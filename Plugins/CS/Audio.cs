
using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace iOSNativePlugin
{
    public static class Audio
    {
        [DllImport("__Internal")] static extern void Audio_Init(Action audioSessionRouteChangedCallback,
            ULongCallback audioInterruptionCallback);
        [DllImport("__Internal")] static extern bool Audio_SetActive(bool active);
        [DllImport("__Internal")] static extern bool Audio_GetAudioInterrupted();
        [DllImport("__Internal")] static extern bool Audio_GetPrefersNoInterruptionsFromSystemAlerts();
        [DllImport("__Internal")] static extern void Audio_SetPrefersNoInterruptionsFromSystemAlerts(bool prefersNoInterruptions);
        [DllImport("__Internal")] static extern float Audio_SystemVolume();
        [DllImport("__Internal")] static extern double Audio_InputLatency();
        [DllImport("__Internal")] static extern double Audio_OutputLatency();
        [DllImport("__Internal")] static extern double Audio_SampleRate();
        [DllImport("__Internal")] static extern bool Audio_IsBluetoothHeadphonesConnected();
        [DllImport("__Internal")] static extern void Audio_SetAudioExclusive(bool exclusive);


        static bool inited;
        static void Init()
        {
            if (inited) return;
            Audio_Init(OnAudioSessionRouteChanged, OnAudioInterruption);
            inited = true;
        }

        /// <summary>
        /// 设置AVAudioSession的激活状态
        /// </summary>
        /// <param name="active"></param>
        /// <returns>是否成功抢占音频优先权</returns>
        public static bool SetActive(bool active)
        {
            return Audio_SetActive(active);
        }

#region AudioInterruption

        // https://developer.apple.com/documentation/avfaudio/avaudiosession/setprefersnointerruptionsfromsystemalerts(_:)?language=objc

        /// <summary>
        /// <para>设置系统铃声时是否中断Audio Session</para>
        /// <para>Beginning in iOS 14, users can set a global preference that indicates whether the system displays incoming calls using a banner or a full-screen display style.
        /// If using the banner style, setting this value to true prevents the system from interrupting the audio session with incoming call notifications,
        /// and gives the user an opportunity to accept or decline the call. The system only interrupts the audio session if the user accepts the call.</para>
        /// <para>Enabling this preference can improve the user experience of apps with audio sessions that
        /// you don’t want to interrupt, such as those that record audiovisual media or that you use for music performance.</para>
        /// <para>This preference has no effect if the device uses the full-screen display style—the system interrupts the audio session on incoming calls.</para>
        /// </summary>
        public static bool PrefersNoInterruptionsFromSystemAlerts
        {
            get => Audio_GetPrefersNoInterruptionsFromSystemAlerts();
            set => Audio_SetPrefersNoInterruptionsFromSystemAlerts(value);
        }

        static Action<AVAudioSessionInterruptionType> audioInterruptionEvent;

        public static bool AudioInterrupted => Audio_GetAudioInterrupted();

        /// <summary>
        /// 游戏音频中断与恢复事件
        /// </summary>
        public static event Action<AVAudioSessionInterruptionType> AudioInterruptionEvent
        {
            add
            {
                Init();
                audioInterruptionEvent += value;
            }
            remove => audioInterruptionEvent -= value;
        }

        [MonoPInvokeCallback(typeof(ULongCallback))]
        static void OnAudioInterruption(ulong type)
        {
            audioInterruptionEvent?.Invoke((AVAudioSessionInterruptionType)type);
        }

#endregion

#region AudioSessionRouteChanged

        static Action audioSessionRouteChangedEvent;

        /// <summary>
        /// 玩家音频设备变更事件
        /// </summary>
        public static event Action AudioSessionRouteChangedEvent
        {
            add
            {
                Init();
                audioSessionRouteChangedEvent += value;
            }
            remove => audioSessionRouteChangedEvent -= value;
        }

        [Obsolete("Use AudioSessionRouteChangedEvent instead")]
        public static event Action OnAudioSessionRouteChangedEvent
        {
            add => AudioSessionRouteChangedEvent += value;
            remove => AudioSessionRouteChangedEvent -= value;
        }


        [MonoPInvokeCallback(typeof(Action))]
        static void OnAudioSessionRouteChanged()
        {
            audioSessionRouteChangedEvent?.Invoke();
        }

#endregion

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

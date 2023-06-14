using System;
using System.Runtime.InteropServices;

namespace iOSNativePlugin
{
    public static class iOSNative
    {
        [DllImport("__Internal")]
        private static extern void _initialize();
        
        [DllImport("__Internal")]
        static extern string _GetBundleIdentifier();
        
        
        /// <summary>
        /// 初始化整个iOSNative插件
        /// </summary>
        public static void Initialize(){
            _initialize();
        }
        
        /// <summary>
        /// 获取当前应用的Bundle Identifier
        /// </summary>
        /// <returns>Bundle Identifier (与<c>Application.identifier</c>一致)</returns>
        public static string GetBundleIdentifier()
        {
            return _GetBundleIdentifier();
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
            
            /// <summary>
            /// 判断当前设备iCloud是否可用
            /// </summary>
            /// <returns></returns>
            public static bool IsICloudAvailable()
            {
                return _IsICloudAvailable();
            }
            
            /// <summary>
            /// 判断当前设备iCloud是否可用
            /// </summary>
            /// <returns>是否同步成功</returns>
            public static bool Synchronize()
            {
                return _Synchronize();
            }
            
            /// <summary>
            /// 清空iCloud存档
            /// <para><b> >此方法可用性未知</b></para>
            /// </summary>
            /// <returns>是否清除成功</returns>
            public static bool ClearICloudSave()
            {
                return _ClearICloudSave();
            }

            /// <summary>
            /// 从iCloud读取String值
            /// </summary>
            /// <param name="key">键</param>
            /// <param name="defaultValue">默认值</param>
            /// <returns></returns>
            public static string iCloudGetStringValue(string key, string defaultValue)
            {
                return _iCloudGetString(key, defaultValue);
            }

            /// <summary>
            /// 保存String值到iCloud
            /// </summary>
            /// <param name="key">键</param>
            /// <param name="value">值</param>
            /// <returns></returns>
            public static bool iCloudSaveStringValue(string key, string value)
            {
                return _iCloudSaveString(key, value);
            }

            /// <summary>
            /// 从iCloud读取Int值
            /// </summary>
            /// <param name="key">键</param>
            /// <param name="defaultValue">默认值</param>
            /// <returns></returns>
            public static int iCloudGetIntValue(string key, int defaultValue)
            {
                return _iCloudGetInt(key, defaultValue);
            }

            /// <summary>
            /// 保存Int值到iCloud
            /// </summary>
            /// <param name="key">键</param>
            /// <param name="value">值</param>
            /// <returns></returns>
            public static bool iCloudSaveIntValue(string key, int value)
            {
                return _iCloudSaveInt(key, value);
            }

            /// <summary>
            /// 从iCloud读取Float值
            /// </summary>
            /// <param name="key">键</param>
            /// <param name="defaultValue">默认值</param>
            /// <returns></returns>
            public static float iCloudGetFloatValue(string key, float defaultValue)
            {
                return _iCloudGetFloat(key, defaultValue);
            }

            /// <summary>
            /// 保存Float值到iCloud
            /// </summary>
            /// <param name="key">键</param>
            /// <param name="value">值</param>
            /// <returns></returns>
            public static bool iCloudSaveFloatValue(string key, float value)
            {
                return _iCloudSaveFloat(key, value);
            }

            /// <summary>
            /// 从iCloud读取Bool值
            /// </summary>
            /// <param name="key">键</param>
            /// <param name="defaultValue">默认值</param>
            /// <returns></returns>
            public static bool iCloudGetBoolValue(string key, bool defaultValue)
            {
                return _iCloudGetBool(key, defaultValue);
            }

            /// <summary>
            /// 保存Bool值到iCloud
            /// </summary>
            /// <param name="key">键</param>
            /// <param name="value">值</param>
            /// <returns></returns>
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
            
            /// <summary>
            /// 推送本地定时通知
            /// </summary>
            /// <param name="msg">通知内容</param>
            /// <param name="title">标题</param>
            /// <param name="identifier">为通知指定一个标识符，相同的标识符会被系统判定为同一个通知</param>
            /// <param name="delay">延迟待定delay秒后推送此通知</param>
            public static void PushNotification(string msg, string title, string identifier, int delay)
            {
                _PushNotification(msg, title, identifier, delay);
            }
            
            /// <summary>
            /// 移除某个待定通知
            /// <para><b>>对于已经推送的通知无效</b></para>
            /// </summary>
            /// <param name="identifier">要移除的通知的标识符</param>
            public static void RemovePendingNotifications(string identifier)
            {
                _RemovePendingNotifications(identifier);
            }
            
            /// <summary>
            /// 移除所有待定通知
            /// </summary>
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

            /// <summary>
            /// 判断当前系统状态栏是否被隐藏
            /// </summary>
            /// <returns><c>true</c> - 隐藏 <para><c>false</c> - 显示</para></returns>
            public static bool IsStatusBarHidden()
            {
                return _IsStatusBarHidden();
            }

            /// <summary>
            /// 状态栏显示隐藏时的动画类型
            /// </summary>
            public enum UIStatusBarAnimation
            {
                None,
                Fade,
                Slide,
            }
            
            /// <summary>
            /// 设置状态栏的隐藏状态
            /// </summary>
            /// <param name="hidden">隐藏</param>
            /// <param name="withAnimation">隐藏显示时的动画类型，无动画/渐变/滑动</param>
            public static void SetStatusBarHidden(bool hidden, UIStatusBarAnimation withAnimation = UIStatusBarAnimation.None)
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
            
            /// <summary>
            /// 设置状态栏的样式
            /// </summary>
            /// <param name="style">样式：白色、黑色、自动（根据系统暗色主题）</param>
            /// <param name="animated">应用渐变动画</param>
            public static void SetStatusBarStyle(UIStatusBarStyle style, bool animated = false)
            {
                _SetStatusBarStyle((int)style, animated);
            }
                
            /// <summary>
            /// 在应用内顶部展示一个内容为<c>alertString</c>，时长<c>duration</c>秒的横幅
            /// </summary>
            /// <param name="alertString">内容</param>
            /// <param name="duration">时长 default - 5</param>
            public static void ShowTempAlert(string alertString, int duration = 5)
            {
                _ShowTempAlert(alertString, duration);
            }
        }
        public static class Device
        {
            [DllImport("__Internal")]
            static extern bool _IsSuperuser();
            
            [DllImport("__Internal")]
            static extern void _SetAudioExclusive(bool exclusive);
            
            [DllImport("__Internal")]
            static extern void _PlayHaptics(int style, float intensity);
                
            [DllImport("__Internal")]
            static extern string _GetCountryCode();
            
            
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
            
            
            public enum UIImpactFeedbackStyle
            {
                Light,
                Medium,
                Heavy,
                Soft,
                Rigid
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
        public static class NativeShare
        {
            [DllImport("__Internal")]
            private static extern void _Share(string message, string url, string imagePath);
            [DllImport("__Internal")]
            private static extern void _SaveFileDialog(string content, string fileName);
            [DllImport("__Internal")]
            private static extern void _SelectFileDialog(string ext);
            
            /// <summary>
            ///  调用系统分享功能
            /// </summary>
            /// <param name="message">分享内容</param>
            /// <param name="url">分享链接</param>
            /// <param name="imagePath">分享图片的本地路径</param>
            /// <param name="closeCallback">用户关闭分享面板的回调</param>
            public static void Share(string message, string url = "", string imagePath = "", Action closeCallback = null)
            {
                _Share(message, url, imagePath);
                iOSCallbackHelper.Instance.SetShareCloseCallback(closeCallback);
            }
            
            /// <summary>
            /// 调用系统保存文件对话框，允许玩家选择保存文件的路径
            /// </summary>
            /// <param name="content">文件内容</param>
            /// <param name="fileName">文件名.后缀名</param>
            /// <param name="callback">玩家关闭对话框回调</param>
            /// <returns>保存成功</returns>
            public static bool SaveFileDialog(string content, string fileName, Action callback = null)
            {
                _SaveFileDialog(content, fileName);
                iOSCallbackHelper.Instance.SetSaveFileCallback(callback);
                return true; 
            }
            
            /// <summary>
            /// 调用系统选择文件对话框，允许玩家选择文件
            /// </summary>
            /// <param name="ext">文件类型（拓展名）</param>
            /// <param name="callback">玩家选择并读取文件后的回调（String）</param>
            /// <param name="failedCallback">文件读取失败的回调（文件类型无效）</param>
            public static void SelectFileDialog(string ext, Action<string> callback = null, Action failedCallback = null)
            {
                iOSCallbackHelper.Instance.SetFileSelectedCallback(callback);
                iOSCallbackHelper.Instance.SetFileSelectedFailedCallback(failedCallback);
                _SelectFileDialog(ext); 
            }
       }
    }
}



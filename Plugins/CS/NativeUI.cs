using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace iOSNativePlugin
{
    public static class NativeUI
    {
        [DllImport("__Internal")] static extern void NativeUI_SafariViewFromUrl(string url, CompletionCallback onCompletionCallback);
        [DllImport("__Internal")] static extern void NativeUI_SafariPageSheetFromUrl(string url, CompletionCallback onCompletionCallback);
        [DllImport("__Internal")] static extern void NativeUI_RegisterStatusBarOrientationChangeCallback(OrientationChangeCallback callback);
        [DllImport("__Internal")] static extern void NativeUI_UnregisterStatusBarOrientationChangeCallback();
        [DllImport("__Internal")] static extern int NativeUI_GetStatusBarOrientation();
        [DllImport("__Internal")] static extern void NativeUI_SetStatusBarOrientation(int orientation);
        [DllImport("__Internal")] static extern bool NativeUI_IsStatusBarHidden();
        [DllImport("__Internal")] static extern void NativeUI_ShowTempMessage(string alertString, int duration = 5);
        [DllImport("__Internal")] static extern void NativeUI_SetStatusBarHidden(bool hidden, int withAnimation);
        [DllImport("__Internal")] static extern void NativeUI_SetStatusBarStyle(int style, bool animated);
        [DllImport("__Internal")] static extern void NativeUI_ShowDialog(string title, string message, string[] actions, int count, int style, DialogSelectionCallback callback);

        public static bool HideHomeIndicator
        {
            get
            {
#if UNITY_IOS //&& UNITY_2017_2_OR_NEWER
                return UnityEngine.iOS.Device.hideHomeButton;
#else
                return false;
#endif
            }
            set
            {
#if UNITY_IOS //&& UNITY_2017_2_OR_NEWER
                UnityEngine.iOS.Device.hideHomeButton = value;
#endif
            }
        }
        
        
        
        /// <summary>
        /// 调用游戏内Safari窗口打开url
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="onCompletionCallback">用户关闭窗口回调</param>
        public static void SafariViewFromUrl(string url, Action onCompletionCallback = null)
        {
            NativeUI_SafariViewFromUrl(url, OnSafariViewCompletionCallback);
            _onSafariViewComplete = onCompletionCallback;
        }
        
        /// <summary>
        /// 调用游戏内Safari窗口打开url（以UIModalPresentationPageSheet方式）
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="onCompletionCallback">用户关闭窗口回调</param>
        public static void SafariPageSheetFromUrl(string url, Action onCompletionCallback = null)
        {
            NativeUI_SafariPageSheetFromUrl(url, OnSafariViewCompletionCallback);
            _onSafariViewComplete = onCompletionCallback;
        }

        private static Action _onSafariViewComplete;
        
        [MonoPInvokeCallback(typeof(CompletionCallback))]
        static void OnSafariViewCompletionCallback()
        {
            if (_onSafariViewComplete != null)
                _onSafariViewComplete();

            _onSafariViewComplete = null;
        }
        
        

        static Action<UIInterfaceOrientation> _onStatusBarOrientationChanged;
            
        /// <summary>
        /// UI朝向变更事件
        /// </summary>
        public static event Action<UIInterfaceOrientation> OnStatusBarOrientationChanged
        {
            add
            {
                _onStatusBarOrientationChanged += value;
                NativeUI_RegisterStatusBarOrientationChangeCallback(OnStatusBarOrientationChangeCallback);
            }
            remove
            {
                _onStatusBarOrientationChanged -= value;
                    
                if(_onStatusBarOrientationChanged == null)
                    NativeUI_UnregisterStatusBarOrientationChangeCallback();
            }
        }
            
            
        [MonoPInvokeCallback(typeof(OrientationChangeCallback))]
        static void OnStatusBarOrientationChangeCallback(int orientation)
        {
            if (_onStatusBarOrientationChanged != null)
                _onStatusBarOrientationChanged((UIInterfaceOrientation)orientation);
        }
            
        /// <summary>
        /// 当前UI的朝向
        /// </summary>
        public static UIInterfaceOrientation StatusBarOrientation
        {
            get
            {
                return (UIInterfaceOrientation)NativeUI_GetStatusBarOrientation();
            }
            set
            {
                NativeUI_SetStatusBarOrientation((int)value);
            }
        }
            
        /// <summary>
        /// 判断当前系统状态栏是否被隐藏
        /// </summary>
        /// <returns><c>true</c> - 隐藏 <para><c>false</c> - 显示</para></returns>
        public static bool IsStatusBarHidden()
        {
            return NativeUI_IsStatusBarHidden();
        }

           
            
        /// <summary>
        /// 设置状态栏的隐藏状态
        /// </summary>
        /// <param name="hidden">隐藏</param>
        /// <param name="withAnimation">隐藏显示时的动画类型，无动画/渐变/滑动</param>
        public static void SetStatusBarHidden(bool hidden, UIStatusBarAnimation withAnimation = UIStatusBarAnimation.None)
        {
            NativeUI_SetStatusBarHidden(hidden, (int)withAnimation);
        }
            
            
        /// <summary>
        /// 设置状态栏的样式
        /// </summary>
        /// <param name="style">样式：白色、黑色、自动（根据系统暗色主题）</param>
        /// <param name="animated">应用渐变动画</param>
        public static void SetStatusBarStyle(UIStatusBarStyle style, bool animated = false)
        {
            NativeUI_SetStatusBarStyle((int)style, animated);
        }
                
        /// <summary>
        /// 在应用内顶部展示一个内容为<c>alertString</c>，时长<c>duration</c>秒的横幅
        /// </summary>
        /// <param name="alertString">内容</param>
        /// <param name="duration">时长 default - 5</param>
        public static void ShowTempMessage(string alertString, int duration = 5)
        {
            NativeUI_ShowTempMessage(alertString, duration);
        }


            
        /// <summary>
        /// 显示一个对话框，允许用户进行回应
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="message">对话框内容</param>
        /// <param name="callback">回调（参数int，依据用户的选择，回调对应action的index）</param>
        /// <param name="style">对话框样式（UIAlertControllerStyle）</param>
        /// <param name="actions">（params数组）对话框选项（action）<para><b>注 - UIAlertActionStyle会影响最终呈现在玩家屏幕上的选项顺序，但不会影响回调中的index顺序</b></para></param>
        public static void ShowDialog(string title, string message, Action<int> callback, UIAlertControllerStyle style, params UIAlertAction[] actions)
        {
            if(actions == null || actions.Length <= 0)
                return;

            string[] actionsArray = new string[actions.Length];
                
            for (int i = 0; i < actions.Length; i++)
            {
                actionsArray[i] = actions[i];
            }

            NativeUI_ShowDialog(title, message, actionsArray, actions.Length, (int)style, OnDialogSelectionCallback);
            ShowDialogCallback = callback;
        }
            

        private static Action<int> ShowDialogCallback;
            
        [MonoPInvokeCallback(typeof(DialogSelectionCallback))]
        static void OnDialogSelectionCallback(int selection)
        {
            if (ShowDialogCallback != null)
                ShowDialogCallback(selection);

            ShowDialogCallback = null;
        }
    }
}

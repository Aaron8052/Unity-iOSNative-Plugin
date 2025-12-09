using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace iOSNativePlugin
{
    using @char = System.Byte;
    public static class NativeUI
    {
        [DllImport("__Internal")] static extern bool NativeUI_UIAccessibilityIsBoldTextEnabled();
        [DllImport("__Internal")] static extern void NativeUI_RegisterUIAccessibilityBoldTextStatusDidChangeNotification(Action @event);
        [DllImport("__Internal")] static extern int NativeUI_PreferredContentSizeCategory();
        [DllImport("__Internal")] static extern void NativeUI_RegisterUIContentSizeCategoryDidChangeNotification(Action @event);
        [DllImport("__Internal")] static extern void NativeUI_GetUnityViewSize(ref double x, ref double y);
        [DllImport("__Internal")] static extern void NativeUI_OpenUrl(string url);
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
        [DllImport("__Internal")] static extern void NativeUI_ShowDialog(string title, string message, string[] actions, int count, int style, UIPopoverArrowDirection arrowDir,
            double posX, double posY, double width, double height,
            DialogSelectionCallback callback);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        unsafe struct UIActionInfo
        {
            @char* title, image, identifier;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        unsafe struct UIMenuInfo
        {
            bool isTopMenu;
            @char* title, image, identifier;
            UIMenuOptions options;
            UIMenuInfo* childrenMenus;
            UIActionInfo* childrenActions;
            int childrenMenusCount, childrenActionsCount;
        }

        /// <summary>
        /// 系统设置 - 粗体文本
        /// </summary>
        /// <returns></returns>
        public static bool UIAccessibilityIsBoldTextEnabled() => NativeUI_UIAccessibilityIsBoldTextEnabled();

        [Obsolete("Use UIAccessibilityBoldTextStatusChange instead")]
        public static event Action OnUIAccessibilityBoldTextStatusChange
        {
            add => UIAccessibilityBoldTextStatusChange += value;
            remove => UIAccessibilityBoldTextStatusChange -= value;
        }

        /// <summary>
        /// 系统设置 - 粗体文本 状态变更事件
        /// </summary>
        public static event Action UIAccessibilityBoldTextStatusChange
        {
            add
            {
                NativeUI_RegisterUIAccessibilityBoldTextStatusDidChangeNotification(OnUIAccessibilityBoldTextStatusDidChangeNotification);
                uiAccessibilityBoldTextStatusChange += value;
            }
            remove => uiAccessibilityBoldTextStatusChange -= value;
        }

        static event Action uiAccessibilityBoldTextStatusChange;

        [MonoPInvokeCallback(typeof(Action))]
        static void OnUIAccessibilityBoldTextStatusDidChangeNotification()
        {
            uiAccessibilityBoldTextStatusChange?.Invoke();
        }


        /// <summary>
        /// 获取系统字体大小，UIContentSizeCategory.Large 为标准大小
        /// <returns>(string) UIContentSizeCategory: https://developer.apple.com/documentation/uikit/uicontentsizecategory?language=objc</returns>
        /// </summary>
        public static UIContentSizeCategory PreferredContentSizeCategory =>
            (UIContentSizeCategory)NativeUI_PreferredContentSizeCategory();

        /// <summary>
        /// 获取系统字体大小缩放比例，1f 为 100% 大小
        /// </summary>
        public static float PreferredContentSizeCategoryScale
        {
            get
            {
                return PreferredContentSizeCategory switch
                {
                    UIContentSizeCategory.ExtraSmall => 0.80f,
                    UIContentSizeCategory.Small => 0.85f,
                    UIContentSizeCategory.Medium => 0.9f,
                    UIContentSizeCategory.Large => 1f,
                    UIContentSizeCategory.ExtraLarge => 1.1f,
                    UIContentSizeCategory.ExtraExtraLarge => 1.2f,
                    UIContentSizeCategory.ExtraExtraExtraLarge => 1.35f,
                    UIContentSizeCategory.AccessibilityMedium => 1.6f,
                    UIContentSizeCategory.AccessibilityLarge => 1.9f,
                    UIContentSizeCategory.AccessibilityExtraLarge => 2.35f,
                    UIContentSizeCategory.AccessibilityExtraExtraLarge => 2.75f,
                    UIContentSizeCategory.AccessibilityExtraExtraExtraLarge => 3.1f,
                    _ => 1f,
                };
            }
        }

        [Obsolete("Use UIContentSizeCategoryChange instead")]
        public static event Action OnUIContentSizeCategoryChange
        {
            add => UIContentSizeCategoryChange += value;
            remove => UIContentSizeCategoryChange -= value;
        }

        /// <summary>
        /// 系统字体大小变更事件
        /// </summary>
        public static event Action UIContentSizeCategoryChange
        {
            add
            {
                NativeUI_RegisterUIContentSizeCategoryDidChangeNotification(OnUIContentSizeCategoryDidChangeNotification);
                uiContentSizeCategoryChange += value;
            }
            remove => uiContentSizeCategoryChange -= value;
        }

        static event Action uiContentSizeCategoryChange;

        [MonoPInvokeCallback(typeof(Action))]
        static void OnUIContentSizeCategoryDidChangeNotification()
        {
            uiContentSizeCategoryChange?.Invoke();
        }

        /// <summary>
        /// 获取游戏的逻辑分辨率，用于与iOS原生UI进行交互、计算
        /// </summary>
        public static Vector2 UnityViewSize
        {
            get
            {
                double x = 0, y = 0;
                NativeUI_GetUnityViewSize(ref x, ref y);
                return new Vector2((float)x, (float)y);
            }
        }
        
        public static bool HideHomeIndicator
        {
#if UNITY_IOS //&& UNITY_2017_2_OR_NEWER
            get => UnityEngine.iOS.Device.hideHomeButton;
#else
            get => false;
#endif
            set
            {
#if UNITY_IOS //&& UNITY_2017_2_OR_NEWER
                UnityEngine.iOS.Device.hideHomeButton = value;
#endif
            }
        }


        /// <summary>
        /// 打开 Url，与 Application.OpenURL 功能一致
        /// </summary>
        /// <param name="url">Url</param>
        public static void OpenURL(string url)
        {
            NativeUI_OpenUrl(url);
        }


        /// <summary>
        /// 调用游戏内 Safari 窗口打开 Url
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="onCompletionCallback">用户关闭窗口回调</param>
        public static void SafariViewFromUrl(string url, Action onCompletionCallback = null)
        {
            NativeUI_SafariViewFromUrl(url, OnSafariViewCompletionCallback);
            onSafariViewComplete = onCompletionCallback;
        }
        
        /// <summary>
        /// 调用游戏内 Safari 窗口打开 Url（以 UIModalPresentationPageSheet 方式）
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="onCompletionCallback">用户关闭窗口回调</param>
        public static void SafariPageSheetFromUrl(string url, Action onCompletionCallback = null)
        {
            NativeUI_SafariPageSheetFromUrl(url, OnSafariViewCompletionCallback);
            onSafariViewComplete = onCompletionCallback;
        }

        static Action onSafariViewComplete;
        
        [MonoPInvokeCallback(typeof(CompletionCallback))]
        static void OnSafariViewCompletionCallback()
        {
            onSafariViewComplete?.Invoke();
            onSafariViewComplete = null;
        }
        
        

        static Action<UIInterfaceOrientation> onStatusBarOrientationChanged;

        [Obsolete("Use StatusBarOrientationChanged instead")]
        public static event Action<UIInterfaceOrientation> OnStatusBarOrientationChanged
        {
            add => StatusBarOrientationChanged += value;
            remove => StatusBarOrientationChanged -= value;
        }

        /// <summary>
        /// UI 朝向变更事件
        /// </summary>
        public static event Action<UIInterfaceOrientation> StatusBarOrientationChanged
        {
            add
            {
                onStatusBarOrientationChanged += value;
                NativeUI_RegisterStatusBarOrientationChangeCallback(OnStatusBarOrientationChangeCallback);
            }
            remove
            {
                onStatusBarOrientationChanged -= value;
                    
                if(onStatusBarOrientationChanged == null)
                    NativeUI_UnregisterStatusBarOrientationChangeCallback();
            }
        }
            
            
        [MonoPInvokeCallback(typeof(OrientationChangeCallback))]
        static void OnStatusBarOrientationChangeCallback(int orientation)
        {
            onStatusBarOrientationChanged?.Invoke((UIInterfaceOrientation)orientation);
        }
            
        /// <summary>
        /// 当前 UI 的朝向
        /// </summary>
        public static UIInterfaceOrientation StatusBarOrientation
        {
            get => (UIInterfaceOrientation)NativeUI_GetStatusBarOrientation();
            set => NativeUI_SetStatusBarOrientation((int)value);
        }
            
        /// <summary>
        /// 判断当前系统状态栏是否被隐藏
        /// </summary>
        /// <returns></returns>
        public static bool IsStatusBarHidden() => NativeUI_IsStatusBarHidden();


        /// <summary>
        /// 设置状态栏的隐藏状态
        /// </summary>
        /// <param name="hidden">隐藏</param>
        /// <param name="withAnimation">隐藏显示时的动画类型，无动画/渐变/滑动</param>
        public static void SetStatusBarHidden(bool hidden, UIStatusBarAnimation withAnimation = UIStatusBarAnimation.None)
            => NativeUI_SetStatusBarHidden(hidden, (int)withAnimation);


        /// <summary>
        /// 设置状态栏的样式
        /// </summary>
        /// <param name="style">样式：白色、黑色、自动（根据系统暗色主题，iOS 16 以上根据画面亮度自动调整）</param>
        /// <param name="animated">应用渐变动画</param>
        public static void SetStatusBarStyle(UIStatusBarStyle style, bool animated = false)
            => NativeUI_SetStatusBarStyle((int)style, animated);

        /// <summary>
        /// 在应用内顶部展示一个内容为 <c>alertString</c>，时长 <c>duration</c> 秒的横幅
        /// </summary>
        /// <param name="alertString">内容</param>
        /// <param name="duration">时长，默认5秒</param>
        public static void ShowTempMessage(string alertString, int duration = 5)
            => NativeUI_ShowTempMessage(alertString, duration);


        /// <summary>
        /// 显示一个对话框，允许用户进行回应
        /// </summary>
        /// <param name="title">对话框标题（Nullable）</param>
        /// <param name="message">对话框内容（Nullable）</param>
        /// <param name="callback">回调（参数int，依据用户的选择，回调对应 action 在数组中的 index）</param>
        /// <param name="style">对话框样式（UIAlertControllerStyle）</param>
        /// <param name="actions">对话框选项 params<para><b>注 - 每个 action 的 UIAlertActionStyle 的不同会影响最终呈现在玩家屏幕上的选项顺序，但不会影响回调中的 index 顺序</b></para></param>
        public static void ShowDialog(string title, string message, Action<int> callback, UIAlertControllerStyle style, params UIAlertAction[] actions)
        {
            var pos = UnityViewSize;
            pos.x /= 2; // 将对话框放置在屏幕底部中间位置
            ShowDialog(title, message, callback, style, UIPopoverArrowDirection.Any, new Rect(pos, Vector2.one), actions);
        }

        /// <summary>
        /// 显示一个样式为 UIAlertControllerStyle.Alert 的对话框，允许用户进行回应
        /// </summary>
        /// <param name="title">对话框标题（Nullable）</param>
        /// <param name="message">对话框内容（Nullable）</param>
        /// <param name="callback">回调（参数int，依据用户的选择，回调对应 action 在数组中的 index）</param>
        /// <param name="actions">对话框选项 params<para><b>注 - 每个 action 的 UIAlertActionStyle 的不同会影响最终呈现在玩家屏幕上的选项顺序，但不会影响回调中的 index 顺序</b></para></param>
        public static void ShowAlert(string title, string message, Action<int> callback, params UIAlertAction[] actions)
            => ShowDialog(title, message, callback, UIAlertControllerStyle.Alert, UIPopoverArrowDirection.Any, Rect.zero, actions);

        /// <summary>
        /// 显示一个样式为 UIAlertControllerStyle.ActionSheet 的对话框，允许用户进行回应
        /// </summary>
        /// <param name="title">对话框标题（Nullable）</param>
        /// <param name="message">对话框内容（Nullable）</param>
        /// <param name="callback">回调（参数int，依据用户的选择，回调对应 action 在数组中的 index）</param>
        /// <param name="actions">对话框选项 params<para><b>注 - 每个 action 的 UIAlertActionStyle 的不同会影响最终呈现在玩家屏幕上的选项顺序，但不会影响回调中的 index 顺序</b></para></param>
        public static void ShowActionSheet(string title, string message, Action<int> callback, params UIAlertAction[] actions)
        {
            var pos = UnityViewSize;
            pos.x /= 2; // 将对话框放置在屏幕底部中间位置
            ShowActionSheet(title, message, callback, pos, actions);
        }
        
        /// <summary>
        /// 显示一个样式为 UIAlertControllerStyle.ActionSheet 的对话框，允许用户进行回应
        /// </summary>
        /// <param name="title">对话框标题（Nullable）</param>
        /// <param name="message">对话框内容（Nullable）</param>
        /// <param name="callback">回调（参数int，依据用户的选择，回调对应 action 在数组中的 index）</param>
        /// <param name="pos">iPad 设备上分享对话框的显示位置，调用 NativeUI.UnityViewSize 获取游戏的视图大小</param>
        /// <param name="actions">对话框选项 params<para><b>注 - 每个 action 的 UIAlertActionStyle 的不同会影响最终呈现在玩家屏幕上的选项顺序，但不会影响回调中的 index 顺序</b></para></param>
        public static void ShowActionSheet(string title, string message, Action<int> callback, Vector2 pos, params UIAlertAction[] actions)
            => ShowDialog(title, message, callback, UIAlertControllerStyle.ActionSheet, UIPopoverArrowDirection.Any, new Rect(pos, Vector2.one), actions);

        /// <summary>
        /// 显示一个样式为 UIAlertControllerStyle.ActionSheet 的对话框，允许用户进行回应
        /// </summary>
        /// <param name="title">对话框标题（Nullable）</param>
        /// <param name="message">对话框内容（Nullable）</param>
        /// <param name="callback">回调（参数int，依据用户的选择，回调对应 action 在数组中的 index）</param>
        /// <param name="arrowDir">箭头指向方向</param>
        /// <param name="bound">iPad 设备上分享对话框的显示目标bound，对话框将会指向该bound，调用 NativeUI.UnityViewSize 获取游戏的视图大小</param>
        /// <param name="actions">对话框选项 params<para><b>注 - 每个 action 的 UIAlertActionStyle 的不同会影响最终呈现在玩家屏幕上的选项顺序，但不会影响回调中的 index 顺序</b></para></param>
        public static void ShowActionSheet(string title, string message, Action<int> callback, UIPopoverArrowDirection arrowDir,
            Rect bound, params UIAlertAction[] actions)
            => ShowDialog(title, message, callback, UIAlertControllerStyle.ActionSheet, arrowDir, bound, actions);

        static void ShowDialog(string title, string message, Action<int> callback, UIAlertControllerStyle style, UIPopoverArrowDirection arrowDir,
            Rect bound, params UIAlertAction[] actions)
        {
            if(actions == null || actions.Length <= 0)
                return;

            string[] actionsArray = new string[actions.Length];
                
            for (int i = 0; i < actions.Length; i++)
            {
                actionsArray[i] = actions[i];
            }

            NativeUI_ShowDialog(title, message, actionsArray, actions.Length, (int)style, arrowDir,
                bound.x, bound.y, bound.width, bound.height, OnDialogSelectionCallback);
            dialogCallbacks.Push(callback);
        }
            
        static readonly Stack<Action<int>> dialogCallbacks = new Stack<Action<int>>(1);
            
        [MonoPInvokeCallback(typeof(DialogSelectionCallback))]
        static void OnDialogSelectionCallback(int selection)
        {
            if(dialogCallbacks.TryPop(out var callback))
                callback?.Invoke(selection);
        }
    }
}

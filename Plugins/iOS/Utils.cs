using System;

namespace iOSNativePlugin
{
    delegate void DialogSelectionCallback(int selection);
    delegate void ShareCloseCallback();
    delegate void FileSelectCallback(bool selected, string content);
    delegate void FileSavedCallback(bool saved);
    delegate void OrientationChangeCallback(int orientation);
    delegate void CompletionCallback();
    /// <summary>
    /// 指定原生分享的内容、类型
    /// </summary>
    public struct ShareObject
    {
        /// <summary>
        /// 指定对话框按钮的内容、样式
        /// </summary>
        /// <param name="actionWithTitle">按钮内容</param>
        /// <param name="style">按钮文字样式</param>
        public ShareObject(string content, ShareObjectType type = ShareObjectType.NSString)
        {
            _content = content;
            _type = (int)type;
        }

        readonly string _content;
        readonly int _type;
                
        public override string ToString()
        {
            return _type + _content;
        }

        public static implicit operator string(ShareObject obj)
        {
            return obj.ToString();
        }
    }

	public enum ShareObjectType
	{
		NSString = 0,//string
		URL = 1,//Link
		ImagePath = 2,//图片本地路径
	}


    /// <summary>
    /// 当前的UI朝向
    /// </summary>
    public enum UIInterfaceOrientation
    {
        Unknown,
        Portrait,
        PortraitUpsideDown,
        LandscapeLeft,
        LandscapeRight,
    }
    /// <summary>
    /// 设备的物理朝向
    /// </summary>
    public enum UIDeviceOrientation
    {
        Unknown,
        Portrait,
        PortraitUpsideDown,
        LandscapeLeft,
        LandscapeRight,
        FaceUp,
        FaceDown,
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
    /// 对话框样式
    /// </summary>
    public enum UIAlertControllerStyle
    {
        /// <summary>
        /// 位于屏幕底部的Action对话框
        /// </summary>
        ActionSheet = 0,
                
        /// <summary>
        /// 位于屏幕中间的对话框
        /// </summary>
        Alert
    }
            
    /// <summary>
    /// 对话框选项的样式
    /// </summary>
    public enum UIAlertActionStyle
    {
        /// <summary>
        /// 默认蓝色按钮
        /// </summary>
        Default = 0,
                
        /// <summary>
        /// 蓝色加粗按钮
        /// </summary>
        Cancel,
                
        /// <summary>
        /// 红色按钮
        /// </summary>
        Destructive
    }
    
    /// <summary>
    /// 指定对话框按钮的内容、样式
    /// </summary>
    public struct UIAlertAction
    {
        /// <summary>
        /// 指定对话框按钮的内容、样式
        /// </summary>
        /// <param name="actionWithTitle">按钮内容</param>
        /// <param name="style">按钮文字样式</param>
        public UIAlertAction(string actionWithTitle, UIAlertActionStyle style = UIAlertActionStyle.Default)
        {
            _title = actionWithTitle;
            _style = (int)style;
        }

        readonly string _title;
        readonly int _style;
                
        public override string ToString()
        {
            return _style + _title;
        }

        public static implicit operator string(UIAlertAction action)
        {
            return action.ToString();
        }
    }
    
    public enum UIImpactFeedbackStyle
    {
        Light,
        Medium,
        Heavy,
        Soft,
        Rigid
    }
}

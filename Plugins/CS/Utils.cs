using System;
using System.Runtime.InteropServices;

namespace iOSNativePlugin
{
    public static class Utils
    {
        [DllImport("__Internal")] static extern unsafe void FreeCPtr(void* ptr);

        public static unsafe void FreePtr(IntPtr ptr) => FreeCPtr(ptr.ToPointer());

        public static unsafe void FreePtr(void* ptr) => FreeCPtr(ptr);

        public static unsafe string StrFromPtr(char* ptr)
        {
            if (ptr == null)
                return string.Empty;
            return new string(ptr);
        }

        public static unsafe string PtrToStr(char* ptr)
        {
            var str = StrFromPtr(ptr);
            FreePtr(ptr);
            return str;
        }
    }


    delegate void SaveImageToAlbumCallback(bool saved);
    delegate void DialogSelectionCallback(int selection);
    delegate void ShareCloseCallback();
    delegate void FileSelectCallback(bool selected, string content);
    delegate void BoolCallback(bool saved);
    delegate void OrientationChangeCallback(int orientation);
    delegate void CompletionCallback();
    delegate void UserSettingsChangeCallback();
    delegate void LongCallback(long value);

    public enum UIContentSizeCategory
    {
        Unspecified = -1,
        ExtraSmall = 0,                             // 80%
        Small = 1,                                  // 85%
        Medium = 2,                                 // 90%
        Large = 3,                                  // 100%
        ExtraLarge = 4,                             // 110%
        ExtraExtraLarge = 5,                        // 120%
        ExtraExtraExtraLarge = 6,                   // 135%
        AccessibilityMedium = 7,                    // 160%
        AccessibilityLarge = 8,                     // 190%
        AccessibilityExtraLarge = 9,                // 235%
        AccessibilityExtraExtraLarge = 10,          // 275%
        AccessibilityExtraExtraExtraLarge = 11,     // 310%
    }

    static class CF_OPTIONS
    {
        public const ulong
            kCFCalendarUnitEra = (1UL << 1),
            kCFCalendarUnitYear = (1UL << 2),
            kCFCalendarUnitMonth = (1UL << 3),
            kCFCalendarUnitDay = (1UL << 4),
            kCFCalendarUnitHour = (1UL << 5),
            kCFCalendarUnitMinute = (1UL << 6),
            kCFCalendarUnitSecond = (1UL << 7),
            //kCFCalendarUnitWeek API_DEPRECATED("Use kCFCalendarUnitWeekOfYear or kCFCalendarUnitWeekOfMonth instead", macos(10.4,10.10), ios(2.0,8.0), watchos(2.0,2.0), tvos(9.0,9.0)) = (1UL << 8),
            kCFCalendarUnitWeekday = (1UL << 9),
            kCFCalendarUnitWeekdayOrdinal = (1UL << 10),
            kCFCalendarUnitQuarter = (1UL << 11),
            kCFCalendarUnitWeekOfMonth = (1UL << 12),
            kCFCalendarUnitWeekOfYear = (1UL << 13),
            kCFCalendarUnitYearForWeekOfYear = (1UL << 14),
            kCFCalendarUnitDayOfYear = (1UL << 16);
    };

    /// <summary>
    /// https://developer.apple.com/documentation/foundation/nscalendarunit
    /// </summary>
    [Flags] public enum NSCalendarUnit : ulong
    {
        Era                = CF_OPTIONS.kCFCalendarUnitEra,
        Year               = CF_OPTIONS.kCFCalendarUnitYear,
        Month              = CF_OPTIONS.kCFCalendarUnitMonth,
        Day                = CF_OPTIONS.kCFCalendarUnitDay,
        Hour               = CF_OPTIONS.kCFCalendarUnitHour,
        Minute             = CF_OPTIONS.kCFCalendarUnitMinute,
        Second             = CF_OPTIONS.kCFCalendarUnitSecond,
        Weekday            = CF_OPTIONS.kCFCalendarUnitWeekday,
        WeekdayOrdinal     = CF_OPTIONS.kCFCalendarUnitWeekdayOrdinal,
        Quarter            = CF_OPTIONS.kCFCalendarUnitQuarter,
        WeekOfMonth        = CF_OPTIONS.kCFCalendarUnitWeekOfMonth,
        WeekOfYear         = CF_OPTIONS.kCFCalendarUnitWeekOfYear,
        YearForWeekOfYear  = CF_OPTIONS.kCFCalendarUnitYearForWeekOfYear,
        Nanosecond         = (1 << 15),
        DayOfYear          = CF_OPTIONS.kCFCalendarUnitDayOfYear,
    }

    /// <summary>
    /// https://developer.apple.com/documentation/foundation/nsdatecomponents?language=objc
    /// </summary>
    public readonly struct NSDateComponents
    {
        public NSDateComponents(NSCalendarUnit components, DateTime fromDate)
        {
            date = fromDate;
            this.components = components;
        }

        public readonly DateTime date;
        public readonly NSCalendarUnit components;
    }


    /// <summary>
    /// 指定原生分享的内容、类型
    /// </summary>
    public readonly struct ShareObject
    {
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
    public readonly struct UIAlertAction
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

using System;
using System.Runtime.InteropServices;

namespace iOSNativePlugin
{
    public static class Notification
     {
         [DllImport("__Internal")] static extern void Notification_Initialize();
         [DllImport("__Internal")] static extern void Notification_PushNotification(string msg, string title, string identifier, int delay, bool repeats);
         [DllImport("__Internal")] static extern void Notification_PushNotification_Date(string msg, string title, string identifier,  long year, long month, long day, long hour, long minute, long second, ulong units, bool repeats);
         [DllImport("__Internal")] static extern void Notification_RemovePendingNotifications(string identifier);
         [DllImport("__Internal")] static extern void Notification_RemoveAllPendingNotifications();
                
         /// <summary>
         /// 初始化通知系统，无需主动调用
         /// </summary>
         public static void Initialize()
             => Notification_Initialize();

         /// <summary>
         /// 推送本地定时通知
         /// </summary>
         /// <param name="msg">通知内容</param>
         /// <param name="title">标题</param>
         /// <param name="identifier">为通知指定一个标识符，相同的标识符会被系统判定为同一个通知</param>
         /// <param name="delay">等待 delay 秒后推送此通知</param>
         public static void PushNotification(string msg, string title, string identifier, int delay)
             => Notification_PushNotification(msg, title, identifier, delay, false);

         /// <summary>
         /// 推送本地定时通知（循环）
         /// </summary>
         /// <param name="msg">通知内容</param>
         /// <param name="title">标题</param>
         /// <param name="identifier">为通知指定一个标识符，相同的标识符会被系统判定为同一个通知</param>
         /// <param name="delay">等待 delay 秒后推送此通知</param>
         /// <param name="repeats">循环</param>
         public static void PushNotification(string msg, string title, string identifier, int delay, bool repeats)
             => Notification_PushNotification(msg, title, identifier, delay, repeats);

         /// <summary>
         /// 推送本地定时通知
         /// </summary>
         /// <param name="msg">通知内容</param>
         /// <param name="title">标题</param>
         /// <param name="identifier">为通知指定一个标识符，相同的标识符会被系统判定为同一个通知</param>
         /// <param name="dateComp">见 https://developer.apple.com/documentation/usernotifications/uncalendarnotificationtrigger，时区为本地时区</param>
         public static void PushNotification(string msg, string title, string identifier, in NSDateComponents dateComp)
             => PushNotification(msg, title, identifier, dateComp, false);

         /// <summary>
         /// 推送本地定时通知（循环）
         /// </summary>
         /// <param name="msg">通知内容</param>
         /// <param name="title">标题</param>
         /// <param name="identifier">为通知指定一个标识符，相同的标识符会被系统判定为同一个通知</param>
         /// <param name="dateComp">见 https://developer.apple.com/documentation/usernotifications/uncalendarnotificationtrigger，时区为本地时区</param>
         /// <param name="repeats">循环</param>
         public static void PushNotification(string msg, string title, string identifier, in NSDateComponents dateComp, bool repeats)
         {
             Notification_PushNotification_Date(msg, title, identifier,
                 dateComp.date.Year, dateComp.date.Month, dateComp.date.Day,
                 dateComp.date.Hour, dateComp.date.Minute, dateComp.date.Second,
                 (ulong)dateComp.components,
                    repeats);
         }

         /// <summary>
         /// 移除某个待定通知（对于已经推送的通知无效）
         /// </summary>
         /// <param name="identifier">要移除的通知的标识符</param>
         public static void RemovePendingNotifications(string identifier)
             => Notification_RemovePendingNotifications(identifier);

         /// <summary>
         /// 移除所有待定通知（对于已经推送的通知无效）
         /// </summary>
         public static void RemoveAllPendingNotifications()
             => Notification_RemoveAllPendingNotifications();
     }
}

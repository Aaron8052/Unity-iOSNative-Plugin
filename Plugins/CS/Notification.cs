using System.Runtime.InteropServices;

namespace iOSNativePlugin
{
    public static class Notification
     {
         
         [DllImport("__Internal")]
         private static extern void _InitializeNotification();
                
         [DllImport("__Internal")]
         private static extern void _PushNotification(string msg, string title, string identifier, int delay);
    
         [DllImport("__Internal")]
         private static extern void _RemovePendingNotifications(string identifier);
    
         [DllImport("__Internal")]
         private static extern void _RemoveAllPendingNotifications();
                
         /// <summary>
         /// 初始化通知系统
         /// </summary>
         public static void Initialize()
         {
             _InitializeNotification();
         }
                
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
}

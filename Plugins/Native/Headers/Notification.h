#import "../Utils.mm"

@interface Notification : NSObject

+(void)init;
+(void)PushNotification:(NSString *)msg
                  title:(NSString *)title
             identifier:(NSString *)identifier
                  delay:(NSInteger)time;
+(void)RemovePendingNotifications:(NSString *)identifier;
+(void)RemoveAllPendingNotifications;

@end

extern "C"
{
    void Notification_Initialize(){
        [Notification init];
    }
    void Notification_PushNotification(const char *msg, const char *title, const char *identifier, int delay){
        [Notification PushNotification:[NSString stringWithUTF8String:msg ?: ""]
                              title:[NSString stringWithUTF8String:title ?: ""]
                         identifier:[NSString stringWithUTF8String:identifier ?: ""]
                              delay:(NSInteger)delay];
    }
    void Notification_RemovePendingNotifications(const char *identifier){
        [Notification RemovePendingNotifications:[NSString stringWithUTF8String:identifier]];
    }
    void Notification_RemoveAllPendingNotifications(){
        [Notification RemoveAllPendingNotifications];
    }
}

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
    void _InitializeNotification(){
        [Notification init];
    }
    void _PushNotification(const char *msg, const char *title, const char *identifier, int delay){
        [Notification PushNotification:[NSString stringWithUTF8String:msg ?: ""]
                              title:[NSString stringWithUTF8String:title ?: ""]
                         identifier:[NSString stringWithUTF8String:identifier ?: ""]
                              delay:(NSInteger)delay];
    }
    void _RemovePendingNotifications(const char *identifier){
        [Notification RemovePendingNotifications:[NSString stringWithUTF8String:identifier]];
    }
    void _RemoveAllPendingNotifications(){
        [Notification RemoveAllPendingNotifications];
    }
}

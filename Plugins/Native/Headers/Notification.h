#import "../Utils.mm"

@interface Notification : NSObject

+(void)init;
+(void)PushNotification:(NSString *)msg
                  title:(NSString *)title
             identifier:(NSString *)identifier
                  delay:(NSInteger)time
                repeats:(BOOL)repeats;
+(void)PushNotification:(NSString *)msg
                  title:(NSString *)title
             identifier:(NSString *)identifier
                  date:(NSDateComponents *)dateComp
                repeats:(BOOL)repeats;
+(void)RemovePendingNotifications:(NSString *)identifier;
+(void)RemoveAllPendingNotifications;

@end

extern "C"
{
    void Notification_Initialize(){
        [Notification init];
    }
    void Notification_PushNotification(const char *msg, const char *title, const char *identifier, int delay, bool repeats)
    {
        [Notification PushNotification:[NSString stringWithUTF8String:msg ?: ""]
                              title:[NSString stringWithUTF8String:title ?: ""]
                         identifier:[NSString stringWithUTF8String:identifier ?: ""]
                              delay:(NSInteger)delay
                               repeats:repeats];
    }
    void Notification_PushNotification_Date(const char *msg, const char *title, const char *identifier, const char *date, NSCalendarUnit units, bool repeats)
    {
        
        
        NSDateFormatter * formatter = [[NSDateFormatter alloc] init];
        [formatter setDateFormat:@"yyyy-MM-dd HH:mm:ss"];
        NSDate *nsDate = [formatter dateFromString:[NSString stringWithUTF8String:date ?: ""]];
        NSDateComponents *components = [[NSCalendar currentCalendar]components:units fromDate:nsDate];
        
        
        [Notification PushNotification:[NSString stringWithUTF8String:msg ?: ""]
                                 title:[NSString stringWithUTF8String:title ?: ""]
                            identifier:[NSString stringWithUTF8String:identifier ?: ""]
                                  date:components
                               repeats:repeats];
    }
    void Notification_RemovePendingNotifications(const char *identifier){
        [Notification RemovePendingNotifications:[NSString stringWithUTF8String:identifier]];
    }
    void Notification_RemoveAllPendingNotifications(){
        [Notification RemoveAllPendingNotifications];
    }
}

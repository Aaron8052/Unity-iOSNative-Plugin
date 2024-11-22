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
    void Notification_PushNotification_Date(const char *msg, const char *title, const char *identifier, long year, long month, long day, long hour, long min, long sec, bool repeats)
    {
        NSDateComponents *components = [[NSDateComponents alloc] init];
        if(year > -1)   components.year = year;
        if(month > -1)  components.month = month;
        if(day > -1)    components.day = day;
        if(hour > -1)   components.hour = hour;
        if(min > -1)    components.minute = min;
        if(sec > -1)    components.second = sec;
        
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

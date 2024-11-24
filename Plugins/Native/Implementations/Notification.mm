#import <UserNotifications/UserNotifications.h>
#import "../Headers/Notification.h"

@implementation Notification

static UNUserNotificationCenter *notificationCenter;
static BOOL notificationGranted;

+(void)init
{
    if(notificationCenter == nil)
    {
        notificationCenter = [UNUserNotificationCenter currentNotificationCenter];

        [notificationCenter requestAuthorizationWithOptions:
         (UNAuthorizationOptionBadge | UNAuthorizationOptionAlert | UNAuthorizationOptionSound)
         completionHandler:^(BOOL granted, NSError * _Nullable error)
         {
            notificationGranted = granted;
            if(error != nil){
                LOG([NSString stringWithFormat: @"NotificationErrorDes: %@", [error localizedDescription]]);
                LOG([NSString stringWithFormat: @"NotificationErrorReason: %@", [error localizedFailureReason]]);
            }
        }];
    }
    else
    {
        [notificationCenter getNotificationSettingsWithCompletionHandler:^(UNNotificationSettings * _Nonnull settings){
            notificationGranted = settings.authorizationStatus == UNAuthorizationStatusAuthorized;
        }];
    }
}

+(void)PushNotification:(NSString *)msg
                  title:(NSString *)title
             identifier:(NSString *)identifier
                  delay:(NSInteger)time
                repeats:(BOOL)repeats
{
    [Notification init];
    if(!notificationGranted)
        return;

    UNTimeIntervalNotificationTrigger *trigger = [UNTimeIntervalNotificationTrigger triggerWithTimeInterval:time repeats:repeats];
   
    [Notification InternalPushNotification:msg
                                     title:title
                                identifier:identifier
                                   trigger:trigger];
}

+(void)PushNotification:(NSString *)msg
                  title:(NSString *)title
             identifier:(NSString *)identifier
                  date:(NSDateComponents *)dateComp
                repeats:(BOOL)repeats
{
    [Notification init];
    if(!notificationGranted)
        return;
    
    UNCalendarNotificationTrigger *trigger = [UNCalendarNotificationTrigger triggerWithDateMatchingComponents:dateComp repeats:repeats];
   
    // dateComp 数据无效
    if(trigger == nil) return;
    
    [Notification InternalPushNotification:msg
                                     title:title
                                identifier:identifier
                                   trigger:trigger];
}

+(void)InternalPushNotification:(NSString *)msg
                  title:(NSString *)title
             identifier:(NSString *)identifier
                trigger:(UNNotificationTrigger*)trigger
{
    UNMutableNotificationContent* content = [[UNMutableNotificationContent alloc] init];
    content.title = title ?: @"";
    content.body = msg;
    content.sound = [UNNotificationSound defaultSound];
    
    UNNotificationRequest *request = [UNNotificationRequest requestWithIdentifier:identifier content:content trigger:trigger];
    
    [notificationCenter addNotificationRequest:request withCompletionHandler:^(NSError * _Nullable error) {
        NSLog(@"Error: %@", error);
    }];
}

+(void)RemovePendingNotifications:(NSString *)identifier
{
    if(notificationGranted){
        [notificationCenter removePendingNotificationRequestsWithIdentifiers:@[identifier]];
    }

}

+(void)RemoveAllPendingNotifications
{
    if(notificationGranted){
        [notificationCenter removeAllPendingNotificationRequests];
    }
}
@end


    


#import <UserNotifications/UserNotifications.h>
#import "../Headers/Notification.h"

@implementation Notification

static UNUserNotificationCenter *notificationCenter;
static BOOL notificationGranted;

+(void)init{
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
delay:(NSInteger)time{
    [Notification init];
    if(notificationGranted){
        UNMutableNotificationContent* content = [[UNMutableNotificationContent alloc] init];
        content.title = title ?: @"";
        content.body = msg;
        content.sound = [UNNotificationSound defaultSound];
        
        UNTimeIntervalNotificationTrigger *trigger = [UNTimeIntervalNotificationTrigger triggerWithTimeInterval:time repeats:NO];
        
        UNNotificationRequest *request = [UNNotificationRequest requestWithIdentifier:identifier content:content trigger:trigger];
        
        [notificationCenter addNotificationRequest:request withCompletionHandler:^(NSError * _Nullable error) {
            NSLog(@"Error: %@", error);
        }];
    }
    
}


+(void)RemovePendingNotifications:(NSString *)identifier{
    if(notificationGranted){
        [notificationCenter removePendingNotificationRequestsWithIdentifiers:@[identifier]];
    }

}

+(void)RemoveAllPendingNotifications{
    if(notificationGranted){
        [notificationCenter removeAllPendingNotificationRequests];
    }
}
@end


    


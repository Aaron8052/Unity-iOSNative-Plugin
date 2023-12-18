#import "iOSNative.h"

@implementation iOSApplication

static iOSApplication* instance = nil;
//获取单例
+(instancetype)Instance {
    
    if(instance == nil)
    {
        instance = [[iOSApplication alloc] init];
    }
    return instance;
}


+(void)OpenAppSettings
{
    NSURL* url = [NSURL URLWithString:UIApplicationOpenSettingsURLString];
    [[UIApplication sharedApplication]openURL:url options:@{} completionHandler:^(BOOL success)
     {
        if(success)
            LOG(@"Open App Settings Success");
            
        else
            LOG(@"Open App Settings Failed");
        
    }];
}
+(NSString *)GetBundleIdentifier
{
    return [[NSBundle mainBundle] bundleIdentifier];
}
+(NSString *)GetVersion
{
    return [[[NSBundle mainBundle] infoDictionary] objectForKey:@"CFBundleShortVersionString"];
}
+(NSString *)GetBundleVersion
{
    return [[[NSBundle mainBundle] infoDictionary] objectForKey:(NSString *)kCFBundleVersionKey];
}

+(BOOL)GetUserSettingsBool:(NSString *) identifier
{
    return [[NSUserDefaults standardUserDefaults] boolForKey:identifier];
}
+(NSString *)GetUserSettingsString:(NSString *) identifier
{
    return [[NSUserDefaults standardUserDefaults] stringForKey:identifier];
}
+(float)GetUserSettingsFloat:(NSString *) identifier
{
    return [[NSUserDefaults standardUserDefaults] floatForKey:identifier];
}
+(NSInteger)GetUserSettingsInt:(NSString *) identifier
{
    return [[NSUserDefaults standardUserDefaults] integerForKey:identifier];
}

static BOOL IsUserSettingChangeCallbackRegistered = NO;

+(void)RegisterUserSettingsChangeCallback:(UserSettingsChangeCallback) callback
{
    if(IsUserSettingChangeCallbackRegistered)
        return;
    
    [[NSNotificationCenter defaultCenter] addObserver:[self Instance] selector:@selector(OnUserSettingsChanged) name:NSUserDefaultsDidChangeNotification object:nil];
    
    USerSettingsChangedCallback = callback;
    IsUserSettingChangeCallbackRegistered = YES;
}

+(void)UnregisterUserSettingsChangeCallback
{
    if(!IsUserSettingChangeCallbackRegistered)
        return;
    
    [[NSNotificationCenter defaultCenter] removeObserver:[self Instance] name:NSUserDefaultsDidChangeNotification object:nil];
    
    USerSettingsChangedCallback = nil;
    IsUserSettingChangeCallbackRegistered = NO;
    
}
UserSettingsChangeCallback USerSettingsChangedCallback;
-(void)OnUserSettingsChanged
{
    if(USerSettingsChangedCallback != nil)
        USerSettingsChangedCallback();
}
@end





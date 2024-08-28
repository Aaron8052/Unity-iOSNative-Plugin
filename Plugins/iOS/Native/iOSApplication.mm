#import "iOSNative.h"

@implementation iOSApplication

//static iOSApplication* instance = nil;
//获取单例
+ (instancetype)Instance {
    static iOSApplication *instance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
    
        instance = [[iOSApplication alloc] init];
    });
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

// Bool
+(void)SetUserSettingsBool:(NSString *) identifier value:(bool) value
{
    [[NSUserDefaults standardUserDefaults]setObject:value forKey:identifier];
}
+(BOOL)GetUserSettingsBool:(NSString *) identifier
{
    return [[NSUserDefaults standardUserDefaults] boolForKey:identifier];
}

// String
+(void)SetUserSettingsString:(NSString *) identifier value:(NSString *) value
{
    [[NSUserDefaults standardUserDefaults]setObject:value forKey:identifier];
}
+(NSString *)GetUserSettingsString:(NSString *) identifier
{
    return [[NSUserDefaults standardUserDefaults] stringForKey:identifier];
}

// Float
+(void)SetUserSettingsFloat:(NSString *) identifier value:(float) value
{
    [[NSUserDefaults standardUserDefaults]setObject:value forKey:identifier];
}
+(float)GetUserSettingsFloat:(NSString *) identifier
{
    return [[NSUserDefaults standardUserDefaults] floatForKey:identifier];
}

// Int
+(void)SetUserSettingsInt:(NSString *) identifier value:(NSInteger) value
{
    [[NSUserDefaults standardUserDefaults]setObject:value forKey:identifier];
}
+(NSInteger)GetUserSettingsInt:(NSString *) identifier
{
    return [[NSUserDefaults standardUserDefaults] integerForKey:identifier];
}

static BOOL IsUserSettingChangeCallbackRegistered;

+(void)RegisterUserSettingsChangeCallback:(UserSettingsChangeCallback) callback
{
    if(IsUserSettingChangeCallbackRegistered)
        return;
    
    dispatch_async(dispatch_get_main_queue(), ^{
        [[NSNotificationCenter defaultCenter] addObserver:[self Instance] selector:@selector(OnUserSettingsChanged) name:NSUserDefaultsDidChangeNotification object:nil];
    });
    
    
    USerSettingsChangedCallback = callback;
    IsUserSettingChangeCallbackRegistered = YES;
}
-(void)dealloc
{
    [[NSNotificationCenter defaultCenter] removeObserver:self];
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





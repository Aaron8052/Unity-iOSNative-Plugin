#import "iOSNative.h"

@implementation iOSApplication
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
@end





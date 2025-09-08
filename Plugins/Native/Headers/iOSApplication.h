#import "../Utils.mm"

@interface iOSApplication : NSObject

+(void)OpenAppSettings;
+(NSString *)GetBundleIdentifier;
+(NSString *)GetVersion;
+(NSString *)GetBundleVersion;
+(NSString *)GetAlternateIconName;
+(void)SetAlternateIconName:(NSString *)iconName;

+(void)SetUserSettingsBool:(NSString *) identifier value:(bool) value;
+(BOOL)GetUserSettingsBool:(NSString *) identifier;
+(void)SetUserSettingsString:(NSString *) identifier value:(NSString *) value;
+(NSString *)GetUserSettingsString:(NSString *) identifier;
+(void)SetUserSettingsFloat:(NSString *) identifier value:(float) value;
+(float)GetUserSettingsFloat:(NSString *) identifier;
+(void)SetUserSettingsInt:(NSString *) identifier value:(NSInteger) value;
+(NSInteger)GetUserSettingsInt:(NSString *) identifier;

+(void)RegisterUserSettingsChangeCallback:(UserSettingsChangeCallback) callback;
+(void)UnregisterUserSettingsChangeCallback;

@end

extern "C"
{

    const char* iOSApplication_GetAlternateIconName()
    {
        NSString * str = [iOSApplication GetAlternateIconName];
        if(str == nil){
            return nullptr;
        }
        
        return StringCopy([str UTF8String]);
    }

    void iOSApplication_SetAlternateIconName(const char* iconName){
        if(iconName  == nullptr){
            [iOSApplication SetAlternateIconName:nil];
            return;
        }
        [iOSApplication SetAlternateIconName:NSStringFromCStr(iconName)];
    }


    const char* iOSApplication_GetBundleIdentifier()
    {
        return StringCopy([[iOSApplication GetBundleIdentifier] UTF8String]);
    }
    const char* iOSApplication_GetVersion()
    {
        return StringCopy([[iOSApplication GetVersion] UTF8String]);
    }
    const char* iOSApplication_GetBundleVersion()
    {
        return StringCopy([[iOSApplication GetBundleVersion] UTF8String]);
    }

    void iOSApplication_OpenAppSettings(){
        [iOSApplication OpenAppSettings];
    }

    void iOSApplication_SetUserSettingsBool(const char* identifier, bool value){
        [iOSApplication SetUserSettingsBool:NSStringFromCStr(identifier)
                                      value:value];
    }
    bool iOSApplication_GetUserSettingsBool(const char* identifier)
    {
        if(identifier == nil)
            return NO;
        
        return [iOSApplication GetUserSettingsBool: NSStringFromCStr(identifier)];
    }
    void iOSApplication_SetUserSettingsString(const char* identifier, const char* value){
        [iOSApplication SetUserSettingsString:NSStringFromCStr(identifier)
                                      value:NSStringFromCStr(value)];
    }
    const char* iOSApplication_GetUserSettingsString(const char* identifier)
    {
        if(identifier == nil)
            return nil;
        
        return StringCopy([[iOSApplication GetUserSettingsString: NSStringFromCStr(identifier)] UTF8String]);
    }

    void iOSApplication_SetUserSettingsFloat(const char* identifier, float value){
        [iOSApplication SetUserSettingsFloat:NSStringFromCStr(identifier)
                                      value:value];
    }
    float iOSApplication_GetUserSettingsFloat(const char* identifier)
    {
        if(identifier == nil)
            return 0;
        
        return [iOSApplication GetUserSettingsFloat: NSStringFromCStr(identifier)];
    }

    void iOSApplication_SetUserSettingsInt(const char* identifier, long value){
        [iOSApplication SetUserSettingsInt:NSStringFromCStr(identifier)
                                      value:value];
    }
    long iOSApplication_GetUserSettingsInt(const char* identifier)
    {
        if(identifier == nil)
            return 0;
        
        return [iOSApplication GetUserSettingsInt: NSStringFromCStr(identifier)];
    }
    void iOSApplication_RegisterUserSettingsChangeCallback(UserSettingsChangeCallback callback)
    {
        [iOSApplication RegisterUserSettingsChangeCallback:callback];
    }
    void iOSApplication_UnregisterUserSettingsChangeCallback()
    {
        [iOSApplication UnregisterUserSettingsChangeCallback];
    }
}

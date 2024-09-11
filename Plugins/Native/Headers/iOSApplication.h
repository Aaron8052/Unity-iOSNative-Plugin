#import "../Utils.mm"

@interface iOSApplication : NSObject

+(void)OpenAppSettings;
+(NSString *)GetBundleIdentifier;
+(NSString *)GetVersion;
+(NSString *)GetBundleVersion;

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
    const char* _GetBundleIdentifier()
    {
        return StringCopy([[iOSApplication GetBundleIdentifier] UTF8String]);
    }
    const char* _GetVersion()
    {
        return StringCopy([[iOSApplication GetVersion] UTF8String]);
    }
    const char* _GetBundleVersion()
    {
        return StringCopy([[iOSApplication GetBundleVersion] UTF8String]);
    }

    void _OpenAppSettings(){
        [iOSApplication OpenAppSettings];
    }

    void _SetUserSettingsBool(const char* identifier, bool value){
        [iOSApplication SetUserSettingsBool:[NSString stringWithUTF8String:identifier]
                                      value:value];
    }
    bool _GetUserSettingsBool(const char* identifier)
    {
        if(identifier == nil)
            return NO;
        
        return [iOSApplication GetUserSettingsBool: [NSString stringWithUTF8String:identifier]];
    }

    void _SetUserSettingsString(const char* identifier, const char* value){
        [iOSApplication SetUserSettingsString:[NSString stringWithUTF8String:identifier]
                                      value:[NSString stringWithUTF8String:value]];
    }
    const char* _GetUserSettingsString(const char* identifier)
    {
        if(identifier == nil)
            return nil;
        
        return StringCopy([[iOSApplication GetUserSettingsString: [NSString stringWithUTF8String:identifier]] UTF8String]);
    }

    void _SetUserSettingsFloat(const char* identifier, float value){
        [iOSApplication SetUserSettingsFloat:[NSString stringWithUTF8String:identifier]
                                      value:value];
    }
    float _GetUserSettingsFloat(const char* identifier)
    {
        if(identifier == nil)
            return 0;
        
        return [iOSApplication GetUserSettingsFloat: [NSString stringWithUTF8String:identifier]];
    }

    void _SetUserSettingsInt(const char* identifier, long value){
        [iOSApplication SetUserSettingsInt:[NSString stringWithUTF8String:identifier]
                                      value:value];
    }
    long _GetUserSettingsInt(const char* identifier)
    {
        if(identifier == nil)
            return 0;
        
        return [iOSApplication GetUserSettingsInt: [NSString stringWithUTF8String:identifier]];
    }
    void _RegisterUserSettingsChangeCallback(UserSettingsChangeCallback callback)
    {
        [iOSApplication RegisterUserSettingsChangeCallback:callback];
    }
    void _UnregisterUserSettingsChangeCallback()
    {
        [iOSApplication UnregisterUserSettingsChangeCallback];
    }
}

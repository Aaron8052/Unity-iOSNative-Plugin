#import "../Utils.mm"

@interface Device : NSObject
+(BOOL)IsIPhoneNotchScreen;
+(BOOL)IsIPad;
+(NSInteger)GetDeviceOrientation;
+(BOOL)IsMacCatalyst;
+(BOOL)IsSuperuser;
+(void)PlayHaptics:(int)style _intensity:(float)intensity;
+(NSString *)GetCountryCode;

@end

extern "C"
{
    bool Device_IsIPhoneNotchScreen()
    {
        return [Device IsIPhoneNotchScreen];
    }
    bool Device_IsIPad()
    {
        return [Device IsIPad];
    }
    int Device_GetDeviceOrientation(){
        return (int)[Device GetDeviceOrientation];
    }
    bool Device_IsMacCatalyst(){
        return [Device IsMacCatalyst];
    }
    bool Device_IsSuperuser(){
        return [Device IsSuperuser];
    }
    const char* Device_GetCountryCode(){
        return StringCopy([[Device GetCountryCode] UTF8String]);
    }
    void Device_PlayHaptics(int style, float intensity){
        [Device PlayHaptics:style _intensity:intensity];
    }
}

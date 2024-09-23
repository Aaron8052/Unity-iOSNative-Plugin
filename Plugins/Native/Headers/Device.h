#import "../Utils.mm"

@interface Device : NSObject

+(NSInteger)GetDeviceOrientation;
+(BOOL)IsMacCatalyst;
+(BOOL)IsSuperuser;
+(void)PlayHaptics:(int)style _intensity:(float)intensity;
+(NSString *)GetCountryCode;

@end

extern "C"
{
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

#import "../Utils.mm"

@interface Device : NSObject

+(NSInteger)GetDeviceOrientation;
+(BOOL)IsBluetoothHeadphonesConnected;
+(BOOL)IsMacCatalyst;
+(BOOL)IsSuperuser;
+(void)SetAudioExclusive:(BOOL)exclusiveOn;
+(void)PlayHaptics:(int)style _intensity:(float)intensity;
+(NSString *)GetCountryCode;

@end

extern "C"
{
    int _GetDeviceOrientation(){
        return (int)[Device GetDeviceOrientation];
    }
    bool _IsBluetoothHeadphonesConnected(){
        return [Device IsBluetoothHeadphonesConnected];
    }
    bool _IsMacCatalyst(){
        return [Device IsMacCatalyst];
    }
    bool _IsSuperuser(){
        return [Device IsSuperuser];
    }
    const char* _GetCountryCode(){
        return StringCopy([[Device GetCountryCode] UTF8String]);
    }
    void _SetAudioExclusive(bool exclusive){
        [Device SetAudioExclusive:exclusive];
    }

    void _PlayHaptics(int style, float intensity){
        [Device PlayHaptics:style _intensity:intensity];
    }
}

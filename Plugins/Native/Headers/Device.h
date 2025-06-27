#import "../Utils.mm"

@interface Device : NSObject
+(BOOL)IsIPhoneNotchScreen;
+(BOOL)IsIPad;
+(NSInteger)GetDeviceOrientation;
+(BOOL)IsMacCatalyst;
+(BOOL)IsSuperuser;
+(void)PlayHaptics:(int)style _intensity:(float)intensity;
+(NSString *)GetLocaleISOCode;
+(NSString *)GetLanguageISOCode;
+(NSArray<NSString*>*)GetLanguageISOCodes;

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
    void Device_PlayHaptics(int style, float intensity){
        [Device PlayHaptics:style _intensity:intensity];
    }
    const char* Device_GetLocaleISOCode(){
        return StringCopy([[Device GetLocaleISOCode] UTF8String]);
    }
    const char* Device_GetLanguageISOCode(){
        return StringCopy([[Device GetLanguageISOCode] UTF8String]);
    }
    char** Device_GetLanguageCodes(long* count)
    {
        auto array = [Device GetLanguageISOCodes];
        *count = array.count;
        char** languages = (char**)malloc(sizeof(char*) * array.count);
        for (long i = 0; i < *count; i++)
        {
            languages[i] = StringCopy([array[i] UTF8String]);
        }
        return languages;
    }
}

#import "../Utils.mm"

@interface iCloudKeyValueStore : NSObject

+(BOOL)DeleteKey:(NSString*)key;
+(void)InitICloud;
+(BOOL)IsICloudAvailable;
+(BOOL)IsKeyExists:(NSString *)key;
+(BOOL)ClearICloudSave;
+(BOOL)Synchronize;

+(double)GetFloat:(NSString *)key defaultValue:(double)defaultValue;
+(BOOL)SetFloat:(NSString *)key setValue:(double)value;

+(int)GetInt:(NSString *)key defaultValue:(int)defaultValue;
+(BOOL)SetInt:(NSString *)key setValue:(long)value;

+(BOOL)GetBool:(NSString *)key defaultValue:(BOOL)defaultValue;
+(BOOL)SetBool:(NSString *)key setValue:(BOOL)value;

+(NSString *)GetString:(NSString *)key defaultValue:(NSString *)defaultValue;
+(BOOL)SetString:(NSString *)key setValue:(NSString *)value;

@end

extern "C"
{
    void iCloudKeyValueStore_Initialize(){
        [iCloudKeyValueStore InitICloud];
    }

    bool iCloudKeyValueStore_IsICloudAvailable(){
        return [iCloudKeyValueStore IsICloudAvailable];
    }
    bool iCloudKeyValueStore_ICloudKeyExists(const char* key){
        return [iCloudKeyValueStore IsKeyExists:NSStringFromCStr(key)];
    }
    bool iCloudKeyValueStore_ICloudDeleteKey(const char* key){
        return [iCloudKeyValueStore DeleteKey:NSStringFromCStr(key)];
    }
    bool iCloudKeyValueStore_ClearICloudSave(){
        return [iCloudKeyValueStore ClearICloudSave];
    }
    bool iCloudKeyValueStore_Synchronize(){
        return [iCloudKeyValueStore Synchronize];
    }
    const char* iCloudKeyValueStore_GetString(const char *key, const char *defaultValue)
    {
        return StringCopy([[iCloudKeyValueStore GetString:NSStringFromCStr(key)
                                             defaultValue:NSStringFromCStr(defaultValue)] UTF8String]);
    }
    bool iCloudKeyValueStore_SaveString(const char *key, const char *value)
    {
        return [iCloudKeyValueStore SetString:NSStringFromCStr(key)
                                     setValue:NSStringFromCStr(value)];
    }
    int iCloudKeyValueStore_GetInt(const char *key, int defaultValue)
    {
        return [iCloudKeyValueStore GetInt:NSStringFromCStr(key)
                              defaultValue:defaultValue];
    }
    bool iCloudKeyValueStore_SaveInt(const char *key, int value)
    {
        return [iCloudKeyValueStore SetInt:NSStringFromCStr(key)
                                  setValue:value];
    }
    float iCloudKeyValueStore_GetFloat(const char *key, float defaultValue)
    {
        return [iCloudKeyValueStore GetFloat:NSStringFromCStr(key)
                                defaultValue:defaultValue];
    }
    bool iCloudKeyValueStore_SaveFloat(const char *key, float value)
    {
        return [iCloudKeyValueStore SetFloat:NSStringFromCStr(key)
                                    setValue:value];
    }
    bool iCloudKeyValueStore_GetBool(const char *key, bool defaultValue)
    {
        return [iCloudKeyValueStore GetBool:NSStringFromCStr(key)
                               defaultValue:defaultValue];
    }
    bool iCloudKeyValueStore_SaveBool(const char *key, bool value)
    {
        return [iCloudKeyValueStore SetBool:NSStringFromCStr(key)
                                   setValue:value];
    }
}

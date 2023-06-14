#import "iOSNative.h"

@implementation iOSNative
+(void)init{
    [Notification init];
    [iCloudKeyValueStore init];
}

@end

char* StringCopy(const char* string)
{
    if (string == NULL)
        return NULL;
    
    char* newString = (char*)malloc(strlen(string) + 1);
    strcpy(newString, string);

    return newString;
}

extern "C"
{
    void _initialize(){
        [iOSNative init];
    }
    bool _IsStatusBarHidden(){
        return [NativeUI IsStatusBarHidden];
    }
    void _SetStatusBarHidden(bool hidden, int withAnimation){
        [NativeUI SetStatusBarHidden:hidden withAnimation:withAnimation];
    }
    void _SetStatusBarStyle(int style, bool animated){
        [NativeUI SetStatusBarStyle:style animated:animated];
    }

    void _ShowTempAlert(const char* alertString, int duration = 5){
        [NativeUI ShowTempAlert:[NSString stringWithUTF8String:alertString ?: ""] duration:duration];
    }
    
    
    const char* _GetBundleIdentifier()
    {
        return StringCopy([[Device GetBundleIdentifier] UTF8String]);
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


    void _Share(const char* message, const char* url, const char* imagePath)
    {
        [NativeShare shareMsg:[NSString stringWithUTF8String:message ?: ""] addUrl:[NSString stringWithUTF8String:url ?: ""] imgPath:[NSString stringWithUTF8String:imagePath ?: ""]];
    }
    void _SaveFileDialog(const char* content, const char* fileName)
    {
        [NativeShare SaveFileDialog:[NSString stringWithUTF8String:content ?: ""] fileName:[NSString stringWithUTF8String:fileName ?: ""]];
    }
    void _SelectFileDialog(const char* ext)
    {
        [NativeShare SelectFileDialog:[NSString stringWithUTF8String:ext ?: ""]];
    }
    
    
    void _PushNotification(const char *msg, const char *title, const char *identifier, int delay){
        [Notification PushNotification:[NSString stringWithUTF8String:msg ?: ""]
                              title:[NSString stringWithUTF8String:title ?: ""]
                         identifier:[NSString stringWithUTF8String:identifier ?: ""]
                              delay:(NSInteger)delay];
    }
    void _RemovePendingNotifications(const char *identifier){
        [Notification RemovePendingNotifications:[NSString stringWithUTF8String:identifier]];
    }
    void _RemoveAllPendingNotifications(){
        [Notification RemoveAllPendingNotifications];
    }
    
    
    
    bool _IsICloudAvailable(){
        return [iCloudKeyValueStore IsICloudAvailable];
    }
    bool _ClearICloudSave(){
        return [iCloudKeyValueStore ClearICloudSave];
    }
    bool _Synchronize(){
        return [iCloudKeyValueStore Synchronize];
    }
    const char* _iCloudGetString(const char *key, const char *defaultValue)
    {
        return StringCopy([[iCloudKeyValueStore iCloudGetString:[NSString stringWithUTF8String:key] defaultValue:[NSString stringWithUTF8String:defaultValue]] UTF8String]);
    }
    bool _iCloudSaveString(const char *key, const char *value)
    {
        return [iCloudKeyValueStore iCloudSaveString:[NSString stringWithUTF8String:key] setValue:[NSString stringWithUTF8String:value]];
    }   
    int _iCloudGetInt(const char *key, int defaultValue)
    {
        return [iCloudKeyValueStore iCloudGetInt:[NSString stringWithUTF8String:key] defaultValue:defaultValue];
    }   
    bool _iCloudSaveInt(const char *key, int value)
    {
        return [iCloudKeyValueStore iCloudSaveInt:[NSString stringWithUTF8String:key] setValue:value];
    }
    float _iCloudGetFloat(const char *key, float defaultValue)
    {
        return [iCloudKeyValueStore iCloudGetFloat:[NSString stringWithUTF8String:key] defaultValue:defaultValue];
    }  
    bool _iCloudSaveFloat(const char *key, float value)
    {
        return [iCloudKeyValueStore iCloudSaveFloat:[NSString stringWithUTF8String:key] setValue:value];
    }
    bool _iCloudGetBool(const char *key, bool defaultValue)
    {
        return [iCloudKeyValueStore iCloudGetBool:[NSString stringWithUTF8String:key] defaultValue:defaultValue];
    }
    bool _iCloudSaveBool(const char *key, bool value)
    {
        return [iCloudKeyValueStore iCloudSaveBool:[NSString stringWithUTF8String:key] setValue:value];
    }
}

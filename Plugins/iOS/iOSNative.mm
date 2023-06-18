#import "iOSNative.h"

@implementation iOSNative
+(void)init{
    [Notification init];
    [iCloudKeyValueStore init];
}

+(NSString *)GetBundleIdentifier{
    return [[NSBundle mainBundle] bundleIdentifier];
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
    const char* _GetBundleIdentifier()
    {
        return StringCopy([[iOSNative GetBundleIdentifier] UTF8String]);
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

    void _ShowTempMessage(const char* alertString, int duration = 5){
        [NativeUI ShowTempMessage:[NSString stringWithUTF8String:alertString ?: ""] duration:duration];
    }
    void _ShowDialog(const char* title, const char* message, const char** actions, int count, int style, DialogSelectionCallback callback)
    {
        if(count <= 0)
            return;
        
        NSMutableArray *actionsArray = [NSMutableArray array];
        
        for(int i = 0; i< count; i++){
            NSString *str = [NSString stringWithUTF8String:actions[i]];
            [actionsArray addObject:str];
        }
        
        [NativeUI ShowDialog:[NSString stringWithUTF8String:title ?: ""]
                     message:[NSString stringWithUTF8String:message ?: ""]
                  actions:actionsArray
                       style:style
                    callback:callback];
    }
    
    
    bool IsMacCatalyst(){
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


    void _Share(const char* message, const char* url, const char* imagePath, ShareCloseCallback callback)
    {
        [NativeShare shareMsg:[NSString stringWithUTF8String:message ?: ""]
         addUrl:[NSString stringWithUTF8String:url ?: ""]
        imgPath:[NSString stringWithUTF8String:imagePath ?: ""]
        callback:callback];
    }
    void _SaveFileDialog(const char* content, const char* fileName, FileSavedCallback callback)
    {
        [NativeShare SaveFileDialog:[NSString stringWithUTF8String:content ?: ""] fileName:[NSString stringWithUTF8String:fileName ?: ""]
        callback:callback];

    }
    void _SelectFileDialog(const char* ext, FileSelectCallback callback)
    {
        [NativeShare SelectFileDialog:[NSString stringWithUTF8String:ext ?: ""]
        callback:callback];
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

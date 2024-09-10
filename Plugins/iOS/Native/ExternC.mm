#import "iOSNative.h"

extern "C"
{
    //GameKit
    void _ShowGameCenterView(CompletionCallback callback)
    {
        [iOSGameKit ShowGameCenterView:callback];
    }
    void _LoadScore(const char* leaderboardID, LongCallback callback)
    {
        [iOSGameKit LoadScore:[NSString stringWithUTF8String:leaderboardID] callback:callback];
    }


    //iOSApplication
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
    

    //NativeUI
    void _SafariViewFromUrl(const char* url, CompletionCallback onCompletionCallback)
    {
        if(url == NULL)
            return;
        
        [NativeUI SafariViewFromUrl:[NSString stringWithUTF8String:url]
               onCompletionCallback:onCompletionCallback];
    }
    
    void _RegisterStatusBarOrientationChangeCallback(OrientationChangeCallback callback)
    {
        [NativeUI RegisterStatusBarOrientationChangeCallback:callback];
    }
    void _UnregisterStatusBarOrientationChangeCallback(){
        [NativeUI UnregisterStatusBarOrientationChangeCallback];
    }
    int _GetStatusBarOrientation(){
        return (int)[NativeUI GetStatusBarOrientation];
    }
    void _SetStatusBarOrientation(int orientation){
        [NativeUI SetStatusBarOrientation:orientation];
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
    

    //Device
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


    //NativeShare
    void _SaveImageToAlbum(const char* imagePath, SaveImageToAlbumCallback callback){
        [NativeShare SaveImageToAlbum:[NSString stringWithUTF8String:imagePath ?: ""] callback:callback];
    }
    void _Share(const char* message, const char* url, const char* imagePath, ShareCloseCallback callback)
    {
        [NativeShare ShareMessage:[NSString stringWithUTF8String:message ?: ""]
         addUrl:[NSString stringWithUTF8String:url ?: ""]
        imgPath:[NSString stringWithUTF8String:imagePath ?: ""]
        callback:callback];
    }
    void _ShareObjects(const char** objects, int count, ShareCloseCallback callback)
    {
        if(count <= 0)
            return;
        
        NSMutableArray<NSString*> *objectsArray = [NSMutableArray array];
        
        for(int i = 0; i< count; i++){
            NSString *str = [NSString stringWithUTF8String:objects[i]];
            [objectsArray addObject:str];
        }
        
        [NativeShare ShareObject:objectsArray callback:callback];
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
    
    
    //Notification
    void _InitializeNotification(){
        [Notification init];
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
    
    
    //iCloudKeyValueStore
    void _InitializeICloud(){
        [iCloudKeyValueStore InitICloud];
    }
    
    bool _IsICloudAvailable(){
        return [iCloudKeyValueStore IsICloudAvailable];
    }
    bool _ICloudKeyExists(const char* key){
        return [iCloudKeyValueStore IsKeyExists:[NSString stringWithUTF8String:key ?: nil]];
    }
    bool _ICloudDeleteKey(const char* key){
        return [iCloudKeyValueStore DeleteKey:[NSString stringWithUTF8String:key ?: nil]];
    }
    bool _ClearICloudSave(){
        return [iCloudKeyValueStore ClearICloudSave];
    }
    bool _Synchronize(){
        return [iCloudKeyValueStore Synchronize];
    }
    const char* _iCloudGetString(const char *key, const char *defaultValue)
    {
        return StringCopy([[iCloudKeyValueStore GetString:[NSString stringWithUTF8String:key] 
                                             defaultValue:[NSString stringWithUTF8String:defaultValue]] UTF8String]);
    }
    bool _iCloudSaveString(const char *key, const char *value)
    {
        return [iCloudKeyValueStore SetString:[NSString stringWithUTF8String:key] 
                                     setValue:[NSString stringWithUTF8String:value]];
    }
    int _iCloudGetInt(const char *key, int defaultValue)
    {
        return [iCloudKeyValueStore GetInt:[NSString stringWithUTF8String:key] 
                              defaultValue:defaultValue];
    }
    bool _iCloudSaveInt(const char *key, int value)
    {
        return [iCloudKeyValueStore SetInt:[NSString stringWithUTF8String:key] 
                                  setValue:value];
    }
    float _iCloudGetFloat(const char *key, float defaultValue)
    {
        return [iCloudKeyValueStore GetFloat:[NSString stringWithUTF8String:key] 
                                defaultValue:defaultValue];
    }
    bool _iCloudSaveFloat(const char *key, float value)
    {
        return [iCloudKeyValueStore SetFloat:[NSString stringWithUTF8String:key] 
                                    setValue:value];
    }
    bool _iCloudGetBool(const char *key, bool defaultValue)
    {
        return [iCloudKeyValueStore GetBool:[NSString stringWithUTF8String:key] 
                               defaultValue:defaultValue];
    }
    bool _iCloudSaveBool(const char *key, bool value)
    {
        return [iCloudKeyValueStore SetBool:[NSString stringWithUTF8String:key] 
                                   setValue:value];
    }
}

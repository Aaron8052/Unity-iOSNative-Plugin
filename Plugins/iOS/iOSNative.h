#import <Foundation/Foundation.h>
#import "UnityAppController.h"

extern UIViewController *UnityGetGLViewController();

static void LOG(NSString* log){
    @autoreleasepool {
        NSLog(@"[iOS Native] %@", log);
    }
}
static void SendCallback(const char* method, const char* msg){
    UnitySendMessage("iOSCallbackHelper", method, msg);
}



@interface iOSNative : NSObject
+(void)init;
@end



@interface NativeShare : NSObject
+(void)shareMsg:(NSString *)message addUrl:(NSString *)url imgPath:(NSString *)filePath;
+(void)SaveFileDialog:(NSString *)content fileName:(NSString *)fileName;
+(void)SelectFileDialog:(NSString *)ext;
@end



@interface Notification : NSObject
+(void)init;
+(void)PushNotification:(NSString *)msg title:(NSString *)title identifier:(NSString *)identifier delay:(NSInteger)time;
+(void)RemovePendingNotifications:(NSString *)identifier;
+(void)RemoveAllPendingNotifications;
@end



@interface iCloudKeyValueStore : NSObject
+(void)init;
+(BOOL)IsICloudAvailable;
+(BOOL)KeyExists:(NSString *)key;
+(BOOL)ClearICloudSave;
+(double)iCloudGetFloat:(NSString *)key defaultValue:(double)defaultValue;
+(BOOL)iCloudSaveFloat:(NSString *)key setValue:(double)value;
+(int)iCloudGetInt:(NSString *)key defaultValue:(int)defaultValue;
+(BOOL)iCloudSaveInt:(NSString *)key setValue:(long)value;
+(BOOL)iCloudGetBool:(NSString *)key defaultValue:(BOOL)defaultValue;
+(BOOL)iCloudSaveBool:(NSString *)key setValue:(BOOL)value;
+(NSString *)iCloudGetString:(NSString *)key defaultValue:(NSString *)defaultValue;
+(BOOL)iCloudSaveString:(NSString *)key setValue:(NSString *)value;
+(BOOL)Synchronize;
@end



@interface Device : NSObject
+(NSString *)GetBundleIdentifier;
+(BOOL)IsSuperuser;
+(void)SetAudioExclusive:(BOOL)exclusiveOn;
+(void)PlayHaptics:(int)style _intensity:(float)intensity;
+(NSString *)GetCountryCode;
@end



@interface NativeUI : NSObject
+(BOOL)IsStatusBarHidden;
+(void)SetStatusBarHidden:(BOOL)hidden withAnimation:(NSInteger)withAnimation;
+(void)SetStatusBarStyle:(NSInteger)style animated:(BOOL)animated;
+(void)ShowTempAlert:(NSString *)alertString duration:(NSInteger)duration;
+(void)ShowTempAlert:(NSString *)alertString;
@end

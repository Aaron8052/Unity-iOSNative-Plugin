#import <Foundation/Foundation.h>
#import "UnityAppController.h"
#import "Utils.mm"

extern UIViewController *UnityGetGLViewController();



@interface iOSApplication : NSObject
+(NSString *)GetBundleIdentifier;
@end



@interface NativeShare : NSObject
+(void)shareMsg:(NSString *)message addUrl:(NSString *)url imgPath:(NSString *)filePath callback:(ShareCloseCallback)callback;
+(void)SaveFileDialog:(NSString *)content fileName:(NSString *)fileName callback:(FileSavedCallback)callback;
+(void)SelectFileDialog:(NSString *)ext  callback:(FileSelectCallback)callback;
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
+(NSInteger)GetDeviceOrientation;
+(BOOL)IsBluetoothHeadphonesConnected;
+(BOOL)IsMacCatalyst;
+(BOOL)IsSuperuser;
+(void)SetAudioExclusive:(BOOL)exclusiveOn;
+(void)PlayHaptics:(int)style _intensity:(float)intensity;
+(NSString *)GetCountryCode;
@end



@interface NativeUI : NSObject
+(void)RegisterStatusBarOrientationChangeCallback:(OrientationChangeCallback)callback;
+(void)UnregisterStatusBarOrientationChangeCallback;
+(NSInteger)GetStatusBarOrientation;
+(void)SetStatusBarOrientation:(NSInteger)orientation;
+(BOOL)IsStatusBarHidden;
+(void)SetStatusBarHidden:(BOOL)hidden withAnimation:(NSInteger)withAnimation;
+(void)SetStatusBarStyle:(NSInteger)style animated:(BOOL)animated;
+(void)ShowTempMessage:(NSString *)alertString duration:(NSInteger)duration;
+(void)ShowTempMessage:(NSString *)alertString;
+(void)ShowDialog:(NSString *) title message:(NSString *)message actions:(NSMutableArray*)actions
            style:(NSInteger)style
         callback:(DialogSelectionCallback)callback;
@end
//此文件包含了所有类的头定义以及通用的一些方法
//所有类的实现都在对应的同名.mm文件中
#import <Foundation/Foundation.h>
#import "UnityAppController.h"

extern UIViewController *UnityGetGLViewController();

static void LOG(NSString* log){
    @autoreleasepool {
        NSLog(@"[iOS Native] %@", log);
    }
}




@interface iOSNative : NSObject
+(void)init;
@end



@interface iOSShare : NSObject
+(void)shareMsg:(NSString *)message addUrl:(NSString *)url imgPath:(NSString *)filePath;
+(void)SaveFileDialog:(NSString *)content fileName:(NSString *)fileName;
+(BOOL)SelectFileDialog:(NSString *)ext;
@end



@interface iOSNotification : NSObject
+(void)init;
+(void)PushNotification:(NSString *)msg title:(NSString *)title identifier:(NSString *)identifier delay:(NSInteger)time;
+(void)RemovePendingNotifications:(NSString *)identifier;
+(void)RemoveAllPendingNotificaions;
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



@interface iOSDevice : NSObject
+(void)PlayHaptics:(int)style _intensity:(float)intensity;
+(NSString *)GetCountryCode;
@end



@interface iOSUIView : NSObject
+(void)SetStatusBarHidden:(BOOL)hidden;
+(void)SetStatusBarStyle:(NSInteger)style;
+(void)ShowTempAlert:(NSString *)alertString duration:(NSInteger)duration;
+(void)ShowTempAlert:(NSString *)alertString;
@end

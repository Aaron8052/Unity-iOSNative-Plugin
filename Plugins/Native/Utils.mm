// 包含本插件内的共享内容
#import <UIKit/UIKit.h>
#import <Foundation/Foundation.h>
#import "UnityAppController.h"

extern UIViewController *UnityGetGLViewController();

static void LOG(NSString* log){
    @autoreleasepool {
        NSLog(@"[iOS Native] %@", log);
    }
}



static BOOL StringIsNullOrEmpty(NSString* str)
{
    return str == nil || [str isEqualToString:@""];
}

static NSString* NSStringFromCStr(const char* str){
    return [NSString stringWithUTF8String:str ?: ""];
}

static UIImage* UIImageFromString(NSString* str){
    if(StringIsNullOrEmpty(str))
        return nil;
    if([[NSFileManager defaultManager] fileExistsAtPath:str])
        return [UIImage imageWithContentsOfFile:str] ?: nil;
    
    if (@available(iOS 13.0, *))
        return [UIImage systemImageNamed:str] ?: nil;
    return nil;
}

static char* StringCopy(const char* string)
{
    if (string == NULL)
        return NULL;
    
    char* newString = (char*)malloc(strlen(string) + 1);
    strcpy(newString, string);

    return newString;
}

extern "C" void FreeCPtr(void* ptr)
{
    free(ptr);
}




static NSDate* DateFromLong(long year, long month, long day, long hour, long minute, long second)
{
    NSDateComponents *components = [[NSDateComponents alloc] init];
    components.year = year;
    components.month = month;
    components.day = day;
    components.hour = hour;
    components.minute = minute;
    components.second = second;
    NSCalendar *calendar = [NSCalendar currentCalendar];
    NSDate *date = [calendar dateFromComponents:components];
    return date;
}

typedef void (*Action)();
typedef void (*SaveImageToAlbumCallback)(bool);
typedef void (*ShareCloseCallback)();
typedef void (*BoolCallback)(bool);
typedef void (*FileSelectCallback)(bool, const char*);
typedef void (*StringCallback)(char*);
typedef void (*DialogSelectionCallback)(int);
typedef void (*OrientationChangeCallback)(int);
typedef void (*CompletionCallback)();
typedef void (*UserSettingsChangeCallback)();
typedef void (*LongCallback)(long);
typedef void (*ULongCallback)(unsigned long);

static void InitUIPopoverViewController(UIViewController *viewController, float posX, float posY)
{
    if(viewController == nil || UI_USER_INTERFACE_IDIOM() != UIUserInterfaceIdiomPad)
        return;
    
    if (@available(iOS 13.0, *)) {
        viewController.modalPresentationStyle = UIModalPresentationAutomatic;
    } else {
        viewController.modalPresentationStyle = UIModalPresentationPopover;
    }
    
    viewController.popoverPresentationController.sourceView = UnityGetGLViewController().view;
    viewController.popoverPresentationController.sourceRect = CGRectMake(posX, posY, 1, 1 );
}

static void InitUIPopoverViewController(UIViewController *viewController)
{
    if(viewController == nil || UI_USER_INTERFACE_IDIOM() != UIUserInterfaceIdiomPad)
        return;
    
    UIView* unityView = UnityGetGLViewController().view;
    CGSize size = unityView.frame.size;
    
    // 不指定位置的话默认设到屏幕中间
    InitUIPopoverViewController(viewController, size.width / 2, size.height);
}

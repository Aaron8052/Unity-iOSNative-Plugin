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

static char* StringCopy(const char* string)
{
    if (string == NULL)
        return NULL;
    
    char* newString = (char*)malloc(strlen(string) + 1);
    strcpy(newString, string);

    return newString;
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
typedef void (*FileSavedCallback)(bool);
typedef void (*FileSelectCallback)(bool, const char*);
typedef void (*DialogSelectionCallback)(int);
typedef void (*OrientationChangeCallback)(int);
typedef void (*CompletionCallback)();
typedef void (*UserSettingsChangeCallback)();
typedef void (*LongCallback)(long);

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

// 包含本插件内的共享内容

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

typedef void (*SaveImageToAlbumCallback)(bool);
typedef void (*ShareCloseCallback)();
typedef void (*FileSavedCallback)(bool);
typedef void (*FileSelectCallback)(bool, const char*);
typedef void (*DialogSelectionCallback)(int);
typedef void (*OrientationChangeCallback)(int);
typedef void (*CompletionCallback)();
typedef void (*UserSettingsChangeCallback)();
typedef void (*LongCallback)(long);

#import "iOSNative.h"
#import <MobileCoreServices/MobileCoreServices.h>
#import <UIKit/UIKit.h>



@implementation NativeShare

+(void)shareMsg:(NSString *)message addUrl:(NSString *)url imgPath:(NSString *)filePath  callback:(ShareCloseCallback)callback
{
    NSMutableArray *items = [NSMutableArray new];//创建分享内容List
    [items addObject:message ?: @""];//添加message到List
    [items addObject:[NSURL URLWithString:url ?: @""]];//添加URL到List

    UIImage *image = [UIImage imageWithContentsOfFile:filePath ?: @""];//从filePath获取Image
    if(image != nil)//判断是否存在image
        [items addObject:image];//添加image到list
    
    
    //初始化UI控制器
    UIActivityViewController *activity = [[UIActivityViewController alloc] initWithActivityItems:items applicationActivities:nil];
 
    //为iPad初始化分享界面
    if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad) {
        activity.popoverPresentationController.sourceView = activity.view;//设置目标弹窗
        //设置弹窗位置以及大小
        activity.popoverPresentationController.sourceRect = CGRectMake( UnityGetGLViewController().view.frame.size.width / 2, UnityGetGLViewController().view.frame.size.height / 2, 1, 1 );
    }
    //显示分享界面
    [UnityGetGLViewController() presentViewController:activity animated:YES completion:nil];
    
    
    __block UIActivityViewController *activityRef = activity;
    activity.completionWithItemsHandler = ^(UIActivityType , BOOL completed, NSArray *returnedItems, NSError *activityError){
        if(activityError != nil){
            LOG([NSString stringWithFormat: @"Error while sharing : %@", activityError]);
        }
        
        if(activityRef && callback != nil){
            callback();
        }
        activityRef = nil;
    };
    
}
+(void)SaveFileDialog:(NSString *)content fileName:(NSString *)fileName callback:(FileSavedCallback)callback
{
    filePickerAction = 2;
    NSData *fileData = [content dataUsingEncoding:NSUTF8StringEncoding];
    NSURL *fileUrl = [NSURL fileURLWithPath:[NSTemporaryDirectory() stringByAppendingPathComponent:fileName]];
    [fileData writeToURL:fileUrl atomically:YES];
    
    UIDocumentPickerViewController *documentPicker = [[UIDocumentPickerViewController alloc] initWithURL:fileUrl inMode:UIDocumentPickerModeExportToService];
    documentPicker.delegate = [self instance];
    tempFileName = fileName;
    tempFileUrl = fileUrl;
    tempContent = content;
    OnFileSavedCallback = callback;
    documentPicker.modalPresentationStyle = UIModalPresentationFullScreen;
    
    if (@available(iOS 13.0, *)) {
        documentPicker.shouldShowFileExtensions = YES;
    }
    
    [[UIApplication sharedApplication].delegate.window.rootViewController presentViewController:documentPicker animated:YES completion:nil];
}



+(void)SelectFileDialog:(NSString *)ext callback:(FileSelectCallback)callback
{
    filePickerAction = 1;
    targetExt = ext;
    OnFileSelectedCallback = callback;
     UIDocumentPickerViewController *documentPicker = [[UIDocumentPickerViewController alloc] initWithDocumentTypes:@[@"public.data"] inMode:UIDocumentPickerModeImport];
    documentPicker.delegate = [self instance];
    [[UIApplication sharedApplication].delegate.window.rootViewController presentViewController:documentPicker animated:YES completion:nil];
}


+ (instancetype)instance {
    static NativeShare *instance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        instance = [[NativeShare alloc] init];
    });
    return instance;
}
static NSString* tempFileName;
static NSURL* tempFileUrl;
static NSString* tempContent;
static NSString * targetExt;

static FileSavedCallback OnFileSavedCallback  = nil;
static FileSelectCallback OnFileSelectedCallback = nil;

static int filePickerAction = 0;//0: none 1: PickingFile 2: SavingFile
- (void)documentPicker:(UIDocumentPickerViewController *)controller didPickDocumentsAtURLs:(NSArray<NSURL *> *)urls {
    if (urls.count == 0) {
        // No file selected
        return;
    }
    
    NSURL *selectedFileUrl = urls.firstObject;
    NSString *selectedFileName = selectedFileUrl.lastPathComponent;


    
    //Picking File
    if(filePickerAction == 1){
        NSString *extension = [selectedFileName pathExtension];
        if([extension isEqual:targetExt]){
            NSURL *url = urls[0];
            NSString *content = [NSString stringWithContentsOfURL:url encoding:NSUTF8StringEncoding error:nil];
            if(OnFileSelectedCallback != nil){
                OnFileSelectedCallback(YES, [content UTF8String]);
            }
            
        }
        else if(OnFileSelectedCallback != nil)
        {
            OnFileSelectedCallback(NO, "Incorrect File Type");
        }
        OnFileSelectedCallback = nil;
        
       
    }
    //Saving File
    else if (filePickerAction == 2)
    {
        NSString *extension = [tempFileName pathExtension];
        //删除已有文件
        if (![selectedFileName.pathExtension isEqualToString:extension]) {
            NSError *deleteError;
            [[NSFileManager defaultManager] removeItemAtURL:tempFileUrl error:&deleteError];
            if (deleteError) {
                NSLog(@"Error deleting temp file: %@", deleteError.localizedDescription);
            }

            if(OnFileSavedCallback != nil)
                OnFileSavedCallback(NO);
            return;
        }
        
        NSError *writeError;
        BOOL success = [tempContent writeToURL:selectedFileUrl atomically:YES encoding:NSUTF8StringEncoding error:&writeError];
        
        if (success) {
            if(OnFileSavedCallback != nil)
                OnFileSavedCallback(YES);
        } else {
            if(OnFileSavedCallback != nil)
                OnFileSavedCallback(NO);
        }
        OnFileSavedCallback = nil;
        NSError *deleteError;
        [[NSFileManager defaultManager] removeItemAtURL:tempFileUrl error:&deleteError];
        if (deleteError) {
            NSLog(@"Error deleting temp file: %@", deleteError.localizedDescription);
        }
        
    }
    filePickerAction = 0;

}
@end

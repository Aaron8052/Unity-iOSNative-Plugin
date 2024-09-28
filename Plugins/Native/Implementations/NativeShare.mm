#import <MobileCoreServices/MobileCoreServices.h>
#import <UIKit/UIKit.h>
#import <Photos/Photos.h>
#import "../Headers/NativeShare.h"


@implementation NativeShare

//获取单例
+(instancetype)Instance {
    
    static NativeShare *instance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
    
        instance = [[NativeShare alloc] init];
    });
    return instance;
}


+(void)SaveImageToAlbum:(UIImage *)image callback:(SaveImageToAlbumCallback)callback
{
    if(image == nil){
        
        if(callback != nil)
        {
            callback(NO);
        }
        return;
    }
    
    if(!image)
    {
        if(callback != nil)
        {
            callback(NO);
        }
        return;
    }
    
    [[PHPhotoLibrary sharedPhotoLibrary] performChanges:^{
            [PHAssetChangeRequest creationRequestForAssetFromImage:image];
        } completionHandler:^(BOOL success, NSError * _Nullable error) {
            if(callback != nil)
            {
                callback(success);
            }
        }];
}

+(void)ShareObject:(NSMutableArray<NSString*>*)objects
              posX:(CGFloat)posX posY:(CGFloat)posY
          callback:(ShareCloseCallback)callback
{
    if(objects == nil || objects == nil){
        if(callback != nil){
            callback();
        }
        return;
    }
        
    
    
    NSMutableArray *items = [NSMutableArray new];//创建分享内容List
    
    for (int i = 0; i < objects.count; i++) {
        NSString *objectStr = objects[i];
        
        NSString *firstChar = [objectStr substringToIndex:1];
        NSString *str = [objectStr substringFromIndex:1];
        NSInteger objectType = [firstChar intValue];
        
        switch (objectType) {
            case 0://string
                [items addObject:str ?: @""];
                break;
                
            case 1: //url
                [items addObject:[NSURL URLWithString:str ?: @""]];
                break;
                
            case 2://image
            {
                UIImage *image = [UIImage imageWithContentsOfFile:str ?: @""];//从filePath获取Image
                if(image != nil)//判断是否存在image
                    [items addObject:image];//添加image到list
            }
                break;
                
            default:
                break;
        }
    }
    
    
    //初始化UI控制器
    UIActivityViewController *activity = [[UIActivityViewController alloc] initWithActivityItems:items applicationActivities:nil];
 
    //为iPad初始化分享界面
    InitUIPopoverViewController(activity, posX, posY);
    
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
    documentPicker.delegate = [self Instance];
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
    documentPicker.delegate = [self Instance];
    [[UIApplication sharedApplication].delegate.window.rootViewController presentViewController:documentPicker animated:YES completion:nil];
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

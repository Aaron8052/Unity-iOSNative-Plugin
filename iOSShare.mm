#import "iOSNative.h"


static void SendCallback(const char* method,const char* msg){
    UnitySendMessage("iOSCallbackHelper", method, msg);
}

@implementation iOSShare

+(void)shareMsg:(NSString *)message addUrl:(NSString *)url imgPath:(NSString *)filePath
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
        
        if(activityRef){
            SendCallback("OnShareCloseCallback", "Closed");
        }
        activityRef = nil;
    };
    
}
@end

#import <SafariServices/SafariServices.h>
#import "../Utils.mm"

//SFSafariViewControllerDelegate 实现SafariView回调协议
@interface NativeUI : NSObject <SFSafariViewControllerDelegate>
+(CGSize)GetUnityViewSize;
+(void)OpenUrl:(NSString *)url;
+(void)SafariViewFromUrl:(NSString *)url onCompletionCallback:(CompletionCallback)callback;
+(void)SafariPageSheetFromUrl:(NSString *)url onCompletionCallback:(CompletionCallback)callback;
+(void)RegisterStatusBarOrientationChangeCallback:(OrientationChangeCallback)callback;
+(void)UnregisterStatusBarOrientationChangeCallback;
+(NSInteger)GetStatusBarOrientation;
+(void)SetStatusBarOrientation:(NSInteger)orientation;
+(BOOL)IsStatusBarHidden;
+(void)SetStatusBarHidden:(BOOL)hidden withAnimation:(NSInteger)withAnimation;
+(void)SetStatusBarStyle:(NSInteger)style animated:(BOOL)animated;
+(void)ShowTempMessage:(NSString *)alertString duration:(NSInteger)duration;
+(void)ShowTempMessage:(NSString *)alertString;
+(void)ShowDialog:(NSString *) title
          message:(NSString *)message
          actions:(NSMutableArray*)actions
            style:(UIAlertControllerStyle)style
             posX:(CGFloat)posX posY:(CGFloat)posY
         callback:(DialogSelectionCallback)callback;
@end

extern "C"
{
    void NativeUI_GetUnityViewSize(double &x, double &y)
    {
        CGSize size = [NativeUI GetUnityViewSize];
        x = size.width;
        y = size.height;
    }

    void NativeUI_OpenUrl(const char* url)
    {
        if(url == NULL)
            return;
        
        [NativeUI OpenUrl:[NSString stringWithUTF8String:url]];
    }

    void NativeUI_SafariViewFromUrl(const char* url, CompletionCallback onCompletionCallback)
    {
        if(url == NULL)
            return;
        
        [NativeUI SafariViewFromUrl:[NSString stringWithUTF8String:url]
               onCompletionCallback:onCompletionCallback];
    }

    void NativeUI_SafariPageSheetFromUrl(const char* url, CompletionCallback onCompletionCallback)
    {
        if(url == NULL)
            return;
        
        [NativeUI SafariPageSheetFromUrl:[NSString stringWithUTF8String:url]
               onCompletionCallback:onCompletionCallback];
    }
    void NativeUI_RegisterStatusBarOrientationChangeCallback(OrientationChangeCallback callback)
    {
        [NativeUI RegisterStatusBarOrientationChangeCallback:callback];
    }
    void NativeUI_UnregisterStatusBarOrientationChangeCallback(){
        [NativeUI UnregisterStatusBarOrientationChangeCallback];
    }
    int NativeUI_GetStatusBarOrientation(){
        return (int)[NativeUI GetStatusBarOrientation];
    }
    void NativeUI_SetStatusBarOrientation(int orientation){
        [NativeUI SetStatusBarOrientation:orientation];
    }
    bool NativeUI_IsStatusBarHidden(){
        return [NativeUI IsStatusBarHidden];
    }
    void NativeUI_SetStatusBarHidden(bool hidden, int withAnimation){
        [NativeUI SetStatusBarHidden:hidden withAnimation:withAnimation];
    }
    void NativeUI_SetStatusBarStyle(int style, bool animated){
        [NativeUI SetStatusBarStyle:style animated:animated];
    }

    void NativeUI_ShowTempMessage(const char* alertString, int duration = 5){
        [NativeUI ShowTempMessage:[NSString stringWithUTF8String:alertString ?: ""] duration:duration];
    }

    void NativeUI_ShowDialog(const char* title, const char* message, const char** actions, int count, int style, double posX, double posY, DialogSelectionCallback callback)
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
                       style:(UIAlertControllerStyle)style
                        posX:posX posY:posY
                    callback:callback];
    }
}

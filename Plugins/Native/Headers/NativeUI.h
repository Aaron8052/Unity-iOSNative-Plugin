#import <SafariServices/SafariServices.h>
#import "../Utils.mm"

//SFSafariViewControllerDelegate 实现SafariView回调协议
@interface NativeUI : NSObject <SFSafariViewControllerDelegate>
+(void)SafariViewFromUrl:(NSString *)url onCompletionCallback:(CompletionCallback)callback;
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

extern "C"
{
    void _SafariViewFromUrl(const char* url, CompletionCallback onCompletionCallback)
    {
        if(url == NULL)
            return;
        
        [NativeUI SafariViewFromUrl:[NSString stringWithUTF8String:url]
               onCompletionCallback:onCompletionCallback];
    }

    void _RegisterStatusBarOrientationChangeCallback(OrientationChangeCallback callback)
    {
        [NativeUI RegisterStatusBarOrientationChangeCallback:callback];
    }
    void _UnregisterStatusBarOrientationChangeCallback(){
        [NativeUI UnregisterStatusBarOrientationChangeCallback];
    }
    int _GetStatusBarOrientation(){
        return (int)[NativeUI GetStatusBarOrientation];
    }
    void _SetStatusBarOrientation(int orientation){
        [NativeUI SetStatusBarOrientation:orientation];
    }
    bool _IsStatusBarHidden(){
        return [NativeUI IsStatusBarHidden];
    }
    void _SetStatusBarHidden(bool hidden, int withAnimation){
        [NativeUI SetStatusBarHidden:hidden withAnimation:withAnimation];
    }
    void _SetStatusBarStyle(int style, bool animated){
        [NativeUI SetStatusBarStyle:style animated:animated];
    }

    void _ShowTempMessage(const char* alertString, int duration = 5){
        [NativeUI ShowTempMessage:[NSString stringWithUTF8String:alertString ?: ""] duration:duration];
    }
    void _ShowDialog(const char* title, const char* message, const char** actions, int count, int style, DialogSelectionCallback callback)
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
                       style:style
                    callback:callback];
    }
}

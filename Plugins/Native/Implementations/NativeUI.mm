#import <UIKit/UIView.h>
#import "../Headers/NativeUI.h"


@implementation NativeUI
+(CGSize)GetUnityViewSize
{
    return UnityGetGLViewController().view.frame.size;
}
//获取单例
+(instancetype)Instance {
    
    static NativeUI *instance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
    
        instance = [[NativeUI alloc] init];
    });
    return instance;
}


+(void)OpenUrl:(NSString *)url
{
    NSURL * nsUrl = [NSURL URLWithString:url];
    
    if(nsUrl == nil)
        return;
    if([[UIApplication sharedApplication] canOpenURL:nsUrl])
        [[UIApplication sharedApplication] openURL:nsUrl options:@{} completionHandler:nil];
}

+(void)SafariViewFromUrl:(NSString *)url onCompletionCallback:(CompletionCallback)callback
{
    if(url == nil || url.length <= 0){
        return;
    }
    
    NSURL * nsUrl = [NSURL URLWithString:url];
    
    if(nsUrl == nil)
    {
        return;
    }

    SFSafariViewController* safariView = [[SFSafariViewController alloc] initWithURL:nsUrl];
    safariView.delegate = [NativeUI Instance];
    SafariViewCompleteCallback = callback;
    [UnityGetGLViewController() presentViewController:safariView animated:YES completion:nil];

}

+(void)SafariPageSheetFromUrl:(NSString *)url onCompletionCallback:(CompletionCallback)callback
{
    if(url == nil || url.length <= 0){
        return;
    }
    
    NSURL * nsUrl = [NSURL URLWithString:url];
    
    if(nsUrl == nil)
    {
        return;
    }
    
    SFSafariViewController* safariView = [[SFSafariViewController alloc] initWithURL:nsUrl];
    safariView.delegate = [NativeUI Instance];
    safariView.modalPresentationStyle = UIModalPresentationPageSheet;
    SafariViewCompleteCallback = callback;
    [UnityGetGLViewController() presentViewController:safariView animated:YES completion:nil];
    
    
}
static CompletionCallback SafariViewCompleteCallback;

-(void)safariViewControllerDidFinish:(SFSafariViewController *)controller
{
    [controller dismissViewControllerAnimated:YES completion:nil];
    
    if(SafariViewCompleteCallback != nil)
        SafariViewCompleteCallback();
    
    SafariViewCompleteCallback = nil;
}


static OrientationChangeCallback StatusBarOrientationChangeCallback;


//负责接收UI朝向改变事件
-(void)OnStatusBarOrientationChange{
    if(StatusBarOrientationChangeCallback == nil)
        return;
    
    StatusBarOrientationChangeCallback((int)[NativeUI GetStatusBarOrientation]);
}

BOOL StatusBarOrientationChangeCallbackRegistered;

//注册UI朝向改变事件
+(void)RegisterStatusBarOrientationChangeCallback:(OrientationChangeCallback)callback
{
    
    if(StatusBarOrientationChangeCallbackRegistered)
        return;
    
    [[NSNotificationCenter defaultCenter] addObserver:[self Instance] selector:@selector(OnStatusBarOrientationChange) name:UIDeviceOrientationDidChangeNotification object:nil];
    
    
    StatusBarOrientationChangeCallback = callback;
    StatusBarOrientationChangeCallbackRegistered = YES;
}

//取消注册UI朝向改变事件
+(void)UnregisterStatusBarOrientationChangeCallback
{
    if(!StatusBarOrientationChangeCallbackRegistered)
        return;
    
    [[NSNotificationCenter defaultCenter]removeObserver:[self Instance] name:UIDeviceOrientationDidChangeNotification object:nil];
    
    StatusBarOrientationChangeCallback = nil;
    StatusBarOrientationChangeCallbackRegistered = NO;
}
-(void)dealloc
{
    [[NSNotificationCenter defaultCenter] removeObserver:self];
}
+(NSInteger)GetStatusBarOrientation{
    return [UIApplication sharedApplication].statusBarOrientation;
}
+(void)SetStatusBarOrientation:(NSInteger)orientation{
    [UIApplication sharedApplication].statusBarOrientation = (UIInterfaceOrientation)orientation;
}
+(BOOL)IsStatusBarHidden {
    return [UIApplication sharedApplication].isStatusBarHidden;
    //return [UnityGetGLViewController() prefersStatusBarHidden];
}


+(void)SetStatusBarHidden:(BOOL)hidden
            withAnimation:(NSInteger)withAnimation {
    /* if([NativeUI IsStatusBarHidden] == hidden)
        return; */
        
    dispatch_async(dispatch_get_main_queue(), ^{
        [[UIApplication sharedApplication] setStatusBarHidden:hidden
                                                withAnimation:(UIStatusBarAnimation)withAnimation];
        [UnityGetGLViewController() setNeedsStatusBarAppearanceUpdate];
        UIWindow * keyWindow = [UIApplication sharedApplication].keyWindow;
        [keyWindow setNeedsLayout];
        [keyWindow layoutIfNeeded];
    });
}

+(void)SetStatusBarStyle:(NSInteger)style animated:(BOOL)animated{
    
    UIStatusBarStyle statusBarStyle = (UIStatusBarStyle)style;
    
    dispatch_async(dispatch_get_main_queue(), ^{
        [[UIApplication sharedApplication] setStatusBarStyle:statusBarStyle animated:animated];
        [UnityGetGLViewController() setNeedsStatusBarAppearanceUpdate];
        UIWindow * keyWindow = [UIApplication sharedApplication].keyWindow;
        [keyWindow setNeedsLayout];
        [keyWindow layoutIfNeeded];
    });
    
}


+(void)ShowTempMessage:(NSString *)alertString{
    [NativeUI ShowTempMessage:alertString duration:5];
}

+(void)ShowTempMessage:(NSString *)alertString
            duration:(NSInteger)duration
{
    //初始化label对象
    __block UILabel *label = [[UILabel alloc] init];
    label.text = alertString ?: @"";
    
    //创建毛玻璃效果背景
    UIBlurEffect *blurFX = [UIBlurEffect effectWithStyle:UIBlurEffectStyleExtraLight];
    
    //在iOS13及以上系统跟随系统暗色主题
    if (@available(iOS 13.0, *)){
        label.textColor = [UIColor labelColor];
        label.backgroundColor = [UIColor clearColor];
        
        if(UITraitCollection.currentTraitCollection.userInterfaceStyle == UIUserInterfaceStyleDark){
            blurFX = [UIBlurEffect effectWithStyle:UIBlurEffectStyleDark];
        }
    }
    else{
        label.textColor = [UIColor labelColor];
        label.backgroundColor  = [UIColor whiteColor];
    }
    
    //设置label自适应
    [label sizeToFit];
    
    
    __block UIVisualEffectView *vfxView = [[UIVisualEffectView alloc] initWithEffect:blurFX];

    vfxView.layer.cornerRadius = 10;
    vfxView.clipsToBounds = YES;
    
    //创建Rect
    CGRect frame = label.frame;
    
    //设置位置，自动适应安全区
    if (@available(iOS 11.0, *)) {
        UIEdgeInsets safeAreaInsets = UIApplication.sharedApplication.keyWindow.safeAreaInsets;
        frame.origin.y = safeAreaInsets.top + 20;
    } else {
        frame.origin.y = 20;
    }
    frame.origin.x = (UnityGetGLViewController().view.frame.size.width - frame.size.width) /2;
    
    //扩大背景框的大小
    frame.size.width += 20;
    frame.size.height += 20;
    vfxView.frame = frame;
    
    //将背景框应用到label
    label.frame = frame;
    label.center = CGPointMake(CGRectGetWidth(vfxView.bounds) / 2, CGRectGetHeight(vfxView.bounds) / 2);
    //设置圆角
    //label.layer.cornerRadius = CGRectGetHeight(label.bounds)/2.5;
    //label.layer.masksToBounds = YES;
    label.textAlignment = NSTextAlignmentCenter;
    

    [vfxView.contentView addSubview:label];
    [UnityGetGLViewController().view addSubview:vfxView];
   
    //初始位置
    vfxView.transform = CGAffineTransformMakeTranslation(0, CGRectGetHeight(vfxView.bounds));
    
    //播放出现动画
    [UIView animateWithDuration:0.5 animations:^{
        
        vfxView.transform = CGAffineTransformIdentity;
    }];
    
    //延迟duration秒后播放退出动画
    dispatch_after(dispatch_time(DISPATCH_TIME_NOW, (int64_t)((int64_t)duration * NSEC_PER_SEC)), dispatch_get_main_queue(), ^{
        [UIView animateWithDuration:0.5 animations:^{
            vfxView.transform = CGAffineTransformMakeTranslation(0, -label.frame.size.height * 3);
        } completion:^(BOOL finished) {
            [label removeFromSuperview];
            [vfxView removeFromSuperview];
            
            vfxView = nil;
            label = nil;
        }];
    });
    
}

+(void)ShowDialog:(NSString *) title
          message:(NSString *)message
          actions:(NSMutableArray*)actions
            style:(UIAlertControllerStyle)style
             posX:(CGFloat)posX posY:(CGFloat)posY
         callback:(DialogSelectionCallback)callback
{
    
    
    
    UIAlertController *alertController = [UIAlertController alertControllerWithTitle:title message:message preferredStyle:(UIAlertControllerStyle)style];
    
    if (style == UIAlertControllerStyleActionSheet)
    {
        InitUIPopoverViewController(alertController, posX, posY);
    }
    
    UIAlertAction *action;
    
    for(int i = 0; i < actions.count; i++){
        NSString *actionStr = actions[i];
        
        NSString *firstChar = [actionStr substringToIndex:1];
        NSInteger style = [firstChar intValue];
        
        action = [UIAlertAction actionWithTitle:[actionStr substringFromIndex:1]
                    style:(UIAlertActionStyle)style
                    handler:^(UIAlertAction * _Nonnull action) {
            
            if(callback != nil)
                callback(i);
        }];
        
        [alertController addAction:action];
        
    }
    
    
    [UnityGetGLViewController() presentViewController:alertController animated:YES completion:nil];
}
@end

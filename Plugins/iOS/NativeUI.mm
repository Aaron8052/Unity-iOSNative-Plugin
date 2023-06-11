#import "iOSNative.h"
#import <UIKit/UIView.h>
@implementation NativeUI

+(BOOL)IsStatusBarHidden {
    return [UIApplication sharedApplication].isStatusBarHidden;
}


+(void)SetStatusBarHidden:(BOOL)hidden
            withAnimation:(NSInteger)withAnimation {
    if([NativeUI IsStatusBarHidden] == hidden)
        return;
        
    dispatch_async(dispatch_get_main_queue(), ^{
        [[UIApplication sharedApplication] setStatusBarHidden:hidden withAnimation:(UIStatusBarAnimation)withAnimation];
    });
}

+(void)SetStatusBarStyle:(NSInteger)style animated:(BOOL)animated{
    
    UIStatusBarStyle statusBarStyle = (UIStatusBarStyle)style;
    
    dispatch_async(dispatch_get_main_queue(), ^{
        [[UIApplication sharedApplication] setStatusBarStyle:statusBarStyle animated:animated];
    });
    
}


+(void)ShowTempAlert:(NSString *)alertString{
    [NativeUI ShowTempAlert:alertString duration:5];
}

+(void)ShowTempAlert:(NSString *)alertString
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



@end

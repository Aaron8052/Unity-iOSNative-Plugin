#import <UIKit/UIFeedbackGenerator.h>
#import "iOSNative.h"
#import <AVFoundation/AVFoundation.h>

@implementation Device
+(NSInteger)GetDeviceOrientation{
    return [[UIDevice currentDevice]orientation];
}

+(BOOL)IsBluetoothHeadphonesConnected{
    AVAudioSession* audioSession = [AVAudioSession sharedInstance];
    AVAudioSessionPortDescription *firstOutput = [[[audioSession currentRoute]outputs]firstObject];
    
    if([firstOutput portType] == AVAudioSessionPortBluetoothA2DP ||
       [firstOutput portType] == AVAudioSessionPortBluetoothHFP )
    {
        return true;
    }
    return false;
}

+(BOOL)IsMacCatalyst{
#if TARGET_OS_MACCATALYST
    return YES;
#else
    return NO;
#endif
}

+(BOOL)IsSuperuser
{
    if([[NSFileManager defaultManager] fileExistsAtPath:@"User/Applications/"])
        return YES;
    
    if([[NSFileManager defaultManager] fileExistsAtPath:@"/Applications/Cydia.app"])
        return YES;
    
    if([[NSFileManager defaultManager] fileExistsAtPath:@"/private/var/lib/apt"])
        return YES;
    
    if([[NSFileManager defaultManager] fileExistsAtPath:@"/var/lib/dpkg"])
        return YES;
    
    if([[NSFileManager defaultManager] fileExistsAtPath:@"/var/lib/trollstore"])
        return YES;
    
    if([[NSFileManager defaultManager] fileExistsAtPath:@"/var/lib/serotonin"])
        return YES;
    
    return NO;
}

+(void)SetAudioExclusive:(BOOL)exclusiveOn{
    NSError* error = nil;
    AVAudioSession *audioSession = [AVAudioSession sharedInstance];
    
    if(exclusiveOn){
        [audioSession setCategory:AVAudioSessionCategoryPlayback withOptions:AVAudioSessionCategoryOptionMixWithOthers
            error:&error];
    }
    else{
        [audioSession setCategory:AVAudioSessionCategoryAmbient
            error:&error];
    }
    
    [audioSession setActive:YES error:&error];
}

+(void)PlayHaptics:(int)style _intensity:(float)intensity//参数int style，float intensity
{
    if(@available(iOS 10.0, *)){//检测ios版本
        UIImpactFeedbackGenerator *feedBackGenertor = [[UIImpactFeedbackGenerator alloc] initWithStyle:(UIImpactFeedbackStyle)style];
        
        //注：intensity设置仅在iOS13或以上可用
        if (@available(iOS 13.0, *)) {
            [feedBackGenertor impactOccurredWithIntensity:intensity];
        } else {
            [feedBackGenertor impactOccurred];
        }
    }
    
}

+(NSString *)GetCountryCode{
    NSLocale *currentLocale = [NSLocale currentLocale];
    NSString *countryCode = [currentLocale countryCode];
    return countryCode;
}
@end

#import <UIKit/UIFeedbackGenerator.h>
#import "iOSNative.h"

@implementation iOSDevice

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

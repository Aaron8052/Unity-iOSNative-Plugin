#import "iOSNative.h"

@implementation iOSApplication
+(NSString *)GetBundleIdentifier{
    return [[NSBundle mainBundle] bundleIdentifier];
}
+(NSString *)GetVersion{
    return [[[NSBundle mainBundle] infoDictionary] objectForKey:@"CFBundleShortVersionString"];
}
+(NSString *)GetBundleVersion{
    return [[[NSBundle mainBundle] infoDictionary] objectForKey:(NSString *)kCFBundleVersionKey];
}
@end





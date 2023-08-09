#import "iOSNative.h"

@implementation iOSApplication
+(NSString *)GetBundleIdentifier{
    return [[NSBundle mainBundle] bundleIdentifier];
}
+(NSString *)GetVersion{
    return [[NSBundle mainBundle] valueForKey:@"CFBundleShortVersionString"];
}
+(NSString *)GetBundleVersion{
    return [[NSBundle mainBundle] valueForKey:(NSString *)kCFBundleVersionKey];
}
@end





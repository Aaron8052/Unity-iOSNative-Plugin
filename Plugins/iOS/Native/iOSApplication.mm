#import "iOSNative.h"

@implementation iOSApplication
+(NSString *)GetBundleIdentifier{
    return [[NSBundle mainBundle] bundleIdentifier];
}

@end





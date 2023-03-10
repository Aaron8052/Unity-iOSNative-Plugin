#import "iOSNative.h"


static NSUbiquitousKeyValueStore *keyValueStore;
static NSDictionary *cloudDictionary;
static BOOL iCloudInited;
static BOOL availability;
static BOOL noCloudAlertShown;
static NSInteger availabilityAttemptsCount;

@implementation iCloudKeyValueStore
+(BOOL)userLoggedIn{
    return [[NSFileManager defaultManager] ubiquityIdentityToken] != nil;
}
+(void) init{
    if(!iCloudInited && [iCloudKeyValueStore userLoggedIn]){
        @autoreleasepool{
            NSLog(@"init iCloud!");
        }
        keyValueStore = [NSUbiquitousKeyValueStore defaultStore];
        
        cloudDictionary = [keyValueStore dictionaryRepresentation];
        availabilityAttemptsCount = 0;
        iCloudInited = YES;
    }
}

+(BOOL)checkForAvailability{
    if(availabilityAttemptsCount > 20){
        return availability;
    }
    
    if(![iCloudKeyValueStore userLoggedIn] || !iCloudInited || !keyValueStore){
        availability = NO;
    }
    else if(iCloudInited && !availability && availabilityAttemptsCount < 20){
        NSDate *nowDate = [NSDate date];
        NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
        [dateFormatter setDateFormat:@"yyyy-MM-dd HH:mm:ss"];
        
        
        NSString *valueString = [NSString stringWithFormat: @"NS init %@", [dateFormatter stringFromDate:nowDate]];
        
        NSString *keyString = @"initKey";
        [keyValueStore setString:valueString forKey:keyString];
        
        if([[keyValueStore objectForKey:keyString] isEqualToString:valueString])
        {
            availability = [keyValueStore synchronize];
        }
        else{
            availability = NO;
        }

    }
    LOG([NSString stringWithFormat: @"iCloud availability: %@", availability ? @"YES": @"NO"]);
    availabilityAttemptsCount ++;
    return availability;
    
}
+(BOOL)IsICloudAvailable
{
    BOOL available = [iCloudKeyValueStore checkForAvailability];
    
    if(!available && !noCloudAlertShown){
        [iOSNotification PushNotification:@"iCloud save is not available at this point!" title:nil identifier:@"iCloudUnavailable" delay:1];
        
        noCloudAlertShown = YES;
    }
    
    return available;
}

+(BOOL)KeyExists:(NSString *)key
{
    if([iCloudKeyValueStore IsICloudAvailable]){

        return [cloudDictionary objectForKey:key] != nil ;
    }
    return NO;
}

+(BOOL)ClearICloudSave{
    if([iCloudKeyValueStore IsICloudAvailable]){
        @autoreleasepool {
            NSLog(@"Clearing iCloud Saves");
        }
        NSArray *keyArray = [cloudDictionary allKeys];

        for(int i = 0; i < keyArray.count; i++){
            @autoreleasepool {
                NSString *key = [keyArray objectAtIndex:i];
                if(key != NULL){
                    [keyValueStore removeObjectForKey:key];
                }
            }
        }
        return [keyValueStore synchronize];
    }
    return NO;
}

//static double ??????Float
+(double)iCloudGetFloat:(NSString *)key defaultValue:(double)defaultValue
{
    if([iCloudKeyValueStore IsICloudAvailable]){

        if ([iCloudKeyValueStore KeyExists:key]){
            return [keyValueStore doubleForKey:key];
        }
        //[iCloudKeyValueStore iCloudSaveFloat:key setValue:defaultValue];
        return defaultValue;
            
    }
    return 0.0;
}

//static BOOL ??????Float
+(BOOL)iCloudSaveFloat:(NSString *)key setValue:(double)value
{
    if([iCloudKeyValueStore IsICloudAvailable]){

        [keyValueStore setDouble:value forKey:key];
        //return [keyValueStore synchronize];
        return YES;
    }
    return NO;
}

//static int ??????Int
+(int)iCloudGetInt:(NSString *)key defaultValue:(int)defaultValue
{
    if([iCloudKeyValueStore IsICloudAvailable]){
  
        if ([iCloudKeyValueStore KeyExists:key]){
            return [[keyValueStore objectForKey:key] intValue];
        }
        //[iCloudKeyValueStore iCloudSaveInt:key setValue:defaultValue];
        return defaultValue;
    }
    return 0;
}

//static BOOL ??????Int
+(BOOL)iCloudSaveInt:(NSString *)key setValue:(long)value
{
    if([iCloudKeyValueStore IsICloudAvailable]){
 
        [keyValueStore setLongLong:value forKey:key];
        //return [keyValueStore synchronize];
        return YES;
    }
    return NO;
}

//static BOOL ??????Bool
+(BOOL)iCloudGetBool:(NSString *)key defaultValue:(BOOL)defaultValue
{
    if([iCloudKeyValueStore IsICloudAvailable]){

        if ([iCloudKeyValueStore KeyExists:key]){
            return [keyValueStore boolForKey:key];
        }
        //[iCloudKeyValueStore iCloudSaveBool:key setValue:defaultValue];
        return defaultValue;
    }
    return NO;
}

//static BOOL ??????Bool
+(BOOL)iCloudSaveBool:(NSString *)key setValue:(BOOL)value
{
    if([iCloudKeyValueStore IsICloudAvailable]){

        [keyValueStore setBool:value forKey:key];
        //return [keyValueStore synchronize];
        return YES;
    }
    return NO;
}

//static NSString ??????String
+(NSString *)iCloudGetString:(NSString *)key defaultValue:(NSString *)defaultValue
{
    if([iCloudKeyValueStore IsICloudAvailable]){

        if ([iCloudKeyValueStore KeyExists:key]){
            return [keyValueStore stringForKey:key];
        }
        //[iCloudKeyValueStore iCloudSaveString:key setValue:defaultValue];
        return defaultValue;
        
    }
    return @"";
}

//static BOOL ??????String
+(BOOL)iCloudSaveString:(NSString *)key setValue:(NSString *)value
{
    if([iCloudKeyValueStore IsICloudAvailable]){

        [keyValueStore setString:value forKey:key];
        //return [keyValueStore synchronize];
        return YES;
    }
    return NO;
}

+(BOOL) Synchronize{
    return [keyValueStore synchronize];
}
@end


    


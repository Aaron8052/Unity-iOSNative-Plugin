#import <AVFoundation/AVFoundation.h>
#import "../Headers/Audio.h"

@implementation Audio

static Action audioSessionRouteChangedEvent;
static ULongCallback audioInterruptionEvent;
static BOOL inited;

+(void)Init:(Action)audioSessionRouteChangedCallback
    audioInterruptionCallback:(ULongCallback)audioInterruptionCallback
{
    if(inited)
        return;
    audioSessionRouteChangedEvent = audioSessionRouteChangedCallback;
    [[NSNotificationCenter defaultCenter] addObserver: [Audio class]
                                             selector: @selector(OnAudioSessionRouteChanged:)
                                                 name: AVAudioSessionRouteChangeNotification
                                               object: nil];
    
    audioInterruptionEvent = audioInterruptionCallback;
    [[NSNotificationCenter defaultCenter] addObserver: [Audio class]
                                             selector: @selector(OnAudioInterruptionEvent:)
                                                 name: AVAudioSessionInterruptionNotification
                                               object: nil];
    inited = YES;
}

+(BOOL)SetActive:(BOOL)active
{
    __autoreleasing NSError* outError = nil;
    auto success = [[AVAudioSession sharedInstance] setActive:active error:&outError];
    if((outError) != nil)
        return NO;
    if(success)
        audioInterrupted = NO;
    return success;
}

static BOOL audioInterrupted;
+(bool)GetAudioInterrupted
{
    return audioInterrupted;
}

+(bool)GetPrefersNoInterruptionsFromSystemAlerts
{
    if (@available(iOS 14.5, *))
        return [[AVAudioSession sharedInstance] prefersNoInterruptionsFromSystemAlerts];
    return false;
}

+(void)SetPrefersNoInterruptionsFromSystemAlerts:(BOOL)prefersNoInterruptions
{
    if (@available(iOS 14.5, *))
        [[AVAudioSession sharedInstance] setPrefersNoInterruptionsFromSystemAlerts:prefersNoInterruptions error:nil];
}

+(void)OnAudioInterruptionEvent:(NSNotification *)notification
{
    auto userInfo = notification.userInfo;
    auto typeValue = [userInfo[AVAudioSessionInterruptionTypeKey] unsignedIntegerValue];
    auto type = (AVAudioSessionInterruptionType)typeValue;
    audioInterrupted = type == AVAudioSessionInterruptionTypeBegan;
    
    /*switch (type) {
        case AVAudioSessionInterruptionTypeBegan:
            audioInterrupted = true;
            break;
            
        default:
            audioInterrupted = false;
            break;
    }*/
    
    if(audioInterruptionEvent != nil)
        audioInterruptionEvent(typeValue);
}

+(void)OnAudioSessionRouteChanged:(NSNotification *)notification
{
    if(audioSessionRouteChangedEvent != nil)
        audioSessionRouteChangedEvent();
}


+(float)SystemVolume
{
    return [[AVAudioSession sharedInstance] outputVolume];
}

+(double)InputLatency
{
    return [[AVAudioSession sharedInstance] inputLatency];
}

+(double)OutputLatency
{
    return [[AVAudioSession sharedInstance] outputLatency];
}

+(double)SampleRate
{
    return [[AVAudioSession sharedInstance] sampleRate];
}

+(BOOL)IsBluetoothHeadphonesConnected
{
    AVAudioSession* audioSession = [AVAudioSession sharedInstance];
    NSArray<AVAudioSessionPortDescription *> *outputs = [[audioSession currentRoute]outputs];
    
    for(int i = 0 ; i < outputs.count; i++)
    {
        AVAudioSessionPortDescription * port = outputs[i];
        if([port portType] == AVAudioSessionPortBluetoothA2DP ||
           [port portType] == AVAudioSessionPortBluetoothHFP )
        {
            return true;
        }
    }
    
    return false;
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

@end

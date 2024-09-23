#import <AVFoundation/AVFoundation.h>
#import "../Headers/Audio.h"

@implementation Audio

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

#import "../Utils.mm"

@interface Audio : NSObject

+(void)Init:(Action)OnAudioSessionRouteChangedCallback;
+(float)SystemVolume;
+(double)InputLatency;
+(double)OutputLatency;
+(double)SampleRate;
+(BOOL)IsBluetoothHeadphonesConnected;
+(void)SetAudioExclusive:(BOOL)exclusiveOn;

@end

extern "C"
{
    void Audio_Init(Action OnAudioSessionRouteChangedCallback)
    {
        [Audio Init:OnAudioSessionRouteChangedCallback];
    }
    float Audio_SystemVolume()
    {
        return [Audio SystemVolume];
    }
    double Audio_InputLatency()
    {
        return [Audio InputLatency];
    }
    double Audio_OutputLatency()
    {
        return [Audio OutputLatency];
    }
    double Audio_SampleRate()
    {
        return [Audio SampleRate];
    }
    bool Audio_IsBluetoothHeadphonesConnected()
    {
        return [Audio IsBluetoothHeadphonesConnected];
    }
    void Audio_SetAudioExclusive(bool exclusive){
        [Audio SetAudioExclusive:exclusive];
    }
}

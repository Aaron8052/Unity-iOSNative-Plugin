#import "../Utils.mm"

@interface Audio : NSObject

+(void)Init:(Action)audioSessionRouteChangedCallback
    audioInterruptionCallback:(ULongCallback)audioInterruptionCallback;
+(BOOL)SetActive:(BOOL)active;
+(bool)GetAudioInterrupted;
+(bool)GetPrefersNoInterruptionsFromSystemAlerts;
+(void)SetPrefersNoInterruptionsFromSystemAlerts:(BOOL)prefersNoInterruptions;
+(float)SystemVolume;
+(double)InputLatency;
+(double)OutputLatency;
+(double)SampleRate;
+(BOOL)IsBluetoothHeadphonesConnected;
+(void)SetAudioExclusive:(BOOL)exclusiveOn;

@end

extern "C"
{
    void Audio_Init(Action audioSessionRouteChangedCallback,
                    ULongCallback audioInterruptionCallback)
    {
        [Audio Init:audioSessionRouteChangedCallback
                    audioInterruptionCallback:audioInterruptionCallback];
    }
    bool Audio_SetActive(bool active){
        return [Audio SetActive:active];
    }
    bool Audio_GetAudioInterrupted(){
        return [Audio GetAudioInterrupted];
    }
    bool Audio_GetPrefersNoInterruptionsFromSystemAlerts(){
        return [Audio GetPrefersNoInterruptionsFromSystemAlerts];
    }
    void Audio_SetPrefersNoInterruptionsFromSystemAlerts(bool prefersNoInterruptions){
        [Audio SetPrefersNoInterruptionsFromSystemAlerts:prefersNoInterruptions];
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

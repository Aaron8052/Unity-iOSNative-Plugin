#import <GameKit/GameKit.h>
#import "../Utils.mm"

//GKGameCenterControllerDelegate 实现GameCenterView回调协议
@interface iOSGameKit : NSObject <GKGameCenterControllerDelegate>

+(void)ShowGameCenterView:(CompletionCallback)callback;
+(void)LoadScore:(NSString *)leaderboardID callback:(LongCallback)callback;

@end

extern "C"
{
    void iOSGameKit_ShowGameCenterView(CompletionCallback callback)
    {
        [iOSGameKit ShowGameCenterView:callback];
    }
    void iOSGameKit_LoadScore(const char* leaderboardID, LongCallback callback)
    {
        [iOSGameKit LoadScore:[NSString stringWithUTF8String:leaderboardID] callback:callback];
    }
}

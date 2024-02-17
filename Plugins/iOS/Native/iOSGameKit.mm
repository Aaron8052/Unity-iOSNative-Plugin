#import "iOSNative.h"
#import <GameKit/GameKit.h>

@implementation iOSGameKit

//Unity自带的GameCenter获取定期刷新的排行榜分数会报错，怀疑用了过时的api，所以这里自己实现一个
+(void)LoadScore:(NSString *)leaderboardID callback:(LongCallback)callback
{
    //与Unity自带的api类似
    if([GKLocalPlayer localPlayer].isAuthenticated)
    {
        //设置排行榜对象以及要拉取的玩家
        GKLeaderboard *leaderBoard = [[GKLeaderboard alloc] initWithPlayers:@[[GKLocalPlayer localPlayer]]];
        
        
        if(leaderBoard != nil)
        {
            leaderBoard.identifier = leaderboardID;
            
            [leaderBoard loadScoresWithCompletionHandler:^(NSArray<GKScore *> * _Nullable scores, NSError * _Nullable error) 
             {
                //检测有没有成功获取
                if(error != nil || scores.count <= 0)
                {
                    if(callback != nil){
                        callback(-1);
                    }
                    return;
                }
                
                //成功获取到分数，返回第一个结果，类型为long
                GKScore *userScore = scores.firstObject;
                
                if(callback != nil)
                {
                    callback(userScore.value);
                }
                
            }];
            return;
        }
        
        //没有拉取到排行榜对象，返回-1
        if(callback != nil)
        {
            callback(-1);
        }
    }
}

@end





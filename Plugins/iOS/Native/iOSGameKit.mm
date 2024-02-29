#import "iOSNative.h"


@implementation iOSGameKit

//获取单例
+(instancetype)Instance {
    
    static iOSGameKit *instance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
    
        instance = [[iOSGameKit alloc] init];
    });
    return instance;
}


+(void)ShowGameCenterView:(CompletionCallback)callback
{
    //没登录
    if(![GKLocalPlayer localPlayer].isAuthenticated)
    {
        LOG(@"Cannot show Game Center view, user didn't logged in");
        if(callback)
        {
            callback();
        }
        callback = nil;
        return;
    }
    
    GKGameCenterViewController *gameCenterView = [[GKGameCenterViewController alloc] init];
    if (gameCenterView != nil)
    {
        gameCenterView.gameCenterDelegate = [self Instance];
        GameCenterViewControllerDidFinishCallback = callback;
        [UnityGetGLViewController() presentViewController:gameCenterView animated:YES completion:nil];
    }
}

static CompletionCallback GameCenterViewControllerDidFinishCallback;

-(void)gameCenterViewControllerDidFinish:(nonnull GKGameCenterViewController *)gameCenterViewController
{
    if(GameCenterViewControllerDidFinishCallback)
    {
        GameCenterViewControllerDidFinishCallback();
    }
    GameCenterViewControllerDidFinishCallback = nil;
}


//Unity自带的GameCenter获取定期刷新的排行榜分数会报错，怀疑用了过时的api，所以这里自己实现一个
+(void)LoadScore:(NSString *)leaderboardID callback:(LongCallback)callback
{
    //没登录
    if(![GKLocalPlayer localPlayer].isAuthenticated)
    {
        LOG(@"NO leaderboard");
        //没有拉取到排行榜对象，返回-1
        if(callback != nil)
        {
            callback(-1);
        }
        return;
    }
        
    
    //设置排行榜对象以及要拉取的玩家
    //iOS14及以上系统
    if (@available(iOS 14.0, *))
    {
        [GKLeaderboard loadLeaderboardsWithIDs:@[leaderboardID] completionHandler:^(NSArray<GKLeaderboard *> * _Nullable leaderboards,
            NSError * _Nullable error)
        {
                                    
            //加载错误
            if(error != nil)
            {
                NSLog(@"Error loading leaderboard with id: %@", leaderboardID);
                NSLog(@"%@", error.localizedDescription);
                
                if(callback != nil)
                {
                    callback(-1);
                }
                return;
            }
            
            if(leaderboards.count <= 0)
            {
                NSLog(@"No leaderboard found with id: %@", leaderboardID);
                
                if(callback != nil)
                {
                    callback(-1);
                }
                return;
            }
            
            
            //获取第一个排行榜
            GKLeaderboard *leaderboard = leaderboards.firstObject;
            
            //设置筛选范围并加载数据

            [leaderboard loadEntriesForPlayerScope:GKLeaderboardPlayerScopeGlobal timeScope:GKLeaderboardTimeScopeAllTime range:NSMakeRange(1, 100) completionHandler:^(GKLeaderboardEntry * _Nullable_result localPlayerEntry, NSArray<GKLeaderboardEntry *> * _Nullable entries, NSInteger totalPlayerCount, NSError * _Nullable error)
            {
                if(error != nil)
                {
                    NSLog(@"Error loading leaderboard entries");
                    NSLog(@"%@", error.localizedDescription);
                    
                    if(callback != nil)
                    {
                        callback(-1);
                    }
                    return;
                }
                
                if(localPlayerEntry != nil)
                {
                    NSLog(@"Local player entry score: %ld", (long)[localPlayerEntry score]);
                    if(callback != nil)
                    {
                        callback([localPlayerEntry score]);
                    }
                    return;
                }
                
                NSLog(@"No player score entry found with id: %@", leaderboardID);
               
                
                if(callback != nil)
                {
                    callback(-1);
                }
            }];
                                    
                                    
        }];
        return;
    }
    
    
    //iOS14以下系统不支持Recurring排行榜，如果加载的是Recurring排行榜则返回-1
    GKLeaderboard *leaderboard = [[GKLeaderboard alloc] initWithPlayers:@[[GKLocalPlayer localPlayer]]];
    
    if(leaderboard != nil)
    {
        LOG(@"Have leaderboard");
        leaderboard.identifier = leaderboardID;
        
        [leaderboard loadScoresWithCompletionHandler:^(NSArray<GKScore *> * _Nullable scores, NSError * _Nullable error)
         {
            //检测有没有成功获取
            if(error != nil || scores.count <= 0)
            {
                if(callback != nil){
                    callback(-1);
                }
                return;
            }
            if(scores.count <= 0){
                LOG(@"No score");
            }
            //成功获取到分数，返回第一个结果，类型为long
            GKScore *userScore = scores.firstObject;
            LOG([NSString stringWithFormat:@"Have score: %lld", userScore.value]  );
            if(callback != nil)
            {
                callback(userScore.value);
            }
            
        }];
        return;
    }
    LOG(@"NO leaderboard");
    //没有拉取到排行榜对象，返回-1
    if(callback != nil)
    {
        callback(-1);
    }
}



@end





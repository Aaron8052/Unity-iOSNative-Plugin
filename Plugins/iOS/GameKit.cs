using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace iOSNativePlugin
{
    public static class GameKit
    {
        [DllImport("__Internal")]
        private static extern void _ShowGameCenterView(CompletionCallback callback);
        
        [DllImport("__Internal")]
        private static extern void _LoadScore(string leaderboardID, LongCallback callback);
        
        
        
        #region Show Game Center View

        /// <summary>
        /// 显示Game Center界面
        /// </summary>
        /// <param name="gameCenterViewControllerDidFinish">用户关闭GC以及GC调用失败时的回调</param>
        public static void ShowGameCenterView(Action gameCenterViewControllerDidFinish)
        {
            _ShowGameCenterView(OnScoreCallback);
        }
        
        
        private static event CompletionCallback _gameCenterViewControllerDidFinishCallback;
        
        [MonoPInvokeCallback(typeof(CompletionCallback))]
        static void OnScoreCallback()
        {
            if (_gameCenterViewControllerDidFinishCallback != null)
                _gameCenterViewControllerDidFinishCallback();

            _gameCenterViewControllerDidFinishCallback = null;
        }

        #endregion
        
        
        #region Load Score
        //计时Recurring排行榜在iOS14以下不支持，会返回-1
        /// <summary>
        /// 加载指定排行榜当前用户的分数
        /// </summary>
        /// <param name="leaderboardID">排行榜ID</param>
        /// <param name="callback">分数回调，获取失败时返回-1</param>
        public static void LoadScore(string leaderboardID, Action<long> callback)
        {
            _LoadScore(leaderboardID, OnScoreCallback);
            _onScoreCallback = callback;
            
        }
        
        private static event Action<long> _onScoreCallback;
        
        [MonoPInvokeCallback(typeof(LongCallback))]
        static void OnScoreCallback(long score)
        {
            if (_onScoreCallback != null)
                _onScoreCallback(score);

            _onScoreCallback = null;
        }
        
        #endregion
    }
}

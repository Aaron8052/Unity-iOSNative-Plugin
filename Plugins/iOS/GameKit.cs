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
        private static extern void _LoadScore(string leaderboardID, LongCallback callback);


        public static void LoadScore(string leaderboardID, Action<long> callback)
        {
            _onScoreCallback = callback;
            _LoadScore(leaderboardID, OnScoreCallback);
        }
        
        private static Action<long> _onScoreCallback;
        
        [MonoPInvokeCallback(typeof(LongCallback))]
        static void OnScoreCallback(long score)
        {
            if (_onScoreCallback != null)
                _onScoreCallback(score);

            _onScoreCallback = null;
        }
    }
}

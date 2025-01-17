using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace iOSNativePlugin
{
    public static class Keyboard
    {
        static readonly KeyCode[] GCKeyCodeMatch =
        {
            KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I,
            KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R,
            KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z,
            //TODO: ...
        };
        [DllImport("__Internal")] static extern void Keyboard_RegisterKeyPressCallback(LongCallback callback);
        [DllImport("__Internal")] static extern void Keyboard_RegisterKeyReleaseCallback(LongCallback callback);
        [DllImport("__Internal")] static extern bool Keyboard_IsKeyboardSupported();
        [DllImport("__Internal")] static extern bool Keyboard_IsAnyKeyPressed();

        public static bool IsKeyboardSupported() => Keyboard_IsKeyboardSupported();
        public static bool IsAnyKeyPressed() => Keyboard_IsAnyKeyPressed();

        public static event Action<KeyCode> OnKeyPressed;
        public static event Action<KeyCode> OnKeyReleased;

#if !UNITY_EDITOR && UNITY_IOS
        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            Keyboard_RegisterKeyPressCallback(OnKeyPressedCallback);
            Keyboard_RegisterKeyReleaseCallback(OnKeyReleasedCallback);
        }
#endif
        [MonoPInvokeCallback(typeof(LongCallback))]
        static void OnKeyPressedCallback(long GCKeyCode)
        {
            OnKeyPressed?.Invoke(GCKeyCodeMatch[GCKeyCode]);
        }

        [MonoPInvokeCallback(typeof(LongCallback))]
        static void OnKeyReleasedCallback(long GCKeyCode)
        {
            OnKeyReleased?.Invoke(GCKeyCodeMatch[GCKeyCode]);
        }

    }
}

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
        [DllImport("__Internal")] static extern void Keyboard_RegisterKeyPressCallback(LongCallback callback);
        [DllImport("__Internal")] static extern void Keyboard_RegisterKeyReleaseCallback(LongCallback callback);
        [DllImport("__Internal")] static extern bool Keyboard_IsKeyboardSupported();
        [DllImport("__Internal")] static extern bool Keyboard_IsAnyKeyPressed();

        public static event Action<KeyCode> OnKeyPressed;
        public static event Action<KeyCode> OnKeyReleased;

        static void Init()
        {

        }

        [MonoPInvokeCallback(typeof(LongCallback))]
        static void OnKeyPressedCallback(long GCKeyCode)
        {
            OnKeyPressed?.Invoke();
        }

        [MonoPInvokeCallback(typeof(LongCallback))]
        static void OnKeyReleasedCallback(long GCKeyCode)
        {
            OnKeyReleased?.Invoke();
        }

        static KeyCode GetKeyCodeFromGCKeyCode(long GCKeyCode)
        {
            return GCKeyCode switch
            {

            }
        }
    }
}

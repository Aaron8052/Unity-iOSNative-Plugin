#import "../Headers/Keyboard.h"


@implementation Keyboard
static BOOL inited;
API_AVAILABLE(ios(14.0)) static GCKeyboard* keyboard;
static LongCallback OnKeyPressedCallback;
static LongCallback OnKeyReleasedCallback;

+(void)Init
{
    if(inited)
        return;
    if (@available(iOS 14.0, *)) {
        keyboard = GCKeyboard.coalescedKeyboard;
        [[NSNotificationCenter defaultCenter] addObserver:[Keyboard class]
                                                 selector:@selector(OnKeyboardConnected:)
                                                     name:GCKeyboardDidConnectNotification
                                                   object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:[Keyboard class]
                                                 selector:@selector(OnKeyboardDisconnected:)
                                                     name:GCKeyboardDidDisconnectNotification
                                                   object:nil];
    }
    inited = YES;
}
+(void)RegisterKeyPressCallback:(LongCallback)callback
{
    OnKeyPressedCallback = callback;
}

+(void)RegisterKeyReleaseCallback:(LongCallback)callback
{
    OnKeyReleasedCallback = callback;
}

+(void)OnKeyboardConnected:(NSNotification *)notification
{
    if (@available(iOS 14.0, *)) {
        keyboard = notification.object;
        keyboard.keyboardInput.keyChangedHandler = ^(GCKeyboardInput * _Nonnull keyboard, GCControllerButtonInput * _Nonnull key, GCKeyCode keyCode, BOOL pressed)
        {
            if(pressed){
                if(OnKeyPressedCallback)
                    OnKeyPressedCallback(keyCode);
            }
            else{
                if(OnKeyReleasedCallback)
                    OnKeyReleasedCallback(keyCode);
            }
        };
    }
}

+(void)OnKeyboardDisconnected:(NSNotification *)notification
{
    if (@available(iOS 14.0, *)) {
        
        if(keyboard != nil)
            keyboard.keyboardInput.keyChangedHandler = nil;
        keyboard = nil;
        
    }
}

+(BOOL)IsKeyboardSupported
{
    if (@available(iOS 14.0, *))
    {
        [Keyboard Init];
        return keyboard != nil;
    } else {
        return NO;
    }
}

+(BOOL)IsAnyKeyPressed
{
    if (@available(iOS 14.0, *))
    {
        [Keyboard Init];
        return keyboard.keyboardInput.anyKeyPressed;
    } else {
        return false;
    }
}

@end

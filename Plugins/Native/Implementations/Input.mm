#import "../Headers/Input.h"


@implementation Input
static BOOL inited;
API_AVAILABLE(ios(14.0)) static GCKeyboard* keyboard;
NSMutableSet *pressedKeys;

+(void)Init
{
    if(inited)
        return;
    if (@available(iOS 14.0, *)) {
        keyboard = GCKeyboard.coalescedKeyboard;
        pressedKeys = [NSMutableSet init];
        [[NSNotificationCenter defaultCenter] addObserver:[Input class]
                                                 selector:@selector(OnKeyboardConnected:)
                                                     name:GCKeyboardDidConnectNotification
                                                   object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:[Input class]
                                                 selector:@selector(OnKeyboardDisconnected:)
                                                     name:GCKeyboardDidDisconnectNotification
                                                   object:nil];
    }
    inited = YES;
}

+(void)OnKeyboardConnected:(NSNotification *)notification
{
    if (@available(iOS 14.0, *)) {
        keyboard = notification.object;
        keyboard.keyboardInput.keyChangedHandler = ^(GCKeyboardInput * _Nonnull keyboard, GCControllerButtonInput * _Nonnull key, GCKeyCode keyCode, BOOL pressed)
        {
            NSNumber *KeyCodeNumber = [NSNumber numberWithLong:keyCode];
            if(pressed){
                [pressedKeys addObject:KeyCodeNumber];
            }
            else{
                [pressedKeys removeObject:KeyCodeNumber];
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
        [Input Init];
        return keyboard != nil;
    } else {
        return NO;
    }
    
}

+(BOOL)IsAnyKeyPressed
{
    if (@available(iOS 14.0, *))
    {
        [Input Init];
        return keyboard.keyboardInput.anyKeyPressed;
    } else {
        return false;
    }
}

+(BOOL)GetKey:(GCKeyCode)keyCode
API_AVAILABLE(ios(14.0))
{
    if (@available(iOS 14.0, *)){
        [Input Init];
        NSNumber *KeyCodeNumber = [NSNumber numberWithLong:keyCode];
        return [pressedKeys containsObject:KeyCodeNumber];
    }
    return NO;
    
}

@end

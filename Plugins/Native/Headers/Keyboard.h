#import "../Utils.mm"
#import <GameController/GameController.h>

@interface Keyboard : NSObject
+(void)RegisterKeyPressCallback:(LongCallback)callback;
+(void)RegisterKeyReleaseCallback:(LongCallback)callback;
+(BOOL)IsKeyboardSupported;
+(BOOL)IsAnyKeyPressed;
@end

extern "C"
{

void Keyboard_RegisterKeyPressCallback(LongCallback callback)
{
    [Keyboard RegisterKeyPressCallback:callback];
}

void Keyboard_RegisterKeyReleaseCallback(LongCallback callback)
{
    [Keyboard RegisterKeyReleaseCallback:callback];
}

bool Keyboard_IsKeyboardSupported()
{
    return [Keyboard IsKeyboardSupported];
}

bool Keyboard_IsAnyKeyPressed()
{
    return [Keyboard IsAnyKeyPressed];
}
}

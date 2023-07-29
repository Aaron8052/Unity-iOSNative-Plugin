static void LOG(NSString* log){
    @autoreleasepool {
        NSLog(@"[iOS Native] %@", log);
    }
}

static char* StringCopy(const char* string)
{
    if (string == NULL)
        return NULL;
    
    char* newString = (char*)malloc(strlen(string) + 1);
    strcpy(newString, string);

    return newString;
}

typedef void (*ShareCloseCallback)();
typedef void (*FileSavedCallback)(bool);
typedef void (*FileSelectCallback)(bool, const char*);
typedef void (*DialogSelectionCallback)(int);
typedef void (*OrientationChangeCallback)(int);

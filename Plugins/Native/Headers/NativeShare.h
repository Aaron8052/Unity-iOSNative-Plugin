#import "../Utils.mm"

@interface NativeShare : NSObject

+(void)SaveImageToAlbum:(NSString *)imagePath
               callback:(SaveImageToAlbumCallback)callback;
+(void)ShareMessage:(NSString *)message
             addUrl:(NSString *)url
            imgPath:(NSString *)imgPath
           callback:(ShareCloseCallback)callback;
+(void)ShareObject:(NSMutableArray<NSString*>*)objects
          callback:(ShareCloseCallback)callback;
+(void)SaveFileDialog:(NSString *)content
             fileName:(NSString *)fileName
             callback:(FileSavedCallback)callback;
+(void)SelectFileDialog:(NSString *)ext
               callback:(FileSelectCallback)callback;

@end

extern "C"
{
    void _SaveImageToAlbum(const char* imagePath, SaveImageToAlbumCallback callback){
        [NativeShare SaveImageToAlbum:[NSString stringWithUTF8String:imagePath ?: ""] callback:callback];
    }
    void _Share(const char* message, const char* url, const char* imagePath, ShareCloseCallback callback)
    {
        [NativeShare ShareMessage:[NSString stringWithUTF8String:message ?: ""]
         addUrl:[NSString stringWithUTF8String:url ?: ""]
        imgPath:[NSString stringWithUTF8String:imagePath ?: ""]
        callback:callback];
    }
    void _ShareObjects(const char** objects, int count, ShareCloseCallback callback)
    {
        if(count <= 0)
            return;
        
        NSMutableArray<NSString*> *objectsArray = [NSMutableArray array];
        
        for(int i = 0; i< count; i++){
            NSString *str = [NSString stringWithUTF8String:objects[i]];
            [objectsArray addObject:str];
        }
        
        [NativeShare ShareObject:objectsArray callback:callback];
    }
    void _SaveFileDialog(const char* content, const char* fileName, FileSavedCallback callback)
    {
        [NativeShare SaveFileDialog:[NSString stringWithUTF8String:content ?: ""] fileName:[NSString stringWithUTF8String:fileName ?: ""]
        callback:callback];

    }
    void _SelectFileDialog(const char* ext, FileSelectCallback callback)
    {
        [NativeShare SelectFileDialog:[NSString stringWithUTF8String:ext ?: ""]
        callback:callback];
    }
}

#import "../Utils.mm"

@interface NativeShare : NSObject

+(void)SaveImageToAlbum:(UIImage *)image
               callback:(SaveImageToAlbumCallback)callback;
+(void)ShareObject:(NSMutableArray<NSString*>*)objects
              posX:(CGFloat)posX posY:(CGFloat)posY
          callback:(ShareCloseCallback)callback;
+(void)SaveFileDialog:(NSString *)content
             fileName:(NSString *)fileName
             callback:(FileSavedCallback)callback;
+(void)SelectFileDialog:(NSString *)ext
               callback:(FileSelectCallback)callback;

@end

extern "C"
{
   void NativeShare_SaveImageToAlbum(char* bytes, long length, SaveImageToAlbumCallback callback){
        NSData *data = [NSData dataWithBytes:bytes length:length]
        UIImage *image = [UIImage imageWithData:data];
        [NativeShare SaveImageToAlbum:image callback:callback];
    }

    void NativeShare_SaveImageToAlbum(const char* imagePath, SaveImageToAlbumCallback callback){
        if(imagePath == nil){
            if(callback != nil)
            {
                callback(NO);
            }
            return;
        }
        UIImage *image = [UIImage imageWithContentsOfFile:[NSString stringWithUTF8String:imagePath]];
        [NativeShare SaveImageToAlbum:image callback:callback];
    }
    void NativeShare_Share(const char* message, const char* url, const char* imagePath, double posX, double posY, ShareCloseCallback callback)
    {
        NSMutableArray<NSString*>* array = [NSMutableArray new];
        
        if(message != nil)
            [array addObject:[NSString stringWithFormat:@"0%@", [NSString stringWithUTF8String:message]]];
        
        if(url != nil)
            [array addObject:[NSString stringWithFormat:@"1%@", [NSString stringWithUTF8String:url]]];
        
        if(imagePath != nil)
            [array addObject:[NSString stringWithFormat:@"2%@", [NSString stringWithUTF8String:imagePath]]];
        
        
        [NativeShare ShareObject:array posX:posX posY:posY callback:callback];
    }
    void NativeShare_ShareObjects(const char** objects, int count, double posX, double posY,ShareCloseCallback callback)
    {
        if(count <= 0)
            return;
        
        NSMutableArray<NSString*> *objectsArray = [NSMutableArray array];
        
        for(int i = 0; i< count; i++){
            NSString *str = [NSString stringWithUTF8String:objects[i]];
            [objectsArray addObject:str];
        }
        
        [NativeShare ShareObject:objectsArray posX:posY posY:posY callback:callback];
    }
    void NativeShare_SaveFileDialog(const char* content, const char* fileName, FileSavedCallback callback)
    {
        [NativeShare SaveFileDialog:[NSString stringWithUTF8String:content ?: ""] fileName:[NSString stringWithUTF8String:fileName ?: ""]
        callback:callback];

    }
    void NativeShare_SelectFileDialog(const char* ext, FileSelectCallback callback)
    {
        [NativeShare SelectFileDialog:[NSString stringWithUTF8String:ext ?: ""]
        callback:callback];
    }
}

#import "../Utils.mm"

@interface NativeShare : NSObject

+(void)CopyImageToClipboard:(UIImage*)image;
+(void)CopyStringToClipboard:(NSString*)string;
+(void)CopyUrlToClipboard:(NSURL*)url;
+(void)SaveImageToAlbum:(UIImage *)image
               callback:(SaveImageToAlbumCallback)callback;
+(void)ShareObject:(NSMutableArray<NSString*>*)objects
              posX:(CGFloat)posX posY:(CGFloat)posY
             width:(CGFloat)width height:(CGFloat)height
          callback:(ShareCloseCallback)callback;
+(void)SaveFileDialog:(NSString *)content
             fileName:(NSString *)fileName
             callback:(BoolCallback)callback;
+(void)SelectFileDialog:(NSString *)ext
               callback:(FileSelectCallback)callback;

@end

extern "C"
{
    void NativeShare_CopyImageToClipboard(const char* imagePath)
    {
        if(imagePath == nil)
            return;
        UIImage *image = [UIImage imageWithContentsOfFile:NSStringFromCStr(imagePath)];
        [NativeShare CopyImageToClipboard:image];
    }
    void NativeShare_CopyImageBytesToClipboard(char* bytes, long length)
    {
        if(bytes == nil)
            return;
        NSData *data = [NSData dataWithBytes:bytes length:length];
        UIImage *image = [UIImage imageWithData:data];
        [NativeShare CopyImageToClipboard:image];
    }
    void NativeShare_CopyStringToClipboard(const char* string)
    {
        if(string == nil)
            return;
        NSString* str = NSStringFromCStr(string);
        [NativeShare CopyStringToClipboard:str];
    }
    void NativeShare_CopyUrlToClipboard(const char* url)
    {
        if(url == nil)
            return;
        NSString* str = NSStringFromCStr(url);
        NSURL* nsUrl = [NSURL URLWithString:str];
        [NativeShare CopyUrlToClipboard:nsUrl];
    }
    void NativeShare_SaveImageBytesToAlbum(char* bytes, long length, SaveImageToAlbumCallback callback){
       if(bytes == nil){
           if(callback != nil)
           {
               callback(NO);
           }
           return;
       }
        NSData *data = [NSData dataWithBytes:bytes length:length];
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
        UIImage *image = [UIImage imageWithContentsOfFile:NSStringFromCStr(imagePath)];
        [NativeShare SaveImageToAlbum:image callback:callback];
    }
    void NativeShare_Share(const char* message, const char* url, const char* imagePath, double posX, double posY, double width, double height,ShareCloseCallback callback)
    {
        NSMutableArray<NSString*>* array = [NSMutableArray new];
        
        if(message != nil)
            [array addObject:[NSString stringWithFormat:@"0%@", NSStringFromCStr(message)]];
        
        if(url != nil)
            [array addObject:[NSString stringWithFormat:@"1%@", NSStringFromCStr(url)]];
        
        if(imagePath != nil)
            [array addObject:[NSString stringWithFormat:@"2%@", NSStringFromCStr(imagePath)]];
        
        
        [NativeShare ShareObject:array
                            posX:posX posY:posY
                           width:width height:height
                        callback:callback];
    }
    void NativeShare_ShareObjects(const char** objects, int count, double posX, double posY, double width, double height, ShareCloseCallback callback)
    {
        if(count <= 0)
            return;
        
        NSMutableArray<NSString*> *objectsArray = [NSMutableArray array];
        
        for(int i = 0; i< count; i++){
            NSString *str = NSStringFromCStr(objects[i]);
            [objectsArray addObject:str];
        }
        
        [NativeShare ShareObject:objectsArray
                            posX:posY posY:posY
                           width:width height:height
                        callback:callback];
    }
    void NativeShare_SaveFileDialog(const char* content, const char* fileName, BoolCallback callback)
    {
        [NativeShare SaveFileDialog:NSStringFromCStr(content) fileName:NSStringFromCStr(fileName)
        callback:callback];

    }
    void NativeShare_SelectFileDialog(const char* ext, FileSelectCallback callback)
    {
        [NativeShare SelectFileDialog:NSStringFromCStr(ext)
        callback:callback];
    }
}

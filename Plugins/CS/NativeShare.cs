using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace iOSNativePlugin
{
    public static class NativeShare
    {
        [DllImport("__Internal")] private static extern void NativeShare_Share(string message, string url, string imagePath, double posX, double posY, ShareCloseCallback callback);
		[DllImport("__Internal")] private static extern void NativeShare_ShareObjects(string[] objects, int count, double posX, double posY, ShareCloseCallback callback);
        [DllImport("__Internal")] private static extern void NativeShare_SaveFileDialog(string content, string fileName, FileSavedCallback callback);
        [DllImport("__Internal")] private static extern void NativeShare_SelectFileDialog(string ext, FileSelectCallback callback);
        [DllImport("__Internal")] private static extern void NativeShare_SaveImageBytesToAlbum(byte[] bytes, long length, SaveImageToAlbumCallback callback);
        [DllImport("__Internal")] private static extern void NativeShare_SaveImageToAlbum(string imagePath, SaveImageToAlbumCallback callback);
        [DllImport("__Internal")] private static extern void NativeShare_CopyImageToClipboard(string imagePath);
        [DllImport("__Internal")] private static extern void NativeShare_CopyImageBytesToClipboard(byte[] bytes, long length);
        [DllImport("__Internal")] private static extern void NativeShare_CopyStringToClipboard(string @string);
        [DllImport("__Internal")] private static extern void NativeShare_CopyUrlToClipboard(string url);
        
        
        public static void CopyImageToClipboard(string imagePath)
            => NativeShare_CopyImageToClipboard(imagePath);

        public static void CopyImageToClipboard(byte[] bytes)
            => NativeShare_CopyImageBytesToClipboard(bytes, bytes.Length);

        public static void CopyImageToClipboard(Texture2D texture)
        {
            var bytes = texture.EncodeToPNG();
            var length = bytes.Length;
            NativeShare_CopyImageBytesToClipboard(bytes, length);
        }
        
        public static void CopyStringToClipboard(string @string)
            => NativeShare_CopyStringToClipboard(@string);

        public static void CopyUrlToClipboard(string url)
            => NativeShare_CopyUrlToClipboard(url);


        [MonoPInvokeCallback(typeof(SaveImageToAlbumCallback))]
        static void OnShareCloseCallback(bool saved)
        {
            OnSaveImageToAlbumCallback?.Invoke(saved);
            OnSaveImageToAlbumCallback = null;
        }

        static event Action<bool> OnSaveImageToAlbumCallback;
        
        /// <summary>
        /// 保存图片（本地绝对路径）到相册（App安装后首次调用会申请相册权限）
        /// </summary>
        /// <param name="imagePath">图片文件的本地绝对路径</param>
        /// <param name="callback">保存成功回调</param>
        public static void SaveImageToAlbum(string imagePath, Action<bool> callback = null)
        {
            OnSaveImageToAlbumCallback = callback;
            NativeShare_SaveImageToAlbum(imagePath, OnShareCloseCallback);
        }
        
        public static void SaveImageToAlbum(byte[] bytes, Action<bool> callback = null)
        {
            OnSaveImageToAlbumCallback = callback;
            NativeShare_SaveImageBytesToAlbum(bytes, bytes.Length, OnShareCloseCallback);
        }
        
        public static void SaveImageToAlbum(Texture2D texture, Action<bool> callback = null)
        {
            var bytes = texture.EncodeToPNG();
            var length = bytes.Length;
            OnSaveImageToAlbumCallback = callback;
            NativeShare_SaveImageBytesToAlbum(bytes, length, OnShareCloseCallback);
        }
        
        /// <summary>
        ///  调用系统分享功能
        /// </summary>
        /// <param name="message">分享内容</param>
        /// <param name="url">分享链接</param>
        /// <param name="imagePath">分享图片的本地路径</param>
        /// <param name="closeCallback">用户关闭分享面板的回调</param>
        public static void Share(string message, string url = "", string imagePath = "", Action closeCallback = null)
        {
            var pos = NativeUI.UnityViewSize;
            pos.x /= 2; // 将对话框放置在屏幕底部中间位置
            Share(message, url, imagePath, pos, closeCallback);
        }
        public static void Share(string message, string url = "", string imagePath = "", Vector2 pos = default(Vector2), Action closeCallback = null)
        {
            OnShareClose = closeCallback;
            NativeShare_Share(message, url, imagePath, pos.x, pos.y, OnShareCloseCallback);
        }

        /// <summary>
        /// 调用系统分享功能
        /// </summary>
        /// <param name="closeCallback">用户关闭分享面板的回调</param>
        /// <param name="shareObjects">分享内容</param>
        public static void ShareObjects(Action closeCallback = null, params ShareObject[] shareObjects)
        {
            var pos = NativeUI.UnityViewSize;
            pos.x /= 2; // 将对话框放置在屏幕底部中间位置
            ShareObjects(closeCallback, pos, shareObjects);
        }
		public static void ShareObjects(Action closeCallback = null, Vector2 pos = default(Vector2), params ShareObject[] shareObjects)
        {
            if(shareObjects == null || shareObjects.Length <= 0)
                return;

            string[] objectsArray = new string[shareObjects.Length];
                
            for (int i = 0; i < shareObjects.Length; i++)
            {
                objectsArray[i] = shareObjects[i];
            }

            NativeShare_ShareObjects(objectsArray, objectsArray.Length, pos.x, pos.y, OnShareCloseCallback);
            OnShareClose = closeCallback;
        }
        static event Action OnShareClose;
            
            
        [MonoPInvokeCallback(typeof(ShareCloseCallback))]
        static void OnShareCloseCallback()
        {
            OnShareClose?.Invoke();
            OnShareClose = null;
        }
            
        /// <summary>
        /// 调用系统保存文件对话框，允许玩家选择保存文件的路径
        /// </summary>
        /// <param name="content">文件内容</param>
        /// <param name="fileName">文件名.后缀名</param>
        /// <param name="callback">玩家关闭对话框回调</param>
        /// <returns>保存成功</returns>
        public static bool SaveFileDialog(string content, string fileName, Action callback = null)
        {
            NativeShare_SaveFileDialog(content, fileName, OnFileSavedCallback);
            OnFileSaved = callback;
            return true; 
        }
            
        static event Action OnFileSaved;
            
        [MonoPInvokeCallback(typeof(FileSavedCallback))]
        static void OnFileSavedCallback(bool saved)
        {
            if (saved)
                OnFileSaved?.Invoke();
            OnFileSaved = null;
        }
            
        /// <summary>
        /// 调用系统选择文件对话框，允许玩家选择文件
        /// </summary>
        /// <param name="ext">文件类型（拓展名）</param>
        /// <param name="callback">玩家选择并读取文件后的回调（String）</param>
        /// <param name="failedCallback">文件读取失败的回调（文件类型无效）</param>
        public static void SelectFileDialog(string ext, Action<string> callback = null, Action failedCallback = null)
        {
            OnFiledSelected = callback;
            OnFileSelectFailed = failedCallback;
            NativeShare_SelectFileDialog(ext, OnFileSelectedCallback); 
        }
            
        static event Action<string> OnFiledSelected;
        static event Action OnFileSelectFailed;
            
            
        [MonoPInvokeCallback(typeof(FileSelectCallback))]
        static void OnFileSelectedCallback(bool selected, string content)
        {
            if (selected)
                OnFiledSelected?.Invoke(content);
            else
                OnFileSelectFailed?.Invoke();
            OnFileSaved = null;
            OnFileSelectFailed = null;
        }
    }
}

using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace iOSNativePlugin
{
    public static class NativeShare
    {
        [DllImport("__Internal")] static extern void NativeShare_Share(string message, string url, string imagePath, double posX, double posY, ShareCloseCallback callback);
		[DllImport("__Internal")] static extern void NativeShare_ShareObjects(string[] objects, int count, double posX, double posY, ShareCloseCallback callback);
        [DllImport("__Internal")] static extern void NativeShare_SaveFileDialog(string content, string fileName, BoolCallback callback);
        [DllImport("__Internal")] static extern void NativeShare_SelectFileDialog(string ext, FileSelectCallback callback);
        [DllImport("__Internal")] static extern void NativeShare_SaveImageBytesToAlbum(byte[] bytes, long length, SaveImageToAlbumCallback callback);
        [DllImport("__Internal")] static extern void NativeShare_SaveImageToAlbum(string imagePath, SaveImageToAlbumCallback callback);
        [DllImport("__Internal")] static extern void NativeShare_CopyImageToClipboard(string imagePath);
        [DllImport("__Internal")] static extern void NativeShare_CopyImageBytesToClipboard(byte[] bytes, long length);
        [DllImport("__Internal")] static extern void NativeShare_CopyStringToClipboard(string @string);
        [DllImport("__Internal")] static extern void NativeShare_CopyUrlToClipboard(string url);
        
        /// <summary>
        /// 拷贝图片到剪切板中
        /// </summary>
        /// <param name="imagePath">图片本地绝对路径</param>
        public static void CopyImageToClipboard(string imagePath)
            => NativeShare_CopyImageToClipboard(imagePath);

        /// <summary>
        /// 拷贝图片到剪切板中
        /// </summary>
        /// <param name="bytes">图片字节</param>
        public static void CopyImageToClipboard(byte[] bytes)
            => NativeShare_CopyImageBytesToClipboard(bytes, bytes.Length);

        /// <summary>
        /// 拷贝图片到剪切板中
        /// </summary>
        /// <param name="texture">图片 Texture2D</param>
        public static void CopyImageToClipboard(Texture2D texture)
        {
            var bytes = texture.EncodeToPNG();
            var length = bytes.Length;
            NativeShare_CopyImageBytesToClipboard(bytes, length);
        }

        /// <summary>
        /// 拷贝字符串到剪切板中
        /// </summary>
        /// <param name="string">字符串</param>
        public static void CopyStringToClipboard(string @string)
            => NativeShare_CopyStringToClipboard(@string);

        /// <summary>
        /// 拷贝 Url 到剪切板中
        /// </summary>
        /// <param name="url">Url</param>
        public static void CopyUrlToClipboard(string url)
            => NativeShare_CopyUrlToClipboard(url);

        static event Action<bool> onSaveImageToAlbumCallback;

        [MonoPInvokeCallback(typeof(SaveImageToAlbumCallback))]
        static void OnShareCloseCallback(bool saved)
        {
            onSaveImageToAlbumCallback?.Invoke(saved);
            onSaveImageToAlbumCallback = null;
        }

        /// <summary>
        /// 保存图片到相册，App 首次调用会申请相册权限
        /// </summary>
        /// <param name="imagePath">图片文件的本地绝对路径</param>
        /// <param name="callback">保存结果回调，返回 bool</param>
        public static void SaveImageToAlbum(string imagePath, Action<bool> callback = null)
        {
            onSaveImageToAlbumCallback = callback;
            NativeShare_SaveImageToAlbum(imagePath, OnShareCloseCallback);
        }

        /// <summary>
        /// 保存图片到相册，App 首次调用会申请相册权限
        /// </summary>
        /// <param name="bytes">图片字节</param>
        /// <param name="callback">保存结果回调，返回 bool</param>
        public static void SaveImageToAlbum(byte[] bytes, Action<bool> callback = null)
        {
            onSaveImageToAlbumCallback = callback;
            NativeShare_SaveImageBytesToAlbum(bytes, bytes.Length, OnShareCloseCallback);
        }

        /// <summary>
        /// 保存图片到相册，App 首次调用会申请相册权限
        /// </summary>
        /// <param name="texture">图片 Texture2D</param>
        /// <param name="callback">保存结果回调，返回 bool</param>
        public static void SaveImageToAlbum(Texture2D texture, Action<bool> callback = null)
        {
            var bytes = texture.EncodeToPNG();
            var length = bytes.Length;
            onSaveImageToAlbumCallback = callback;
            NativeShare_SaveImageBytesToAlbum(bytes, length, OnShareCloseCallback);
        }
        
        /// <summary>
        /// 调用系统分享功能
        /// </summary>
        /// <param name="message">分享文本</param>
        /// <param name="url">分享链接</param>
        /// <param name="imagePath">分享图片的本地绝对路径</param>
        /// <param name="closeCallback">用户关闭分享面板的回调</param>
        public static void Share(string message, string url = "", string imagePath = "", Action closeCallback = null)
        {
            var pos = NativeUI.UnityViewSize;
            pos.x /= 2; // 将对话框放置在屏幕底部中间位置
            Share(message, url, imagePath, pos, closeCallback);
        }

        /// <summary>
        /// 调用系统分享功能
        /// </summary>
        /// <param name="message">分享文本</param>
        /// <param name="url">分享链接</param>
        /// <param name="imagePath">分享图片的本地绝对路径</param>
        /// <param name="pos">iPad 设备上分享对话框的显示位置，调用 NativeUI.UnityViewSize 获取游戏的视图大小</param>
        /// <param name="closeCallback">用户关闭分享面板的回调</param>
        public static void Share(string message, string url = "", string imagePath = "", Vector2 pos = default(Vector2), Action closeCallback = null)
        {
            OnShareClose = closeCallback;
            NativeShare_Share(message, url, imagePath, pos.x, pos.y, OnShareCloseCallback);
        }

        /// <summary>
        /// 调用系统分享功能
        /// </summary>
        /// <param name="closeCallback">用户关闭分享面板的回调</param>
        /// <param name="shareObjects">自定义分享内容 ShareObject 数组</param>
        public static void ShareObjects(Action closeCallback = null, params ShareObject[] shareObjects)
        {
            var pos = NativeUI.UnityViewSize;
            pos.x /= 2; // 将对话框放置在屏幕底部中间位置
            ShareObjects(closeCallback, pos, shareObjects);
        }

        /// <summary>
        /// 调用系统分享功能
        /// </summary>
        /// <param name="closeCallback">用户关闭分享面板的回调</param>
        /// <param name="pos">iPad 设备上分享对话框的显示位置，调用 NativeUI.UnityViewSize 获取游戏的视图大小</param>
        /// <param name="shareObjects">自定义分享内容 ShareObject 数组</param>
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
            
        [MonoPInvokeCallback(typeof(BoolCallback))]
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

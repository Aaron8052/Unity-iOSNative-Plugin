using System;
using System.Runtime.InteropServices;
using AOT;

namespace iOSNativePlugin
{
    public static class NativeShare
    {
        [DllImport("__Internal")]
        private static extern void _Share(string message, string url, string imagePath, ShareCloseCallback callback);
		[DllImport("__Internal")]
		private static extern void _ShareObjects(string[] objects, int count, ShareCloseCallback callback);
        [DllImport("__Internal")]
        private static extern void _SaveFileDialog(string content, string fileName, FileSavedCallback callback);
        [DllImport("__Internal")]
        private static extern void _SelectFileDialog(string ext, FileSelectCallback callback);
            
        /// <summary>
        ///  调用系统分享功能
        /// </summary>
        /// <param name="message">分享内容</param>
        /// <param name="url">分享链接</param>
        /// <param name="imagePath">分享图片的本地路径</param>
        /// <param name="closeCallback">用户关闭分享面板的回调</param>
        public static void Share(string message, string url = "", string imagePath = "", Action closeCallback = null)
        {
            _Share(message, url, imagePath, OnShareCloseCallback);
            OnShareClose = closeCallback;
        }
        /// <summary>
        /// 调用系统分享功能
        /// </summary>
        /// <param name="closeCallback">用户关闭分享面板的回调</param>
        /// <param name="shareObjects">分享内容</param>
		public static void ShareObjects(Action closeCallback = null, params ShareObject[] shareObjects)
        {
            if(shareObjects == null || shareObjects.Length <= 0)
                return;

            string[] objectsArray = new string[shareObjects.Length];
                
            for (int i = 0; i < shareObjects.Length; i++)
            {
                objectsArray[i] = shareObjects[i];
            }

            _ShareObjects(objectsArray, objectsArray.Length, OnShareCloseCallback);
            OnShareClose = closeCallback;
        }
        static event Action OnShareClose;
            
            
        [MonoPInvokeCallback(typeof(ShareCloseCallback))]
        static void OnShareCloseCallback()
        {
            if(OnShareClose != null)
                OnShareClose.Invoke();
                
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
            _SaveFileDialog(content, fileName, OnFileSavedCallback);
            OnFileSaved = callback;
            return true; 
        }
            
        static event Action OnFileSaved;
            
        [MonoPInvokeCallback(typeof(FileSavedCallback))]
        static void OnFileSavedCallback(bool saved)
        {
            if (saved && OnFileSaved != null)
            {
                OnFileSaved.Invoke();
            }
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
            _SelectFileDialog(ext, OnFileSelectedCallback); 
        }
            
        static event Action<string> OnFiledSelected;
        static event Action OnFileSelectFailed;
            
            
        [MonoPInvokeCallback(typeof(FileSelectCallback))]
        static void OnFileSelectedCallback(bool selected, string content)
        {
            if (selected)
            {
                if(OnFiledSelected != null)
                    OnFiledSelected.Invoke(content);
            }
            else
            {
                if(OnFileSelectFailed != null)
                    OnFileSelectFailed.Invoke();
            }
            OnFileSaved = null;
            OnFileSelectFailed = null;
        }
    }
}

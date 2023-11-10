using System.Runtime.InteropServices;

namespace iOSNativePlugin
{
    public static class iCloudKeyValueStore
    {
        [DllImport("__Internal")]
        private static extern void _InitializeICloud();
            
        [DllImport("__Internal")]
        private static extern bool _Synchronize();

        [DllImport("__Internal")]
        private static extern bool _IsICloudAvailable();

        [DllImport("__Internal")]
        static extern bool _ICloudKeyExists(string key);
        
        [DllImport("__Internal")]
        private static extern bool _ClearICloudSave();

        [DllImport("__Internal")]
        private static extern string _iCloudGetString(string key, string defaultValue);

        [DllImport("__Internal")]
        private static extern bool _iCloudSaveString(string key, string value);

        [DllImport("__Internal")]
        private static extern int _iCloudGetInt(string key, int defaultValue);

        [DllImport("__Internal")]
        private static extern bool _iCloudSaveInt(string key, int value);

        [DllImport("__Internal")]
        private static extern float _iCloudGetFloat(string key, float defaultValue);

        [DllImport("__Internal")]
        private static extern bool _iCloudSaveFloat(string key, float value);

        [DllImport("__Internal")]
        private static extern bool _iCloudGetBool(string key, bool defaultValue);

        [DllImport("__Internal")]
        private static extern bool _iCloudSaveBool(string key, bool value);

        /// <summary>
        /// 初始化iCloud
        /// </summary>
        public static void Initialize()
        {
            _InitializeICloud();
        }
            
        /// <summary>
        /// 判断当前设备iCloud是否可用
        /// </summary>
        /// <returns></returns>
        public static bool IsICloudAvailable()
        {
            return _IsICloudAvailable();
        }
        /// <summary>
        /// 判断当前iCloud是否包含键
        /// </summary>
        /// <param name="key">要判断的key</param>
        /// <returns>iCloud云存储包含该key</returns>
        public static bool ContainsKey(string key)
        {
            return _ICloudKeyExists(key);
        } 
        /// <summary>
        /// 判断当前设备iCloud是否可用
        /// </summary>
        /// <returns>是否同步成功</returns>
        public static bool Synchronize()
        {
            return _Synchronize();
        }
            
        /// <summary>
        /// 清空iCloud存档
        /// <para><b> >此方法可用性未知</b></para>
        /// </summary>
        /// <returns>是否清除成功</returns>
        public static bool ClearICloudSave()
        {
            return _ClearICloudSave();
        }

        /// <summary>
        /// 从iCloud读取String值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetString(string key, string defaultValue)
        {
            return _iCloudGetString(key, defaultValue);
        }

        /// <summary>
        /// 保存String值到iCloud
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool SetString(string key, string value)
        {
            return _iCloudSaveString(key, value);
        }

        /// <summary>
        /// 从iCloud读取Int值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetInt(string key, int defaultValue)
        {
            return _iCloudGetInt(key, defaultValue);
        }

        /// <summary>
        /// 保存Int值到iCloud
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool SetInt(string key, int value)
        {
            return _iCloudSaveInt(key, value);
        }

        /// <summary>
        /// 从iCloud读取Float值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static float GetFloat(string key, float defaultValue)
        {
            return _iCloudGetFloat(key, defaultValue);
        }

        /// <summary>
        /// 保存Float值到iCloud
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool SetFloat(string key, float value)
        {
            return _iCloudSaveFloat(key, value);
        }

        /// <summary>
        /// 从iCloud读取Bool值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool GetBool(string key, bool defaultValue)
        {
            return _iCloudGetBool(key, defaultValue);
        }

        /// <summary>
        /// 保存Bool值到iCloud
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool SetBool(string key, bool value)
        {
            return _iCloudSaveBool(key, value);
        }
    }
}

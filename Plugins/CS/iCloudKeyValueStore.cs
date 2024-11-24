using System.Runtime.InteropServices;

namespace iOSNativePlugin
{
    public static class iCloudKeyValueStore
    {
        [DllImport("__Internal")] static extern void iCloudKeyValueStore_Initialize();
        [DllImport("__Internal")] static extern bool iCloudKeyValueStore_Synchronize();
        [DllImport("__Internal")] static extern bool iCloudKeyValueStore_IsICloudAvailable();
        [DllImport("__Internal")] static extern bool iCloudKeyValueStore_ICloudKeyExists(string key);
        [DllImport("__Internal")] static extern bool iCloudKeyValueStore_ICloudDeleteKey(string key);
        [DllImport("__Internal")] static extern bool iCloudKeyValueStore_ClearICloudSave();
        [DllImport("__Internal")] static extern string iCloudKeyValueStore_GetString(string key, string defaultValue);
        [DllImport("__Internal")] static extern bool iCloudKeyValueStore_SaveString(string key, string value);
        [DllImport("__Internal")] static extern int iCloudKeyValueStore_GetInt(string key, int defaultValue);
        [DllImport("__Internal")] static extern bool iCloudKeyValueStore_SaveInt(string key, int value);
        [DllImport("__Internal")] static extern float iCloudKeyValueStore_GetFloat(string key, float defaultValue);
        [DllImport("__Internal")] static extern bool iCloudKeyValueStore_SaveFloat(string key, float value);
        [DllImport("__Internal")] static extern bool iCloudKeyValueStore_GetBool(string key, bool defaultValue);
        [DllImport("__Internal")] static extern bool iCloudKeyValueStore_SaveBool(string key, bool value);

        /// <summary>
        /// 初始化iCloud
        /// </summary>
        public static void Initialize() => iCloudKeyValueStore_Initialize();

        /// <summary>
        /// 判断当前设备iCloud是否可用
        /// </summary>
        /// <returns>是否可用</returns>
        public static bool IsICloudAvailable() => iCloudKeyValueStore_IsICloudAvailable();

        /// <summary>
        /// 判断当前iCloud是否包含键
        /// </summary>
        /// <param name="key">要判断的key</param>
        /// <returns>iCloud云存储包含该key</returns>
        public static bool ContainsKey(string key) => iCloudKeyValueStore_ICloudKeyExists(key);

        /// <summary>
        /// 删除Key
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Key存在并删除成功</returns>
        public static bool DeleteKey(string key) => iCloudKeyValueStore_ICloudDeleteKey(key);

        /// <summary>
        /// 强制同步iCloud云存档至Apple服务器
        /// </summary>
        /// <returns>是否同步成功</returns>
        public static bool Synchronize() => iCloudKeyValueStore_Synchronize();

        /// <summary>
        /// 清空iCloud存档
        /// </summary>
        /// <returns>是否清除成功</returns>
        public static bool ClearICloudSave() => iCloudKeyValueStore_ClearICloudSave();

        /// <summary>
        /// 从iCloud读取String值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetString(string key, string defaultValue) => iCloudKeyValueStore_GetString(key, defaultValue);

        /// <summary>
        /// 保存String值到iCloud
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool SetString(string key, string value) => iCloudKeyValueStore_SaveString(key, value);

        /// <summary>
        /// 从iCloud读取Int值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetInt(string key, int defaultValue) => iCloudKeyValueStore_GetInt(key, defaultValue);

        /// <summary>
        /// 保存Int值到iCloud
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool SetInt(string key, int value) => iCloudKeyValueStore_SaveInt(key, value);

        /// <summary>
        /// 从iCloud读取Float值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static float GetFloat(string key, float defaultValue) => iCloudKeyValueStore_GetFloat(key, defaultValue);

        /// <summary>
        /// 保存Float值到iCloud
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool SetFloat(string key, float value) => iCloudKeyValueStore_SaveFloat(key, value);

        /// <summary>
        /// 从iCloud读取Bool值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool GetBool(string key, bool defaultValue) => iCloudKeyValueStore_GetBool(key, defaultValue);

        /// <summary>
        /// 保存Bool值到iCloud
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool SetBool(string key, bool value) => iCloudKeyValueStore_SaveBool(key, value);
    }
}
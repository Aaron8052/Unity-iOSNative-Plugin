# Unity-iOSNative-Plugin
 iOS Native Plugin for Unity

# 简介
适用于Unity引擎的iOS端Native插件，后续会开发更多功能，敬请期待

作者：Aaron（AKA. FengYan, 枫炎）

开源协议：[MIT License](https://github.com/Aaron8052/Unity-iOSNative-Plugin/blob/main/LICENSE)

# 信息

### IDE
[(macOS) Xcode](https://developer.apple.com/download/all/?q=Xcode)

### 兼容性
- iOS10及以上
- Unity2017.4及以上（更早版本未测试）

# 使用
## 导入
- 直接将所有文件(`.h`, `.mm`, `.cs`)导入Unity项目的Assets/Plugins/iOS中
- 调用 iOSNative.cs 中的方法和**子类**即可 (iOSCallbackHelper.cs不用管，运行时会根据需要自动初始化，也可以提前放入场景中)

## 模块介绍
- `iOSNative.cs` 插件与UnityC#项目的接口，调用里面的方法可以实现与iOS的OC代码交互
- `iOSCallbackHelper.cs` 用于接收从OC代码中的回调，在需要时会自动创建对应的游戏物体（如果有需要可以给这个脚本加上DontDestroyOnLoad，以防止物体在切换场景时被卸载）
- `iOSNative.h` 头文件，本Native插件的所有类的声明以及公开方法都在这里面
- `iOSNative.mm` 负责将插件的方法暴露给UnityC#端以进行交互
- `iCloudKeyValueStore.mm` 负责iCloud相关功能的实现
- `Device.mm` 负责 iPhone 设备相关功能的实现
- `Notification.mm` 负责 iOS 本地通知推送的实现
- `NativeShare.mm` 负责 iOS 自带的分享功能的实现
- `NativeUI.mm` 包含部分 iOS Native UI的功能（比如应用内展示横幅，可用于应用内消息推送）

## 子类/功能介绍
#### `Initialize()`

- 初始化整个iOSNative插件

### iCloudKeyValueStore

#### `IsICloudAvailable()`

- 判断当前设备iCloud是否可用

#### `Synchronize()`

- 强制同步iCloud云存档至Apple服务器（Bool返回值：是否同步成功）

#### `ClearICloudSave()`

> 此方法可用性未经测试

- 清空iCloud存档

#### `iCloudGetString/Int/Float/BoolValue(string key, var defaultValue)`

- 从iCloud读取目标数据

#### `iCloudSaveString/Int/Float/BoolValue(string key, var value)`

- 保存数据到iCloud

### Notification
#### `PushNotification(string msg, string title, string identifier, int delay)`

- 推送本地通知：msg，title：标题（可留空），identifier：本通知的标识符，相同的标识符会被系统判定为同一个通知，delay：延迟待定delay秒后推送此通知

#### `RemovePendingNotifications(string identifier)`

- 移除某个待定通知（对于已经推送的通知无效）

#### `RemoveAllPendingNotifications()`

- 移除所有待定通知

### NativeUI
#### `IsStatusBarHidden()`

- 判断当前系统状态栏是否被隐藏

#### `SetStatusBarHidden(bool hidden, UIStatusBarAnimation withAnimation = UIStatusBarAnimation.Fade)`

- 设置状态栏的隐藏状态
- withAnimation：隐藏显示时的动画类型，无动画/渐变/滑动

#### `SetStatusBarStyle(UIStatusBarStyle style)`

- 设置状态栏的样式（白色、黑色、自动）

#### `ShowTempAlert(string alertString, int duration = 5)`

- 在应用内顶部展示一个内容为alertString，时长duration秒的横幅

### Device
#### `SetAudioExclusive(bool exclusive)`
- 调用此方法可静音/暂停设备后台正在播放的音频
- exclusive为true时：静音后台音频
- exclusive为false时：游戏音频可与后台音频同时播放

#### `PlayHaptics(UIImpactFeedbackStyle style, float intensity)`

> 此方法可用性未经测试

- style 震动强度，Intensity强度（0.0 - 1.0）

#### `GetCountryCode()`

- 获取当前设备的ISO地区码（ISO 3166-1 alpha-2）

### NativeShare

#### `Share(string message, string url = "", string imagePath = "", Action closeCallback = null)`

- 调用系统分享功能 message：分享文本字符串，url：链接，imagePath：要分享的图片的本地路径，closeCallback：用户关闭分享面板的回调

#### `SaveFileDialog(string content, string fileName, Action callback = null)`

- 调用系统保存文件对话框，允许玩家选择保存文件的路径
- callback：玩家关闭对话框回调

#### `SelectFileDialog(string ext, Action<string> callback = null, Action failedCallback = null)`

- 调用系统选择文件对话框，允许玩家选择文件
 - callback：玩家选择并读取文件后的回调（String）
 - failedCallback：文件读取失败的回调（文件类型无效）

## 注意事项
- 在调用插件方法之前先调用 `iOSNative.cs` 中的Initialize方法进行插件初始化

# 已知问题
- `ClearICloudSave()` 会导致游戏卡死

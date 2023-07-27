# Unity-iOSNative-Plugin
适用于Unity引擎的iOS端Native插件

### 兼容性
- iOS11及以上
- Unity2017.4及以上（更早版本未测试）

# 项目分支（Branch）

- main：开发分支，此分支中的**代码不保证完全稳定且可用**
- release/latest：最新发布分支，此分支**包含最新并稳定的代码**，适合想要**不断接收插件更新**的用户
- release/版本号：每个发布版本的分支，**包含对应发布版本的代码**，适合**长期使用某个版本**的用户

# 使用
## 导入
### Unity 2019.3 或更新
- 选择一个本仓库中的任意一个**release分支**（除main之外）
- 前往Unity的 **Package Manager**
- 点击**左上角的加号**
- 选择 `Add package from git URL`
- 输入链接 `https://github.com/Aaron8052/Unity-iOSNative-Plugin.git#BranchName`

> 请将上方链接中的的 `BranchName` 更换为你选择的版本分支的名称

#### 如果显示导入失败
- **将本仓库完整地Clone到你的电脑中**
- 切换分支为你想要的**release分支**（除main之外）
- 在 **Package Manager** 选择 `Add package from disk`
- 在弹出的**文件选择对话框**中**找到刚才Clone的仓库**，**进入并选择** `package.json` 即可成功导入

-------------------------------------

### Unity 2019.3 之前的版本
- 切换仓库分支为你想要的**release分支**（除main之外）
- 下载本仓库中的全部内容
- 将仓库的 `Plugins` 文件夹中的**全部内容**导入到你的**Unity项目**中的 `Plugins` 文件夹即可

## 调用插件
- 调用本插件请确保引入 `iOSNativePlugin` 命名空间
- 然后调用 `iOSNative` 类中的[方法或其子类中的方法](#子类功能介绍)即可


## 模块介绍

| 模块名                      | 功能                                     |
|--------------------------|----------------------------------------|
| `iOSNative.cs`           | 插件与UnityC#项目的接口，调用里面的方法可以实现与iOS的OC代码交互 |
| `Utils.cs`               | cs辅助文件，包含了enum、struct等对象的声明 |
| `iOSCallbackHelper.cs`   | 用于接收从OC代码中的回调                          |
| `iOSNative.h`            | 头文件，本Native插件的所有类的声明以及公开方法都在这里面        |
| `iOSNative.mm`           | 负责将插件的方法暴露给UnityC#端以进行交互               |
| `iCloudKeyValueStore.mm` | 负责iCloud相关功能的实现                        |
| `Device.mm`              | 负责 iPhone 设备相关功能的实现                    |
| `Notification.mm`        | 负责 iOS 本地通知推送的实现                       |
| `NativeShare.mm`         | 负责 iOS 自带的分享功能的实现                      |
| `NativeUI.mm`            | 包含部分 iOS Native UI的功能（比如应用内显示/隐藏状态栏）   |

## 子类/功能介绍

> 方法名前标有星号 “*” 表示该方法尚未经测试，可用性未知

> 在调用插件方法之前先调用 `Initialize` 方法进行插件初始化

### 父类 - iOSNative

| 方法                      | 功能                       |
|-------------------------|--------------------------|
| `GetBundleIdentifier()` | 获取当前应用的Bundle Identifier |

### 子类 - iOSNative.iCloudKeyValueStore

| 方法                        | 功能                                     |
|---------------------------|----------------------------------------|
| `Initialize()`          | 初始化iCloud         |
| `IsICloudAvailable()`     | 判断当前设备iCloud是否可用                       |
| `Synchronize()`           | 强制同步iCloud云存档至Apple服务器（Bool返回值：是否同步成功） |
| * `ClearICloudSave()`     | 清空iCloud存档                             |
| `iCloudGetStringValue()`  | 从iCloud读取String值                       |
| `iCloudGetIntValue()`     | 从iCloud读取Int值                          |
| `iCloudGetFloatValue()`   | 从iCloud读取Float值                        |
| `iCloudGetBoolValue()`    | 从iCloud读取Bool值                         |
| `iCloudSaveStringValue()` | 保存String值到iCloud                       |
| `iCloudSaveIntValue()`    | 保存Int值到iCloud                          |
| `iCloudSaveFloatValue()`  | 保存Float值到iCloud                        |
| `iCloudSaveBoolValue()`   | 保存Bool值到iCloud                         |

### 子类 - iOSNative.Notification

| 方法                                | 功能                    |
|-----------------------------------|-----------------------|
| `Initialize()`          | 初始化通知系统         |
| `PushNotification()`              | 推送本地定时通知              |
| `RemovePendingNotifications()`    | 移除某个待定通知（对于已经推送的通知无效） |
| `RemoveAllPendingNotifications()` | 移除所有待定通知              |

### 子类 - iOSNative.NativeUI
| 事件                   | 功能                                      |
|------------------------|-----------------------------------------|
| `OnStatusBarOrientationChanged` | UI朝向变更事件 |

| 属性                    | 功能                                      |
|------------------------|-----------------------------------------|
| `StatusBarOrientation` | 当前UI的朝向 |

| 方法                     | 功能                                      |
|------------------------|-----------------------------------------|
| `StatusBarOrientation` | 当前UI的朝向 |
| `IsStatusBarHidden()`  | 判断当前系统状态栏是否被隐藏                          |
| `SetStatusBarHidden()` | 设置状态栏的隐藏状态                              |
| `SetStatusBarStyle()`  | 设置状态栏的样式（白色、黑色、自动）                      |
| `ShowTempAlert()`      | 在应用内顶部展示一个内容为alertString，时长duration秒的横幅 |

### 子类 - iOSNative.Device

| 方法                      | 功能                             |
|-------------------------|-----------------------------------|
| `GetDeviceOrientation()` | 获取当前设备的物理朝向 |
| * `IsBluetoothHeadphonesConnected()` | 判断玩家当前是否连接了蓝牙耳机  |
| `IsMacCatalyst()`       | 判断当前app是否运行在Mac Catalyst环境下     |
| `IsSuperuser()`         | 判断当前设备是否越狱                        |
| `SetAudioExclusive()`   | 调用此方法可静音/暂停设备后台正在播放的音频    |
| * `PlayHaptics()`       | 震动                                |
| `GetCountryCode()`      | 获取当前设备的ISO地区码（ISO 3166-1 alpha-2） |

### 子类 - iOSNative.NativeShare

| 方法                   | 功能                        |
|----------------------|---------------------------|
| `Share()`            | 调用系统分享功能                  |
| `SaveFileDialog()`   | 调用系统保存文件对话框，允许玩家选择保存文件的路径 |
| `SelectFileDialog()` | 调用系统选择文件对话框，允许玩家选择文件      |

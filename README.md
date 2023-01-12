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
- 调用 iOSNative.cs 中的方法即可 (iOSCallbackHelper.cs不用管，运行时需要时会自动初始化，也可以提前放入场景中)

## 模块介绍
- `iOSNative.cs` 插件与UnityC#项目的接口，调用里面的方法可以实现与iOS的OC代码交互
- `iOSCallbackHelper.cs` 用于接收从OC代码中的回调，在需要时会自动创建对应的游戏物体（如果有需要可以给这个脚本加上DontDestroyOnLoad，以防止物体在切换场景时被卸载）
- `iOSNative.h` 头文件，本Native插件的所有类的声明以及公开方法都在这里面
- `iOSNative.mm` 负责将插件的方法暴露给UnityC#端以进行交互
- `iCloudKeyValueStore.mm` 负责iCloud相关功能的实现
- `iOSDevice.mm` 负责 iPhone 设备相关功能的实现
- `iOSNotification.mm` 负责 iOS 本地通知推送的实现
- `iOSShare.mm` 负责 iOS 自带的分享功能的实现
- `iOSUIView.mm` 包含部分 iOS Native UI的功能（比如应用内展示横幅，可用于应用内消息推送）

## 注意事项
- 在调用插件方法之前先调用 `iOSNative.cs` 中的initialize方法进行插件初始化

# 已知问题
- ClearICloudSave() 会导致游戏卡死

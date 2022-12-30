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
- 直接将所有文件(.h, .mm, .cs)的导入Unity项目的Assets/Plugins/iOS中
- 调用 iOSNative.cs 中的方法即可 (iOSCallbackHelper.cs不用管，运行时需要时会自动初始化，也可以提前放入场景中)

# 已知问题
- ClearICloudSave() 会导致游戏卡死

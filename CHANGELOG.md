# v2.3.0
## API 改动
- 取消原iOSNative.cs，将所有子类拆分为独立的类，现在调用api时不再需要调用iOSNative类

## 新增功能
- `NativeUI.OnStatusBarOrientationChanged`
UI朝向变更事件

# v2.2.1

## 新增功能

### iOSNative.NativeUI

- `StatusBarOrientation`
当前UI的朝向

### iOSNative.Device
- `GetDeviceOrientation()`
获取当前设备的物理朝向

# v2.2.0

## API 改动
- 增加Utils.cs文件
- 将原iOSNative.cs中所有的enum、struct类型的对象移动至Utils.cs中，调用时不再需要调用其父类

## 新增功能

### iOSNative.Device

- `IsBluetoothHeadphonesConnected()`
判断玩家当前是否连接了蓝牙耳机

- `IsMacCatalyst()` 
判断当前app是否运行在Mac Catalyst环境下

# v2.1.1

- 加入[设备越狱检测功能](https://github.com/Aaron8052/Unity-iOSNative-Plugin/blob/main/README.md#issuperuser)

# v2.1.0
- 首个支持Unity Package Manager的版本

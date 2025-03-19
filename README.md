# Unity-iOSNative-Plugin
适用于Unity引擎的iOS端Native插件
## IDE
### C#
- [JetBrains Rider](https://www.jetbrains.com/rider/)
### Objetive-C++
- [Xcode](https://developer.apple.com/xcode/)

## 兼容性
- iOS12 及以上
- Unity2021.3及以上（更早版本未测试）

## 项目分支（Branch）

- main：开发分支，此分支中的**代码不保证完全稳定且可用**
- release/latest：最新发布分支，此分支**包含最新并稳定的代码**，适合想要**不断接收插件更新**的用户
- release/版本号：每个发布版本的分支，**包含对应发布版本的代码**，适合**长期使用某个版本**的用户

# 使用
## 导入
### Package Manager
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

### 源代码导入
- 切换仓库分支为你想要的**release分支**（除main之外）
- 下载本仓库中的全部内容
- 将仓库的 `Plugins` 文件夹中的**全部内容**导入到你的**Unity项目**中的 `Plugins` 文件夹即可

## 调用插件
- 调用本插件请确保引入 `iOSNativePlugin` 命名空间
- 然后调用 [C#类](#c类功能介绍) 中的方法

## C#类/功能介绍

建议前往[Wiki](https://github.com/Aaron8052/Unity-iOSNative-Plugin/wiki)查看更详细的API文档
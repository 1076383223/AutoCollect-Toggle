# AutoCollect Toggle —— 植物大战僵尸重植版（PvZ Replanted）自动收阳光 · 目标玩家切换 Mod

一个基于 [MelonLoader](https://github.com/LavaGang/MelonLoader) 的 Mod：  
**自动收集场上所有阳光/硬币，并让你随时切换「算给玩家 1 还是玩家 2」**。

> 适用场景：双人合作（Co-op）模式。你用键鼠、朋友用手柄，你嫌手动收阳光太累——  
> 装上本 Mod 后阳光自动进池子，**合作模式可以选择自动收给谁，所以两人都可以受益**，  
> 且收阳光发生在游戏逻辑层、**完全不占用你的鼠标**，放植物时不会冲突。

---

## 功能特性

- ✅ 自动收集场上所有阳光（无需点鼠标，零冲突）
- ✅ 可切换目标玩家：**玩家 1 / 玩家 2**
  - 游戏内**左上角按钮**点击切换
  - 随时按 **F9** 热键切换
  - 选择写入配置文件，**重启游戏后记住上次的选择**
- ✅ 独立运行，**不依赖** ReplantAPI 等其它 Mod（当然留着也无害）

---

## 适用环境

| 项目  | 要求                                         |
| --- | ------------------------------------------ |
| 游戏  | 植物大战僵尸重植版（PvZ Replanted，Steam 版 v1.5.1468） |
| 加载器 | MelonLoader v0.7.x（IL2CPP / net6 加载方式）     |
| 运行库 | .NET 6 运行时（MelonLoader 首次启动会自动下载安装）        |
| 模式  | 合作（Co-op）模式最有意义；单人也可用                      |

---

## 安装（最简单，推荐普通玩家）

1. **装好 MelonLoader**（图形安装器）：<https://github.com/LavaGang/MelonLoader.Installer>
   - 运行安装器 → 选中你的 `Replanted.exe`（游戏启动路径：Replanted.exe） → 点 Install。
2. **首次启动一次游戏**，让 MelonLoader 生成好运行环境（会自动装 .NET 6 运行时）。
3. 到本仓库的 **Releases** 页面，下载 `AutoCollect.dll`。
4. 把 `AutoCollect.dll` 丢进游戏目录下的 `Mods\` 文件夹，例如：
   ```
   <你的游戏目录>\PvZ Replanted\Mods\AutoCollect.dll
   ```

5. 重启游戏即可。

> ⚠️ 如果你之前装过**其它**自动收阳光 Mod（例如 SAutoCollectMod），请把它从 `Mods\` 里  
> 删掉或改名（如 `SAutoCollectMod.dll.bak`），否则会有**两个收集器同时收**，导致异常。

---

## 使用方法

- 进入游戏后，**左上角**会出现一个按钮：
  - `自动收阳光 -> 玩家1`
  - `自动收阳光 -> 玩家2`
  - 点击按钮即可切换。
- 或随时按 **F9** 切换（游戏中、菜单里都行）。
- 当前选择会保存到 `UserData\MelonPreferences.cfg` 的 `[AutoCollect]` 分区（`TargetPlayer=0` 或 `1`），  
  想手动改也行，或直接在游戏里切。
  装上 mod 之后 Steam 成就会被屏蔽；想要恢复成就，打开 MelonLoader 安装器点 Uninstall 卸载整个加载器。

---

## 从源码构建（给会点代码的朋友）

前置：安装 **.NET 6 SDK**（不是运行时）：<https://dotnet.microsoft.com/download/dotnet/6.0>

```bash
# 1) 先把游戏目录路径改到 build.bat 顶部的 GAMEDIR（或下面手动指定）
# 2) 用 MelonLoader 成功启动过一次游戏（生成 MelonLoader\Il2CppAssemblies\）

dotnet build -c Release -p:GameDir="你的游戏目录\PvZ Replanted"
# 产物：bin\Release\net6.0\AutoCollect.dll
```

编译出的 `AutoCollect.dll` 放进 `Mods\` 即可，与 Release 版一致。

---

## 常见问题 / 排错

**Q：游戏里没出现按钮，也不自动收阳光？**  
A：多半是 Mod 没被加载。看日志 `MelonLoader\Latest.log`，应出现：  
`Melon Assembly loaded: '.\Mods\AutoCollect.dll'` 和 `2 Mods loaded.`  
（只有 1 个说明本 Mod 没加载）。确认 DLL 在 `Mods\` 下、且 MelonLoader 已正确安装。

**Q：切到「玩家 2」后阳光还是只进玩家 1？**  
A：理论上 `Coin.Collect(int)` 的 `int` 即玩家编号（0=玩家1，1=玩家2）。若实测不符，  
说明该参数语义可能不同，请在 Issues 里反馈现象，作者会调整。

**Q：会封号 / 有风险吗？**  
A：这是本地单机 Mod，不联机外挂、不改他人数据。但任何 Mod 都有极小概率触发反作弊或版本不兼容，  
建议只在离线/私人合作局使用，且游戏更新后留意是否需要重新构建。

**Q：需要 ReplantAPI 吗？**  
A：不需要。本 Mod 直接基于游戏 IL2CPP 类型实现，独立运行；你原本装了 ReplantAPI 留着也无害。如果没有生效也可以把ReplantAPI丢入Mods文件夹看看。
[https://gamebanana.com/mods/629661](https://gamebanana.com/mods/629661)，下载 `replantapi_201.zip`

---

## 致谢 / 开源参考

- 思路与原始自动收阳光实现参考自开源项目 [Enaium/pvz-mod-AutoCollect](https://github.com/Enaium/pvz-mod-AutoCollect)。
- 加载器 [MelonLoader](https://github.com/LavaGang/MelonLoader)。

---

## 免责声明

本 Mod 仅供学习与交流，不附带任何游戏本体文件，亦不用于商业用途。  
使用本 Mod 产生的任何后果由使用者自行承担。请支持正版游戏。
本项目全程依赖AI构建，在本机测试成功，如有问题可以反馈，我看一下能不能解决

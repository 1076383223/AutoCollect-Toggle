@echo off
setlocal
REM 自动收阳光 · 目标玩家切换 Mod 构建脚本
REM 前置条件：
REM   1) 已安装 .NET 6 SDK: https://dotnet.microsoft.com/download/dotnet/6.0
REM      （装完在命令行能跑 dotnet --version 即可，例如 6.0.x）
REM   2) 已用 MelonLoader 成功启动过一次游戏，
REM      以便 MelonLoader 生成 MelonLoader\Il2CppAssemblies\ 下的引用 DLL。
REM   3) 把下面的 GAMEDIR 改成你自己的游戏目录（含 Replanted.exe 的那一层）。

set "GAMEDIR=C:\Games\PvZ Replanted"
set "LOG=%~dp0build.log"

echo [%time%] 开始构建（GameDir=%GAMEDIR%）... > "%LOG%"
dotnet build -c Release -p:GameDir="%GAMEDIR%" >> "%LOG%" 2>&1
if errorlevel 1 (
  echo.
  echo [BUILD FAILED] 构建失败，详细信息见 build.log：
  echo    %LOG%
  echo.
  type "%LOG%"
  echo.
  echo 常见原因：1) 没装 .NET 6 SDK  2) dotnet 无法在命令行运行
  echo           3) GAMEDIR 路径不对，或还没用 MelonLoader 启动过游戏（缺 Il2CppAssemblies）
  pause
  goto :eof
)

REM 部署：若你之前装过其它自动收阳光 Mod（如 SAutoCollectMod），请禁用它，
REM 否则会出现两个收集器同时收阳光。这里顺手把常见的那个改名备份。
if exist "%GAMEDIR%\Mods\SAutoCollectMod.dll" (
  move /Y "%GAMEDIR%\Mods\SAutoCollectMod.dll" "%GAMEDIR%\Mods\SAutoCollectMod.dll.bak" >nul
  echo 已备份旧 SAutoCollectMod.dll -^> SAutoCollectMod.dll.bak（避免重复收阳光）
)

copy /Y "bin\Release\net6.0\AutoCollect.dll" "%GAMEDIR%\Mods\AutoCollect.dll"
echo.
echo [OK] 已部署 AutoCollect.dll 到 Mods，重启游戏生效。
echo      进入游戏后左上角有按钮，按 F9 也能切换 玩家1/玩家2。
echo      构建日志: %LOG%
pause

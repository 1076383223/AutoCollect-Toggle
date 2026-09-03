#nullable disable
using HarmonyLib;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(AutoCollect.ModEntry), "AutoCollect Toggle", "1.0.0", "WorkBuddy")]

namespace AutoCollect;

public class ModEntry : MelonMod
{
    private static MelonLogger.Instance _log;
    private static int _targetPlayer = 0; // 0 = 玩家1, 1 = 玩家2

    private const string Category = "AutoCollect";
    private const string KeyTarget = "TargetPlayer";

    private static MelonPreferences_Entry<int> _targetEntry;

    public override void OnInitializeMelon()
    {
        _log = LoggerInstance;

        // 注册可调参数，写入 UserData/MelonPreferences.cfg，重启后记住选择
        var cat = MelonPreferences.CreateCategory(Category);
        _targetEntry = cat.CreateEntry<int>(KeyTarget, 0,
            "自动收阳光的目标玩家 (0 = 玩家1, 1 = 玩家2)");
        _targetPlayer = _targetEntry.Value;
        if (_targetPlayer != 0 && _targetPlayer != 1) _targetPlayer = 0;

        // 关键：注册 Harmony 补丁（来自 AutoCollect.Framework.BoardUpdatePatch）
        var harmony = new HarmonyLib.Harmony("AutoCollect.Toggle");
        harmony.PatchAll();

        _log.Msg($"[AutoCollect] 目标玩家 = {(_targetPlayer == 0 ? "玩家1" : "玩家2")}  (按 F9 或点左上角按钮切换)");
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            ToggleTarget();
        }
    }

    public override void OnGUI()
    {
        // 左上角按钮：点击切换目标玩家
        var label = _targetPlayer == 0
            ? "自动收阳光 -> 玩家1"
            : "自动收阳光 -> 玩家2";
        if (GUI.Button(new Rect(10, 10, 230, 30), label))
        {
            ToggleTarget();
        }
        GUI.Label(new Rect(10, 44, 300, 20), "按 F9 也可切换目标玩家");
    }

    private static void ToggleTarget()
    {
        _targetPlayer = _targetPlayer == 0 ? 1 : 0;
        if (_targetEntry != null) _targetEntry.Value = _targetPlayer;
        _log?.Msg($"[AutoCollect] 已切换 -> {(_targetPlayer == 0 ? "玩家1" : "玩家2")}");
    }

    // 供 BoardUpdatePatch 读取当前目标玩家
    public static int TargetPlayer => _targetPlayer;
}

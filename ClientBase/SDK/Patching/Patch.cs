using HarmonyLib;
using System;
using System.Reflection;

namespace ClientBase.SDK.Patching
{
    internal static class Patch
    {
        public static readonly HarmonyLib.Harmony Instance = new HarmonyLib.Harmony("ClientBase");

        private static HarmonyMethod GetPatchMethod(string methodName)
        {
            var method = typeof(Patch).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);
            return method != null ? new HarmonyMethod(method) : null;
        }

        private static void DoPatch(MethodInfo targetMethod, HarmonyMethod prefix = null, HarmonyMethod postfix = null)
        {
            try
            {
                Instance.Patch(targetMethod, prefix, postfix);
            }
            catch (Exception ex)
            {
                Logging.Log("[Patch Error] Failed to patch '" + targetMethod?.Name + "':\n" + ex, LType.Error);
            }
        }

        public static void Init()
        {
            try
            {
                DoPatch(
                    typeof(NetworkManager).GetMethod("Method_Public_Void_Player_PDM_1"),
                    GetPatchMethod(nameof(OnPlayerJoinPatch))
                );
                Logging.Log("OnPlayerJoin patch applied successfully.", LType.Success);
            }
            catch (Exception ex)
            {
                Logging.Log("[Patch Error] Failed to patch OnPlayerJoin:\n" + ex, LType.Error);
            }

            try
            {
                DoPatch(
                    typeof(NetworkManager).GetMethod("Method_Public_Void_Player_0"),
                    GetPatchMethod(nameof(OnPlayerLeavePatch))
                );
                Logging.Log("OnPlayerLeave patch applied successfully.", LType.Success);
            }
            catch (Exception ex)
            {
                Logging.Log("[Patch Error] Failed to patch OnPlayerLeave:\n" + ex, LType.Error);
            }
        }

        #region OnPlayerJoin/Leave
        private static void OnPlayerJoinPatch(VRC.Player __0)
        {
            Logging.Log($"Player joined: {__0.field_Private_APIUser_0.displayName}", LType.Join);
        }

        private static void OnPlayerLeavePatch(VRC.Player __0)
        {
            Logging.Log($"Player left: {__0.field_Private_APIUser_0.displayName}", LType.Leave);
        }
        #endregion
    }
}

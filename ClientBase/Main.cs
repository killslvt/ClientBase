using MelonLoader;
using System;
using System.Collections;
using UnityEngine;
using VRC.Core;
using ClientBase.Features.Movement;
using ClientBase.SDK;
using System.Threading.Tasks;

[assembly: MelonInfo(typeof(ClientBase.Main), "ClientBase", "1.0.0", "mhmswiss")]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonColor(ConsoleColor.Blue)]

namespace ClientBase
{
    //This is just a base for anyone wanting to make a client
    //Made by mhmswiss | https://discord.gg/Js5HJaWX2S
    internal class Main : MelonMod
    {
        #region ApplicationStart&Quit
        [Obsolete]
        public override void OnApplicationStart()
        {
            Logging.InitConsole();

            Logging.Log("Init Patches", LType.Info);
            Task.Run(() => SDK.Patching.Patch.Init());

            Logging.Log("Loading ClientBase", LType.Info);
            MelonCoroutines.Start(WaitForQuickMenu());
        }

        public override void OnApplicationQuit()
        {
            //Nothing to do here.
            //Mostly used for saving configs or closing external apps for console logs
        }
        #endregion

        #region QMHandling
        private static IEnumerator WaitForQuickMenu()
        {
            while (APIUser.CurrentUser == null)
                yield return null;

            Logging.Log("Waiting for Quick Menu...", LType.Info);

            while (Camera.main == null || GameObject.Find("Canvas_QuickMenu(Clone)") == null)
                yield return null;

            Logging.Log("Quick Menu Loaded!", LType.Success);
            Loader.QMLoader.Init();
        }
        #endregion

        #region UpdateHandling
        public override void OnUpdate()
        {
            Flight.Update();
        }

        public override void OnFixedUpdate()
        {
            
        }
        #endregion

        #region SceneHandling
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            Logging.Log("Scene loaded: " + sceneName + " (Build Index: " + buildIndex + ")", LType.Debug);
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            Logging.Log("Scene unloaded: " + sceneName + " (Build Index: " + buildIndex + ")", LType.Debug);
        }
        #endregion
    }
}

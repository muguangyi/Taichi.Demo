/*
 * This file is part of Taichi.Demo project.
 *
 * (c) MuGuangyi <muguangyi@hotmail.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

using Taichi.Foundation;
using UnityEngine;
using Taichi.ILRuntime;
using Taichi.Asset;
using System;
using Taichi.Gameplay;
using Taichi.Async;

namespace Taichi.Demo
{
    public class Main : MonoBehaviour, IScriptLoader
    {
        private class AsyncScriptAsset : Async<byte[]>
        {
            private bool finished = false;

            public AsyncScriptAsset(IAsync<IAsset> asset)
            {
                asset.Completed += a =>
                {
                    SetResult(asset.GetResult()?.Cast<TextAsset>().bytes);
                    this.finished = true;
                };
            }

            protected override AsyncState OnUpdate()
            {
                return this.finished ? AsyncState.Succeed : AsyncState.Running;
            }
        }

        [Resolve] private static IAssetFactory factory = null;
        [Resolve] private static IScriptDomain domain = null;
        [Resolve] private static IGameInstance game = null;

        private void Start()
        {
            Assembler.ImportModuleInstance<Main, Main>(this);
        }

        private Type[] Contexts { get; } = new Type[] { typeof(Player) };

        private async void OnResolve()
        {
            game.Goto(new MainStage());

            domain.Loader = this;
            await domain.LoadAssemblyAsync("IL/ILScripts.il");

            domain.Start("IL.MainEntry", "Main");

            Main.OnTest += HandleTest;

            Trigger();
        }

        private static void HandleTest(string a, int b)
        {
            Debug.Log($"{a}, {b}....");
        }

        public static event Action<string, int> OnTest;

        public static void Trigger()
        {
            OnTest?.Invoke("Hello", 6);
        }

        public byte[] Load(string file)
        {
            using (var asset = factory.Load(file, typeof(TextAsset)))
            {
                return asset.Cast<TextAsset>().bytes;
            }
        }

        public IAsync<byte[]> LoadAsync(string file)
        {
            return new AsyncScriptAsset(factory.LoadAsync(file, typeof(TextAsset)));
        }
    }
}

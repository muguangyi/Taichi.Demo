using Taichi.Foundation;
using Taichi.Logger;
using Taichi.Coroutine;
using System;
using System.Collections;
using UnityEngine;
using Taichi.Demo;
using System.Collections.Generic;

namespace IL
{
    public interface ILog
    {
        void Print(object msg);
    }

    internal class Log : ILog
    {
        public void Print(object msg)
        {
            this.Warn(msg);
        }
    }

    public class DelegateTest
    {
        public event Action<string> OnTest;
        public event Action<int> OnTest2;
        public event Action<string, string> OnTest3;
        public event Action<Dictionary<int, uint>> OnTest4;

        public void Trigger()
        {
            OnTest?.Invoke("DelegateTest");
            OnTest2?.Invoke(10);
            OnTest3?.Invoke("Yes", "OK");
        }
    }

    public class MainGame
    {
        [Resolve] private static IMonoDriver MonoDriver = null;
        [Resolve] private static ILog Log = null;

        private DelegateTest delegateTest = null;

        public void Start()
        {
            Assembler.ImportModule<ILog, Log>();

            Assembler.ImportModuleInstance<MainGame, MainGame>(this);

            MonoDriver.StartCoroutine(Coroutine());

            delegateTest = new DelegateTest();
            delegateTest.OnTest += OnDelegateTest;
            delegateTest.OnTest2 += OnDelegateTest2;
            delegateTest.OnTest3 += OnDelegateTest3;
            delegateTest.OnTest4 += OnDelegateTest4Dict;

            delegateTest.Trigger();

            Main.OnTest += OnDelegateTest4;
            Main.Trigger();
        }

        private IEnumerator Coroutine()
        {
            Log.Print("Start coroutine");

            yield return new WaitForSeconds(5);

            Log.Print("Wait 5 seconds...");

            Log.Print("Hello IL entry");

            //throw new Exception();
        }

        private void OnDelegateTest(string name)
        {
            Log.Print($"Return by delegate: {name}");
        }

        private void OnDelegateTest2(int id)
        {
            Log.Print($"Return by delegate2: {id}");
        }

        private void OnDelegateTest3(string a, string b)
        {
            Log.Print($"Return by delegate3: {a}, {b}");
        }

        private void OnDelegateTest4(string a, int b)
        {
            Log.Print($"Return by delegate4: {a}, {b}");
        }

        private void OnDelegateTest4Dict(Dictionary<int, uint> result)
        {

        }
    }

    public class MainEntry
    {
        private static MainGame game = null;

        public static void Main()
        {
            game = new MainGame();
            game.Start();
        }
    }
}

using CatCode.AsyncTools;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class TestAsyncAwait : MonoBehaviour
    {
        private CancellationTokenSource _cts;
        [SerializeField] private Button _button;

        public async void OnEnable()
        {
            _cts = new CancellationTokenSource();
            try
            {
                await _button.WaitButtonClickToTask(_cts.Token);
                Debug.Log("Task Completed");
                await UniTaskUtils.EventToTask(h => _button.onClick.AddListener(h), h => _button.onClick.RemoveListener(h), _cts.Token);
                Debug.Log("UniTask Completed");
                await AwaitableUtils.EventToAwaitable(h => _button.onClick.AddListener(h), h => _button.onClick.RemoveListener(h), _cts.Token);
                Debug.Log("Awaitable Completed");
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Catch");
            }
            finally
            {
                _cts.Dispose();
                _cts = null;
            }
        }


        public void OnDisable()
        {
            _cts?.Cancel();
        }
    }

    public static class UIAsyncExtensions
    {
        public static Task WaitButtonClickToTask(this Button button, CancellationToken token)
            => TaskUtils.EventToTask(h => button.onClick.AddListener(h), h => button.onClick.RemoveListener(h), token);

    }
}
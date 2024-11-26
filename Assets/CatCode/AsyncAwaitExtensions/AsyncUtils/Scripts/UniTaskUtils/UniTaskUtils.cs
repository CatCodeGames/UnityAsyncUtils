using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine.Events;

namespace CatCode.AsyncTools
{
    public static class UniTaskUtils
    {
        public static UniTask EventToTask(Action<Action> subscribe, Action<Action> unsubscribe, Func<bool> predicate, CancellationToken token)
        {
            var tcs = new UniTaskCompletionSource();
            if (token.IsCancellationRequested)
            {
                tcs.TrySetCanceled();
                return tcs.Task;
            }

            if (predicate())
            {
                tcs.TrySetResult();
                return tcs.Task;
            }

            CancellationTokenRegistration ctr = default;
            ctr = token.Register(OnCancel);

            subscribe(EventHandler);

            return tcs.Task;

            void EventHandler()
            {
                if (!predicate())
                    return;
                unsubscribe(EventHandler);
                ctr.Dispose();
                tcs.TrySetResult();
            }

            void OnCancel()
            {
                unsubscribe(EventHandler);
                ctr.Dispose();
                tcs.TrySetResult();
            }
        }

        public static UniTask EventToTask(Action<Action> subscribe, Action<Action> unsubscribe, CancellationToken token)
        {
            var tcs = new UniTaskCompletionSource();
            if (token.IsCancellationRequested)
            {
                tcs.TrySetCanceled();
                return tcs.Task;
            }
            CancellationTokenRegistration ctr = default;
            ctr = token.Register(OnCancel);

            subscribe(EventHandler);

            return tcs.Task;

            void EventHandler()
            {
                unsubscribe(EventHandler);
                ctr.Dispose();
                tcs.TrySetResult();
            }

            void OnCancel()
            {
                unsubscribe(EventHandler);
                ctr.Dispose();
                tcs.TrySetCanceled();
            }
        }


        public static UniTask CallbackToTask(Action<Action> method, CancellationToken token)
        {
            var tcs = new UniTaskCompletionSource();
            if (token.IsCancellationRequested)
            {
                tcs.TrySetCanceled();
                return tcs.Task;
            }
            CancellationTokenRegistration ctr = default;
            ctr = token.Register(OnCancel);

            method(OnCompleted);

            return tcs.Task;

            void OnCompleted()
            {
                ctr.Dispose();
                tcs.TrySetResult();
            }

            void OnCancel()
            {
                ctr.Dispose();
                tcs.TrySetResult();
            }
        }


        public static UniTask EventToTask(Action<UnityAction> subscribe, Action<UnityAction> unsubscribe, Func<bool> predicate, CancellationToken token)
        {
            var tcs = new UniTaskCompletionSource();
            if (token.IsCancellationRequested)
            {
                tcs.TrySetCanceled();
                return tcs.Task;
            }

            if (predicate())
            {
                tcs.TrySetResult();
                return tcs.Task;
            }

            CancellationTokenRegistration ctr = default;
            ctr = token.Register(OnCancel);

            subscribe(EventHandler);

            return tcs.Task;

            void EventHandler()
            {
                if (!predicate())
                    return;
                unsubscribe(EventHandler);
                ctr.Dispose();
                tcs.TrySetResult();
            }

            void OnCancel()
            {
                unsubscribe(EventHandler);
                ctr.Dispose();
                tcs.TrySetResult();
            }
        }

        public static UniTask EventToTask(Action<UnityAction> subscribe, Action<UnityAction> unsubscribe, CancellationToken token)
        {
            var tcs = new UniTaskCompletionSource();
            if (token.IsCancellationRequested)
            {
                tcs.TrySetCanceled();
                return tcs.Task;
            }
            CancellationTokenRegistration ctr = default;
            ctr = token.Register(OnCancel);

            subscribe(EventHandler);

            return tcs.Task;

            void EventHandler()
            {
                unsubscribe(EventHandler);
                ctr.Dispose();
                tcs.TrySetResult();
            }

            void OnCancel()
            {
                unsubscribe(EventHandler);
                ctr.Dispose();
                tcs.TrySetCanceled();
            }
        }

    }
}
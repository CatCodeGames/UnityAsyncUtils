using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace CatCode.AsyncTools
{
    public static class TaskUtils
    {
        public static Task EventToTask<T>(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe, Func<bool> predicate, CancellationToken token)
        {
            var tcs = new TaskCompletionSource<bool>();

            if (token.IsCancellationRequested)
            {
                tcs.SetCanceled();
                return tcs.Task;
            }

            if (predicate != null && predicate())
            {
                tcs.SetResult(true);
                return tcs.Task;
            }

            Action<T> handler = null;
            CancellationTokenRegistration ctr = default;

            ctr = token.Register(OnCancel);

            handler = (predicate != null)
                ? EventHandlerWithPredicate
                : EventHandler;
            subscribe(handler);

            return tcs.Task;

            void EventHandler(T value)
            {
                unsubscribe(handler);
                ctr.Dispose();
                tcs.SetResult(true);
            }

            void EventHandlerWithPredicate(T value)
            {
                if (!predicate())
                    return;
                EventHandler(value);
            }

            void OnCancel()
            {
                unsubscribe(handler);
                ctr.Dispose();
                tcs.SetCanceled();
            }
        }

        public static Task EventToTask(Action<Action> subscribe, Action<Action> unsubscribe, Func<bool> predicate, CancellationToken token)
        {
            var tcs = new TaskCompletionSource<bool>();

            if (token.IsCancellationRequested)
            {
                tcs.SetCanceled();
                return tcs.Task;
            }

            if (predicate != null && predicate())
            {
                tcs.SetResult(true);
                return tcs.Task;
            }

            Action handler = null;
            CancellationTokenRegistration ctr = default;

            ctr = token.Register(OnCancel);

            handler = (predicate != null)
                ? EventHandlerWithPredicate
                : EventHandler;
            subscribe(handler);

            return tcs.Task;


            void EventHandler()
            {
                unsubscribe(handler);
                ctr.Dispose();
                tcs.SetResult(true);
            }

            void EventHandlerWithPredicate()
            {
                if (!predicate())
                    return;
                EventHandler();
            }

            void OnCancel()
            {
                unsubscribe(handler);
                ctr.Dispose();
                tcs.SetCanceled();
            }
        }

        public static Task EventToTask(Action<Action> subscribe, Action<Action> unsubscribe, CancellationToken token)
            => EventToTask(subscribe, unsubscribe, null, token);

        public static Task EventToTask<T>(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe, CancellationToken token)
            => EventToTask(subscribe, unsubscribe, null, token);


        public static Task CallbackToTask(Action<Action> method, CancellationToken token)
        {
            var tcs = new TaskCompletionSource<bool>();
            if (token.IsCancellationRequested)
            {
                tcs.SetCanceled();
                return tcs.Task;
            }
            CancellationTokenRegistration ctr = default;
            ctr = token.Register(OnCancel);

            method(OnCompleted);

            return tcs.Task;

            void OnCompleted()
            {
                ctr.Dispose();
                tcs.SetResult(true);
            }

            void OnCancel()
            {
                ctr.Dispose();
                tcs.SetCanceled();
            }
        }




        public static Task EventToTask<T>(Action<UnityAction<T>> subscribe, Action<UnityAction<T>> unsubscribe, Func<bool> predicate, CancellationToken token)
        {
            var tcs = new TaskCompletionSource<bool>();

            if (token.IsCancellationRequested)
            {
                tcs.SetCanceled();
                return tcs.Task;
            }

            if (predicate != null && predicate())
            {
                tcs.SetResult(true);
                return tcs.Task;
            }

            UnityAction<T> handler = null;
            CancellationTokenRegistration ctr = default;

            ctr = token.Register(OnCancel);

            handler = (predicate != null)
                ? EventHandlerWithPredicate
                : EventHandler;
            subscribe(handler);

            return tcs.Task;


            void EventHandler(T value)
            {
                unsubscribe(handler);
                ctr.Dispose();
                tcs.SetResult(true);
            }

            void EventHandlerWithPredicate(T value)
            {
                if (!predicate())
                    return;
                EventHandler(value);
            }

            void OnCancel()
            {
                unsubscribe(handler);
                ctr.Dispose();
                tcs.SetCanceled();
            }
        }

        public static Task EventToTask(Action<UnityAction> subscribe, Action<UnityAction> unsubscribe, Func<bool> predicate, CancellationToken token)
        {
            var tcs = new TaskCompletionSource<bool>();

            if (token.IsCancellationRequested)
            {
                tcs.SetCanceled();
                return tcs.Task;
            }

            if (predicate != null && predicate())
            {
                tcs.SetResult(true);
                return tcs.Task;
            }

            UnityAction handler = null;
            CancellationTokenRegistration ctr = default;

            ctr = token.Register(OnCancel);

            handler = (predicate != null)
                ? EventHandlerWithPredicate
                : EventHandler;
            subscribe(handler);

            return tcs.Task;


            void EventHandler()
            {
                unsubscribe(handler);
                ctr.Dispose();
                tcs.SetResult(true);
            }

            void EventHandlerWithPredicate()
            {
                if (!predicate())
                    return;
                EventHandler();
            }

            void OnCancel()
            {
                unsubscribe(handler);
                ctr.Dispose();
                tcs.SetCanceled();
            }
        }

        public static Task EventToTask<T>(Action<UnityAction<T>> subscribe, Action<UnityAction<T>> unsubscribe, CancellationToken token)
            => EventToTask(subscribe, unsubscribe, null, token);

        public static Task EventToTask(Action<UnityAction> subscribe, Action<UnityAction> unsubscribe, CancellationToken token)
            => EventToTask(subscribe, unsubscribe, null, token);
    }
}
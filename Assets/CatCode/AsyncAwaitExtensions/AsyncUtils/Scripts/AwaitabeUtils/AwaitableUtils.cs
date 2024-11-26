using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace CatCode.AsyncTools
{
    public static class AwaitableUtils
    {
        public static Awaitable EventToAwaitable<T>(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe, Func<bool> predicate, CancellationToken token)
        {
            var tcs = new AwaitableCompletionSource();

            if (token.IsCancellationRequested)
            {
                tcs.SetCanceled();
                return tcs.Awaitable;
            }

            if (predicate())
            {
                tcs.SetResult();
                return tcs.Awaitable;
            }

            Action<T> handler = null;
            CancellationTokenRegistration ctr = default;

            ctr = token.Register(OnCancel);

            handler = (predicate != null)
                ? EventHandlerWithPredicate
                : EventHandler;
            subscribe(handler);

            return tcs.Awaitable;

            void EventHandler(T value)
            {
                unsubscribe(EventHandler);
                ctr.Dispose();
                tcs.SetResult();
            }

            void EventHandlerWithPredicate(T value)
            {
                if (!predicate())
                    return;
                EventHandler(value);
            }

            void OnCancel()
            {
                unsubscribe(EventHandler);
                ctr.Dispose();
                tcs.SetCanceled();
            }
        }

        public static Awaitable EventToAwaitable(Action<Action> subscribe, Action<Action> unsubscribe, Func<bool> predicate, CancellationToken token)
        {
            var tcs = new AwaitableCompletionSource();

            if (token.IsCancellationRequested)
            {
                tcs.SetCanceled();
                return tcs.Awaitable;
            }

            if (predicate())
            {
                tcs.SetResult();
                return tcs.Awaitable;
            }

            Action handler = null;
            CancellationTokenRegistration ctr = default;

            ctr = token.Register(OnCancel);

            handler = (predicate != null)
                ? EventHandlerWithPredicate
                : EventHandler;
            subscribe(handler);

            return tcs.Awaitable;

            void EventHandler()
            {
                unsubscribe(EventHandler);
                ctr.Dispose();
                tcs.SetResult();
            }

            void EventHandlerWithPredicate()
            {
                if (!predicate())
                    return;
                EventHandler();
            }

            void OnCancel()
            {
                unsubscribe(EventHandler);
                ctr.Dispose();
                tcs.SetCanceled();
            }
        }

        public static Awaitable EventToAwaitable(Action<Action> subscribe, Action<Action> unsubscribe, CancellationToken token)
            => EventToAwaitable(subscribe, unsubscribe, null, token);

        public static Awaitable EventToAwaitable<T>(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe, CancellationToken token)
            => EventToAwaitable(subscribe, unsubscribe, null, token);



        public static Awaitable EventToAwaitable<T>(Action<UnityAction<T>> subscribe, Action<UnityAction<T>> unsubscribe, Func<bool> predicate, CancellationToken token)
        {
            var tcs = new AwaitableCompletionSource();

            if (token.IsCancellationRequested)
            {
                tcs.SetCanceled();
                return tcs.Awaitable;
            }

            if (predicate())
            {
                tcs.SetResult();
                return tcs.Awaitable;
            }

            UnityAction<T> handler = null;
            CancellationTokenRegistration ctr = default;

            ctr = token.Register(OnCancel);

            handler = (predicate != null)
                ? EventHandlerWithPredicate
                : EventHandler;
            subscribe(handler);

            return tcs.Awaitable;

            void EventHandler(T value)
            {
                unsubscribe(handler);
                ctr.Dispose();
                tcs.SetResult();
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

        public static Awaitable EventToAwaitable(Action<UnityAction> subscribe, Action<UnityAction> unsubscribe, Func<bool> predicate, CancellationToken token)
        {
            var tcs = new AwaitableCompletionSource();

            if (token.IsCancellationRequested)
            {
                tcs.SetCanceled();
                return tcs.Awaitable;
            }

            if (predicate())
            {
                tcs.SetResult();
                return tcs.Awaitable;
            }

            UnityAction handler = null;
            CancellationTokenRegistration ctr = default;

            ctr = token.Register(OnCancel);

            handler = (predicate != null)
                ? EventHandlerWithPredicate
                : EventHandler;
            subscribe(handler);

            return tcs.Awaitable;

            void EventHandler()
            {
                unsubscribe(handler);
                ctr.Dispose();
                tcs.SetResult();
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

        public static Awaitable EventToAwaitable<T>(Action<UnityAction<T>> subscribe, Action<UnityAction<T>> unsubscribe, CancellationToken token)
            => EventToAwaitable(subscribe, unsubscribe, null, token);

        public static Awaitable EventToAwaitable(Action<UnityAction> subscribe, Action<UnityAction> unsubscribe, CancellationToken token)
            => EventToAwaitable(subscribe, unsubscribe, null, token);



        public static Awaitable CallbackToTask(Action<Action> method, CancellationToken token)
        {
            var tcs = new AwaitableCompletionSource();
            if (token.IsCancellationRequested)
            {
                tcs.SetCanceled();
                return tcs.Awaitable;
            }
            CancellationTokenRegistration ctr = default;
            ctr = token.Register(OnCancel);
            method(OnCompleted);
            return tcs.Awaitable;

            void OnCompleted()
            {
                ctr.Dispose();
                tcs.SetResult();
            }

            void OnCancel()
            {
                ctr.Dispose();
                tcs.SetCanceled();
            }
        }
    }
}
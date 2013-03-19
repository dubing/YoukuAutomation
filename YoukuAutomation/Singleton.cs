using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YoukuAutomation
{
    /// <summary>
    /// Singleton
    /// </summary>    
    public static class Singleton<T> where T : new                                                                                                                   ()
    {
        /// <summary>
        /// LockKey
        /// </summary>
        private static object LockKey = new object();

        /// <summary>
        /// Get an instance of T
        /// </summary>
        /// <returns></returns>
        public static T GetInstance()
        {
            return GetInstance(null);
        }

        /// <summary>
        /// Get an instance of T
        /// </summary>
        /// <param name="onCreateInstance">The on create instance.</param>
        /// <returns></returns>
        public static T GetInstance(Func<T> onCreateInstance)
        {
            if (_Instance == null)
            {
                lock (LockKey)
                {
                    if (_Instance == null)
                    {
                        try
                        {
                            if (onCreateInstance == null)
                                _Instance = new T();
                            else
                                _Instance = onCreateInstance();
                        }
                        catch
                        {
                            _Instance = default(T);
                        }
                    }
                }
            }
            return _Instance;
        }
        private static T _Instance;


        /// <summary>
        /// Get an instance of T and set to instance
        /// </summary>
        /// <param name="lockKey">The lock key.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="onCreateInstance">The on create instance.</param>
        /// <returns></returns>
        public static T GetInstance(object lockKey, T instance, Func<T> onCreateInstance)
        {
            if (instance == null)
            {
                if (lockKey == null)
                    lockKey = LockKey;
                lock (lockKey)
                {
                    if (instance == null)
                    {
                        try
                        {
                            if (onCreateInstance == null)
                                instance = new T();
                            else
                                instance = onCreateInstance();
                        }
                        catch
                        {
                            instance = default(T);
                        }
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Release the instance of T
        /// </summary>
        public static void ReleaseInstance()
        {
            lock (LockKey)
            {
                IDisposable id = _Instance as IDisposable;
                if (id != null)
                    id.Dispose();

                _Instance = default(T);
            }
        }

        /// <summary>
        /// 线程安全的执行任务
        /// </summary>
        /// <param name="lockCondition">The lock condition.</param>
        /// <param name="lockObject">The lock object.</param>
        /// <param name="action">The action.</param>
        public static void TakeAction(Func<bool> lockCondition, object lockObject, Action action)
        {
            if (lockCondition())
            {
                lock (lockObject)
                {
                    if (lockCondition())
                    {
                        action();
                    }
                }
            }
        }
    }
}

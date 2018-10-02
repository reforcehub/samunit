using System;
using System.Collections.Generic;

namespace SamUnit.Scripts.Application.Common
{
    /// <summary>
    /// シーン間やりとりを行いたい変数とかを管理してくれるセッションを提供するクラス
    /// </summary>
    [Serializable]
    public class Session : SingletonMonoBehaviour<Session>
    {
        private static Dictionary<string, object> instances = new Dictionary<string, object>();

        /// <summary>
        /// セッションを新しく始める
        /// </summary>
        public void New()
        {
            instances.Clear();
        }

        /// <summary>
        /// セッションを登録
        /// </summary>
        /// <param name="sessionName"></param>
        /// <param name="instance"></param>
        public void Create(string sessionName, object instance)
        {
            instances.Add(sessionName, instance);
        }

        /// <summary>
        /// セッションを取得する
        /// </summary>
        /// <param name="sessionName"></param>
        public object Read(string sessionName)
        {
            if (instances.ContainsKey(sessionName))
            {
                return instances[sessionName];
            }

            return null;
        }

        /// <summary>
        /// 対象セッションを削除する
        /// </summary>
        /// <param name="sessionName"></param>
        public void Delete(string sessionName)
        {
            if (instances.ContainsKey(sessionName))
            {
                instances.Remove(sessionName);
            }
        }

        /// <summary>
        /// 対象セッションを更新
        /// </summary>
        /// <param name="sessionName"></param>
        /// <param name="instance"></param>
        public void Update(string sessionName, object instance)
        {
            if (instances.ContainsKey(sessionName))
            {
                instances[sessionName] = instance;
                return;
            }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.LogError($"[SessionNullException] {sessionName}は存在しません。");
#endif
        }

        // hidden construct
        private Session() {}
    }
}
using UnityEngine;

/// <summary>
/// シングルトンMonoBehaviourクラス
/// </summary>
/// <typeparam name="T">TargetSingleton</typeparam>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	// Singleton Instance field
	private static T instance = null;

	// Singleton インスタンスオブジェクト
	private static object instanceObject = new object();

	public static T Instance
	{
		get
		{
			if (instance != null)
			{
				return instance;
			}

			// インスタンスがない場合に探す
			instance = FindObjectOfType<T>();

			// 同時にインスタンス生成を呼ばないためにlockする
			lock (instanceObject)
			{
				// Findで見つからなかった場合、新しくオブジェクトを生成
				if (instance == null)
				{
					GameObject singleton = new GameObject {name = typeof(T).Name};

					instance = singleton.AddComponent<T>();

					DontDestroyOnLoad(singleton);
				}
			}

			return instance;
		}
	}
}

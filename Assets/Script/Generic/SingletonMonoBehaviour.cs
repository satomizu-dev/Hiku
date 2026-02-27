using System;
using UnityEngine;

namespace Script.Generic {
	public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
		protected abstract bool IsDontDestroyOnLoad { get; }

		private static T _instance;

		public static T Instance {
			get {
				if (_instance) return _instance;
			
				Type t = typeof(T);
				_instance = (T)FindObjectOfType(t);
				if (!_instance) { Debug.LogError(t + " is nothing."); }

				return _instance;
			}
		}

		protected virtual void Awake() {
			if (this != Instance) { Destroy(this); return; }
			if (IsDontDestroyOnLoad)  DontDestroyOnLoad(gameObject);
		}
	}
}
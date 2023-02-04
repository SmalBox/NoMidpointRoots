using System;
using System.Collections.Generic;


//事件系统
// 1. 定义消息枚举
// 在MessengerEventType中定义要监听的消息枚举值。
// 2. 监听
// Messenger<float>.AddListener(MessengerEventType.DEFAULT_TYPE, MyEventHandler);
// 3. 发送消息广播
// Messenger<float>.Broadcast(MessengerEventType.DEFAULT_TYPE, 1.0f);
// 4. 移除监听
// Messenger<float>.RemoveListener(MessengerEventType.DEFAULT_TYPE, MyEventHandler);
// 有注册就一定有注销，切记！！！
namespace Auto.Logic.Core.Message
{
	/// <summary>
	/// 保存事件ID
	/// </summary>
	public enum MessengerEventType
	{
		// 游戏开始结束
		PROGRESS_START,
		PROGRESS_END,
		
		// 声音事件
		VOLUME_MUSICAL, // 音乐音量
		VOLUME_SOUND_EFFECT, // 音效音量
		
		// Log
		LOG_NORMAL,
		LOG_WARNING,
		LOG_ERROR,
		
		// 信息
		INFO_TIME,
		INFO_SCORE,
		
		// 数据变动
		DATA_CHANGE_SCORE,
	}

	public enum MessengerMode
	{
		DONT_REQUIRE_LISTENER,
		REQUIRE_LISTENER,
	}

	static internal class MessengerInternal
	{
		static public Dictionary<MessengerEventType, Delegate> eventTable =
			new Dictionary<MessengerEventType, Delegate>();

		static public readonly MessengerMode DEFAULT_MODE = MessengerMode.REQUIRE_LISTENER;

		static public void OnListenerAdding(MessengerEventType eventType, Delegate listenerBeingAdded)
		{
			if (!eventTable.ContainsKey(eventType))
			{
				eventTable.Add(eventType, null);
			}

			Delegate d = eventTable[eventType];
			if (d != null && d.GetType() != listenerBeingAdded.GetType())
			{
				throw new ListenerException(string.Format(
					"Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}",
					eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
			}
		}

		static public void OnListenerRemoving(MessengerEventType eventType, Delegate listenerBeingRemoved)
		{
			if (eventTable.ContainsKey(eventType))
			{
				Delegate d = eventTable[eventType];

				if (d == null)
				{
					throw new ListenerException(string.Format(
						"Attempting to remove listener with for event type {0} but current listener is null.",
						eventType));
				}
				else if (d.GetType() != listenerBeingRemoved.GetType())
				{
					throw new ListenerException(string.Format(
						"Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}",
						eventType, d.GetType().Name, listenerBeingRemoved.GetType().Name));
				}
			}
			else
			{
				throw new ListenerException(string.Format(
					"Attempting to remove listener for type {0} but Messenger doesn't know about this event type.",
					eventType));
			}
		}

		static public void OnListenerRemoved(MessengerEventType eventType)
		{
			if (eventTable[eventType] == null)
			{
				eventTable.Remove(eventType);
			}
		}

		static public void OnBroadcasting(MessengerEventType eventType, MessengerMode mode)
		{
			if (mode == MessengerMode.REQUIRE_LISTENER && !eventTable.ContainsKey(eventType))
			{
				throw new MessengerInternal.BroadcastException(
					string.Format("Broadcasting message {0} but no listener found.", eventType));
			}
		}

		static public BroadcastException CreateBroadcastSignatureException(MessengerEventType eventType)
		{
			return new BroadcastException(string.Format(
				"Broadcasting message {0} but listeners have a different signature than the broadcaster.",
				eventType));
		}

		[Serializable]
		public class BroadcastException : Exception
		{
			public BroadcastException(string msg)
				: base(msg)
			{
			}
		}

		[Serializable]
		public class ListenerException : Exception
		{
			public ListenerException(string msg)
				: base(msg)
			{
			}
		}
	}


	// No parameters
	static public class Messenger
	{
		private static Dictionary<MessengerEventType, Delegate> eventTable = MessengerInternal.eventTable;

		static public void AddListener(MessengerEventType eventType, Action handler)
		{
			MessengerInternal.OnListenerAdding(eventType, handler);
			eventTable[eventType] = (Action)eventTable[eventType] + handler;
		}

		static public void RemoveListener(MessengerEventType eventType, Action handler)
		{
			MessengerInternal.OnListenerRemoving(eventType, handler);
			eventTable[eventType] = (Action)eventTable[eventType] - handler;
			MessengerInternal.OnListenerRemoved(eventType);
		}

		static public void Broadcast(MessengerEventType eventType)
		{
			Broadcast(eventType, MessengerInternal.DEFAULT_MODE);
		}

		static public void Broadcast(MessengerEventType eventType, MessengerMode mode)
		{
			MessengerInternal.OnBroadcasting(eventType, mode);
			Delegate d;
			if (eventTable.TryGetValue(eventType, out d))
			{
				Action callback = d as Action;
				if (callback != null)
				{
					callback();
				}
				else
				{
					throw MessengerInternal.CreateBroadcastSignatureException(eventType);
				}
			}
		}
	}

	// One parameter
	static public class Messenger<T>
	{
		private static Dictionary<MessengerEventType, Delegate> eventTable = MessengerInternal.eventTable;

		static public void AddListener(MessengerEventType eventType, Action<T> handler)
		{
			MessengerInternal.OnListenerAdding(eventType, handler);
			eventTable[eventType] = (Action<T>)eventTable[eventType] + handler;
		}

		static public void RemoveListener(MessengerEventType eventType, Action<T> handler)
		{
			MessengerInternal.OnListenerRemoving(eventType, handler);
			eventTable[eventType] = (Action<T>)eventTable[eventType] - handler;
			MessengerInternal.OnListenerRemoved(eventType);
		}

		static public void Broadcast(MessengerEventType eventType, T arg1)
		{
			Broadcast(eventType, arg1, MessengerInternal.DEFAULT_MODE);
		}

		static public void Broadcast(MessengerEventType eventType, T arg1, MessengerMode mode)
		{
			MessengerInternal.OnBroadcasting(eventType, mode);
			Delegate d;
			if (eventTable.TryGetValue(eventType, out d))
			{
				Action<T> callback = d as Action<T>;
				if (callback != null)
				{
					callback(arg1);
				}
				else
				{
					throw MessengerInternal.CreateBroadcastSignatureException(eventType);
				}
			}
		}
	}


	// Two parameters
	static public class Messenger<T, U>
	{
		private static Dictionary<MessengerEventType, Delegate> eventTable = MessengerInternal.eventTable;

		static public void AddListener(MessengerEventType eventType, Action<T, U> handler)
		{
			MessengerInternal.OnListenerAdding(eventType, handler);
			eventTable[eventType] = (Action<T, U>)eventTable[eventType] + handler;
		}

		static public void RemoveListener(MessengerEventType eventType, Action<T, U> handler)
		{
			MessengerInternal.OnListenerRemoving(eventType, handler);
			eventTable[eventType] = (Action<T, U>)eventTable[eventType] - handler;
			MessengerInternal.OnListenerRemoved(eventType);
		}

		static public void Broadcast(MessengerEventType eventType, T arg1, U arg2)
		{
			Broadcast(eventType, arg1, arg2, MessengerInternal.DEFAULT_MODE);
		}

		static public void Broadcast(MessengerEventType eventType, T arg1, U arg2, MessengerMode mode)
		{
			MessengerInternal.OnBroadcasting(eventType, mode);
			Delegate d;
			if (eventTable.TryGetValue(eventType, out d))
			{
				Action<T, U> callback = d as Action<T, U>;
				if (callback != null)
				{
					callback(arg1, arg2);
				}
				else
				{
					throw MessengerInternal.CreateBroadcastSignatureException(eventType);
				}
			}
		}
	}


	// Three parameters
	static public class Messenger<T, U, V>
	{
		private static Dictionary<MessengerEventType, Delegate> eventTable = MessengerInternal.eventTable;

		static public void AddListener(MessengerEventType eventType, Action<T, U, V> handler)
		{
			MessengerInternal.OnListenerAdding(eventType, handler);
			eventTable[eventType] = (Action<T, U, V>)eventTable[eventType] + handler;
		}

		static public void RemoveListener(MessengerEventType eventType, Action<T, U, V> handler)
		{
			MessengerInternal.OnListenerRemoving(eventType, handler);
			eventTable[eventType] = (Action<T, U, V>)eventTable[eventType] - handler;
			MessengerInternal.OnListenerRemoved(eventType);
		}

		static public void Broadcast(MessengerEventType eventType, T arg1, U arg2, V arg3)
		{
			Broadcast(eventType, arg1, arg2, arg3, MessengerInternal.DEFAULT_MODE);
		}

		static public void Broadcast(MessengerEventType eventType, T arg1, U arg2, V arg3, MessengerMode mode)
		{
			MessengerInternal.OnBroadcasting(eventType, mode);
			Delegate d;
			if (eventTable.TryGetValue(eventType, out d))
			{
				Action<T, U, V> callback = d as Action<T, U, V>;
				if (callback != null)
				{
					callback(arg1, arg2, arg3);
				}
				else
				{
					throw MessengerInternal.CreateBroadcastSignatureException(eventType);
				}
			}
		}
	}

	// Four parameters
	static public class Messenger<T, U, V, W>
	{
		private static Dictionary<MessengerEventType, Delegate> eventTable = MessengerInternal.eventTable;

		static public void AddListener(MessengerEventType eventType, Action<T, U, V, W> handler)
		{
			MessengerInternal.OnListenerAdding(eventType, handler);
			eventTable[eventType] = (Action<T, U, V, W>)eventTable[eventType] + handler;
		}

		static public void RemoveListener(MessengerEventType eventType, Action<T, U, V, W> handler)
		{
			MessengerInternal.OnListenerRemoving(eventType, handler);
			eventTable[eventType] = (Action<T, U, V, W>)eventTable[eventType] - handler;
			MessengerInternal.OnListenerRemoved(eventType);
		}

		static public void Broadcast(MessengerEventType eventType, T arg1, U arg2, V arg3, W arg4)
		{
			Broadcast(eventType, arg1, arg2, arg3, arg4, MessengerInternal.DEFAULT_MODE);
		}

		static public void Broadcast(MessengerEventType eventType, T arg1, U arg2, V arg3, W arg4,
			MessengerMode mode)
		{
			MessengerInternal.OnBroadcasting(eventType, mode);
			Delegate d;
			if (eventTable.TryGetValue(eventType, out d))
			{
				Action<T, U, V, W> callback = d as Action<T, U, V, W>;
				if (callback != null)
				{
					callback(arg1, arg2, arg3, arg4);
				}
				else
				{
					throw MessengerInternal.CreateBroadcastSignatureException(eventType);
				}
			}
		}
	}
}
// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEngine.Events;

namespace GGS_Framework
{
	public class UnityEventT<T> : UnityEvent<T> { }
	public class UnityEventT<T0, T1> : UnityEvent<T0, T1> { }
	public class UnityEventT<T0, T1, T2> : UnityEvent<T0, T1, T2> { }
	public class UnityEventT<T0, T1, T2, T3> : UnityEvent<T0, T1, T2, T3> { }
}
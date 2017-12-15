// =================================
//
//	Raycaster.cs
//	Created by Takuya Himeji
//
// =================================
using UnityEngine;
using System.Collections;

/// <summary>
/// Raycastを使いやすくまとめたクラスです。
/// </summary>
public class Raycaster : MonoBehaviour
{
	#region Member Field
	// private
	private bool		isHit = false;		// ヒット判定
	private RaycastHit	raycastHit;			// raycasthit情報

	// delegate
	public delegate void OnComplete(RaycastHit hit, bool isHit);	// delegate
	private OnComplete cb;	// コールバック
	#endregion // Member Field

	#region Properties
	/// <summary>
	/// Raycastがヒットしているかどうか
	/// </summary>
	public bool IsHit {
		get { return isHit; }
	}
	#endregion // Properties


	#region Member Methods
	/// <summary>
	/// 前方へレイを飛ばします。
	/// </summary>
	/// <param name="castPoint">起点となるオブジェクトです。</param>
	/// <param name="distance">レイを飛ばす距離です。</param>
	public void RaycastForward (Transform castPoint, float distance, OnComplete callback)
	{
		cb = callback;
		Cast(castPoint, distance, callback);
	}

	// @overload
	/// <summary>
	/// 前方へレイを飛ばします。
	/// </summary>
	/// <param name="castPoint">起点となるオブジェクトです。</param>
	/// <param name="distance">レイを飛ばす距離です。</param>
	/// <param name="targetLayerNames">レイキャストの対象とするレイヤー名を指定します。</param>
	/// <param name="callback">レイキャスト判定を得たときコールバックで結果を返します。</param>
	public void RaycastForward (Transform castPoint, float distance, string[] targetLayerNames, OnComplete callback)
	{
		cb = callback;
		int layerMask = LayerMask.GetMask (targetLayerNames);
		Cast(castPoint, distance, callback, layerMask);
	}

	// @overload
	/// <summary>
	/// 前方へレイを飛ばします。
	/// </summary>
	/// <param name="castPoint">起点となるオブジェクトです。</param>
	/// <param name="distance">レイを飛ばす距離です。</param>
	/// <param name="targetLayerName">レイキャストの対象とするレイヤー名を指定します。</param>
	/// <param name="callback">レイキャスト判定を得たときコールバックで結果を返します。</param>
	public void RaycastForward (Transform castPoint, float distance, string targetLayerName, OnComplete callback)
	{
		cb = callback;
		string[] names = new string[] {targetLayerName};
		// レイヤーマスク設定
		int layerMask = LayerMask.GetMask (names);
		Cast(castPoint, distance, callback, layerMask);
	}

	// @overload
	/// <summary>
	/// 前方へレイを飛ばします。
	/// </summary>
	/// <param name="castPoint">起点となるオブジェクトです。</param>
	/// <param name="distance">レイを飛ばす距離です。</param>
	/// <param name="targetLayerName">レイキャストの対象とするレイヤー名を指定します。</param>
	/// <param name="callback">レイキャスト判定を得たときコールバックで結果を返します。</param>
	public void RaycastForward (Transform castPoint, float distance, LayerMask layerMask, OnComplete callback)
	{
		cb = callback;
		Cast(castPoint, distance, callback, layerMask.value);
	}

	/// <summary>
	/// 足元へレイを飛ばします。
	/// </summary>
	/// <param name="castPoint">起点となるオブジェクトです。</param>
	/// <param name="distance">レイを飛ばす距離です。</param>
	public bool RaycastGrounded (Transform castPoint, float distance)
	{
		return Grounded(castPoint, distance);
	}

	// @overload
	/// <summary>
	/// 足元へレイを飛ばします。
	/// </summary>
	/// <param name="castPoint">起点となるオブジェクトです。</param>
	/// <param name="distance">レイを飛ばす距離です。</param>
	/// <param name="targetLayerNames">レイキャストの対象とするレイヤー名を指定します。</param>
	public bool RaycastGrounded (Transform castPoint, float distance, string[] targetLayerNames)
	{
		return Grounded(castPoint, distance, targetLayerNames);
	}

	// @overload
	/// <summary>
	/// 足元へレイを飛ばします。
	/// </summary>
	/// <param name="castPoint">起点となるオブジェクトです。</param>
	/// <param name="distance">レイを飛ばす距離です。</param>
	/// <param name="targetLayerName">レイキャストの対象とするレイヤー名を指定します。</param>
	public bool RaycastGrounded (Transform castPoint, float distance, string targetLayerName)
	{
		string[] names = new string[] {targetLayerName};
		return Grounded(castPoint, distance, names);
	}

	// レイキャスト処理
	private void Cast (Transform castPoint, float distance, OnComplete callback, int targetLayer = 0)
	{
		// レイキャスト開始
		Vector3		fwd		= castPoint.TransformDirection (Vector3.forward);
		RaycastHit	prevHit = raycastHit;
		if (Physics.Raycast (castPoint.position, fwd, out raycastHit, distance, targetLayer)) {
			isHit = true;						// ヒット
			callback (raycastHit, true);		// コールバック
		}
		else if (isHit) {
			isHit = false;						// ヒット終了
			callback (prevHit, false);			// コールバック
			raycastHit = default(RaycastHit);	// 初期化
		}
	}

	// レイキャスト処理
	private bool Grounded (Transform castPoint, float distance, string[] targetLayerNames = null)
	{
		// レイヤーマスク設定
		int layerMask = 0;
		if (targetLayerNames != null) {
			layerMask = LayerMask.GetMask (targetLayerNames);
		}
		bool tmpHit = false;
		// レイキャスト開始
		Vector3 down = castPoint.TransformDirection (Vector3.down);
		RaycastHit hit;
		if (Physics.Raycast  (castPoint.position, down, out hit, distance, layerMask)) {
			// ヒット
			tmpHit = true;
		}
		return tmpHit;
	}
	#endregion // Member Methods
}

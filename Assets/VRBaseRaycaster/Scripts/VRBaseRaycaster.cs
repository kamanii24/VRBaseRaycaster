// =================================
//
//	VRBaseRaycaster.cs
//	Created by Takuya Himeji
//
// =================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRBaseRaycaster : MonoBehaviour
{
	#region Inspector Settings
	[Header ("User Settings")]
	public Transform	playerHead; 				// 目線となるカメラ

	[Header ("General Settings")]
	public LayerMask	targetLayer;				// 注視対象になるレイヤー
	public bool			raycastEnabled		= true;	// Raycast有効
	public float		distance			= 50f;	// Raycastの有効距離
	public float		fixedProcessTime	= 1f;	// 注視完了時間
	public float		fixedActivateTime	= 0.5f;	// 注視が有効化されるまでの時間
	
	[SerializeField] private Transform	gazePointer;	// 注視ポインター
	[SerializeField] private Image		progress;		// プログレス
	#endregion // Inspector Settings


	#region Member Field
	private float 				processTime = 0f;
	private float				activateTime = 0f;
	private bool 				isProcess = false;
	private bool				isShowGazePointer = false;
	private Raycaster			mainRaycaster;
	private Raycaster			pointerRaycaster;
	private VRBaseEventTrigger	eventTrigger;
	private RaycastHit			prevHit;
	#endregion // Member Field


	#region MonoBehaviour Methods
	// 初期化処理
	private void Awake ()
	{
		// インスタンス化
		mainRaycaster		= new Raycaster ();
		pointerRaycaster 	= new Raycaster ();
	}

	// 開始処理
	private void Start ()
	{
		
	}
	
	// 更新処理
	private void Update ()
	{
		Gaze (); // 注視判定
	}
	#endregion // MonoBehaviour Methods


	#region Member Methods
	// 注視判定
	private void Gaze ()
	{
		// 注視処理
		if (raycastEnabled) {
			mainRaycaster.RaycastForward (playerHead, distance, targetLayer, OnGazeProcessing);
		}

		// 注視点の更新処理
		if (isShowGazePointer) {
			gazePointer.LookAt (playerHead);
			pointerRaycaster.RaycastForward (playerHead, distance, targetLayer, OnUpdateGazePointer);
		}
	}

	// 注視処理を実行する
	private void OnGazeProcessing (RaycastHit hit, bool isHit)
	{
		if (isHit) {
			// 衝突先が違うオブジェクトの場合
			if (eventTrigger != null && !hit.Equals(prevHit))
			{
                eventTrigger = null;
                // 初期化
                isProcess = false;
                progress.fillAmount = 0f;
                processTime = 0f;
                activateTime = 0f;
			}

			if (eventTrigger == null) {
				eventTrigger = hit.collider.GetComponent<VRBaseEventTrigger> ();
				// 注視点の表示
				ShowGazePointer ();
				
				// トリガーが設定されていない
				if (eventTrigger != null) {
					// OnEnterEvent fire.
					eventTrigger.OnEnter.Invoke ();
				}

				prevHit = hit;
			}
			
			if (eventTrigger != null) {
				// 注視が有効化される時間加算
				if (activateTime < fixedActivateTime) {
					activateTime += Time.deltaTime;
				}
				else {
					// OnHoverEvent fire.
					eventTrigger.OnHover.Invoke  ();

					// OnProcessにイベントが登録されている場合
					if (eventTrigger.OnProcess.GetPersistentEventCount () > 0) {
						// 注視更新処理
						if (processTime < fixedProcessTime) {
							processTime += Time.deltaTime;
							progress.fillAmount = processTime / fixedProcessTime;
						}
						else if (!isProcess) {
							// OnProcessEvent fire.
							eventTrigger.OnProcess.Invoke ();
							progress.fillAmount = 1f;
							isProcess = true;
						}
					}
				}
			}
		}
		else {
			if (eventTrigger != null) {
				// OnExitEvent fire.
				eventTrigger.OnExit.Invoke ();
				eventTrigger = null;
			}
			// 注視点の非表示
			HideGazePointer ();
			// 初期化
			isProcess			= false;
			progress.fillAmount = 0f;
			processTime			= 0f;
			activateTime		= 0f;
		}
	}

	// 注視点を更新する
	private void OnUpdateGazePointer (RaycastHit hit, bool isHit)
	{
		if (isHit) {
			gazePointer.position = hit.point;
			float scl = Vector3.Distance (playerHead.position, hit.point);
			gazePointer.localScale = new Vector3 (scl, scl, scl);
		}
	}

	// 注視点の表示
	private void ShowGazePointer ()
	{
		if (gazePointer != null) {
			isShowGazePointer = true;
			gazePointer.gameObject.SetActive (true);
		}
	}

	// 注視点を隠す
	private void HideGazePointer ()
	{
		if (gazePointer != null) {
			isShowGazePointer = false;
			gazePointer.gameObject.SetActive (false);
		}
	}
	#endregion // Member Methods
}
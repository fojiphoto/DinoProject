using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class ITweenMagic : MonoBehaviour
{

	// Use this for initialization

	//Space
	public enum MovementType
	{
		WorldSpace,
		UISpace}
	;

	[Tooltip ("Space in which tween will take place")]
	public MovementType ITweenSpace;

	//Ease Types

	public enum eastype
	{
		linear,
		spring,
		easeInQuad,
		easeOutQuad,
		easeInOutQuad,
		easeInCubic,
		easeOutCubic,
		easeInOutCubic,
		easeInQuart,
		easeOutQuart,
		easeInOutQuart,
		easeInQuint,
		easeOutQuint,
		easeInOutQuint,
		easeInSine,
		easeOutSine,
		easeInOutSine,
		easeInExpo,
		easeOutExpo,
		easeInOutExpo,
		easeInCirc,
		easeOutCirc,
		easeInOutCirc,
		easeInBounce,
		easeOutBounce,
		easeInOutBounce,
		easeInBack,
		easeOutBack,
		easeInOutBack,
		easeInElastic,
		easeOutElastic,
		easeInOutElastic,
	};

	[Tooltip ("tweeen easeType Movement")]
	public eastype EaseTypeMovement;
	//Time
	[Tooltip ("Time(sec) for Tween ")]
	public float timeMovement = 1;
	//Delay Time
	[Tooltip ("Delay Time(sec) for Tween to start ")]
	public float delayMovement;
	public LoopType LoopTypeMovement;
	public bool Movement;
	//For World
	public Vector3 initialPosition3D;
	public Vector3 targetPosition3D;
	//For UI
	public Vector2 initialPosition2D;
	public Vector2 targetPosition2D;
	public UnityEvent movementTweenCompleteEvent;

	public bool CanvasGroupFade;
	public float InitialAlpha;
	public float TargetAlpha;
	public eastype EaseTypeAlpha;
	public float timeAlpha;
	public float delayAlpha;
	public LoopType LoopTypeAlpha;
	public UnityEvent AlphaTweenCompletionEvent;


	[Tooltip ("tweeen easeType Rotation")]
	public eastype EaseTypeRotation;
	[Tooltip ("Time(sec) for Tween ")]
	public float timeRotation = 1;
	[Tooltip ("Delay Time(sec) for Tween to start ")]
	public float delayRotation;
	public LoopType LoopTypeRotation;
	public bool Rotation;
	public Vector3 initialRotation;
	public Vector3 targetRotation;
	public UnityEvent rotationTweenCompleteEvent;



	[Tooltip ("tweeen easeType Scale")]
	public eastype EaseTypeScale;
	[Tooltip ("Time(sec) for Tween ")]
	public float timeScale = 1;
	[Tooltip ("Delay Time(sec) for Tween to start ")]
	public float delayScale;
	public LoopType LoopTypeScale;
	public bool Scale;
	public Vector3 initialScale;
	public Vector3 targetScale;
	public UnityEvent scaleTweenCompleteEvent;



	[Tooltip ("Delay Time(sec) for Tween to start ")]
	public float delayShakeRotation;
	public bool ShakeRotation;
	[Tooltip ("Time(sec) for Tween ")]
	public float timeShakeRotation = 1;
	public float shakeRotationAmountX;
	public float shakeRotationAmountY;
	public float shakeRotationAmountZ;
	public UnityEvent shakeRotationTweenCompleteEvent;


	[Tooltip ("Delay Time(sec) for Tween to start ")]
	public float delayShakePosition;
	public bool ShakePosition;
	[Tooltip ("Time(sec) for Tween ")]
	public float timeShakePosition = 1;
	public float shakePositionAmountX;
	public float shakePositionAmountY;
	public float shakePositionAmountZ;
	public UnityEvent shakePositionTweenCompleteEvent;


	[Tooltip ("Delay Time(sec) for Tween to start ")]
	public float delayShakeScale;
	public bool ShakeScale;
	[Tooltip ("Time(sec) for Tween ")]
	public float timeShakeScale = 1;
	public float shakeScaleAmountX;
	public float shakeScaleAmountY;
	public float shakeScaleAmountZ;
	public UnityEvent shakeScaleTweenCompleteEvent;


	//Loop types
	public enum LoopType
	{
		none,
		pingpong,
		loop}
	;


	public bool playOnAwake = true;


	// Use this for initialization
	void OnEnable ()
	{
		if (!playOnAwake) {
			return;
		}


		if (ITweenSpace.Equals (MovementType.WorldSpace) || ITweenSpace.Equals (MovementType.UISpace)) {

			//Movement
			if (Movement) {		

				if (ITweenSpace.Equals (MovementType.UISpace)) {						
					PlayUIMovement ();
				} else {
					PlayWorldMovement ();
				}

			}	

			//Rotation
			if (Rotation) {
				PlayRotation ();
			}

			//Scale
			if (Scale) {
				PlayScale ();
			}
			if (CanvasGroupFade) {
				PlayAlpha ();
			}

			//Shake
			if (ShakeRotation) {
				PlayShakeRotation ();
			}
			if (ShakePosition) {
				PlayShakePosition ();
			}
			if (ShakeScale) {
				PlayShakeScale ();
			}		
		}

		//Destroy(gameObject);

	}


	//2D OnUpdate EventListners
	public void MoveGuiElement (Vector2 position)
	{
		GetComponent<RectTransform> ().anchoredPosition = position;
	}


	//3D Event Listners
	public void MoveObject (Vector3 position)
	{
		GetComponent<Transform> ().localPosition = position;
	}

	public void RotateObject (Vector3 rotation)
	{
		GetComponent<Transform> ().localEulerAngles = rotation;
	}

	public void ScaleObject (Vector3 scale)
	{
		GetComponent<Transform> ().localScale = scale;
	}

	public void UpdateAlpha (float alpha)
	{
		GetComponent<CanvasGroup> ().alpha = alpha;
	}

	#region MovementMethods

	public void PlayUIMovement ()
	{
		GetComponent<RectTransform> ().anchoredPosition = initialPosition2D;
		iTween.ValueTo (this.gameObject, iTween.Hash ("delay", delayMovement, "from", GetComponent<RectTransform> ().anchoredPosition, "to", targetPosition2D,
			"time", timeMovement, "looptype", LoopTypeMovement.ToString (), "onupdate", "MoveGuiElement", "oncomplete", "OnMovementTweenCompleted", "easetype", EaseTypeMovement.ToString ()));
	}

	public void PlayWorldMovement ()
	{
		GetComponent<Transform> ().localPosition = initialPosition3D;				
		iTween.ValueTo (this.gameObject, iTween.Hash ("delay", delayMovement, "from", GetComponent<Transform> ().localPosition, "to", targetPosition3D,
			"time", timeMovement, "looptype", LoopTypeMovement.ToString (), "oncomplete", "OnMovementTweenCompleted", "onupdate", "MoveObject", "easetype", EaseTypeMovement.ToString ()));
	}

	public void PlayScale ()
	{
		GetComponent<Transform> ().localScale = initialScale;
		iTween.ValueTo (this.gameObject, iTween.Hash ("delay", delayScale, "from", GetComponent<Transform> ().localScale, "to", targetScale, "time", timeScale,
			"looptype", LoopTypeScale.ToString (), "onupdate", "ScaleObject", "oncomplete", "OnScaleTweenCompleted", "easetype", EaseTypeScale.ToString ()));
	}

	public void PlayAlpha ()
	{
		var canvasGroup = GetComponent<CanvasGroup> ();
		if (canvasGroup == null)
			canvasGroup = gameObject.AddComponent<CanvasGroup> ();

		canvasGroup.alpha = InitialAlpha;
		iTween.ValueTo (this.gameObject, iTween.Hash ("delay", delayAlpha, "from", canvasGroup.alpha, "to", TargetAlpha, "time", timeAlpha,
			"looptype", LoopTypeAlpha.ToString (), "onupdate", "UpdateAlpha", "oncomplete", "OnAlphaTweenCompleted", "easetype", EaseTypeAlpha.ToString ()));
	}

	public void PlayRotation ()
	{
		GetComponent<Transform> ().localEulerAngles = initialRotation;
		iTween.ValueTo (this.gameObject, iTween.Hash ("delay", delayRotation, "from", GetComponent<Transform> ().localEulerAngles, "to", targetRotation, 
			"time", timeRotation, "looptype", LoopTypeRotation.ToString (), "onupdate", "RotateObject", "oncomplete", "OnRotationTweenCompleted", "easetype", EaseTypeRotation.ToString ()));
	}

	public void PlayShakeRotation ()
	{
		iTween.ShakeRotation (this.gameObject, iTween.Hash ("name", "ShakeRotationTween", "x", shakeRotationAmountX, "y", shakeRotationAmountY, "z", shakeRotationAmountZ, "time", timeShakeRotation, "delay", delayShakeRotation, "oncomplete", "OnShakeRotationTweenCompleted"));
	}

	public void PlayShakePosition ()
	{
		iTween.ShakePosition (this.gameObject, iTween.Hash ("name", "ShakePositionTween", "x", shakePositionAmountX, "y", shakePositionAmountY, "z", shakePositionAmountZ, "time", timeShakePosition, "delay", delayShakePosition, "oncomplete", "OnShakePositionTweenCompleted"));
	}

	public void PlayShakeScale ()
	{
		iTween.ShakeScale (this.gameObject, iTween.Hash ("name", "ShakeScaleTween", "x", shakeScaleAmountX, "y", shakeScaleAmountY, "z", shakeScaleAmountZ, "time", timeShakeScale, "delay", delayShakeScale, "oncomplete", "OnShakeScaleTweenCompleted"));
	}

	#endregion

	#region CodeMethods

	public void PlayForwardUIMovement ()
	{
		GetComponent<RectTransform> ().anchoredPosition = initialPosition2D;
		iTween.ValueTo (this.gameObject, iTween.Hash ("delay", delayMovement, "from", GetComponent<RectTransform> ().anchoredPosition, "to", targetPosition2D,
			"time", timeMovement, "looptype", LoopTypeMovement.ToString (), "onupdate", "MoveGuiElement", "oncomplete", "OnMovementTweenCompleted", "easetype", EaseTypeMovement.ToString ()));
	}

	public void PlayReverseUIMovement ()
	{
		GetComponent<RectTransform> ().anchoredPosition = targetPosition2D;
		iTween.ValueTo (this.gameObject, iTween.Hash ("delay", delayMovement, "from", GetComponent<RectTransform> ().anchoredPosition, "to", initialPosition2D,
			"time", timeMovement, "looptype", LoopTypeMovement.ToString (), "onupdate", "MoveGuiElement", "oncomplete", "OnMovementTweenCompleted", "easetype", EaseTypeMovement.ToString ()));
	}

	public void PlayForwardWorldMovement ()
	{
		GetComponent<Transform> ().localPosition = initialPosition3D;				
		iTween.ValueTo (this.gameObject, iTween.Hash ("delay", delayMovement, "from", GetComponent<Transform> ().localPosition, "to", targetPosition3D,
			"time", timeMovement, "looptype", LoopTypeMovement.ToString (), "oncomplete", "OnMovementTweenCompleted", "onupdate", "MoveObject", "easetype", EaseTypeMovement.ToString ()));
	}

	public void PlayForwardScale ()
	{
		GetComponent<Transform> ().localScale = initialScale;
		iTween.ValueTo (this.gameObject, iTween.Hash ("delay", delayScale, "from", GetComponent<Transform> ().localScale, "to", targetScale, "time", timeScale,
			"looptype", LoopType.none, "onupdate", "ScaleObject", "oncomplete", "OnScaleTweenCompleted", "easetype", EaseTypeScale.ToString ()));
	}

	public void PlayReverseScale ()
	{
		GetComponent<Transform> ().localScale = targetScale;
		iTween.ValueTo (this.gameObject, iTween.Hash ("delay", delayScale, "from", GetComponent<Transform> ().localScale, "to", initialScale, "time", timeScale,
			"looptype", LoopType.none, "onupdate", "ScaleObject", "oncomplete", "OnScaleTweenCompleted", "easetype", EaseTypeScale.ToString ()));
	}

	public void PlayForwardRotation ()
	{
		GetComponent<Transform> ().localEulerAngles = initialRotation;
		iTween.ValueTo (this.gameObject, iTween.Hash ("delay", delayRotation, "from", GetComponent<Transform> ().localEulerAngles, "to", targetRotation, 
			"time", timeRotation, "looptype", LoopTypeRotation.ToString (), "onupdate", "RotateObject", "oncomplete", "OnRotationTweenCompleted", "easetype", EaseTypeRotation.ToString ()));

	}

	public void PlayReverseRotation ()
	{
		GetComponent<Transform> ().localEulerAngles = targetRotation;
		iTween.ValueTo (this.gameObject, iTween.Hash ("delay", delayRotation, "from", GetComponent<Transform> ().localEulerAngles, "to", initialRotation,
			"time", timeRotation, "looptype", LoopTypeRotation.ToString (), "onupdate", "RotateObject", "oncomplete", "OnRotationTweenCompleted", "easetype", EaseTypeRotation.ToString ()));
	}


	public void StopAllItweensOnCurrentObject ()
	{
		iTween.Stop (gameObject);
	}

	public void StopShakeRotation ()
	{
		iTween.StopByName (this.gameObject, "ShakeRotationTween");
	}

	public void StopShakePosition ()
	{
		iTween.StopByName (this.gameObject, "ShakePositionTween");
	}

	public void StopShakeScale ()
	{
		iTween.StopByName (this.gameObject, "ShakeScaleTween");
	}


	#endregion

	#region Events


	void OnMovementTweenCompleted ()
	{
		if (movementTweenCompleteEvent != null) {
			movementTweenCompleteEvent.Invoke ();
		}
	}

	void OnRotationTweenCompleted ()
	{
		if (rotationTweenCompleteEvent != null) {
			rotationTweenCompleteEvent.Invoke ();
		}
	}

	void OnScaleTweenCompleted ()
	{
		if (scaleTweenCompleteEvent != null) {
			scaleTweenCompleteEvent.Invoke ();
		}
	}

	void OnAlphaTweenCompleted ()
	{
		if (AlphaTweenCompletionEvent != null) {
			AlphaTweenCompletionEvent.Invoke ();
		}
	}

	void OnShakeRotationTweenCompleted ()
	{
		StopShakeRotation ();

		if (shakeRotationTweenCompleteEvent != null) {
			shakeRotationTweenCompleteEvent.Invoke ();
		}
	}

	void OnShakePositionTweenCompleted ()
	{
		StopShakePosition ();

		if (shakePositionTweenCompleteEvent != null) {
			shakePositionTweenCompleteEvent.Invoke ();
		}
	}

	void OnShakeScaleTweenCompleted ()
	{
		StopShakeScale ();

		if (shakeScaleTweenCompleteEvent != null) {
			shakeScaleTweenCompleteEvent.Invoke ();
		}
	}


	#endregion
}




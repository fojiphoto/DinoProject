

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.IO;



public class CustomizeInspector : Editor
{

}

#region ItweenMagicInspector
[CustomEditor (typeof(ITweenMagic)), CanEditMultipleObjects]
public class ItweenMagicInspector : Editor
{
	#if UNITY_EDITOR
	#region properties

	public SerializedProperty 
		MovementType,
		Movement,
		TargetPosition3D,
		InitialPosition3D,
		TargetPosition2D,
		InitialPosition2D,
		EaseTypeMovement,
		timeMovement,
		delayMovement,
		LoopTypeMovement,
		MovementTweenCompletionEvent,


		Rotation,
		InitialRotation,
		TargetRotation,
		EaseTypeRotation,
		timeRotation,
		delayRotation,
		LoopTypeRotation,
		RotationTweenCompletionEvent,

		CanvasGroupFade,
		InitialAlpha,
		TargetAlpha,
		EaseTypeAlpha,
		timeAlpha,
		delayAlpha,
		LoopTypeAlpha,
		AlphaTweenCompletionEvent,

		Scale,
		TargetScale,
		InitialScale,
		EaseTypeScale,
		timeScale,
		delayScale,
		LoopTypeScale,
		ScaleTweenCompletionEvent,


		ShakeRotation,
		timeShakeRotation,
		delayShakeRotation,
		shakeRotationAmountX,
		shakeRotationAmountY,
		shakeRotationAmountZ,
		ShakeRotationTweenCompleteEvent,


		ShakePosition,
		timeShakePosition,
		delayShakePosition,
		shakePositionAmountX,
		shakePositionAmountY,
		shakePositionAmountZ,
		ShakePositionTweenCompleteEvent,


		ShakeScale,
		timeShakeScale,
		delayShakeScale,
		shakeScaleAmountX,
		shakeScaleAmountY,
		shakeScaleAmountZ,
		ShakeScaleTweenCompleteEvent,


		PlayOnAwake;

	#endregion

	void OnEnable ()
	{
		// Setup the SerializedProperties
		MovementType = serializedObject.FindProperty ("ITweenSpace");
		Movement = serializedObject.FindProperty ("Movement");
		InitialPosition3D = serializedObject.FindProperty ("initialPosition3D");
		TargetPosition3D = serializedObject.FindProperty ("targetPosition3D");
		InitialPosition2D = serializedObject.FindProperty ("initialPosition2D");
		TargetPosition2D = serializedObject.FindProperty ("targetPosition2D");
		EaseTypeMovement = serializedObject.FindProperty ("EaseTypeMovement");
		timeMovement = serializedObject.FindProperty ("timeMovement");
		delayMovement = serializedObject.FindProperty ("delayMovement");
		LoopTypeMovement = serializedObject.FindProperty ("LoopTypeMovement");
		MovementTweenCompletionEvent = serializedObject.FindProperty ("movementTweenCompleteEvent");

		Rotation = serializedObject.FindProperty ("Rotation");
		InitialRotation = serializedObject.FindProperty ("initialRotation");
		TargetRotation = serializedObject.FindProperty ("targetRotation");
		EaseTypeRotation = serializedObject.FindProperty ("EaseTypeRotation");
		timeRotation = serializedObject.FindProperty ("timeRotation");
		delayRotation = serializedObject.FindProperty ("delayRotation");
		LoopTypeRotation = serializedObject.FindProperty ("LoopTypeRotation");
		RotationTweenCompletionEvent = serializedObject.FindProperty ("rotationTweenCompleteEvent");

		CanvasGroupFade = serializedObject.FindProperty ("CanvasGroupFade");
		InitialAlpha = serializedObject.FindProperty ("InitialAlpha");
		TargetAlpha = serializedObject.FindProperty ("TargetAlpha");
		EaseTypeAlpha = serializedObject.FindProperty ("EaseTypeAlpha");
		timeAlpha = serializedObject.FindProperty ("timeAlpha");
		delayAlpha = serializedObject.FindProperty ("delayAlpha");
		LoopTypeAlpha = serializedObject.FindProperty ("LoopTypeAlpha");
		AlphaTweenCompletionEvent = serializedObject.FindProperty ("AlphaTweenCompletionEvent");

		Scale = serializedObject.FindProperty ("Scale");
		InitialScale = serializedObject.FindProperty ("initialScale");
		TargetScale = serializedObject.FindProperty ("targetScale");
		EaseTypeScale = serializedObject.FindProperty ("EaseTypeScale");
		timeScale = serializedObject.FindProperty ("timeScale");
		delayScale = serializedObject.FindProperty ("delayScale");
		LoopTypeScale = serializedObject.FindProperty ("LoopTypeScale");
		ScaleTweenCompletionEvent = serializedObject.FindProperty ("scaleTweenCompleteEvent");


		ShakeRotation = serializedObject.FindProperty ("ShakeRotation");
		timeShakeRotation = serializedObject.FindProperty ("timeShakeRotation");
		delayShakeRotation = serializedObject.FindProperty ("delayShakeRotation");
		shakeRotationAmountX = serializedObject.FindProperty ("shakeRotationAmountX");
		shakeRotationAmountY = serializedObject.FindProperty ("shakeRotationAmountY");
		shakeRotationAmountZ = serializedObject.FindProperty ("shakeRotationAmountZ");
		ShakeRotationTweenCompleteEvent = serializedObject.FindProperty ("shakeRotationTweenCompleteEvent");


		ShakePosition = serializedObject.FindProperty ("ShakePosition");
		timeShakePosition = serializedObject.FindProperty ("timeShakePosition");
		delayShakePosition = serializedObject.FindProperty ("delayShakePosition");
		shakePositionAmountX = serializedObject.FindProperty ("shakePositionAmountX");
		shakePositionAmountY = serializedObject.FindProperty ("shakePositionAmountY");
		shakePositionAmountZ = serializedObject.FindProperty ("shakePositionAmountZ");
		ShakePositionTweenCompleteEvent = serializedObject.FindProperty ("shakePositionTweenCompleteEvent");


		ShakeScale = serializedObject.FindProperty ("ShakeScale");
		timeShakeScale = serializedObject.FindProperty ("timeShakeScale");
		delayShakeScale = serializedObject.FindProperty ("delayShakeScale");
		shakeScaleAmountX = serializedObject.FindProperty ("shakeScaleAmountX");
		shakeScaleAmountY = serializedObject.FindProperty ("shakeScaleAmountY");
		shakeScaleAmountZ = serializedObject.FindProperty ("shakeScaleAmountZ");
		ShakeScaleTweenCompleteEvent = serializedObject.FindProperty ("shakeScaleTweenCompleteEvent");


		PlayOnAwake = serializedObject.FindProperty ("playOnAwake");
	}

	ITweenMagic _iTweenMagic;

	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();
		EditorGUILayout.PropertyField (MovementType);


		GUILayout.Space (10);
		EditorGUILayout.PropertyField (Movement);

		_iTweenMagic = (ITweenMagic)target;
		if (Movement.boolValue && MovementType.enumValueIndex == 0) {
			GUILayout.BeginVertical ("box");	
			EditorGUILayout.PropertyField (LoopTypeMovement);
			EditorGUILayout.PropertyField (EaseTypeMovement);
			InitialPosition3DFunction ();
			TargetPosition3DFunction ();
			//EditorGUILayout.PropertyField(TargetPosition3D);
			EditorGUILayout.PropertyField (timeMovement);
			EditorGUILayout.PropertyField (delayMovement);
			EditorGUILayout.PropertyField (MovementTweenCompletionEvent);
			GUILayout.EndVertical ();

		} else if (Movement.boolValue && MovementType.enumValueIndex == 1) {
			GUILayout.BeginVertical ("box");	
			EditorGUILayout.PropertyField (LoopTypeMovement);
			EditorGUILayout.PropertyField (EaseTypeMovement);
			InitialPosition2DFunction ();
			TargetPosition2DFunction ();
			EditorGUILayout.PropertyField (timeMovement);
			EditorGUILayout.PropertyField (delayMovement);
			EditorGUILayout.PropertyField (MovementTweenCompletionEvent);
//			GUILayout.Space(20);
			GUILayout.EndVertical ();
		}

		EditorGUILayout.PropertyField (Rotation);
		if (Rotation.boolValue) {
			GUILayout.BeginVertical ("box");	
			EditorGUILayout.PropertyField (LoopTypeRotation);
			EditorGUILayout.PropertyField (EaseTypeRotation);
			InitialRotationFunction ();
			TargetRotationFunction ();
			EditorGUILayout.PropertyField (timeRotation);
			EditorGUILayout.PropertyField (delayRotation);
			EditorGUILayout.PropertyField (RotationTweenCompletionEvent);
			GUILayout.EndVertical ();
		}
	
		EditorGUILayout.PropertyField (CanvasGroupFade);
		if (CanvasGroupFade.boolValue) {
			GUILayout.BeginVertical ("box");	
			EditorGUILayout.PropertyField (LoopTypeAlpha);
			EditorGUILayout.PropertyField (EaseTypeAlpha);

			EditorGUILayout.PropertyField (InitialAlpha);
			EditorGUILayout.PropertyField (TargetAlpha);
			EditorGUILayout.PropertyField (timeAlpha);
			EditorGUILayout.PropertyField (delayAlpha);
			EditorGUILayout.PropertyField (AlphaTweenCompletionEvent);
			GUILayout.EndVertical ();
		}

		EditorGUILayout.PropertyField (Scale);
	
		if (Scale.boolValue) {
			GUILayout.BeginVertical ("box");
			EditorGUILayout.PropertyField (LoopTypeScale);
			EditorGUILayout.PropertyField (EaseTypeScale);
			InitialScaleFunction ();
			TargetScaleFunction ();
			EditorGUILayout.PropertyField (timeScale);
			EditorGUILayout.PropertyField (delayScale);
			EditorGUILayout.PropertyField (ScaleTweenCompletionEvent);
			GUILayout.EndVertical ();
		}


		EditorGUILayout.PropertyField (ShakeRotation);
		if (ShakeRotation.boolValue) {
			GUILayout.BeginVertical ("box");
			EditorGUILayout.PropertyField (shakeRotationAmountX);
			EditorGUILayout.PropertyField (shakeRotationAmountY);
			EditorGUILayout.PropertyField (shakeRotationAmountZ);
			EditorGUILayout.PropertyField (timeShakeRotation);
			EditorGUILayout.PropertyField (delayShakeRotation);
			EditorGUILayout.PropertyField (ShakeRotationTweenCompleteEvent);
			GUILayout.EndVertical ();
		}


		EditorGUILayout.PropertyField (ShakePosition);
		if (ShakePosition.boolValue) {
			GUILayout.BeginVertical ("box");
			EditorGUILayout.PropertyField (shakePositionAmountX);
			EditorGUILayout.PropertyField (shakePositionAmountY);
			EditorGUILayout.PropertyField (shakePositionAmountZ);
			EditorGUILayout.PropertyField (timeShakePosition);
			EditorGUILayout.PropertyField (delayShakePosition);
			EditorGUILayout.PropertyField (ShakePositionTweenCompleteEvent);
			GUILayout.EndVertical ();
		}


		EditorGUILayout.PropertyField (ShakeScale);
		if (ShakeScale.boolValue) {
			GUILayout.BeginVertical ("box");
			EditorGUILayout.PropertyField (shakeScaleAmountX);
			EditorGUILayout.PropertyField (shakeScaleAmountY);
			EditorGUILayout.PropertyField (shakeScaleAmountZ);
			EditorGUILayout.PropertyField (timeShakeScale);
			EditorGUILayout.PropertyField (delayShakeScale);
			EditorGUILayout.PropertyField (ShakeScaleTweenCompleteEvent);
			GUILayout.EndVertical ();
		}


		EditorGUILayout.PropertyField (PlayOnAwake);

		serializedObject.ApplyModifiedProperties ();
	}

	#region MovementFunctions

	void TargetPosition2DFunction ()
	{
		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (TargetPosition2D);
		if (GUILayout.Button ("CurrentPosition")) {
			_iTweenMagic.targetPosition2D = _iTweenMagic.GetComponent<RectTransform> ().anchoredPosition;
		} else if (GUILayout.Button ("Reset")) {
			_iTweenMagic.targetPosition2D = Vector2.zero;
		}
		GUILayout.EndHorizontal ();
	}

	void InitialPosition2DFunction ()
	{
		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (InitialPosition2D);
		if (GUILayout.Button ("CurrentPosition")) {
			_iTweenMagic.initialPosition2D = _iTweenMagic.GetComponent<RectTransform> ().anchoredPosition;
		} else if (GUILayout.Button ("Reset")) {
			_iTweenMagic.initialPosition2D = Vector2.zero;
		}
		GUILayout.EndHorizontal ();
	}


	void TargetPosition3DFunction ()
	{
		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (TargetPosition3D);
		if (GUILayout.Button ("CurrentPosition")) {
			_iTweenMagic.targetPosition3D = _iTweenMagic.GetComponent<Transform> ().localPosition;
		} else if (GUILayout.Button ("Reset")) {
			_iTweenMagic.targetPosition3D = Vector3.zero;
		}
		GUILayout.EndHorizontal ();
	}

	void InitialPosition3DFunction ()
	{
		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (InitialPosition3D);
		if (GUILayout.Button ("CurrentPosition")) {
			_iTweenMagic.initialPosition3D = _iTweenMagic.GetComponent<Transform> ().localPosition;
		} else if (GUILayout.Button ("Reset")) {
			_iTweenMagic.initialPosition3D = Vector3.zero;
		}
		GUILayout.EndHorizontal ();
	}

	#endregion

	#region ScaleFunctions

	void InitialScaleFunction ()
	{
		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (InitialScale);

		if (GUILayout.Button ("CurrentScale")) {
			_iTweenMagic.initialScale = _iTweenMagic.GetComponent<Transform> ().localScale;
		} else if (GUILayout.Button ("Reset")) {
			_iTweenMagic.initialScale = Vector3.zero;
		}
		GUILayout.EndHorizontal ();
	}

	void TargetScaleFunction ()
	{
		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (TargetScale);

		if (GUILayout.Button ("CurrentScale")) {
			_iTweenMagic.targetScale = _iTweenMagic.GetComponent<Transform> ().localScale;
		} else if (GUILayout.Button ("Reset")) {
			_iTweenMagic.targetScale = Vector3.zero;
		}
		GUILayout.EndHorizontal ();
	}

	#endregion

	#region RotationFunctions

	void InitialRotationFunction ()
	{
		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (InitialRotation);

		if (GUILayout.Button ("CurrentRotation")) {
			_iTweenMagic.initialRotation = _iTweenMagic.GetComponent<Transform> ().localEulerAngles;
		} else if (GUILayout.Button ("Reset")) {
			_iTweenMagic.initialRotation = Vector3.zero;
		}
		GUILayout.EndHorizontal ();
	}

	void TargetRotationFunction ()
	{
		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (TargetRotation);

		if (GUILayout.Button ("CurrentRotation")) {
			_iTweenMagic.targetRotation = _iTweenMagic.GetComponent<Transform> ().localEulerAngles;
		} else if (GUILayout.Button ("Reset")) {
			_iTweenMagic.targetRotation = Vector3.zero;
		}
		GUILayout.EndHorizontal ();
	}

	#endregion

	#region ShakeFunction

	void ShakeFunction ()
	{
		
	}

	#endregion

	#endif
}
#endregion

#region GameGizmosInspector
[CustomEditor (typeof(GameGizmos)), CanEditMultipleObjects]
public class GameGizmosInspector:Editor
{
	#if UNITY_EDITOR
	GameGizmos _gameGizmos;
	public SerializedProperty 
		ChildName,
		StartingNumber,
		Radius,
		SizeForWiredBox;

	void OnEnable ()
	{
		// Setup the SerializedProperties
		ChildName = serializedObject.FindProperty ("childName");
		StartingNumber = serializedObject.FindProperty ("startingNumber");
		Radius = serializedObject.FindProperty ("radius");
		SizeForWiredBox = serializedObject.FindProperty ("size");
		_gameGizmos = (GameGizmos)target;
	}


	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();


		GUILayout.Space (10);
		GUILayout.BeginVertical ("box");	
		GUILayout.Label ("Gizmos");
		GUILayout.Space (5);
		GUILayout.BeginHorizontal ("box");	
		DrawPathChilds ();
		DrawFilledSphereOnChilds ();
		GUILayout.EndHorizontal ();
		GUILayout.Space (5);
		GUILayout.BeginHorizontal ("box");	
		DrawEmptySphereOnSelf ();
		DrawFilledSphereOnSelf ();
		GUILayout.EndHorizontal ();
		GUILayout.Space (5);
		GUILayout.BeginHorizontal ("box");
		DrawWiredBoxOnSelf ();
		DrawIconsOnChilds ();
		GUILayout.EndHorizontal ();
		GUILayout.Space (5);
		RadiusAndSize ();
		GUILayout.EndVertical ();

		GUILayout.Space (10);
		GUILayout.BeginVertical ("box");	
		GUILayout.Label ("Functions");
		GUILayout.Space (5);
		GUILayout.BeginHorizontal ("box");	

		EditorGUILayout.PropertyField (ChildName);
		_gameGizmos.childName = ChildName.stringValue;

		EditorGUILayout.PropertyField (StartingNumber);
		_gameGizmos.startingNumber = StartingNumber.intValue;

		GUILayout.EndHorizontal ();
		GUILayout.Space (5);
		GUILayout.BeginHorizontal ("box");	
		if (GUILayout.Button ("Rename Childs")) {
			_gameGizmos.renameChildsNumerically = true;
			_gameGizmos.Update ();
		}

		if (GUILayout.Button ("Reverse All Childs")) {
			_gameGizmos.reverseAllChilds = true;
			_gameGizmos.Update ();
		}
		GUILayout.EndHorizontal ();

		GUILayout.EndVertical ();

	}

	#region Radius Size

	void RadiusAndSize ()
	{
		GUILayout.BeginHorizontal ("box");
		EditorGUILayout.PropertyField (Radius, new GUIContent ("Radius for Sphere"));
		_gameGizmos.radius = Radius.floatValue;
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ("box");
		EditorGUILayout.PropertyField (SizeForWiredBox, new GUIContent ("Size For Wired Box"));
		_gameGizmos.size = SizeForWiredBox.vector3Value;
		GUILayout.EndHorizontal ();
	}

	#endregion

	#region DrawPathChilds

	bool DrawPathChildsToggle;
	string DrawPathChildsName = "Draw Path on Childs";

	void DrawPathChilds ()
	{
		if (GUILayout.Button (DrawPathChildsName)) {
			DrawPathChildsToggle = !DrawPathChildsToggle;
			if (DrawPathChildsToggle) {
				_gameGizmos.drawPathOnChilds = true;
				DrawPathChildsName = "Remove Path on Childs";
			} else {
				_gameGizmos.drawPathOnChilds = false;
				DrawPathChildsName = "Draw Path on Childs";
			}
			EnableDisableFunctionDrawGizmos ();
		}
	}

	#endregion

	#region DrawFilledSphereOnChilds

	bool DrawFilledSphereChildsToggle;
	string DrawFilledSphereChildsName = "Draw Filled Sphere on Childs";

	void DrawFilledSphereOnChilds ()
	{
		if (GUILayout.Button (DrawFilledSphereChildsName)) {
			DrawFilledSphereChildsToggle = !DrawFilledSphereChildsToggle;
			if (DrawFilledSphereChildsToggle) {
				_gameGizmos.drawFilledSphereOnChilds = true;
				DrawFilledSphereChildsName = "Remove Filled Sphere on Childs";
			} else {
				_gameGizmos.drawFilledSphereOnChilds = false;
				DrawFilledSphereChildsName = "Draw Filled Sphere on Childs";
			}
			EnableDisableFunctionDrawGizmos ();
		}
	}

	#endregion

	#region DrawEmptySphereOnSelf

	bool DrawEmptySphereOnSelfToggle;
	string DrawEmptySphereOnSelfName = "Draw Empty Sphere on Self";

	void DrawEmptySphereOnSelf ()
	{
		if (GUILayout.Button (DrawEmptySphereOnSelfName)) {
			DrawEmptySphereOnSelfToggle = !DrawEmptySphereOnSelfToggle;
			if (DrawEmptySphereOnSelfToggle) {
				_gameGizmos.drawEmptySphereOnSelf = true;
				DrawEmptySphereOnSelfName = "Remove Empty Sphere on Self";
			} else {
				_gameGizmos.drawEmptySphereOnSelf = false;
				DrawEmptySphereOnSelfName = "Draw Empty Sphere on Self";
			}
			EnableDisableFunctionDrawGizmos ();
		}
	}

	#endregion

	#region DrawFilledSphereOnSelf

	bool DrawFilledSphereOnSelfToggle;
	string DrawFilledSphereOnSelfName = "Draw Filled Sphere on Self";

	void DrawFilledSphereOnSelf ()
	{
		if (GUILayout.Button (DrawFilledSphereOnSelfName)) {
			DrawFilledSphereOnSelfToggle = !DrawFilledSphereOnSelfToggle;
			if (DrawFilledSphereOnSelfToggle) {
				_gameGizmos.drawFilledSphereOnSelf = true;
				DrawFilledSphereOnSelfName = "Remove Filled Sphere on Self";
			} else {
				_gameGizmos.drawFilledSphereOnSelf = false;
				DrawFilledSphereOnSelfName = "Draw Filled Sphere on Self";
			}
			EnableDisableFunctionDrawGizmos ();
		}
	}

	#endregion

	#region DrawWiredBoxOnSelf

	bool DrawWiredBoxOnSelfToggle;
	string DrawWiredBoxOnSelfName = "Draw Wired Box on Self";

	void DrawWiredBoxOnSelf ()
	{
		if (GUILayout.Button (DrawWiredBoxOnSelfName)) {
			DrawWiredBoxOnSelfToggle = !DrawWiredBoxOnSelfToggle;
			if (DrawWiredBoxOnSelfToggle) {
				_gameGizmos.drawBoxonSelf = true;
				DrawWiredBoxOnSelfName = "Remove Wired Box on Self";
			} else {
				_gameGizmos.drawBoxonSelf = false;
				DrawWiredBoxOnSelfName = "Draw Wired Box on Self";
			}
			EnableDisableFunctionDrawGizmos ();
		}
	}

	#endregion

	#region DrawIcons

	bool DrawIconsOnChildsToggle;
	string DrawIconsOnChildsName = "Draw Icons On Childs";

	void DrawIconsOnChilds ()
	{
		if (GUILayout.Button (DrawIconsOnChildsName)) {
			DrawIconsOnChildsToggle = !DrawIconsOnChildsToggle;
			if (DrawIconsOnChildsToggle) {
				_gameGizmos.drawIconsOnChilds = true;
				DrawIconsOnChildsName = "Remove Icons On Childs";
			} else {
				_gameGizmos.drawIconsOnChilds = false;
				DrawIconsOnChildsName = "Draw Icons On Childs";
			}
			_gameGizmos.Update ();
		}
	}

	#endregion

	void EnableDisableFunctionDrawGizmos ()
	{
		_gameGizmos.enabled = false;
		_gameGizmos.enabled = true;
	}
	#endif
}
#endregion


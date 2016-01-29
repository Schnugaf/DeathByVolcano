using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BearsBagOfTricks : ScriptableObject
{
	private static Vector3 copiedLocalPosition;
	private static Quaternion copiedLocalRotation;
	private static Vector3 copiedLocalScale;

	private static Vector3 copiedWorldPosition;
	private static Quaternion copiedWorldRotation;
	private static Vector3 copiedWorldScale;

	private static Vector3 roundedTransform;

	// Hotkey characters
	// & = ALT
	// % = CTRL
	// # = SHIFT

	//--------------

	// Generic function for rounding Vector3s: 3.14 -> 3
	public static Vector3 RoundVector3(Vector3 inputTransform)
	{
		return new Vector3(Mathf.Round(inputTransform[0]), Mathf.Round(inputTransform[1]), Mathf.Round(inputTransform[2]));
	}

	public static void RoundPositionCustom(float snap, GameObject obj)
	{
		Undo.RecordObjects(Selection.transforms, "Snap To Grid (Custom)");

			if (snap < 0.01f)
			{
				Debug.Log("Snapper was less than 0.01, setting it to 1.0.");
				snap = 1.0f;
			}
			snap = 1 / snap;
			Vector3 value = obj.transform.localPosition * snap;
			obj.transform.localPosition = RoundVector3(value) / snap;
	}

	//-------------------------------

	[MenuItem("GameObject/Apply Changes To Prefab %Q")]
	public static void ApplyPrefab()
	{
		if (PrefabUtility.GetPrefabType(Selection.activeGameObject) == PrefabType.PrefabInstance || PrefabUtility.GetPrefabType(Selection.activeGameObject) == PrefabType.DisconnectedPrefabInstance)
		{
			EditorApplication.ExecuteMenuItem("GameObject/Apply Changes To Prefab");
		}
		else
		{
			EditorApplication.Beep();
			Debug.LogError("Selection is not a prefab!");
		}
	}

	[MenuItem("GameObject/Revert Selected Prefabs %#&Q")]
	public static void RevertPrefab()
	{
		// TODO: Check what this should be.
		//Undo.RegisterSceneUndo("Revert selected prefabs");

		if (Selection.gameObjects.Length > 0)
		{
			foreach (GameObject obj in Selection.gameObjects)
			{
				PrefabUtility.RevertPrefabInstance(obj);
			}
		}
		else
		{
			EditorApplication.Beep();
			Debug.LogError("Cannot revert prefabs - nothing selected");
		}
	}

	[MenuItem("GameObject/Clear Parent &P")]
	public static void BreakParentHotkey()
	{
		if (!Selection.activeGameObject.transform.parent)
		{
			EditorApplication.Beep();
			Debug.LogError("Selection has no parent!");
		}
		else
		{
			EditorApplication.ExecuteMenuItem("GameObject/Clear Parent");
		}
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/New Child &N")]
	public static void CreateNewChild()
	{
		Transform newChild = new GameObject("New Child").transform;
		if (Selection.activeGameObject)
		{
			newChild.parent = Selection.activeGameObject.transform;
		}
		else
		{
			Debug.Log("Child does not have a parent :(  (New Child gameobject created in root)");
		}
		newChild.localPosition = Vector3.zero;
		newChild.localRotation = Quaternion.identity;
		newChild.localScale = Vector3.one;
		Undo.RegisterCreatedObjectUndo(newChild.gameObject, "Create Child");
		Selection.activeGameObject = newChild.gameObject;
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Move selection step up in the hierarchy &V")]
	public static void HierarchyMoveUp()
	{
		foreach (var t in Selection.transforms)
		{
			if (t.parent != null)
			{
				Undo.SetTransformParent(t, t.parent.parent, "Move up in hierarchy");
			}
			else return;
		}
	}

	public static string AddFloatSignThing(string input)
	{
		return input.Replace(",", "f,").Replace(")", "f)");
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Copy Transform &%c")]
	public static void CopyTransformToClipboard(string transformType = "Location", bool worldSpace = false, bool formatFloatValues = true)
	{
		string output = null;

		if (transformType == "Location" && !worldSpace)
		{
			output = Selection.activeTransform.localPosition.ToString("F4");
		}
		if (transformType == "Location" && worldSpace)
		{
			output = Selection.activeTransform.position.ToString("F4");
		}
		if (transformType == "Rotation" && !worldSpace)
		{
			output = Selection.activeTransform.localRotation.eulerAngles.ToString("F4");
		}
		if (transformType == "Rotation" && worldSpace)
		{
			output = Selection.activeTransform.rotation.eulerAngles.ToString("F4");
		}
		if (transformType == "Scale" && !worldSpace)
		{
			output = Selection.activeTransform.localScale.ToString("F4");
		}
		if (transformType == "Scale" && worldSpace)
		{
			output = Selection.activeTransform.lossyScale.ToString("F4");
		}
		
		if (formatFloatValues)
		{
			output = AddFloatSignThing(output);
		}

		EditorGUIUtility.systemCopyBuffer = output;

		Debug.Log("Copied values clipboard: " + output);
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Copy Transform &%c")]
	public static void CopyTransform()
	{
		copiedLocalPosition = Selection.activeTransform.localPosition;
		copiedLocalRotation = Selection.activeTransform.localRotation;
		copiedLocalScale = Selection.activeTransform.localScale;

		copiedWorldPosition = Selection.activeTransform.position;
		copiedWorldRotation = Selection.activeTransform.rotation;
		copiedWorldScale = Selection.activeTransform.localScale;
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Paste Transform (Local space) &%v")]
	public static void PasteTransformLocal()
	{
		foreach (Transform transform in Selection.transforms)
		{
			Undo.RecordObject(transform, "Paste Local Transform");
			transform.localPosition = copiedLocalPosition;
			transform.localRotation = copiedLocalRotation;
			transform.localScale = copiedLocalScale;
		}
	}

	public static void PastePositionLocal()
	{
		foreach (Transform transform in Selection.transforms)
		{
			Undo.RecordObject(transform, "Paste Local Transform");
			transform.localPosition = copiedLocalPosition;
		}
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Paste Transform (World space) &%#v")]
	public static void PasteTransformGlobal()
	{
		foreach (Transform transform in Selection.transforms)
		{
			Undo.RecordObject(transform, "Paste Global Transform");
			transform.position = copiedWorldPosition;
			transform.rotation = copiedWorldRotation;
			transform.localScale = copiedWorldScale;
		}
	}

	public static void PastePositionGlobal()
	{
		foreach (Transform transform in Selection.transforms)
		{
			Undo.RecordObject(transform, "Paste Global Transform");
			transform.position = copiedWorldPosition;
		}
	}


	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Reset/Position &g")]
	public static void ResetPosition()
	{
		Undo.RecordObjects(Selection.transforms, "Reset Position");
		foreach (Transform transform in Selection.transforms)
		{
			transform.localPosition = new Vector3(0, 0, 0);
		}
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Reset/Rotation &#r")]
	public static void ResetRotation()
	{
		Undo.RecordObjects(Selection.transforms, "Reset Rotation");
		foreach (Transform transform in Selection.transforms)
		{
			transform.localRotation = Quaternion.identity;
		}
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Reset/Rotation X-90 (Blender) &r")]
	public static void ResetRotationBlender()
	{
		Undo.RecordObjects(Selection.transforms, "Reset Rotation X-90 (Blender)");
		foreach (Transform transform in Selection.transforms)
		{
			transform.localRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
		}
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Reset/Rotation X90 (Concept) %&r")]
	public static void ResetRotationConcept()
	{
		Undo.RecordObjects(Selection.transforms, "Reset Rotation X90 (Concept)");
		foreach (Transform transform in Selection.transforms)
		{
			transform.localRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
		}
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Reset/Scale &s")]
	public static void ResetScale()
	{
		Undo.RecordObjects(Selection.transforms, "Reset Scale");

		foreach (Transform transform in Selection.transforms)
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Snap/Position To Grid (World)")]
	public static void RoundPosition()
	{
		Undo.RecordObjects(Selection.transforms, "Snap To Grid (World)");

		foreach (GameObject obj in Selection.gameObjects)
		{
			obj.transform.position = RoundVector3(obj.transform.position);
		}
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Snap/Position To Grid (Custom value) &d")]
	static void DoRoundPositionHotkey()
	{
		Undo.RecordObjects(Selection.gameObjects, "Snap Position To Grid (Custom value)");

		foreach (GameObject obj in Selection.gameObjects)
		{
			RoundPositionCustom(BearsBagOfButtons.snapper, obj);
		}
	}


	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Snap/Position To Grid (Local) #&d")]
	public static void RoundPositionLocal()
	{
		Undo.RecordObjects(Selection.transforms, "Snap To Grid (Local)");

		foreach (GameObject obj in Selection.gameObjects)
		{
			obj.transform.localPosition = RoundVector3(obj.transform.localPosition);
		}
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Select/Siblings with the same name %&Z")]
	public static void SelectSiblingsWithSameName()
	{
		List<Object> newSelection = new List<Object>();
		Transform[] selection = Selection.transforms;

		foreach (Transform go in selection)
		{

			if (go.parent != null)
			{
				foreach (Transform sibling in go.parent)
				{
					if (sibling.name == go.name)
					{
						newSelection.Add(sibling.gameObject);
					}
				}
			}
			else
			{
				Debug.Log("Doesn't work on top level selections at the moment. Manual selection needed. Don't be lazy.");
			}
		}
		Undo.RecordObjects(Selection.objects, "Selection");
		Selection.objects = newSelection.ToArray();
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Mirror along X axis %M")]
	public static void MirrorPositionAlongAxis(string axis)
	{
		Vector3 mirrorVector = Vector3.one;

		if (axis == "X")
		{
			mirrorVector = new Vector3(-1f, 1f, 1f);
		}
		if (axis == "Y")
		{
			mirrorVector = new Vector3(1f, -1f, 1f);
		}
		if (axis == "Z")
		{
			mirrorVector = new Vector3(1f, 1f, -1f);
		}

		Undo.RecordObjects(Selection.transforms, "Mirror Position");

		foreach (GameObject obj in Selection.gameObjects)
		{
			obj.transform.localPosition = Vector3.Scale(obj.transform.localPosition, mirrorVector);
		}
	}


	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Snap/Position To Grid 0.5 %&D")]
	public static void RoundPositionLocalHalf()
	{
		Undo.RecordObjects(Selection.transforms, "Snap To Grid (0.5)");

		foreach (GameObject obj in Selection.gameObjects)
		{
			Vector3 vec3Doubled = obj.transform.localPosition * 2;
			obj.transform.localPosition = RoundVector3(vec3Doubled) / 2;
		}
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Snap/Position To Grid 0.1 #%D")]
	public static void RoundPositionLocalTenth()
	{
		Undo.RecordObjects(Selection.transforms, "Snap To Grid (0.1)");

		foreach (GameObject obj in Selection.gameObjects)
		{
			Vector3 vec3Decupled = obj.transform.localPosition * 10;
			obj.transform.localPosition = RoundVector3(vec3Decupled) / 10;
		}
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Snap/Rotation to nearest 22.5 deg &#F")]
	public static void RoundRotation()
	{
		Undo.RecordObjects(Selection.transforms, "Round Rotation to nearest 22.5 deg");

		foreach (GameObject obj in Selection.gameObjects)
		{
			float snapDegrees = 22.5f;
			Vector3 rotation30deg = obj.transform.localEulerAngles / snapDegrees;
			obj.transform.localEulerAngles = RoundVector3(rotation30deg) * snapDegrees;
		}
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Snap/Scale 1.0 &t")]
	public static void RoundScale()
	{
		Undo.RecordObjects(Selection.transforms, "Round Scale");

		foreach (GameObject obj in Selection.gameObjects)
		{
			obj.transform.localScale = RoundVector3(obj.transform.localScale);
		}
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Snap/Scale 0.5 &%t")]
	public static void RoundScaleHalf()
	{
		Undo.RecordObjects(Selection.transforms, "Round Scale 0.5");

		foreach (GameObject obj in Selection.gameObjects)
		{
			Vector3 scaleDoubled = obj.transform.localScale * 2;
			obj.transform.localScale = RoundVector3(scaleDoubled) / 2;
		}
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Snap/Scale 0.1 &%#t")]
	public static void RoundScaleTenth()
	{
		Undo.RecordObjects(Selection.transforms, "Round Scale 0.1");

		foreach (GameObject obj in Selection.gameObjects)
		{
			Vector3 scaleDoubled = obj.transform.localScale * 10;
			obj.transform.localScale = RoundVector3(scaleDoubled) / 10;
		}
	}

	[MenuItem("Tools/Krillbite/Bear's Bag Of Tricks/Selection/Deselect Random &f")]
	public static void DeselectRandom()
	{
		List<Object> newSelection = new List<Object>();
		Transform[] selection = Selection.transforms;

		foreach (Transform go in selection)
		{
			float num = Random.Range(0.0f, 10.0f);

			if (num > 3.333f)
			{
				newSelection.Add(go.gameObject);
			}
		}
		Selection.objects = newSelection.ToArray();
	}

	[MenuItem("Tools/Krillbite/Selection/Select Parent &C")]
	public static void SelectParent()
	{
		List<Object> newSelection = new List<Object>();
		Transform[] selection = Selection.transforms;

		foreach (Transform go in selection)
		{
			if (go.parent != null)
			{
				newSelection.Add(go.parent.gameObject);
			}
			else
			{
				newSelection.Add(go.gameObject);
			}
		}
		Selection.objects = newSelection.ToArray();
	}

	[MenuItem("Tools/Krillbite/Selection/Select siblings &z")]
	public static void SelectSiblingsFromSelected()
	{
		List<Object> newSelection = new List<Object>();
		Transform[] selection = Selection.transforms;

		foreach (Transform go in selection)
		{
			foreach (Transform child in go.parent)
			{
				newSelection.Add(child.gameObject);
			}
		}
		Selection.objects = newSelection.ToArray();
	}

	[MenuItem("Tools/Krillbite/Selection/Select children &x")]
	public static void SelectChildrenOfSelected()
	{
		List<Object> newSelection = new List<Object>();
		Transform[] selection = Selection.transforms;

		foreach (Transform go in selection)
		{
			foreach (Transform child in go)
			{
				newSelection.Add(child.gameObject);
			}
		}
		Selection.objects = newSelection.ToArray();
	}

	[MenuItem("Tools/Krillbite/Selection/Toggle Selection Enabled %g")]
	public static void ToggleSelectionEnabled()
	{
		foreach (GameObject go in Selection.gameObjects)
		{
			Undo.RecordObject(go.gameObject, "Set Active");
			go.SetActive(!go.activeInHierarchy);
		}
	}
	
	public static void LiftDecal()
	{
		foreach (GameObject go in Selection.gameObjects)
		{
			Undo.RecordObject(go.gameObject, "Lift Decal");
			go.transform.localPosition += new Vector3(0.0f, 0.002f, 0.0f);
		}
	}

	public static void DropToGround(bool useBounds)
	{
		foreach (Transform t in Selection.transforms)
		{
			Undo.RecordObject(t.transform, "Drop To Ground");
			RaycastHit rayHit = new RaycastHit();
			Physics.Raycast(t.position, Vector3.down, out rayHit);
			if (rayHit.point != Vector3.zero)
			{
				if (useBounds)
				{
					if (!t.GetComponent<Renderer>())
					{
						Debug.Log("Drop To Ground: No renderer attached to object, aborting.");
						return;
					}
					t.position = new Vector3(rayHit.point.x, rayHit.point.y + t.gameObject.GetComponent<Renderer>().bounds.extents.y, rayHit.point.z);
				}
				else
				{
					t.position = rayHit.point;
				}
			}
			else
			{
				Debug.Log("Drop To Ground: Raycast did not hit a collider. Aborting.", t);
				EditorApplication.Beep();
			}
		}
	}
	
	public static void FixBoxColliderSize()
	{
		foreach (GameObject go in Selection.gameObjects)
		{
			Undo.RecordObject(Selection.activeGameObject, "Fix Box Collider");

			if (Selection.activeGameObject.GetComponent<BoxCollider>() != null)
			{
				BoxCollider collider = go.gameObject.GetComponent<BoxCollider>();
				go.transform.localScale = Vector3.Scale(collider.size, go.transform.localScale);
				go.transform.localPosition = go.transform.localPosition + collider.center;
				collider.size = Vector3.one;
				collider.center = Vector3.zero;
			}
			else
			{
				Debug.Log(string.Format("Collider not found on GameObject: {0}", go.name));
			}

		}
	}
	public static void MakeScaleUniform()
	{
		foreach (Transform t in Selection.transforms)
		{
			Undo.RecordObject(Selection.activeGameObject.transform, "Make Scale Uniform");

			var avgScale = (t.localScale.x + t.localScale.y + t.localScale.z) / 3;
			t.localScale = new Vector3(avgScale, avgScale, avgScale);
		}
	}
	public static void FindGameObjectsAtPosition()
	{
		foreach (GameObject go in Selection.gameObjects)
		{
			Undo.RecordObject(Selection.activeGameObject, "Fix Box Collider");

			if (Selection.activeGameObject.GetComponent<BoxCollider>() != null)
			{
				BoxCollider collider = go.gameObject.GetComponent<BoxCollider>();
				go.transform.localScale = Vector3.Scale(collider.size, go.transform.localScale);
				go.transform.localPosition = go.transform.localPosition + collider.center;
				collider.size = Vector3.one;
				collider.center = Vector3.zero;
			}
			else
			{
				Debug.Log(string.Format("Collider not found on GameObject: {0}", go.name));
			}

		}
	}

}
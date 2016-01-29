using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BearsBagOfButtons : EditorWindow
{
	//private static EditorWindow window;
	private float fakeTime;
	public static float snapper;
	public static bool Local;
	public static bool World;

	private static Vector4 rangeInputs;
	private static float rangeValue;
	private static float rangeOutput;
	private const string selectedToolTypeKey = "KrillbiteUtilityTool.SelectedToolType";


	private static string findThisGameobject;
	private static string colorStringInput;

	private Color buttonColor = Color.white;

	// & = ALT
	// % = CTRL
	// # = SHIFT

	[MenuItem("Tools/Krillbite/Bear's Bag of Buttons &%#B")]
	static void Init()
	{
		GetWindow<BearsBagOfButtons>();
	}

	void OnGUI()
	{
		title = "BBoT";

		EditorGUILayout.BeginVertical();

		DrawSnap();
		DrawReset();
		DrawCopy();
		DrawMirror();
		DrawSearch();
		DrawTools();
		DrawMapRange();
		DrawColorTools();
		DrawOcclusionTools();

		EditorGUILayout.EndVertical();
	}

	private void DrawSnap()
	{
		GUI.color = buttonColor;

		EditorGUILayout.LabelField("Snap", EditorStyles.boldLabel);

		EditorGUILayout.BeginHorizontal();
		{
			snapper = EditorGUILayout.FloatField(snapper, EditorStyles.miniTextField, GUILayout.MaxWidth(30f));
			if (snapper < 0.01f)
			{
				snapper = 1.0f;
			}

			if (GUILayout.Button("1.0", EditorStyles.miniButtonLeft))
			{
				snapper = 1.0f;
			}
			if (GUILayout.Button(".25", EditorStyles.miniButtonMid))
			{
				snapper = 0.25f;
			}
			if (GUILayout.Button(".10", EditorStyles.miniButtonRight))
			{
				snapper = 0.1f;
			}
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		{
			if (GUILayout.Button("Loc", EditorStyles.miniButtonLeft))
			{
				foreach (GameObject obj in Selection.gameObjects)
				{
					BearsBagOfTricks.RoundPositionCustom(snapper, obj);
				}
			}
			if (GUILayout.Button("Rot", EditorStyles.miniButtonMid))
			{
				BearsBagOfTricks.RoundRotation();
			}
			if (GUILayout.Button("Scl", EditorStyles.miniButtonRight))
			{
				BearsBagOfTricks.RoundScale();
			}
		}
		EditorGUILayout.EndHorizontal();
	}


	private void DrawReset()
	{
		GUI.color = buttonColor;

		EditorGUILayout.LabelField("Reset", EditorStyles.boldLabel);
		EditorGUILayout.BeginHorizontal();
		{
			if (GUILayout.Button("Loc", EditorStyles.miniButtonLeft))
			{
				BearsBagOfTricks.ResetPosition();
			}
			if (GUILayout.Button("Rot", EditorStyles.miniButtonMid))
			{
				BearsBagOfTricks.ResetRotation();
			}
			if (GUILayout.Button("Scl", EditorStyles.miniButtonRight))
			{
				BearsBagOfTricks.ResetScale();
			}
		}
		EditorGUILayout.EndHorizontal();
	}


	private void DrawCopy()
	{
		GUI.color = buttonColor;

		EditorGUILayout.LabelField("Copy", EditorStyles.boldLabel);

		if (GUILayout.Button("Copy",EditorStyles.miniButton))
		{
			BearsBagOfTricks.CopyTransform();
		}

		EditorGUILayout.LabelField("Paste", EditorStyles.boldLabel);

		EditorGUILayout.BeginHorizontal();
		{
			if (GUILayout.Button("Local", EditorStyles.miniButtonLeft))
			{
				BearsBagOfTricks.PasteTransformLocal();
			}
			if (GUILayout.Button("World", EditorStyles.miniButtonRight))
			{
				BearsBagOfTricks.PasteTransformGlobal();
			}
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		{
			if (GUILayout.Button("LocalPos", EditorStyles.miniButtonLeft, GUILayout.Height(20)))
			{
				BearsBagOfTricks.PastePositionLocal();
			}
			if (GUILayout.Button("WorldPos", EditorStyles.miniButtonRight, GUILayout.Height(20)))
			{
				BearsBagOfTricks.PastePositionGlobal();
			}
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.LabelField("Copy To Clipboard", EditorStyles.boldLabel);

		EditorGUILayout.BeginHorizontal();
		{
			if (GUILayout.Button("Local Pos", EditorStyles.miniButtonLeft))
			{
				BearsBagOfTricks.CopyTransformToClipboard("Location");
			}
			if (GUILayout.Button("World Pos", EditorStyles.miniButtonRight))
			{
				BearsBagOfTricks.CopyTransformToClipboard("Location", true);
			}
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		{
			if (GUILayout.Button("Local Rot", EditorStyles.miniButtonLeft))
			{
				BearsBagOfTricks.CopyTransformToClipboard("Rotation");
			}
			if (GUILayout.Button("World Rot", EditorStyles.miniButtonRight))
			{
				BearsBagOfTricks.CopyTransformToClipboard("Rotation", true);
			}
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		{
			if (GUILayout.Button("Local Scl", EditorStyles.miniButtonLeft))
			{
				BearsBagOfTricks.CopyTransformToClipboard("Scale");
			}
			if (GUILayout.Button("World Scl", EditorStyles.miniButtonRight))
			{
				BearsBagOfTricks.CopyTransformToClipboard("Scale", true);
			}
		}
		EditorGUILayout.EndHorizontal();
	}


	private void DrawMirror()
	{
		GUI.color = buttonColor;

		EditorGUILayout.LabelField("Mirror Local Pos", EditorStyles.boldLabel);
		EditorGUILayout.BeginHorizontal();
		{
			if (GUILayout.Button("X", EditorStyles.miniButtonLeft))
			{
				BearsBagOfTricks.MirrorPositionAlongAxis("X");
			}
			if (GUILayout.Button("Y", EditorStyles.miniButtonMid))
			{
				BearsBagOfTricks.MirrorPositionAlongAxis("Y");
			}
			if (GUILayout.Button("Z", EditorStyles.miniButtonRight))
			{
				BearsBagOfTricks.MirrorPositionAlongAxis("Z");
			}
		}
		EditorGUILayout.EndHorizontal();
	}


	private void DrawOcclusionTools()
	{
		GUI.color = buttonColor;

		EditorGUILayout.LabelField("Occlusion Tools", EditorStyles.boldLabel);
		EditorGUILayout.LabelField("Occlusion Portals", EditorStyles.miniLabel);

		EditorGUILayout.BeginHorizontal();
		{
			if (GUILayout.Button("Open", EditorStyles.miniButtonLeft))
			{
				foreach (GameObject go in Selection.gameObjects)
				{
					if (go.GetComponent<OcclusionPortal>())
					{
						go.GetComponent<OcclusionPortal>().open = true;
					}
				}
			}
			if (GUILayout.Button("Close", EditorStyles.miniButtonRight))
			{
				foreach (GameObject go in Selection.gameObjects)
				{
					if (go.GetComponent<OcclusionPortal>())
					{
						go.GetComponent<OcclusionPortal>().open = false;
					}
				}
			}
		}
		EditorGUILayout.EndHorizontal();
	}


	private void DrawColorTools()
	{
		GUI.color = buttonColor;

		EditorGUILayout.LabelField("Color Tools", EditorStyles.boldLabel);

		colorStringInput = EditorGUILayout.TextField(colorStringInput);

		if (GUILayout.Button("255 -> 1"))
		{
			float f;
			float.TryParse(colorStringInput, out f);

			Debug.Log("Input: " + colorStringInput + " | Float: " + f);
			string[] splitString = colorStringInput.Split(' ');
			if (splitString.Length != 3)
			{
				Debug.LogError("Color stuff: Gimme three space separated inputs maddafakka");
				return;
			}
			float r, g, b;
			r = float.Parse(splitString[0]);
			g = float.Parse(splitString[1]);
			b = float.Parse(splitString[2]);

			var colorout = new Vector3(r/255, g/255, b/255).ToString("F3");

			EditorGUIUtility.systemCopyBuffer = BearsBagOfTricks.AddFloatSignThing(colorout);
		}
	}

	private void DrawMapRange()
	{
		GUI.color = buttonColor;

		EditorGUILayout.LabelField("Map range", EditorStyles.boldLabel);

		EditorGUILayout.BeginHorizontal();
		{
			GUI.color = new Color(1.0f, 0.7f, 0.7f);
			rangeInputs.x = EditorGUILayout.FloatField(rangeInputs.x, EditorStyles.numberField);
			rangeInputs.y = EditorGUILayout.FloatField(rangeInputs.y);
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		{
			GUI.color = new Color(0.7f, 0.7f, 1.0f);
			rangeInputs.z = EditorGUILayout.FloatField(rangeInputs.z);
			rangeInputs.w = EditorGUILayout.FloatField(rangeInputs.w);
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		{
			GUI.color = buttonColor;

			rangeValue = EditorGUILayout.FloatField(rangeValue);

			GUI.color = new Color(0.7f, 1.0f, 0.7f);

			if (rangeInputs != Vector4.zero)
			{
				rangeOutput = MapRange(rangeInputs.x, rangeInputs.y, rangeInputs.z, rangeInputs.w, rangeValue);
			}
			EditorGUILayout.FloatField(rangeOutput);
		}
		EditorGUILayout.EndHorizontal();

	}


	private void DrawSearch()
	{
		GUI.color = buttonColor;
		EditorGUILayout.LabelField("Find Gameobject", EditorStyles.boldLabel);

		findThisGameobject = EditorGUILayout.TextField(findThisGameobject);

		EditorGUILayout.BeginHorizontal();

		{
			if (GUILayout.Button("In Children", EditorStyles.miniButtonLeft, GUILayout.ExpandWidth(true)))
			{
				if (!Selection.activeGameObject) return;

				Selection.activeGameObject.transform.Find(findThisGameobject);
			}
			if (GUILayout.Button("In Scene", EditorStyles.miniButtonRight, GUILayout.ExpandWidth(true)))
			{
				Selection.activeGameObject = GameObject.Find(findThisGameobject);
			}
		}
		EditorGUILayout.EndHorizontal();

	}


	private void DrawTools()
	{
		GUI.color = buttonColor;

		EditorGUILayout.LabelField("Tools", EditorStyles.boldLabel);
		EditorGUILayout.LabelField("Drop To Ground", EditorStyles.miniLabel);

		EditorGUILayout.BeginHorizontal();
		{
			if (GUILayout.Button("Pivot", EditorStyles.miniButtonLeft, GUILayout.ExpandWidth(true)))
			{
				BearsBagOfTricks.DropToGround(false);
			}
			if (GUILayout.Button("Bounds", EditorStyles.miniButtonRight, GUILayout.ExpandWidth(true)))
			{
				BearsBagOfTricks.DropToGround(true);
			}
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.LabelField("Scene Tools", EditorStyles.miniLabel);

		if (GUILayout.Button("Rescale Box Coll", EditorStyles.miniButton, GUILayout.ExpandWidth(true)))
		{
			BearsBagOfTricks.FixBoxColliderSize();
		}
		if (GUILayout.Button("Make Scale Uniform", EditorStyles.miniButton, GUILayout.ExpandWidth(true)))
		{
			BearsBagOfTricks.MakeScaleUniform();
		}
		if (GUILayout.Button("Lift Decal", EditorStyles.miniButton, GUILayout.ExpandWidth(true)))
		{
			BearsBagOfTricks.LiftDecal();
		}
	}

	private float MapRange(float a1, float a2, float b1, float b2, float s)
	{
		return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
	}
 
}
using System;
using UnityEngine;
using UnityEditor;
using System.Collections;

public class BearsAssetTools : ScriptableWizard
{
	private static string _AssetPath = string.Empty;

	[MenuItem("Assets/Copy File Path to Clipboard", false, 80)]
	private static void CopyFilePathToClipboard()
	{
		_AssetPath = Application.dataPath.Replace("Assets", AssetDatabase.GetAssetPath(Selection.activeObject));
		_AssetPath = _AssetPath.Replace('/', '\\');
		EditorGUIUtility.systemCopyBuffer = _AssetPath;
	}

	[MenuItem("Assets/Copy Folder Path to Clipboard", false, 80)]
	private static void CopyFolderPathToClipboard()
	{
		_AssetPath = Application.dataPath.Replace("Assets", AssetDatabase.GetAssetPath(Selection.activeObject));
		_AssetPath = _AssetPath.Remove(_AssetPath.LastIndexOf('/'), _AssetPath.Length - _AssetPath.LastIndexOf('/'));
		_AssetPath = _AssetPath.Replace('/', '\\');
		EditorGUIUtility.systemCopyBuffer = _AssetPath;
	}

	[MenuItem("Assets/Copy Raw Asset Path to Clipboard", false, 80)]
	private static void CopyRawAssetPathToClipboard()
	{
		_AssetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
		EditorGUIUtility.systemCopyBuffer = _AssetPath;
	}

	//Code below is not mine - Bear

	/* Author : Altaf
	 * Date : May 20, 2013
	 * Purpose : Context menu to copy, cut & paste items 
	*/

	[MenuItem("Assets/Cut", false, 80)]
	private static void MoveAsset()
	{
		_AssetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
		//Debug.Log("Copied asset at path : " + _AssetPath);
	}

	[MenuItem("Assets/Cut", true)]
	private static bool MoveAssetValidate()
	{
		return (Selection.activeObject != null);
	}

	[MenuItem("Assets/Paste", false, 80)]
	private static void PasteAsset()
	{
		string dstPath = AssetDatabase.GetAssetPath(Selection.activeObject);
		string fileExt = System.IO.Path.GetExtension(dstPath);
		if (!string.IsNullOrEmpty(fileExt))
			dstPath = System.IO.Path.GetDirectoryName(dstPath);
		string fileName = System.IO.Path.GetFileName(_AssetPath);
		string msg = AssetDatabase.MoveAsset(_AssetPath, dstPath + "/" + fileName);
		if (string.IsNullOrEmpty(msg))
		{
			_AssetPath = null;
			//Debug.Log("Pasted asset at path : " + _AssetPath);
		}
		else
			Debug.LogError("Failed to paste asset : " + msg);
	}

	[MenuItem("Assets/Paste", true)]
	private static bool PasteAssetValidate()
	{
		//Have we copied anything?
		if (string.IsNullOrEmpty(_AssetPath))
			return false;
		//Try to paste no where?
		if (Selection.activeObject == null)
			return false;
		//Trying to paste on same asst again?
		if (AssetDatabase.GetAssetPath(Selection.activeObject) == _AssetPath)
			return false;

		return true;
	}
}
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using Random = UnityEngine.Random;

public static class EditorSound
{
    public static void PlayClip(AudioClip clip, int startSample = 0, bool loop = false)
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;

        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod(
            "PlayPreviewClip",
            BindingFlags.Static | BindingFlags.Public,
            null,
            new Type[] { typeof(AudioClip), typeof(int), typeof(bool) },
            null
        );
        method.Invoke(
            null,
            new object[] { clip, startSample, loop }
        );
    }

    public static void StopAllClips()
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;

        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod(
            "StopAllPreviewClips",
            BindingFlags.Static | BindingFlags.Public,
            null,
            new Type[] { },
            null
        );
        method.Invoke(
            null,
            new object[] { }
        );
    }
}

[CustomEditor(typeof(Sound))]
public class OverWriteSoundGUI : Editor
{
    private bool addedToAudioManager;

    public override void OnInspectorGUI()
    {
        var sound = (Sound)target;

        GUI.backgroundColor = new Color32(0, 0, 0, 255);

        if (FindObjectOfType<AudioManager>())
        {
            if (FindObjectOfType<AudioManager>().sounds.Contains(sound))
                addedToAudioManager = true;
            else
                addedToAudioManager = false;
        }

        if (!addedToAudioManager)
        {
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Add To AudioManager", GUILayout.Height(30)))
            {
                if (FindObjectOfType<AudioManager>())
                    FindObjectOfType<AudioManager>().sounds.Add(sound);
                else
                    Debug.LogWarning("No AudioManager found in scene. Add an AudioManager.");
            }
        } else
        {
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Remove From AudioManager", GUILayout.Height(30)))
            {
                if (FindObjectOfType<AudioManager>())
                    FindObjectOfType<AudioManager>().sounds.Remove(sound);
                else
                    Debug.LogWarning("No AudioManager found in scene. Add an AudioManager.");
            }
        }

        GUI.backgroundColor = Color.yellow;

        if (GUILayout.Button("Preview Sound (RAW)", GUILayout.Height(30)))
        {
            if (sound.clips.Length > 0 && sound.clips[0] != null)
            {
                EditorSound.StopAllClips();
                EditorSound.PlayClip(sound.clips[Random.Range(0, sound.clips.Length)]);
            } else
                Debug.LogWarning("No clip(s) selected. Cannot preview sound.");
        }

        if (GUILayout.Button("Stop Sound", GUILayout.Height(30)))
            EditorSound.StopAllClips();

        GUI.backgroundColor = Color.white;

        serializedObject.Update();
        EditorGUI.BeginChangeCheck();
        DrawPropertiesExcluding(serializedObject, "m_Script");
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }
}

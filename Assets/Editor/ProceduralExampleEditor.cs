using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProceduralExample))]
public class ProceduralExampleEditor : Editor
{
    private void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        ProceduralExample procExample = (ProceduralExample)target;

        DrawDefaultInspector();


        
        if (GUILayout.Button("Regenerate"))
        {
            procExample.Generate();
        }
        

        if (GUILayout.Button("Clear"))
        { 
            procExample.ClearCubes();
        }
       
    }
}

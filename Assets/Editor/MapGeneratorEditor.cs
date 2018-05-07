using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor {

    public override void OnInspectorGUI(){

        DrawDefaultInspector();

        MapGenerator myScript = (MapGenerator)target;
        if(GUILayout.Button("Generate Level"))
        {
            myScript.GenerateLevel();
        }

        if(GUILayout.Button("Clear")){
            myScript.Clear();
        }
    }

}
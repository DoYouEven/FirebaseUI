using UnityEngine;
using System.Collections;
using UnityEditor;


public class FirebaseConfigWindow : ICustomEditorWindow
{
    private Editor editor { get; set; }
    private string name { get; set; }
    protected Vector2 scrollPosition;
    public bool requiresDatabase { get; set; }
    public FirebaseConfigWindow(string name)
    {
        this.name = name;
        this.requiresDatabase = false;
    }
    public static FirebaseConfigData configuration;
    public void Focus()
    {
        //tryCreateConfigurationData();
        //if (EditorUtils.GetInventoryManager() != null)
        // EditorUtils.selectedLangDatabase = EditorUtils.GetInventoryManager().lang;
    }
    public static FirebaseConfigData getConfig()
    {
        if (configuration == null)
        {
            var db = AssetDatabase.FindAssets("t:" + typeof(FirebaseConfigData).Name);
            if (db == null || db.Length == 0)
            {
                Debug.Log("Going to create a new Config Object");
                CreateFirebaseConfigObject();
                db = AssetDatabase.FindAssets("t:" + typeof(FirebaseConfigData).Name);
            }
            else
            {
                Debug.Log("Config Object already exists");
            }
            configuration = (FirebaseConfigData)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(db[0]), typeof(FirebaseConfigData));
        }
        return configuration;
    }
    public void Draw()
    {

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace(); // Center horizontally
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, DatabaseEditorStyles.boxStyle, GUILayout.MaxWidth(1000));


        var prevWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 250;

        #region Path selector

        EditorGUILayout.LabelField("Firebase Configuration", DatabaseEditorStyles.labelStyle);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal(DatabaseEditorStyles.boxStyle);

        getConfig().DatabaseUrl = EditorGUILayout.TextField("Enter Database URL", getConfig().DatabaseUrl);

        EditorGUILayout.EndHorizontal();

        GUI.color = Color.white;

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        #endregion


        //editor.DrawDefaultInspector();


        EditorGUILayout.EndScrollView();
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();


        EditorGUIUtility.labelWidth = prevWidth;

        /*
        if (GUI.changed && EditorUtils.selectedLangDatabase != null)
            EditorUtility.SetDirty(EditorUtils.selectedLangDatabase); // To make sure it gets saved.
    */
    }


    static void CreateFirebaseConfigObject()
    {

        string path = "Assets/Editor/FirebaseUI/";

        string assetName = "FirebaseConfig.asset";
        FirebaseConfigData asset = ScriptableObject.CreateInstance(typeof(FirebaseConfigData)) as FirebaseConfigData;  //scriptable object
        // mockdata.PokemonmockData(asset,asset1);
        //Debug.Log(AssetDatabase.GenerateUniqueAssetPath(path + assetName));
        AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(path + assetName));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        //EditorUtility.FocusProjectWindow();
        Debug.Log("Empty Level Database created");
        Selection.activeObject = asset;
        EditorUtility.SetDirty(asset);


    }


    public override string ToString()
    {
        return name;
    }
}


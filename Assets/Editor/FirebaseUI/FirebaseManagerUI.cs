using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class FirebaseManagerUI : EditorWindow
{

    public delegate void eventAction();
    //	public static eventAction RequestUpdate;

    public static FirebaseConfigWindow firebaseConfigWindow { get; set; }
    //public static LevelManagerWindow levelManagerWindow { get; set; }

    public static List<ICustomEditorWindow> editors = new List<ICustomEditorWindow>(1);

    private int toolbarIndex = 0;


    protected string[] editorNames
    {
        get
        {
            string[] items = new string[editors.Count];
            for (int i = 0; i < editors.Count; i++)
            {
                items[i] = editors[i].ToString();
            }

            return items;
        }
    }

    private void OnEnable()
    {
        
        m_Logo = (Texture2D)Resources.Load("firebaseHeader", typeof(Texture2D));
        minSize = new Vector2(600.0f, 400.0f);
        toolbarIndex = 0;
        //		RequestUpdate += UpdateDraws;
        //if (InventoryEditorUtil.selectedDatabase == null)
        //    return;

        CreateEditors();
    }

    public virtual void CreateEditors()
    {
        editors.Clear();
        /*
                levelManagerWindow = new LevelManagerWindow("Level Editor", this);
                levelManagerWindow.requiresDatabase = true;
                //pokemonEditor.childEditors.Add(new PokeEditor("Pokemon", "Pokemons", this));
                levelManagerWindow.childEditors.Add(new LevelEditorTab("Level", "Levels", this));
                levelManagerWindow.childEditors.Add(new GemEditorTab("Gem", "Gems", this));
                editors.Add(levelManagerWindow);


                */
        firebaseConfigWindow = new FirebaseConfigWindow("Firebase Configuration");
        editors.Add(firebaseConfigWindow);
        editors.Add(firebaseConfigWindow);
    }

    [MenuItem("Firebase/Firebase Manager", false, -99)] // Always at the top
    public static void ShowWindow()
    {
        GetWindow<FirebaseManagerUI>(false, "Database Manager", true);
        //GetWindow(typeof(ItemManagerEditor), true, "Item manager", true);
    }

    protected virtual bool CheckDatabase()
    {/*
        if (EditorUtils.selectedDatabase == null)
        {
            ShowItemDatabasePicker();
            return false;
        }
        */
        return true;
    }
    protected virtual void ShowItemDatabasePicker()
    {
        EditorGUILayout.LabelField("Found the following item databases in your project folder:", EditorStyles.largeLabel);
        /*
        var dbs = AssetDatabase.FindAssets("t:" + typeof(LevelAssetDatabase).Name);
        foreach (var db in dbs)
        {
            EditorGUILayout.BeginHorizontal();

            if (EditorUtils.GetItemDatabase(true, false) != null && AssetDatabase.GUIDToAssetPath(db) == AssetDatabase.GetAssetPath(EditorUtils.GetItemDatabase(true, false)))
                GUI.color = Color.green;

            EditorGUILayout.LabelField(AssetDatabase.GUIDToAssetPath(db), DatabaseEditorStyles.labelStyle);
            if (GUILayout.Button("Select", GUILayout.Width(100)))
            {
                EditorUtils.selectedDatabase = (LevelAssetDatabase)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(db), typeof(LevelAssetDatabase));
                OnEnable(); // Re-do editors
            }

            GUI.color = Color.white;
            EditorGUILayout.EndHorizontal();
        }
         */
        EditorGUILayout.Space();
        EditorGUILayout.Space();



        /*
        if (dbs.Length == 0)
        {
            EditorGUILayout.LabelField("No Item databases found, first create one in your assets folder.");
        }
         * */
    }
    private Texture2D m_Logo = null;
   

    protected virtual void DrawToolbar()
    {
        EditorGUILayout.BeginVertical(DatabaseEditorStyles.boxStyle,GUILayout.MinWidth(150));
         GUILayout.Label(m_Logo);
        GUI.color = Color.grey;
        /*
        if (GUILayout.Button("Select Database", DatabaseEditorStyles.toolbarStyle, GUILayout.Width(100)))
        {
            // EditorUtils.selectedDatabase = null;
            toolbarIndex = 0;
        }
         */
        GUI.color = Color.white;

        int before = toolbarIndex;
        toolbarIndex = GUILayout.SelectionGrid(toolbarIndex, editorNames, 1, DatabaseEditorStyles.toolbarStyle);

        if (before != toolbarIndex)
            editors[toolbarIndex].Focus();

        EditorGUILayout.EndVertical();
     
    }


    public void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        DrawToolbar();
        if (CheckDatabase() == false && editors[toolbarIndex].requiresDatabase && !Event.current.shift)
        {
            //drawFooter();
            return;
        }
      
        //		Debug.Log ("drawo?");
        editors[toolbarIndex].Draw();

        EditorGUILayout.EndHorizontal();

        //drawFooter();
    }


    void drawFooter()
    {
        EditorGUILayout.BeginHorizontal();
        GUI.color = Color.grey;
        EditorGUI.LabelField(new Rect(5, 380, 1000, 100), "*************************Created for Reuben for Teamname *************************************************", EditorStyles.label);
        EditorGUILayout.EndHorizontal();
    }
    // Use this for initialization

}

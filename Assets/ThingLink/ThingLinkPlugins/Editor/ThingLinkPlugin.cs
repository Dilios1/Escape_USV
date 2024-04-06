
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using ThingLink;


public class ThingLinkPlugin : EditorWindow
{
    string username;
    string password;
    private Texture logo;
    private Texture invisible;
    private string fileName;

    [HideInInspector]
    private UnityTL tlManager;

    [SerializeField] private GameObject tlObjectPrefab;

    private bool isPanorama = true;

    private GUIStyle guiStyle = new GUIStyle();
    private GUIStyle createAccountLabel = new GUIStyle();
    private GUIStyle createAccountButton = new GUIStyle();

    private EditorWindow pluginWindow;

    private float formMargin = 30;

    private float windowMinWidth = 400;
    private float windowMinHeight = 400;

    [MenuItem("Window / ThingLink")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ThingLinkPlugin));
    }

    private void Awake()
    {
        InitializePlugin();
        pluginWindow = EditorWindow.GetWindow(typeof(ThingLinkPlugin));

        logo = Resources.Load<Texture>("TL_logo");

        createAccountLabel.fontSize = 10;
        createAccountLabel.alignment = TextAnchor.MiddleRight;
        createAccountLabel.normal.textColor = Color.grey;
        createAccountLabel.active.textColor = Color.grey;
        createAccountLabel.focused.textColor = Color.grey;
        createAccountLabel.hover.textColor = Color.white;
        createAccountLabel.onHover.textColor = Color.white;
        createAccountLabel.onNormal.textColor = Color.grey;
        createAccountLabel.onFocused.textColor = Color.grey;
        createAccountLabel.onActive.textColor = Color.grey;
        createAccountLabel.hover.background = (Texture2D)invisible;
        createAccountLabel.onHover.background = (Texture2D)invisible;

        Color createAccountColor = new Color32(96, 160, 226, 255);
        createAccountButton.fontSize = 10;
        createAccountButton.fontStyle = FontStyle.Bold;
        createAccountButton.alignment = TextAnchor.MiddleLeft;
        createAccountButton.normal.textColor = createAccountColor;
        createAccountButton.active.textColor = createAccountColor;
        createAccountButton.focused.textColor = createAccountColor;
        createAccountButton.hover.textColor = Color.white;
        createAccountButton.onHover.textColor = Color.white;
        createAccountButton.onNormal.textColor = createAccountColor;
        createAccountButton.onFocused.textColor = createAccountColor;
        createAccountButton.onActive.textColor = createAccountColor;
        createAccountButton.hover.background = (Texture2D)invisible;
        createAccountButton.onHover.background = (Texture2D)invisible;

    }

    void InitializePlugin()
    {
        //Debug.Log("Initializing Editor plugin...");

        try
        {
            tlManager = FindObjectOfType<UnityTL>();

            if (tlManager == null)
            {
                Debug.Log("Adding ThinklinkPlugin instance to scene. It can be removed when plugin is not in use.");

                GameObject obj = new GameObject();
                obj.name = "ThinglinkPluginManager";
                tlManager = obj.AddComponent<UnityTL>();
            }


            tlManager.loginStatus = "Log in, please";

            string storedUsername = PlayerPrefs.GetString("username");
            if (storedUsername != "")
            {
                username = storedUsername;
            }

        }
        catch (UnityException e)
        {
            Debug.LogError("Error initializing plugin: " + e);
        }
    }

    void OnGUI()
    {

        pluginWindow.minSize = new Vector2(windowMinWidth, windowMinHeight);

        if (tlManager != null)
        {
            if (tlManager.loginStatus == "Logged in")
            {

                //--------L O G O------------
                GUILayout.Space(10); //MARGIN ABOVE LOGO


                GUILayout.BeginHorizontal();
                GUILayout.Space(position.width / 2 - 40);
                GUILayout.Label(logo, GUILayout.Height(20)); //LOGO PICTURE
                GUILayout.EndHorizontal();
                GUILayout.Space(20); //MARGIN UNDER LOGO

                GUILayout.BeginHorizontal();
                GUILayout.Space(position.width / 2 - 110);
                if (ThingLinkPlugin.LinkLabel("Welcome, " + tlManager.realName, Color.white, 20))
                {
                    //Application.OpenURL ("https://thinglink.com");
                }
                GUILayout.EndHorizontal();


                GUILayout.Space(20); // MARGIN UNDER WELCOME TEXT

                GUI.backgroundColor = Color.white;

                //---------F O R M--------------

                GUILayout.BeginHorizontal();
                GUILayout.Space(formMargin);

                //USER LABEL
                GUILayout.Label("Save as");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(formMargin);
                fileName = EditorGUILayout.TextField("", fileName, GUILayout.Width(position.width - formMargin * 2)); //SAVE AS INPUT BOX

                GUILayout.Label((".jpg"), EditorStyles.centeredGreyMiniLabel);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(formMargin);
                isPanorama = GUILayout.Toggle(isPanorama, "Save as 360°");
                GUILayout.EndHorizontal();

                GUILayout.Space(20);

                GUILayout.BeginHorizontal();
                GUILayout.Space(formMargin);
                GUI.backgroundColor = new Color32(112, 176, 255, 255);
                if (GUILayout.Button("Upload", GUILayout.Width(position.width - formMargin * 2), GUILayout.Height(30)))
                {
                    tlManager.autoOpenDone = false;
                    Capture();
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(formMargin);
                GUI.backgroundColor = Color.white;
                tlManager.autoOpen = GUILayout.Toggle(tlManager.autoOpen, "Open ThingLink Editor after uploading completes");
                GUILayout.EndHorizontal();
                //------------------------------

                GUILayout.Space(20);

                GUILayout.BeginHorizontal();
                GUILayout.Space(formMargin);
                GUILayout.Label(tlManager.uploadStatus, EditorStyles.centeredGreyMiniLabel);

                GUILayout.EndHorizontal();

                GUI.backgroundColor = Color.grey;
                GUILayout.Space(position.height - 280);

                //-----------FOOTER-------------
                GUILayout.BeginHorizontal();
                GUILayout.Space(formMargin);

                if (ThingLinkPlugin.LinkLabel("Log out", Color.grey, 8))
                {
                    tlManager.loginStatus = "Logged out";
                }

                GUILayout.Space(position.width * 0.86f - 100);

                if (ThingLinkPlugin.LinkLabel("Visit ThingLink", Color.grey, 8))
                {
                    Application.OpenURL("https://thinglink.com");
                }
                GUILayout.EndHorizontal();
                //----------------------------------

            }
            else // NOT LOGGED IN
            {

                //------L O G O --------
                GUILayout.Space(40);

                GUILayout.BeginHorizontal();

                GUILayout.Space(position.width / 2 - 100);

                GUILayout.Label(logo, GUILayout.Width(200), GUILayout.Height(50)); // LOGO IMAGE

                GUILayout.EndHorizontal();
                //-----------------------
                GUILayout.Space(20);

                //------F O R M --------

                GUILayout.BeginHorizontal();
                GUILayout.Space(formMargin);

                //USER LABEL
                GUILayout.Label("Email");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(formMargin);
                //USER FIELD
                username = EditorGUILayout.TextField("", username, GUILayout.Width(position.width - formMargin * 2));
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                GUILayout.Space(formMargin);
                //PW LABEL
                GUILayout.Label("Password");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(formMargin);
                //PW FIELD
                password = EditorGUILayout.PasswordField("", password, GUILayout.Width(position.width - formMargin * 2));
                GUILayout.EndHorizontal();
                //-----------------------

                GUI.backgroundColor = new Color32(112, 176, 255, 255);
                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                GUILayout.Space(formMargin);

                if (GUILayout.Button("Login", GUILayout.Height(30), GUILayout.Width(position.width - formMargin * 2)))
                {
                    OAuthLogin();
                }

                GUILayout.EndHorizontal();

                GUILayout.Space(10);
                GUILayout.BeginHorizontal();
                GUILayout.Label("Don't have an account?  ", createAccountLabel);
                if (GUILayout.Button("Create Account", createAccountButton))
                {
                    Application.OpenURL("https://thinglink.com");
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(20);
                GUILayout.Label(tlManager.loginStatus, EditorStyles.centeredGreyMiniLabel); // STATUS TEXT

            }
        }
        else
        {
            //Debug.Log("Plugin not initialized");
        }
    }

    void Update()
    {
        if (tlManager == null)
        {
            InitializePlugin();
        }
    }

    void OAuthLogin()
    {
        if (username != "" && password != "")
            tlManager.OAuthLogin(username, password);
        else
            tlManager.loginStatus = "Fill credentials";
    }

    void Capture()
    {
        SceneView.lastActiveSceneView.showGrid = false;
        Camera targetCamera = SceneView.lastActiveSceneView.camera;

        // For some reason (maybe bug in Unity) scene view camera sometimes adds black
        // borders to cube images which appears as black cage around viewpoint in 360 image.
        // To get around this problem we spawn a temporary camera at the same position
        // and use it as capture camera.
        GameObject temp = new GameObject();
        temp.transform.position = targetCamera.transform.position;
        Camera cam = temp.AddComponent<Camera>();

        tlManager.Capture(cam, fileName, isPanorama);
        DestroyImmediate(temp);
    }

    public static bool LinkLabel(string labelText, Color labelColor, /*Vector2 contentOffset,*/ int fontSize)
    {

        GUIStyle stl = EditorStyles.label;

        Color col = stl.normal.textColor;
        //Vector2 os = stl.contentOffset;
        int size = stl.fontSize;

        stl.normal.textColor = labelColor;
        //stl.contentOffset = contentOffset;
        stl.fontSize = fontSize;

        try
        {
            return GUILayout.Button(labelText, stl);
        }
        finally
        {
            stl.normal.textColor = col;
            //stl.contentOffset = os;
            stl.fontSize = size;
        }
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }
}


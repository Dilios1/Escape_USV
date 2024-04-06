using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThingLink;
using UnityEngine.UI;

public class ThingLinkRuntimeManager : MonoBehaviour
{
    private UnityTL tlManager;

    ThingLinkRuntimeManager singleton;

    [HideInInspector]
    private string username;

    [HideInInspector]
    private string password;

    [HideInInspector]
    public InputField usernameField;

    [HideInInspector]
    public InputField passwordField;

    [HideInInspector]
    public InputField fileNameField;

    [HideInInspector]
    public Toggle isPanorama;

    [HideInInspector]
    public Toggle autoOpen;

    [HideInInspector]
    public Text loginInfoText;

    [HideInInspector]
    public GameObject loginPage;

    [HideInInspector]
    public GameObject capturePage;

    [HideInInspector]
    public Button loginButton;

    [HideInInspector]
    public Button uploadButton;

    public Camera captureCamera;

    [HideInInspector]
    public Text uploadStatusText;

    [HideInInspector]
    public Text welcomeText;

    private bool captureWindowIsOpen;

    private void Awake()
    {
        InitializePlugin();

        if (singleton != null)
        {
            Debug.Log("Runtime manager singleton already created");
        }
        else
        {
            singleton = this;
        }


        if (captureCamera == null)
        {
            if (Camera.main != null)
            {
                captureCamera = Camera.main;
                Debug.Log("Capture camera has been set to main camera, you can change the camera from Thinglink plugin root object");
            }
            else
            {
                Debug.Log("Cannot find capture camera for Thinglink Runtime plugin, add it to Thinglink plugin root object.");
            }
        }

        captureWindowIsOpen = false;
    }

    private void OnGUI()
    {
        loginInfoText.text = tlManager.loginStatus;

        if(tlManager.loginStatus == "Logged in")
        {
            if (!captureWindowIsOpen)
            {
                capturePage.SetActive(true);
                fileNameField.Select();
                captureWindowIsOpen = true;
            }
        }

        uploadStatusText.text = tlManager.uploadStatus;
        welcomeText.text = "Welcome, " + tlManager.realName;
    }

    public void ShowLoginPage()
    {
        tlManager.loginStatus = "Log in, please";
        capturePage.SetActive(false);
        captureWindowIsOpen = false;
    }

    public void OAuthLogin()
    {
        username = usernameField.text;
        password = passwordField.text;

        if (username != "" && password != "")
            tlManager.OAuthLogin(username, password);
        else
            tlManager.loginStatus = "Fill credentials";
    }

    void InitializePlugin()
    {

        try
        {
            tlManager = GetComponent<UnityTL>();

            tlManager.loginStatus = "Log in, please";

            string storedUsername = PlayerPrefs.GetString("username");

            if (storedUsername != "")
            {
                username = storedUsername;
                usernameField.text = username;
                passwordField.text = "";
            }

            UpdateAutoOpenStatus();

        }
        catch (UnityException e)
        {
            Debug.Log("Error initializing plugin: " + e);
        }
    }

    private void CheckHotkeys()
    {
        if (capturePage.activeSelf == false) // Login page
        {

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (usernameField.isFocused)
                {
                    passwordField.Select();
                }
                else
                {
                    usernameField.Select();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                loginButton.Select();
                OAuthLogin();
            }
        }
        else // Capture page
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                fileNameField.Select();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                uploadButton.Select();
                Upload();
            }
        }

    }

    public void OpenHomeSite()
    {
        Application.OpenURL("https://thinglink.com");
    }

    public void Upload()
    {
        tlManager.autoOpenDone = false;
        tlManager.Capture(captureCamera, fileNameField.text, isPanorama.isOn);
    }

    public void UpdateAutoOpenStatus()
    {
        tlManager.autoOpen = autoOpen.isOn;
    }

    private void Update()
    {
        CheckHotkeys();
    }

}

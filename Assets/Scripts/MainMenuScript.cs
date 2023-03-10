// ===============================
// AUTHOR     : Rafael Maio (rafael.maio@ua.pt)
// PURPOSE     : Handles the configuration menu.
// SPECIAL NOTES:
// ===============================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class MainMenuScript : MonoBehaviour
{
    /// <summary>
    /// Dropdown containing a list of files containing anchor information.
    /// </summary>
    public Dropdown fileDropDown;

    /// <summary>
    /// Dropdown containing a list of plan images.
    /// </summary>
    public Dropdown planDropDown;

    /// <summary>
    /// Dropdown containing a list of set of models
    /// </summary>
    public Dropdown modelsDropDown;

    /// <summary>
    /// UI with behavior.
    /// </summary>
    public Button startButton;
    public Button backButton;
    public InputField xPlanIF;
    public InputField yPlanIF;
    public Dropdown cloudOrFileDropDown;
    public InputField fileNameIF;
    private bool cloudMode = true;

    /// <summary>
    /// Connection to the main script (bezier).
    /// </summary>
    public bezier mainScript;

    /// <summary>
    /// Main camera.
    /// </summary>
    public GameObject main_camera;

    /// <summary>
    /// Plan object.
    /// </summary>
    public GameObject plan;

    /// <summary>
    /// Existing plans in the Assets/Plans folder
    /// </summary>
    Sprite[] Plans;

    /// <summary>
    /// File name.
    /// </summary>
    private string fileName;

    /// <summary>
    /// Navegation menus.
    /// </summary>
    public GameObject configureMenu;
    public GameObject initialMenu;

    /// <summary>
    /// Plan size
    /// </summary>
    string[] plan_size_file;
    float plan_x = 0;
    float plan_y = 0;

    /// <summary>
    /// Usability Test toggle.
    /// </summary>
    public Toggle usabiliTestToggle;

    /// <summary>
    /// Usability Test script.
    /// </summary>
    public UsabilityTest usabilityTestScript;

    public ScrollViewAdapter scrollViewAdapter;

    /// <summary>
    /// Unity Start function.
    /// </summary>
    void Start()
    {
        //Obtain the files containg the anchor information from Assets/AnchorFiles folder
        fileDropDown.options.Clear();
        TextAsset[] anchorFiles = Resources.LoadAll<TextAsset>("AnchorFiles");
        for(int i = 0; i < anchorFiles.Length; i++)
        {
            fileDropDown.options.Add(new Dropdown.OptionData() { text = anchorFiles[i].name});
        }
        fileDropDown.RefreshShownValue();

        //Obtain the plans for scale comparison from Assets/AnchorFiles folder
        planDropDown.options.Clear();
        Plans = Resources.LoadAll<Sprite>("Plans");
        for (int i = 0; i < Plans.Length; i++)
        {
            planDropDown.options.Add(new Dropdown.OptionData() { text = Plans[i].name });
        }
        planDropDown.RefreshShownValue();
        planDropDown.onValueChanged.AddListener(delegate { change_plan_size(); });

        modelsDropDown.options.Clear();
        TextAsset[] Models = Resources.LoadAll<TextAsset>("Models");
        for (int i = 0; i < Models.Length; i++)
        {
            modelsDropDown.options.Add(new Dropdown.OptionData() { text = Models[i].name });
        }
        modelsDropDown.RefreshShownValue();

        startButton.onClick.AddListener(startConf);

        cloudOrFileDropDown.onValueChanged.AddListener(delegate { changeConfMode(); });

        //Obtain the plan size
        //plan_size_file = File.ReadAllLines("Resources/Plans/plan_sizes.txt");
        TextAsset plan_size_file_Asset = Resources.Load<TextAsset>("Plans/plan_sizes");
        plan_size_file = plan_size_file_Asset.text.Split('\n');
        foreach (var line in plan_size_file)
        {
            if (line.Split(':')[0] == planDropDown.options[planDropDown.value].text)
            {
                xPlanIF.placeholder.GetComponent<Text>().text = "X = "+line.Substring(line.IndexOf("x=")+2, line.IndexOf(",") - line.IndexOf("x=")-2) + "(m)";
                plan_x = float.Parse(line.Substring(line.IndexOf("x=")+2, line.IndexOf(",") - line.IndexOf("x=")-2));
                yPlanIF.placeholder.GetComponent<Text>().text = "Y = " + line.Substring(line.IndexOf("y=")+2, line.Length - line.IndexOf("y=")-2) + "(m)";
                plan_y = float.Parse(line.Substring(line.IndexOf("y=")+2, line.Length - line.IndexOf("y=")-2));
                break;
            }
        }

        backButton.onClick.AddListener(back);
    }

    /// <summary>
    /// Returns to the previous menu or the previous mode.
    /// </summary>
    private void back()
    {
        this.gameObject.SetActive(false);
        initialMenu.SetActive(true);
    }

    /// <summary>
    /// Change the plan image size.
    /// </summary>
    public void change_plan_size()
    {
        foreach (var line in plan_size_file)
        {
            if (line.Split(':')[0] == planDropDown.options[planDropDown.value].text)
            {
                xPlanIF.placeholder.GetComponent<Text>().text = "X = " + line.Substring(line.IndexOf("x=") + 2, line.IndexOf(",") - line.IndexOf("x=") - 2) + "(m)";
                plan_x = float.Parse(line.Substring(line.IndexOf("x=") + 2, line.IndexOf(",") - line.IndexOf("x=") - 2));
                yPlanIF.placeholder.GetComponent<Text>().text = "Y = " + line.Substring(line.IndexOf("y=") + 2, line.Length - line.IndexOf("y=") - 2) + "(m)";
                plan_y = float.Parse(line.Substring(line.IndexOf("y=") + 2, line.Length - line.IndexOf("y=") - 2));
                break;
            }
        }
    }

    /// <summary>
    /// Change the configuration mode (Cloud/Motion tracking)
    /// </summary>
    private void changeConfMode()
    {
        cloudMode = !cloudMode;
        fileNameIF.gameObject.SetActive(!fileNameIF.gameObject.activeSelf);
        fileDropDown.gameObject.SetActive(!fileDropDown.gameObject.activeSelf);
    }

    /// <summary>
    /// Start the configuration, changing the window.
    /// Transfer the plan and the file to the main application.
    /// </summary>
    void startConf()
    {
        activate(true);
        plan.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Plans/" + Plans[planDropDown.value].name);
        //plan.GetComponent<SpriteRenderer>().sprite = LoadSprite(path);
        if (!string.IsNullOrEmpty(xPlanIF.text) || !string.IsNullOrEmpty(yPlanIF.text))
        {
            plan.transform.GetComponent<SpriteRenderer>().size = new Vector2(float.Parse(xPlanIF.text), float.Parse(yPlanIF.text));
        }
        else
        {
            plan.transform.GetComponent<SpriteRenderer>().size = new Vector2(plan_x, plan_y);
        }

        if (cloudMode)
        {
            //fileName = fileDropDown.options[fileDropDown.value].text;
            //mainScript.setFile(Resources.Load<TextAsset>("AnchorFiles/" + fileName + ".json", cloudMode);
        }
        else
        {
            fileName = fileNameIF.text;
            mainScript.setFile("", cloudMode);
        }

        if (usabiliTestToggle.isOn)
        {
            usabilityTestScript.enableTest();
        }

        scrollViewAdapter.FillList();

        this.gameObject.SetActive(false);
        configureMenu.SetActive(true);
    }

    /// <summary>
    /// Load sprite image from path.
    /// </summary>
    /// <param name="path">Image path.</param>
    /// <returns>Sprite image.</returns>
    private Sprite LoadSprite(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        if (System.IO.File.Exists(path))
        {
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
        return null;
    }

    /// <summary>
    /// Get the chosen operating mode.
    /// </summary>
    /// <returns>The chosen operation mode.</returns>
    public bool getCloudMode()
    {
        return cloudMode;
    }

    /// <summary>
    /// Get the chosen file containg the anchor information.
    /// </summary>
    /// <returns>The chosen file containg the anchor information.</returns>
    public string getFileName()
    {
        return fileName;
    }

    /// <summary>
    /// Enable/Disable some relevant components for the desktop configuration.
    /// </summary>
    public void activate(bool activate)
    {
        main_camera.GetComponent<CameraMovement>().enabled = activate;
        plan.GetComponent<SpriteRenderer>().enabled = activate;
        mainScript.gameObject.SetActive(activate);
    }
}
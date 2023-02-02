// ===============================
// AUTHOR     : Rafael Maio (rafael.maio@ua.pt)
// PURPOSE     : Handles the configuration window.
//               Contains all the button listeners.
// SPECIAL NOTES:
// ===============================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ConfigureMenuScript : MonoBehaviour
{
    /// <summary>
    /// UI with behaviour.
    /// </summary>
    public Text AnchorsText;
    public Text HelpText;
    public Button saveButton;
    public Button backButton;
    public Button nextButton;
    public Button previousButton;
    public Image prefabImage;
    public Sprite anchor_sprite;
    public Button removeButton;
    public Text endText;
    public Text numObjectsText;

    /// <summary>
    /// Connection to the main script (bezier).
    /// </summary>
    public bezier mainScript;

    // <summary>
    // Manual Mode UI with behaviour.
    // </summary>
    public Button manualButton;
    public InputField manualInputField;
    public Button okButton;
    public Text idText;
    public Text xText;
    public Text yText;
    public Text zText;
    public Text rotText;
    public Text scaleText;
    public Image manualArrow;
    public Button manualButtonMore;
    public Button manualButtonLess;
    public Button manualCircleButtonId;
    public Button manualCircleButtonX;
    public Button manualCircleButtonY;
    public Button manualCircleButtonZ;
    public Button manualCircleButtonRot;
    public Button manualCircleButtonScale;
    public GameObject manualMenu;

    /// <summary>
    /// Help Menu.
    /// </summary>
    public Button helpButton;
    public Image helpImage;

    /// <summary>
    /// Configuration window.
    /// </summary>
    public GameObject confMainMenu;

    public Button associateButton;

    public GameObject associationMenu;

    public UsabilityTest usabilityTest;

    /// <summary>
    /// Unity Start function.
    /// </summary>
    void Start()
    {
        // Connect listeners to buttons
        saveButton.onClick.AddListener(save);
        backButton.onClick.AddListener(back);
        nextButton.onClick.AddListener(next);
        previousButton.onClick.AddListener(previous);
        nextButton.gameObject.SetActive(false);
        previousButton.gameObject.SetActive(false);
        prefabImage.gameObject.SetActive(false);
        removeButton.onClick.AddListener(remove);
        helpButton.onClick.AddListener(help);

        associateButton.onClick.AddListener(delegate { enabledAssociationMenu(false); });
        associateButton.interactable = false;

        // Manual at start 
        manualButton.onClick.AddListener(show_menu);
        manualCircleButtonX.onClick.AddListener(delegate { selectText(0); });
        manualCircleButtonY.onClick.AddListener(delegate { selectText(1); });
        manualCircleButtonZ.onClick.AddListener(delegate { selectText(2); });
        manualCircleButtonRot.onClick.AddListener(delegate { selectText(3); });
        manualCircleButtonScale.onClick.AddListener(delegate { selectText(4); });
        manualCircleButtonId.onClick.AddListener(delegate { selectText(5); });
        manualButtonMore.onClick.AddListener(delegate { changeManualMoreOrLess(true); });
        manualButtonLess.onClick.AddListener(delegate { changeManualMoreOrLess(false); });
        okButton.onClick.AddListener(changeByInput);

        // Starts with the manual menu hidden 
        changeManualInteractable(false);
        changeRemoveButtonInteractable(false);
    }

    /// <summary>
    /// Change the remove button interactivity.
    /// </summary>
    public void changeRemoveButtonInteractable(bool interactable)
    {
        removeButton.interactable = interactable;
    }

    /// <summary>
    /// Remove a virtual object from the scene.
    /// </summary>
    private void remove()
    {
        mainScript.remove();
    }

    /// <summary>
    /// Show or hide the help image when the help button is clicked.
    /// </summary>
    private void help()
    {
        helpImage.gameObject.SetActive(!helpImage.gameObject.activeSelf);
    }

    /// <summary>
    /// Shows the manual menu and changes the corresponding button text.
    /// </summary>
    void show_menu()
    {
        if (manualButton.GetComponentInChildren<Text>().text.Contains("Show"))
        {
            manualMenu.SetActive(true);
            manualButton.GetComponentInChildren<Text>().text = "Hide Manual";
            mainScript.drawSelectedObjectAxis();
        }
        else
        {
            manualMenu.SetActive(false);
            manualButton.GetComponentInChildren<Text>().text = "Show Manual";
            mainScript.destroySelectedObjectAxis();
        }
    }

    /// <summary>
    /// Position the arrow at the button clicked.
    /// </summary>
    /// <param name="index">Button clicked index. (Higher yy, lower index)</param>
    void selectText(int index)
    {
        switch (index)
        {
            case 0:
                manualArrow.transform.position = new Vector3(manualArrow.transform.position.x, manualCircleButtonX.transform.position.y, manualArrow.transform.position.z);
                break;
            case 1:
                manualArrow.transform.position = new Vector3(manualArrow.transform.position.x, manualCircleButtonY.transform.position.y, manualArrow.transform.position.z);
                break;
            case 2:
                manualArrow.transform.position = new Vector3(manualArrow.transform.position.x, manualCircleButtonZ.transform.position.y, manualArrow.transform.position.z);
                break;
            case 3:
                manualArrow.transform.position = new Vector3(manualArrow.transform.position.x, manualCircleButtonRot.transform.position.y, manualArrow.transform.position.z);
                break;
            case 4:
                manualArrow.transform.position = new Vector3(manualArrow.transform.position.x, manualCircleButtonScale.transform.position.y, manualArrow.transform.position.z);
                break;
            case 5:
                manualArrow.transform.position = new Vector3(manualArrow.transform.position.x, manualCircleButtonId.transform.position.y, manualArrow.transform.position.z);
                break;
        }
    }

    /// <summary>
    /// Translate, rotate or scale the virtual object for the next position, rotation or scale. (0.1f, 1f, 0.1f)
    /// </summary>
    /// <param name="moreOrLess">Negative or position increment.</param>
    void changeManualMoreOrLess(bool moreOrLess)
    {
        if (manualArrow.transform.position.y == manualCircleButtonX.transform.position.y)
        {
            mainScript.changeManualMoreOrLess(0, moreOrLess);
        }
        else if (manualArrow.transform.position.y == manualCircleButtonY.transform.position.y)
        {
            mainScript.changeManualMoreOrLess(1, moreOrLess);
        }
        else if (manualArrow.transform.position.y == manualCircleButtonZ.transform.position.y)
        {
            mainScript.changeManualMoreOrLess(2, moreOrLess);
        }
        else if (manualArrow.transform.position.y == manualCircleButtonRot.transform.position.y)
        {
            mainScript.changeManualMoreOrLess(3, moreOrLess);
        }
        else if ((int)manualArrow.transform.position.y == (int)manualCircleButtonScale.transform.position.y)
        {
            mainScript.changeManualMoreOrLess(4, moreOrLess);
        }
        else if (manualArrow.transform.position.y == manualCircleButtonId.transform.position.y)
        {
            mainScript.changeManualMoreOrLess(5, moreOrLess);
        }
    }

    /// <summary>
    /// Changes the virtual object position, rotation or scale for the new value inserted on the input field.
    /// </summary>
    void changeByInput()
    {
        if (manualArrow.transform.position.y == manualCircleButtonX.transform.position.y)
        {
            mainScript.changeManualByInput(0, (float)Convert.ToDouble(manualInputField.text));
        }
        else if (manualArrow.transform.position.y == manualCircleButtonY.transform.position.y)
        {
            mainScript.changeManualByInput(1, (float)Convert.ToDouble(manualInputField.text));
        }
        else if (manualArrow.transform.position.y == manualCircleButtonZ.transform.position.y)
        {
            mainScript.changeManualByInput(2, (float)Convert.ToDouble(manualInputField.text));
        }
        else if (manualArrow.transform.position.y == manualCircleButtonRot.transform.position.y)
        {
            mainScript.changeManualByInput(3, (float)Convert.ToDouble(manualInputField.text));
        }
        else if ((int)manualArrow.transform.position.y == (int)manualCircleButtonScale.transform.position.y)
        {
            mainScript.changeManualByInput(4, (float)Convert.ToDouble(manualInputField.text));
        }
        else if (manualArrow.transform.position.y == manualCircleButtonId.transform.position.y)
        {
            mainScript.changeManualByInput(5, (float)Convert.ToDouble(manualInputField.text));
        }
    }

    /// <summary>
    /// Changes the manual menu interactability if a virtual object is selected or not.
    /// </summary>
    /// <param name="interactable">Boolean controlling the manual menu interactability</param>
    public void changeManualInteractable(bool interactable)
    {
        manualInputField.interactable = interactable;
        manualButtonMore.interactable = interactable;
        manualButtonLess.interactable = interactable;
        manualCircleButtonX.interactable = interactable;
        manualCircleButtonY.interactable = interactable;
        manualCircleButtonZ.interactable = interactable;
        manualCircleButtonRot.interactable = interactable;
        manualCircleButtonId.interactable = interactable;
        manualCircleButtonScale.interactable = interactable;
        okButton.interactable = interactable;
    }

    /// <summary>
    /// Changes the manual menu interactability if a virtual object is selected or not.
    /// Used for anchors and for the starting prefab.
    /// </summary>
    /// <param name="interactable">Boolean controlling the manual menu interactability</param>
    public void changeManualInteractableAux(bool interactable)
    {
        manualInputField.interactable = interactable;
        manualButtonMore.interactable = interactable;
        manualButtonLess.interactable = interactable;
        manualCircleButtonX.interactable = interactable;
        manualCircleButtonY.interactable = interactable;
        manualCircleButtonZ.interactable = interactable;
        manualCircleButtonRot.interactable = interactable;
        manualCircleButtonId.interactable = false;
        manualCircleButtonScale.interactable = false;
        okButton.interactable = interactable;
    }

    /// <summary>
    /// Changes the manual menu interactability if a virtual object is selected or not.
    /// Used for non-path objects
    /// </summary>
    /// <param name="interactable">Boolean controlling the manual menu interactability</param>
    public void changeManualInteractableAux_v2(bool interactable)
    {
        manualInputField.interactable = interactable;
        manualButtonMore.interactable = interactable;
        manualButtonLess.interactable = interactable;
        manualCircleButtonX.interactable = interactable;
        manualCircleButtonY.interactable = interactable;
        manualCircleButtonZ.interactable = interactable;
        manualCircleButtonRot.interactable = interactable;
        manualCircleButtonId.interactable = false;
        manualCircleButtonScale.interactable = interactable;
        okButton.interactable = interactable;
    }

    /// <summary>
    /// Veirfy if the manual menu is intectable.
    /// </summary>
    /// <returns>The manual menu interactivity.</returns>
    public bool getManualInteractable()
    {
        return manualButtonMore.IsInteractable();
    }

    /// <summary>
    /// Changes the values on the manual menu UI texts.
    /// </summary>
    /// <param name="x">the virtual object x-coordinate.</param>
    /// <param name="y">the virtual object y-coordinate.</param>
    /// <param name="z">the virtual object z-coordinate.</param>
    /// <param name="rot">the virtual object yy rotation.</param>
    /// <param name="scale">the virtual object scale.</param>
    public void changeManualTexts(float x, float y, float z, float rot, float scale, int id)
    {
        xText.text = "X: " + Math.Round(x, 1).ToString("0.0");
        yText.text = "Y: " + Math.Round(y, 1).ToString("0.0");
        zText.text = "Z: " + Math.Round(z, 1).ToString("0.0");
        rotText.text = "Rotation: " + ((int)rot).ToString("0");
        scaleText.text = "Scale: " + Math.Round(scale, 1).ToString("0.0");
        idText.text = "Id: " + id.ToString();
    }

    /// <summary>
    /// Draws the virtual object world coordinate system.
    /// </summary>
    /// <param name="go">Corresponding virtual object.</param>
    public void drawAxis(GameObject go, GameObject initial_go = null)
    {
        if (manualButton.GetComponentInChildren<Text>().text.Contains("Hide"))
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                if (go.transform.GetChild(i).name.Contains("Axis"))
                {
                    go.transform.GetChild(i).gameObject.SetActive(true);
                    if(initial_go != null)
                    {
                        go.transform.GetChild(i).rotation = initial_go.transform.rotation;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Destroy the drawn virtual object world coordinate system.
    /// </summary>
    /// <param name="go">Corresponding virtual object.</param>
    public void destroyAxis(GameObject go)
    {
        for (int i = 0; i < go.transform.childCount; i++)
        {
            if (go.transform.GetChild(i).name.Contains("Axis"))
            {
                go.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Change to the next virtual object.
    /// </summary>
    private void next()
    {
        mainScript.ChangePrefab(1);
    }

    /// <summary>
    /// Change to the previous virtual object.
    /// </summary>
    private void previous()
    {
        mainScript.ChangePrefab(-1);
    }

    /// <summary>
    /// Change feedback text in the side menu.
    /// </summary>
    /// <param name="num">Number of anchors remaining.</param>
    /// <param name="index"></param>
    public void changeAddText(int num = 0, int index = 0)
    {
        if (saveButton.GetComponentInChildren<Text>().text.Contains("Objects"))
        {
            if (num > 0)
            {
                AnchorsText.text = "Set the object position.";
            }
            else
            {
                AnchorsText.text = "The required anchors are already instantiated.";
            }
        }
        else if (saveButton.GetComponentInChildren<Text>().text.Contains("Save"))
        {
            AnchorsText.text = "Press space to add a new object.";
        }
    }

    /// <summary>
    /// Change the save button interactivity.
    /// </summary>
    public void changeSaveButtonInteractable(bool interactable)
    {
        saveButton.interactable = interactable;
    }

    /// <summary>
    /// Get the save button interactivity.
    /// </summary>
    /// <returns>If the save button is interactable.</returns>
    public bool getSaveButtonInteractable()
    {
        return saveButton.interactable;
    }

    /// <summary>
    /// Change the object image on the side menu.
    /// </summary>
    public void changeSprite(int index)
    {
        prefabImage.sprite = mainScript.prefabs.getSpriteMap()[mainScript.prefabs.getPrefabNameArray()[index]];
    }

    /// <summary>
    /// Change the instruction text.
    /// </summary>
    /// <param name="selected">If any object is selected.</param>
    /// <param name="sentence">New sentence to switch the old one.</param>
    public void changeHelpText(bool selected = false, string sentence = "")
    {
        if (sentence == "")
        {
            if (saveButton.GetComponentInChildren<Text>().text.Contains("Anchors"))
            {
                HelpText.text = "Move and rotate sphere to the intended stating position (if any).";
            }
            else if (saveButton.GetComponentInChildren<Text>().text.Contains("Objects"))
            {
                if (selected)
                {
                    HelpText.text = "Move and rotate the anchor to the correct pose.";
                }
                else
                {
                    HelpText.text = "Select the anchor to change its pose.";
                }
            }
            else
            {
                if (selected)
                {
                    HelpText.text = "Change, move, rotate and scale the object to the intended pose.";
                }
                else
                {
                    HelpText.text = "Select the object that you intend to change.";
                }
            }
        }
        else
        {
            HelpText.text = sentence;
        }
    }
    /// <summary>
    /// Change to the next mode or write the scene information in the output file.
    /// </summary>
    void save()
    {
        if (saveButton.GetComponentInChildren<Text>().text.Contains("Objects"))
        {
            backButton.GetComponentInChildren<Text>().text = "Initial Pose";
            saveButton.GetComponentInChildren<Text>().text = "Save";
            mainScript.setMode(bezier.Modes.OBJECTS,true);
            changeAddText(mainScript.getNumOfAnchors());
            associateButton.interactable = true;
            //nextButton.gameObject.SetActive(true);
            //previousButton.gameObject.SetActive(true);
            changeSprite(0);
        }
        else if (saveButton.GetComponentInChildren<Text>().text.Contains("Initial Pose"))
        {
            usabilityTest.startTest();
            backButton.GetComponentInChildren<Text>().text = "Training";
            saveButton.GetComponentInChildren<Text>().text = "Objects";
            mainScript.setMode(bezier.Modes.STARTING, true);
            //nextButton.gameObject.SetActive(true);
            //previousButton.gameObject.SetActive(true);
        }
        else
        {
            mainScript.writeInFile();
        }
    }

    /// <summary>
    /// Returns to the previous menu or the previous mode.
    /// </summary>
    void back()
    {

        if (backButton.GetComponentInChildren<Text>().text.Contains("Conf. Menu"))
        {
            this.gameObject.SetActive(false);
            confMainMenu.SetActive(true);
            mainScript.backToMain();
            handleShowText(false);
        }
        else if(backButton.GetComponentInChildren<Text>().text.Contains("Training"))
        {
            saveButton.GetComponentInChildren<Text>().text = "Initial Pose";
            backButton.GetComponentInChildren<Text>().text = "Conf. Menu";
            mainScript.setMode(bezier.Modes.TRAINING, false);
            associateButton.interactable = false;
        }
        else if (backButton.GetComponentInChildren<Text>().text.Contains("Initial pose"))
        {
            saveButton.interactable = true;
            saveButton.GetComponentInChildren<Text>().text = "Objects";
            backButton.GetComponentInChildren<Text>().text = "Training";
            mainScript.setMode(bezier.Modes.STARTING, false);
            AnchorsText.text = "Set the QRCode position.";
            prefabImage.gameObject.SetActive(false);
            HelpText.text = "Set the QRCode position.";
            associateButton.interactable = false;
        }
    }

    /// <summary>
    /// Usability ending text.
    /// </summary>
    /// <param name="show">Enable usability ending text.</param>
    public void handleShowText(bool show)
    {
        endText.gameObject.SetActive(show);
        if (!show)
        {
            numObjectsText.gameObject.SetActive(show);
        }
    }

    /// <summary>
    /// Usability experience - Show the number of object placed and remaining.
    /// </summary>
    /// <param name="numObjs">Number of placed objects.</param>
    public void changeNumObjectsText(int numObjs)
    {
        if (!numObjectsText.gameObject.activeSelf)
        {
            numObjectsText.gameObject.SetActive(true);
        }
        numObjectsText.text = numObjs.ToString() + "/8";
        switch (numObjs)
        {
            case 0:
                numObjectsText.text = numObjectsText.text + " : 8-738-726-533 - Saco 325x230";
                break;
            case 1:
                numObjectsText.text = numObjectsText.text + " : 8-738-710-491 - Mangueira Flexivel";
                break;
            case 2:
                numObjectsText.text = numObjectsText.text + " : 8-709-918-680 - Pilha alcalina 1,5V LR20";
                break;
            case 3:
                numObjectsText.text = numObjectsText.text + " : 8-738-726-577 - Etiqueta";
                break;
            case 4:
                numObjectsText.text = numObjectsText.text + " : 8-710-103-045 - Anilha de vedação";
                break;
            case 5:
                numObjectsText.text = numObjectsText.text + " : 8-709-918-850 - Acessório de instalação";
                break;
            case 6:
                numObjectsText.text = numObjectsText.text + " : 7-709-003-556 - Acessório Nr.1083";
                break;
            case 7:
                numObjectsText.text = numObjectsText.text + " : 8-731-500-264 - Manípulo";
                break;
        }
    }

    public void enableAssociation(bool enable)
    {
        associateButton.interactable = enable;
    }

    public void enabledAssociationMenu(bool done)
    {
        if (associationMenu.activeSelf)
        {
            associateButton.transform.GetChild(0).GetComponent<Text>().text = "Associate Component";
        }
        else
        {
            associateButton.transform.GetChild(0).GetComponent<Text>().text = "Position Cube";
        }
        associationMenu.SetActive(!associationMenu.activeSelf);
        if (!done)
        {
            mainScript.transformCube(associationMenu.activeSelf);
        }
    }
}
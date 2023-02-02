using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuxConfigurationMenu : MonoBehaviour
{
    public Slider mode;

    public GameObject leanMenu;

    public GameObject axisMenu;

    public Slider rotScaleSlider;

    public GameObject rotationMenu;

    public Slider rotationAxisSlider;

    public Slider transformationsSlider;

    public bezier mainScript;

    // Start is called before the first frame update
    void Start()
    {
        mode.onValueChanged.AddListener(delegate { modeSlide(); });
        rotScaleSlider.onValueChanged.AddListener(delegate { rotScaleSlide(); });
        rotationAxisSlider.onValueChanged.AddListener(delegate { rotationAxisSlide(); });
        transformationsSlider.onValueChanged.AddListener(delegate { transformationsSlide(); });
    }

    public void rotScaleSlide()
    {
        mainScript.changeBetweenSlideRot((int)rotScaleSlider.value);
        if((int)rotScaleSlider.value == 0)
        {
            rotationMenu.SetActive(true);
        }
        else
        {
            rotationMenu.SetActive(false);
        }
    }

    public void modeSlide()
    {
        if ((int)mode.value == 0)
        {
            leanMenu.SetActive(true);
            axisMenu.SetActive(false);
            rotScaleSlide();
            rotationAxisSlide();
            mainScript.turnTranslation(true);
            mainScript.changeTransformation(-1);
        }
        else
        {
            leanMenu.SetActive(false);
            axisMenu.SetActive(true);
            mainScript.turnTranslation(false);
            transformationsSlide();
        }
    }

    public void transformationsSlide()
    {
        mainScript.changeTransformation((int)transformationsSlider.value);
    }

    public void rotationAxisSlide()
    {
        mainScript.changeRotationAxis((int)rotationAxisSlider.value);
    }
}

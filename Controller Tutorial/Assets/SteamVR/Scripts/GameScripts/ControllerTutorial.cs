using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControllerTutorial : MonoBehaviour
{
    public Material HighlightMaterial;
    public Material NormalMaterial;
    public Text text;

    private GameObject TriggerButton;
    private GameObject PadButton;
    private GameObject LeftGripButton;
    private GameObject RightGripButton;
    private GameObject MenuButton;
    private GameObject SystemButton;

    private Material TriggerMaterial;
    private Material PadMaterial;
    private Material LeftGripMaterial;
    private Material RightGripMaterial;
    private Material MenuMaterial;
    private Material SystemMaterial;

    private float secondsToWait = 3.0f;
    private float counter = 0.0f;
    private bool alreadyBeenHere = false;
    private bool menuPressed = false;

    // Use this for initialization
    void Start()
    {
        var trackedController = GetComponent<SteamVR_TrackedController>();
        if (trackedController == null)
        {
            trackedController = gameObject.AddComponent<SteamVR_TrackedController>();
        }

        trackedController.TriggerClicked += new ClickedEventHandler(TriggerClicked);
        trackedController.PadClicked += new ClickedEventHandler(PadClicked);
        trackedController.MenuButtonClicked += new ClickedEventHandler(MenuClicked);
        trackedController.Gripped += new ClickedEventHandler(GripClicked);
        trackedController.SteamClicked += new ClickedEventHandler(SystemClicked);

    }

    // Update is called once per frame
    void Update()
    {
        if (counter >= secondsToWait && !alreadyBeenHere)
        {
            alreadyBeenHere = true;

            Transform model = transform.Find("Model");
            TriggerButton = model.Find("trigger").gameObject;
            PadButton = model.Find("trackpad").gameObject;
            LeftGripButton = model.Find("lgrip").gameObject;
            RightGripButton = model.Find("rgrip").gameObject;
            MenuButton = model.Find("button").gameObject;
            SystemButton = model.Find("sys_button").gameObject;

            TriggerButton.GetComponent<Renderer>().material = HighlightMaterial;

            text.text = "Press the trigger button on the bottom of the controller. It will be highlighted in red.";
        }
        else
        {
            counter += Time.deltaTime;
        }
    }


    void TriggerClicked(object sender, ClickedEventArgs e)
    {
        text.text = "The button highlighted in red is called the trackpad. Notice you can slide your finger on it and it will track your movement. Go ahead and click on it.";
        TriggerButton.GetComponent<Renderer>().material = NormalMaterial;
        PadButton.GetComponent<Renderer>().material = HighlightMaterial;
    }

    void PadClicked(object sender, ClickedEventArgs e)
    {
        text.text = "The buttons highlighted in red are called the grip buttons. Go ahead and click on it.";
        PadButton.GetComponent<Renderer>().material = NormalMaterial;
        LeftGripButton.GetComponent<Renderer>().material = HighlightMaterial;
        RightGripButton.GetComponent<Renderer>().material = HighlightMaterial;
    }

    void GripClicked(object sender, ClickedEventArgs e)
    {
        text.text = "The button highlighted in red is called the menu button. This will be used to bring up in game menus. Go ahead and click on it.";
        LeftGripButton.GetComponent<Renderer>().material = NormalMaterial;
        RightGripButton.GetComponent<Renderer>().material = NormalMaterial;
        MenuButton.GetComponent<Renderer>().material = HighlightMaterial;
    }

    void SystemClicked(object sender, ClickedEventArgs e)
    {
    }

    void MenuClicked(object sender, ClickedEventArgs e)
    {
        if (!menuPressed)
        {
            text.text = "The button highlighted in red is called the system button. For now, don't click it, press the menu button again to go to the next scene.";
            MenuButton.GetComponent<Renderer>().material = NormalMaterial;
            SystemButton.GetComponent<Renderer>().material = HighlightMaterial;
            menuPressed = true;
        }
        else
        {
            SceneManager.LoadScene("MenuDemo", LoadSceneMode.Single);
        }

    }
}

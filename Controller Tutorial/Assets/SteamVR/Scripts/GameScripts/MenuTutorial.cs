using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuTutorial : MonoBehaviour {

    public Text text;


    private float secondsToWait = 6.0f;
    private float counter = 0.0f;
    private bool alreadyBeenHere = false;
    private ButtonOrder nextButton;

    // Use this for initialization
    void Start () {
        var trackedController = GetComponent<SteamVR_TrackedController>();
        if (trackedController == null)
        {
            trackedController = gameObject.AddComponent<SteamVR_TrackedController>();
        }

        trackedController.TriggerClicked += new ClickedEventHandler(TriggerClicked);
        nextButton = ButtonOrder.thisbutton;
    }

    // Update is called once per frame
    void Update () {
        if (counter >= secondsToWait)
        {
            if (!alreadyBeenHere)
            {
                alreadyBeenHere = true;


                text.text = "Aim the laser on the \"This\" button.";
            } else
            {
                Ray raycast = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(raycast, out hit, 15f))
                {
                    switch (hit.collider.tag)
                    {
                        case "ThisButton":
                            if (nextButton == ButtonOrder.thisbutton)
                            {
                                text.text = "Great! Now click the trigger button to select it. ";
                            }
                            
                            break;
                        case "ThatButton":
                            break;
                        case "NextTutorial":
                            break;
                    }
                }
            }
        }
        else
        {
            counter += Time.deltaTime;
        }
    }

    void TriggerClicked(object sender, ClickedEventArgs e)
    {
        Ray raycast = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(raycast, out hit, 15f))
        {
            switch (hit.collider.tag)
            {
                case "ThisButton":
                    if (nextButton == ButtonOrder.thisbutton)
                    {
                        text.text = "Good job! Now click the \"That\" button.";
                        nextButton = ButtonOrder.thatbutton;
                    }
                    break;
                case "ThatButton":
                    if (nextButton == ButtonOrder.thatbutton)
                    {
                        text.text = "Good job! Now let's go to the next tutorial. Click on the \"Next\" button.";
                        nextButton = ButtonOrder.nextbutton;
                    }
                    break;
                case "NextTutorial":
                    if (nextButton == ButtonOrder.nextbutton)
                    {
                        SceneManager.LoadScene("TeleporterDemo", LoadSceneMode.Single);
                    }
                    break;
            }
        }

    }

    private enum ButtonOrder {  thisbutton, thatbutton, nextbutton };
}



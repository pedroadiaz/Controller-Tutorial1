using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeleportingController : MonoBehaviour {

    public Text text;

    private float secondsToWait = 5.0f;
    private float counter = 0.0f;
    private bool alreadyBeenHere = false;
    // Use this for initialization
    Transform reference
    {
        get
        {
            var top = SteamVR_Render.Top();
            return (top != null) ? top.origin : null;
        }
    }



    void Start () {
        var trackedController = GetComponent<SteamVR_TrackedController>();
        if (trackedController == null)
        {
            trackedController = gameObject.AddComponent<SteamVR_TrackedController>();
        }

        trackedController.PadClicked += new ClickedEventHandler(TriggerClicked);

    }

    // Update is called once per frame
    void Update () {
        if (counter >= secondsToWait)
        {
            if (!alreadyBeenHere)
            {
                alreadyBeenHere = true;


                text.text = "Teleporting is how we get around in the Vive. Go ahead and point the laser at the ground.";
            }
            else
            {
            }
        }
        else
        {
            counter += Time.deltaTime;
        }

    }

    void TriggerClicked(object sender, ClickedEventArgs e)
    {
        var t = reference;
        if (t == null)
            return;

        float refY = t.position.y;

        Plane plane = new Plane(Vector3.up, -refY);
        Ray ray = new Ray(this.transform.position, transform.forward);

        bool hasGroundTarget = false;
        float dist = 0f;

        RaycastHit hitInfo;
        hasGroundTarget = Physics.Raycast(ray, out hitInfo);
        dist = hitInfo.distance;

        if (hasGroundTarget && hitInfo.collider.tag == "Teleporting")
        {
            text.text = "Notice you cannot teleport on a house.";
            Vector3 headPosOnGround = new Vector3(SteamVR_Render.Top().head.localPosition.x, 0.0f, SteamVR_Render.Top().head.localPosition.z);
            t.position = ray.origin + ray.direction * dist - new Vector3(t.GetChild(0).localPosition.x, 0f, t.GetChild(0).localPosition.z) - headPosOnGround;
        }

    }
}

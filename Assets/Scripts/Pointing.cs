using UnityEngine;

public enum BoxState
{
    Closed,
    Opened,
    Closed2,
    Opened2
}

public enum CameraState
{
    Up,
    Down
}

public class StateMachine : MonoBehaviour
{
    private BoxState boxState = BoxState.Closed;
    private CameraState cameraState = CameraState.Up;

    private GameObject The_box;



    void Start()
    {
        The_box = GameObject.Find("PrefScaledBox");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            int layerMask = LayerMask.GetMask("Ignore Raycast");
            if (Physics.Raycast(ray, out hit, 10, layerMask))
            {
                if (hit.collider.gameObject.tag == "Box")
                {
                    if (boxState == BoxState.Closed)
                    {
                        OpenBox(hit.transform.gameObject);
                        Debug.Log("Open");
                    }
                    else if (boxState == BoxState.Opened)
                    {
                        CloseBox(hit.transform.gameObject);
                        Debug.Log("Close");
                    }

                    UpdateCameraState();
                }
            }
        }

        

    }

    void OpenBox(GameObject Clicked)
    {
        Clicked.GetComponent<Animator>().Play("OpenLoc");
        boxState = BoxState.Opened;
    }

    void CloseBox(GameObject Clicked)
    {
        Clicked.GetComponent<Animator>().Play("CloseLoc");
        boxState = BoxState.Closed;
    }

    void UpdateCameraState()
    {
        if (cameraState == CameraState.Up)
        {
            this.GetComponent<Animator>().Play("CamMovOpen");
            cameraState = CameraState.Down;
        }
        else if (cameraState == CameraState.Down)
        {

            this.GetComponent<Animator>().Play("Base Layer.CamMoveClose");
            cameraState = CameraState.Up;
        }
    }
}

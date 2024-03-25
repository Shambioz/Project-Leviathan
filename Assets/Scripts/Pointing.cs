using UnityEngine;

public enum BoxState
{
    Closed,
    Opened
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
        The_box = GameObject.Find("ScaledBox");
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
                if (hit.collider.gameObject == The_box)
                {
                    if (boxState == BoxState.Closed)
                    {
                        OpenBox();
                    }
                    else if (boxState == BoxState.Opened)
                    {
                        CloseBox();
                    }

                    UpdateCameraState();
                }
            }
        }
    }

    void OpenBox()
    {
        The_box.GetComponent<Animator>().Play("Open");
        boxState = BoxState.Opened;
    }

    void CloseBox()
    {
        The_box.GetComponent<Animator>().Play("Close");
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

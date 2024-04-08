
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private GameObject The_box1;
    private GameObject The_box2;
    private GameObject The_box3;
    private bool abletoopen = true;
    private bool abletoclose = false;
    private bool isdraweropen = false;
    private bool isdrawerclose = true;


    void Start()
    {
        The_box1 = GameObject.Find("Upper");
        The_box2 = GameObject.Find("Middle");
        The_box3 = GameObject.Find("Lower");
    }

    void Update()
    {
        if (The_box1.GetComponent<BoxCollider>().enabled)
        {
            ManagingDrawers1();
        }
        if (The_box2.GetComponent<BoxCollider>().enabled)
        {
            ManagingDrawers2();
        }
        if (The_box3.GetComponent<BoxCollider>().enabled)
        {
            ManagingDrawers3();
        }
        if (The_box1.GetComponent<BoxCollider>().enabled)
        {
            PlayButton();
        }


    }


    void ManagingDrawers1()
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
                    if (boxState == BoxState.Closed && isdrawerclose)
                    {
                        OpenBox(hit.transform.gameObject);
                        Debug.Log("Open");
                        isdrawerclose = false;
                        isdraweropen = true;

                        The_box2.GetComponent<BoxCollider>().enabled = false;
                        The_box3.GetComponent<BoxCollider>().enabled = false;
                        UpdateCameraState();
                    }
                    else if (boxState == BoxState.Opened && !isdrawerclose)
                    {
                        CloseBox(hit.transform.gameObject);
                        Debug.Log("Close");
                        isdraweropen = false;
                        isdrawerclose = true;


                        The_box2.GetComponent<BoxCollider>().enabled = true;
                        The_box3.GetComponent<BoxCollider>().enabled = true;
                        UpdateCameraState();
                        Debug.Log("hello");
                    }


                }

            }
        }
    }

    void ManagingDrawers2()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            int layerMask = LayerMask.GetMask("Ignore Raycast");
            if (Physics.Raycast(ray, out hit, 10, layerMask))
            {
                if (hit.collider.gameObject.tag == "Box2")
                {
                    if (boxState == BoxState.Closed && isdrawerclose)
                    {
                        OpenBox(hit.transform.gameObject);
                        Debug.Log("Open");
                        isdrawerclose = false;
                        isdraweropen = true;

                        The_box1.GetComponent<BoxCollider>().enabled = false;
                        The_box3.GetComponent<BoxCollider>().enabled = false;
                        UpdateCameraState();
                    }
                    else if (boxState == BoxState.Opened && !isdrawerclose)
                    {
                        CloseBox(hit.transform.gameObject);
                        Debug.Log("Close");
                        isdraweropen = false;
                        isdrawerclose = true;

                        The_box1.GetComponent<BoxCollider>().enabled = true;
                        The_box3.GetComponent<BoxCollider>().enabled = true;
                        UpdateCameraState();
                    }
                }
            }
        }
    }
    void ManagingDrawers3()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            int layerMask = LayerMask.GetMask("Ignore Raycast");
            if (Physics.Raycast(ray, out hit, 10, layerMask))
            {
                if (hit.collider.gameObject.tag == "Box3")
                {
                    if (boxState == BoxState.Closed && isdrawerclose)
                    {
                        OpenBox(hit.transform.gameObject);
                        Debug.Log("Open");
                        isdrawerclose = false;
                        isdraweropen = true;

                        The_box1.GetComponent<BoxCollider>().enabled = false;
                        The_box2.GetComponent<BoxCollider>().enabled = false;
                        UpdateCameraState();
                    }
                    else if (boxState == BoxState.Opened && !isdrawerclose)
                    {
                        CloseBox(hit.transform.gameObject);
                        Debug.Log("Close");
                        isdraweropen = false;
                        isdrawerclose = true;

                        The_box1.GetComponent<BoxCollider>().enabled = true;
                        The_box2.GetComponent<BoxCollider>().enabled = true;
                        UpdateCameraState();
                    }
                }
            }
        }
    }
    void PlayButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            int layerMask = LayerMask.GetMask("Ignore Raycast");
            if (Physics.Raycast(ray, out hit, 10, layerMask))
            {
                if (hit.collider.gameObject.tag == "Button")
                {
                    SceneManager.LoadScene("SampleScene");
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
        if (cameraState == CameraState.Up && isdraweropen)
        {
            this.GetComponent<Animator>().Play("CamMovOpen");
            cameraState = CameraState.Down;
        }
        else if (cameraState == CameraState.Down && isdrawerclose)
        {

            this.GetComponent<Animator>().Play("Base Layer.CamMoveClose");
            cameraState = CameraState.Up;
        }
        
    }
}

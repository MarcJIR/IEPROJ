using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArrowMovement : MonoBehaviour
{
    public float speedOffset = 0.02f;
    private float speed = 0.0f;
    public int ArrowsRemaining = 5;
    public float TimeRemaining = 10.0f;
    public bool TimeRunning = false;
    private GameObject[] targetList;
    public int targetNumber = 1;


    [SerializeField] private Text text;
    [SerializeField] private static List<GameObject> objectsSpawned = new List<GameObject>();

    private Vector3 start = new Vector3(0, 0);
    private Vector3 end = new Vector3(0, 0);
    private Vector3 dir = new Vector3(0, 0);
    
    private Vector3 lineEnd = new Vector3(0, 0);
    private Vector3 lineDir = new Vector3(0, 0);
    private List<GameObject> trajectory = new List<GameObject>();

    public int trajectoryCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        if (UIReceiver.Mode == "Classic") text.text = ArrowsRemaining.ToString();
        else if (UIReceiver.Mode == "Endless")
        {
            text.text = Mathf.CeilToInt(TimeRemaining).ToString();
            TimeRunning = true;
        }
        targetList = GameObject.FindGameObjectsWithTag("Target");
        targetNumber = targetList.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(targetList.Length == 0)
        { 
            targetList = GameObject.FindGameObjectsWithTag("Target");
            targetNumber = targetList.Length;
        }
        if (Time.timeScale == 0) return;
        if(UIReceiver.Mode == "Classic")
        {
            EventSystem.current.SetSelectedGameObject(GameObject.Find("Canvas").transform.Find("ClassicPause").gameObject);
            bool hovering = EventSystem.current.IsPointerOverGameObject();
            if (ArrowsRemaining > 0 && !hovering)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    start = Input.mousePosition;
                    //Debug.Log("Start: " + start);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    end = Input.mousePosition;
                    dir = Vector3.Normalize(start - end);
                    speed = Vector3.Distance(start, end) * speedOffset;
                    foreach (GameObject traj in trajectory)
                    {
                        Destroy(traj);
                    }
                    trajectory.Clear();
                    //Debug.Log(speed);
                    //Debug.Log("Start - End: " + (start - end));
                    //Debug.Log("Start: " + start + ", End: " + end + ", Direction: " + dir);
                    if (dir != Vector3.zero)
                    {
                        GameObject myObject = this.SpawnDefault();
                        ArrowsRemaining--;
                        text.text = ArrowsRemaining.ToString();
                        Vector3 position = myObject.transform.localPosition;
                        position.y = 1.85f;
                        position.z = 1.0f;

                        Vector3 rotation = myObject.transform.localEulerAngles;
                        if (dir.x < 0) position.x = 1.7f;
                        else if (dir.x >= 0) position.x = -1.7f;
                        rotation.z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        rotation.y = 180;
                        myObject.transform.localPosition = position;
                        myObject.transform.localEulerAngles = rotation;

                        Vector3 playerRotation = this.transform.eulerAngles;
                        if (dir.x < 0) playerRotation.y = 0;
                        else if (dir.x >= 0) playerRotation.y = 180;
                        this.transform.eulerAngles = playerRotation;

                        Rigidbody rObject = myObject.GetComponent<Rigidbody>();
                        rObject.constraints = RigidbodyConstraints.FreezePositionZ;
                        rObject.constraints = RigidbodyConstraints.FreezeRotationY;
                        rObject.velocity = new Vector3(dir.x * speed, (dir.y * speed), 0.0f);
                        objectsSpawned.Add(myObject);
                    }
                }
                if(Input.GetMouseButton(0))
                {
                    lineEnd = Input.mousePosition;
                    lineDir = Vector3.Normalize(start - lineEnd);
                    speed = Vector3.Distance(start, lineEnd) * speedOffset;
                    if(trajectory.Count == 0)
                    {
                        for(int i = 0; i < trajectoryCount; i++)
                        {
                            GameObject gObject = GameObject.Instantiate(Resources.Load("Prefabs/Trajectory Marker") as GameObject);
                            trajectory.Add(gObject);
                        }

                        Vector3 position = trajectory[0].transform.localPosition;
                        position.y = 2.42f;
                        position.z = 1.85f;
                        if (lineDir.x < 0) position.x = -3.85f;
                        else if (lineDir.x >= 0) position.x = -2.16f;
                        trajectory[0].transform.localPosition = position;
                        
                        for (int i = 1; i < trajectoryCount; i++)
                        {
                            Vector3 nposition = position + new Vector3(lineDir.x * speed, lineDir.y * speed, 0.0f) * (i * 0.1f) + (0.5f * Physics.gravity * (i * 0.1f) * (i * 0.1f));
                            trajectory[i].transform.localPosition = nposition;
                        }
                    }
                    if(trajectory.Count > 0)
                    {
                        Vector3 position = trajectory[0].transform.localPosition;
                        position.y = 2.42f;
                        position.z = 1.85f;
                        if (lineDir.x < 0) position.x = -3.85f;
                        else if (lineDir.x >= 0) position.x = -2.16f;
                        trajectory[0].transform.localPosition = position;
                        for (int i = 1; i < trajectoryCount; i++)
                        {
                            Vector3 nposition = position + new Vector3(lineDir.x * speed, lineDir.y * speed, 0.0f) * (i * 0.1f) + (0.5f * Physics.gravity * (i * 0.1f) * (i * 0.1f));
                            trajectory[i].transform.localPosition = nposition;
                        }
                    }

                }
            }
            
        }
        if(UIReceiver.Mode == "Endless" && TimeRunning)
        {
            EventSystem.current.SetSelectedGameObject(GameObject.Find("Canvas").transform.Find("EndlessPause").gameObject);
            bool hovering = EventSystem.current.IsPointerOverGameObject();
            if (TimeRemaining > 0.0f && !hovering)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    start = Input.mousePosition;
                    Debug.Log("Start: " + start);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    end = Input.mousePosition;
                    dir = Vector3.Normalize(start - end);
                    Debug.Log("Start - End: " + (start - end));
                    Debug.Log("Start: " + start + ", End: " + end + ", Direction: " + dir);
                    if (dir != Vector3.zero)
                    {
                        GameObject myObject = this.SpawnDefault();
                        Vector3 position = myObject.transform.localPosition;
                        position.y = 0.2f;
                        position.z = 0.0f;

                        Vector3 rotation = myObject.transform.localEulerAngles;
                        if (dir.x < 0) position.x = -0.7f;
                        else if (dir.x >= 0) position.x = 0.7f;
                        rotation.z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        myObject.transform.localPosition = position;
                        myObject.transform.localEulerAngles = rotation;

                        Rigidbody rObject = myObject.GetComponent<Rigidbody>();
                        rObject.constraints = RigidbodyConstraints.FreezePositionZ;
                        rObject.constraints = RigidbodyConstraints.FreezeRotationY;
                        rObject.velocity = new Vector3(dir.x * speed, (dir.y * speed), 0.0f);
                        objectsSpawned.Add(myObject);
                    }
                }
                TimeRemaining -= Time.deltaTime;
                text.text = Mathf.CeilToInt(TimeRemaining).ToString();
            }
            else if (TimeRemaining <= 0.0f)
            {
                TimeRemaining = 0.0f;
                TimeRunning = false;
                LoadManager.Instance.LoadSceneAdditive(SceneNames.ENDLESS_RESULTS, false);
                GameOver();
            }
        }
        foreach(GameObject obj in objectsSpawned)
        {
            if(obj.transform.position.x >= 15.2f || obj.transform.position.x <= -3.2f)
            { 
                if(ArrowsRemaining <= 0)
                {
                    LoadManager.Instance.LoadSceneAdditive(SceneNames.CLASSIC_FAIL, false);
                    GameOver();
                }
                else Destroy(obj);
                objectsSpawned.Remove(obj);
            }
        }
    }

    private void FixedUpdate()
    {
        foreach (GameObject myObject in objectsSpawned)
        {   
            if(myObject != null && myObject.GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                Vector3 rb = myObject.GetComponent<Rigidbody>().velocity;
                Vector3 rotation = myObject.transform.localEulerAngles;
                rotation.z = Mathf.Atan2(rb.y, rb.x) * Mathf.Rad2Deg;
                myObject.transform.localEulerAngles = rotation;
            }
        }
    }

    private GameObject SpawnDefault()
    {
        GameObject myObject = GameObject.Instantiate(Resources.Load("Prefabs/Arrow(marc)") as GameObject, this.transform);
        myObject.SetActive(true);

        return myObject;
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        foreach(GameObject obj in objectsSpawned)
        {
            Destroy(obj);
        }
        objectsSpawned.Clear();
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArrowMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public int ArrowsRemaining = 5;
    public float TimeRemaining = 10.0f;
    public bool TimeRunning = false;
    [SerializeField] private Text text;
    [SerializeField] private GameObject templateObject;
    [SerializeField] private static List<GameObject> objectsSpawned = new List<GameObject>();

    private Vector3 start = new Vector3(0, 0);
    private Vector3 end = new Vector3(0, 0);
    private Vector3 dir = new Vector3(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        templateObject.SetActive(false);
        if (UIReceiver.Mode == "Classic") text.text = ArrowsRemaining.ToString();
        else if (UIReceiver.Mode == "Endless")
        {
            text.text = Mathf.CeilToInt(TimeRemaining).ToString();
            TimeRunning = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(UIReceiver.Mode);
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
                        ArrowsRemaining--;
                        text.text = ArrowsRemaining.ToString();
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
            }
            else if (ArrowsRemaining <= 0)
            {
                LoadManager.Instance.LoadSceneAdditive(SceneNames.CLASSIC_FAIL, false);
                GameOver();
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
    }

    private void FixedUpdate()
    {
        foreach (GameObject myObject in objectsSpawned)
        {   
            if(myObject != null)
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
        GameObject myObject = GameObject.Instantiate(this.templateObject, this.transform);
        myObject.SetActive(true);

        return myObject;
    }

    public static void GameOver()
    {
        Time.timeScale = 0.0f;
        foreach(GameObject obj in objectsSpawned)
        {
            Destroy(obj);
        }
    }
}

//if (Input.touchSupported)
//{
//    if (Input.touchCount > 0)
//    {
//        Touch touch = Input.GetTouch(0);
//        //if(touch.phase == TouchPhase.Began)
//        //{
//        //    Vector3 iPosition = touch.position;
//        //}
//        if (touch.phase == TouchPhase.Ended)
//        {
//            Vector3 dPosition = touch.deltaPosition;
//            xDis = dPosition.x / 10.0f;
//            yDis = dPosition.y / 10.0f;
//            Debug.Log("x: " + xDis + ", y: " + yDis);

//            GameObject myObject = this.SpawnDefault();
//            Vector3 position = myObject.transform.localPosition;
//            position.x = 0.2f;
//            position.y = 0.2f;
//            position.z = 0.0f;
//            myObject.transform.localPosition = position;

//            Rigidbody rObject = myObject.GetComponent<Rigidbody>();
//            rObject.velocity = new Vector3(xDis, yDis, 0.0f);
//            this.objectsSpawned.Add(myObject);
//        }
//    }
//}

//else
//{ }


//if (Input.GetMouseButtonDown(0))
//{
//GameObject myObject = this.SpawnDefault();
//Vector3 position = myObject.transform.localPosition;
//position.x = 0.2f;
//position.y = 0.2f;
//position.z = 0.0f;
//myObject.transform.localPosition = position;

//Rigidbody rObject = myObject.GetComponent<Rigidbody>();
//rObject.velocity = new Vector3(5.0f, 5.0f, 0.0f);
//this.objectsSpawned.Add(myObject);
//}
//Vector3 start = new Vector3(0.0f, 0.0f);
//Vector3 end = new Vector3(0.0f, 0.0f);
//Vector3 distance = new Vector3(0.0f, 0.0f);
//RaycastHit hit;
//Ray ray = cam.ScreenPointToRay(Input.mousePosition);

//if (Input.GetMouseButtonDown(0))
//{
//    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
//    {
//        start = hit.point;
//    }
//}

//if (Input.GetMouseButtonUp(0) && !(start.Equals((0.0f, 0.0f))))
//{
//    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
//    {
//        end = hit.point;
//        distance = start - end;
//    }
//    Debug.Log("Start: " + start + ", End: " + end);
//    Debug.Log("Distance: " + distance);
//    //Vector3 fPos = Input.mousePosition;
//    //xDis = (fPos.x - iPos.x) / 10.0f;
//    //yDis = (fPos.y - iPos.y) / 10.0f;
//    //Debug.Log("Initial: " + iPos + ", Final: " + fPos);
//    //Debug.Log("x: " + xDis + ", y: " + yDis);

//    GameObject myObject = this.SpawnDefault();
//    numArrows--;
//    text.text = numArrows.ToString();
//    Vector3 position = myObject.transform.localPosition;
//    position.x = 0.7f;
//    position.y = 0.2f;
//    position.z = 0.0f;
//    myObject.transform.localPosition = position;


//    Rigidbody rObject = myObject.GetComponent<Rigidbody>();
//    if (rObject == null)
//    {
//        myObject.AddComponent<Rigidbody>();
//        rObject = myObject.GetComponent<Rigidbody>();
//    }
//    //rObject.isKinematic = true;
//    rObject.constraints = RigidbodyConstraints.FreezePositionZ;
//    rObject.velocity = new Vector3(distance.x * 3.0f, -(distance.y * 2.0f), 0.0f);
//    Debug.Log(rObject.velocity);
//    this.objectsSpawned.Add(myObject);
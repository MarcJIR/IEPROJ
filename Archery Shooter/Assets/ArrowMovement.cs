﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArrowMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public int ArrowsRemaining = 5;
    [SerializeField] private Text text;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject templateObject;
    [SerializeField] private LayerMask layermask;
    [SerializeField] private List<GameObject> objectsSpawned = new List<GameObject>();
    private Vector3 start = new Vector3(0, 0);
    private Vector3 end = new Vector3(0, 0);
    private Vector3 dir = new Vector3(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        templateObject.SetActive(false);
        text.text = ArrowsRemaining.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        float xDis;
        float yDis;
        int numArrows = int.Parse(text.text);
        EventSystem.current.SetSelectedGameObject(GameObject.Find("Canvas").transform.Find("ClassicPause").gameObject);
        bool hovering = EventSystem.current.IsPointerOverGameObject();
        if (numArrows > 0 && !hovering)
        {
            if (Input.touchSupported)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    //if(touch.phase == TouchPhase.Began)
                    //{
                    //    Vector3 iPosition = touch.position;
                    //}
                    if (touch.phase == TouchPhase.Ended)
                    {
                        Vector3 dPosition = touch.deltaPosition;
                        xDis = dPosition.x / 10.0f;
                        yDis = dPosition.y / 10.0f;
                        Debug.Log("x: " + xDis + ", y: " + yDis);

                        GameObject myObject = this.SpawnDefault();
                        Vector3 position = myObject.transform.localPosition;
                        position.x = 0.2f;
                        position.y = 0.2f;
                        position.z = 0.0f;
                        myObject.transform.localPosition = position;

                        Rigidbody rObject = myObject.GetComponent<Rigidbody>();
                        rObject.velocity = new Vector3(xDis, yDis, 0.0f);
                        this.objectsSpawned.Add(myObject);
                    }
                }
            }

            else if(Input.mousePresent)
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
                    Debug.Log("Start: " + start + ", End: " + end +  ", Direction: " + dir);
                    if (dir != Vector3.zero)
                    {
                        GameObject myObject = this.SpawnDefault();
                        numArrows--;
                        text.text = numArrows.ToString();
                        Vector3 position = myObject.transform.localPosition;
                        position.x = 0.7f;
                        position.y = 0.2f;
                        position.z = 0.0f;
                        //if(dir.x < 0)
                        //{
                        //    myObject.transform.localRotation
                        //}
                        myObject.transform.localPosition = position;

                        Rigidbody rObject = myObject.GetComponent<Rigidbody>();
                        rObject.constraints = RigidbodyConstraints.FreezePositionZ;
                        rObject.constraints = RigidbodyConstraints.FreezeRotationY;
                        rObject.velocity = new Vector3(dir.x * speed, (dir.y * speed), 0.0f);
                        this.objectsSpawned.Add(myObject);
                    }
                }

                

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
            
            }
        }

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
    }

    private GameObject SpawnDefault()
    {
        GameObject myObject = GameObject.Instantiate(this.templateObject, this.transform);
        myObject.SetActive(true);

        return myObject;
    }
}

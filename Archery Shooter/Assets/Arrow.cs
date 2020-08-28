using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10.0f;
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject templateObject;
    [SerializeField] private List<GameObject> objectsSpawned = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        templateObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float xDis;
        float yDis;
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

        else
        {
            Vector3 start = new Vector3(0.0f, 0.0f);
            Vector3 end = new Vector3(0.0f, 0.0f);
            Vector3 distance = new Vector3(0.0f, 0.0f);
            RaycastHit hit;

            if (Input.GetMouseButtonDown(0))
            { 
                if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    start = hit.point;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    end = hit.point;
                    distance = start - end;
                }
                Debug.Log("Start: " + start + ", End: " + end);
                Debug.Log("Distance: " + distance);
                //Vector3 fPos = Input.mousePosition;
                //xDis = (fPos.x - iPos.x) / 10.0f;
                //yDis = (fPos.y - iPos.y) / 10.0f;
                //Debug.Log("Initial: " + iPos + ", Final: " + fPos);
                //Debug.Log("x: " + xDis + ", y: " + yDis);

                GameObject myObject = this.SpawnDefault();
                Vector3 position = myObject.transform.localPosition;
                position.x = 0.2f;
                position.y = 0.2f;
                position.z = 0.0f;
                myObject.transform.localPosition = position;

                Rigidbody rObject = myObject.GetComponent<Rigidbody>();
                rObject.constraints = RigidbodyConstraints.FreezePositionZ;
                rObject.velocity = new Vector3(distance.x * 3.0f, -(distance.y * 2.0f), 0.0f);
                Debug.Log(rObject.velocity);
                this.objectsSpawned.Add(myObject);
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

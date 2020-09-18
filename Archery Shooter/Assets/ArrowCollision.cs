using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollision : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private bool impacted = false;
    private float skinWidth = 0.1f; //probably doesn't need to be changed 
    private float minimumExtent;
    private float partialExtent;
    private float sqrMinimumExtent;
    private Vector3 previousPosition;
    private Rigidbody myRigidbody;

    private Vector3 lastPosition;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        previousPosition = myRigidbody.position;
        minimumExtent = Mathf.Min(Mathf.Min(GetComponent<Collider>().bounds.extents.x, GetComponent<Collider>().bounds.extents.y), GetComponent<Collider>().bounds.extents.z);
        partialExtent = minimumExtent * (1.0f - skinWidth);
        sqrMinimumExtent = minimumExtent * minimumExtent;

    }


    void FixedUpdate()
    {


        Vector3 direction = transform.position - lastPosition;
        Ray ray = new Ray(lastPosition, direction);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, direction.magnitude, layerMask))
        {
            Impact(hit.point, hit.normal, hit.collider.gameObject);
        }

        this.lastPosition = transform.position;
    }


    void OnCollisionEnter(Collision collision)
    {

        if (impacted == true)
        {
            return;
        }
        Impact(collision.contacts[0].point, collision.contacts[0].normal, collision.collider.gameObject);
        impacted = true;
    }
    void Impact(Vector3 pos, Vector3 normal, GameObject hitObject)
    {
        //Apply damage to the hit gameObject, or whatever
        //Destroy(gameObject);
        myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        if (hitObject.CompareTag("Target"))
        {
            if (UIReceiver.Mode == "Classic")
            {
                LoadManager.Instance.LoadSceneAdditive(SceneNames.CLASSIC_SUCCESS, false);
                ArrowMovement.GameOver();
            }
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingShot : MonoBehaviour
{
    //Set Dynamically
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectiile;
    public bool aimingMode;
    private Rigidbody progectiliRigidbody;
    //Set in Inspector
    public GameObject prefabProgectile;
    public float velocityMult = 8f;
    
    

    private void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }
    private void OnMouseEnter()
    {
        //print("SingShot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }
    private void OnMouseExit()
    {
        //print("SingShot:OnMouseExit()");
        launchPoint.SetActive(false);

    }
    private void OnMouseDown()
    {
        aimingMode = true;
        //Create shot
        projectiile = Instantiate(prefabProgectile) as GameObject;
        projectiile.transform.position = launchPos;
        projectiile.GetComponent<Rigidbody>().isKinematic = true;
        progectiliRigidbody = projectiile.GetComponent<Rigidbody>();
        progectiliRigidbody.isKinematic = true;


    }
    private void Update()
    {
        if (!aimingMode) return;
        Vector3 mousePoint2D = Input.mousePosition;
        mousePoint2D.z = -Camera.main.transform.position.z;
        Vector3 mousePoint3D = Camera.main.ScreenToWorldPoint(mousePoint2D);
        Vector3 mouseDelta = mousePoint3D - launchPos;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if(mouseDelta.magnitude>maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        Vector3 projpos = launchPos + mouseDelta;
        projectiile.transform.position = projpos;
        if(Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            progectiliRigidbody.isKinematic = false;
            progectiliRigidbody.velocity = -mouseDelta * velocityMult;
            FollowCameraToShot.POI = projectiile;
            projectiile = null;
            MissionDemolition.ShowFired();

        }
    }
}

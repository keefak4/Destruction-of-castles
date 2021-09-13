using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClouCrafter : MonoBehaviour
{
    public int numClouds = 40; //number of clouds
    public GameObject cloudPrefab; //Prefab of clouds
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1; //min scale clouds
    public float cloudScaleMax = 3;//max scale clouds
    public float cloudSpeedMult = 0.5f;//speed clouds
    private GameObject[] cloudInstances;
    private void Awake()
    {
        cloudInstances = new GameObject[numClouds];
        GameObject _CloudAnchor = GameObject.Find("CloudAnchor");
        GameObject cloud;
        for(int i = 0;i<numClouds;i++)
        {
            cloud = Instantiate<GameObject>(cloudPrefab);
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y,cloudPosMax.y);
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);//Меньшие облака должны быть блиэе к земеле
            cPos.z = 100 - 90 * scaleU;//меньшие облака должны быть дальше
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            cloud.transform.SetParent(_CloudAnchor.transform);//сделать облако дочерним по отношению к _CloudAnchor
            cloudInstances[i] = cloud;
        }
    }
    private void Update()
    {
        foreach (GameObject cloud in cloudInstances)
        {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;//up speed near cloud
            if(cPos.x <= cloudPosMin.x)
            {
                cPos.x = cloudPosMax.x;
            }
            cloud.transform.position = cPos;

        }    
    }



}

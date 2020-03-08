using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCube : MonoBehaviour
{
    public GameObject cube;
    public GameObject measurementCube;
    private GameObject _posCube; 
    public Vector3 startPos;
    public Vector3 finalPos;
    public Quaternion finalRot;
    private float _counter; 
    void Start()
    {
        startPos = transform.TransformPoint(gameObject.transform.localPosition);
        Instantiate(cube, startPos, Quaternion.identity, gameObject.transform);
        
    }

    // Update is called once per frame
    void Update()
    {
        cube.transform.position = transform.parent.localPosition;
        finalPos = transform.TransformPoint(cube.transform.localPosition);
        finalRot = transform.localRotation;
        Debug.Log(finalRot);
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch) || Input.GetKeyDown(KeyCode.K))
        {
            PlaceCube();
        }

        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch) || Input.GetKeyDown(KeyCode.A))
        {
            DestroyCube();
        }
    }

    void PlaceCube()
    {
        
        _posCube = Instantiate(measurementCube, finalPos, finalRot);
        _posCube.tag = "Trackers"; 
        

    }


    void DestroyCube()
    {
    Destroy(GameObject.FindWithTag("Trackers"));
    }
}
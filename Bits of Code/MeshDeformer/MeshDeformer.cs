using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour
{
    private Mesh _deformingMesh;

    private Vector3[] _originalVertices, _displacedVertices, _vertexVelocities;

    public float springForce = 20f;

    public float damping = 5f;

    public float uniformScale = 1f; 
    // Start is called before the first frame update
    void Start()
    {
        _deformingMesh = GetComponent<MeshFilter>().mesh;
        _originalVertices = _deformingMesh.vertices;
        _displacedVertices = new Vector3[_originalVertices.Length];
        for (int i = 0; i < _originalVertices.Length; i++)
        {
            _displacedVertices[i] = _originalVertices[i];
        }
        _vertexVelocities = new Vector3[_originalVertices.Length];
    }

    // Update is called once per frame
    void Update()
    {
        uniformScale = transform.localScale.x;
        for (int i = 0; i < _displacedVertices.Length; i++)
        {
            UpdateVertex(i);
        }

        _deformingMesh.vertices = _displacedVertices;
        _deformingMesh.RecalculateNormals();

    }

    public void AddDeformingForce(Vector3 point, float force)
    {
        Debug.DrawLine(Camera.main.transform.position, point);

        point = transform.InverseTransformPoint(point);
        for (int i = 0; i < _displacedVertices.Length; i++)
        {
            AddForceToVertex(i, point, force);
        }
    }

    void AddForceToVertex(int i, Vector3 point, float force)
    {
        Vector3 pointToVertex = _displacedVertices[i] - point;
        pointToVertex *= uniformScale;
        float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
        float velocity = attenuatedForce * Time.deltaTime;
        _vertexVelocities[i] += pointToVertex.normalized * velocity; 
    }

    void UpdateVertex(int i)
    {
        Vector3 velocity = _vertexVelocities[i];
        Vector3 displacement = _displacedVertices[i] - _originalVertices[i];
        displacement *= uniformScale;
        velocity -= displacement * springForce * Time.deltaTime;
        velocity *= 1f - damping * Time.deltaTime; 
        _vertexVelocities[i] = velocity;
        _displacedVertices[i] += velocity * (Time.deltaTime / uniformScale);
    }
}

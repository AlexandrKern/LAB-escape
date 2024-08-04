using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRendererSetter : MonoBehaviour
{
    public SkinnedMeshRenderer meshRenderer;
    void Start()
    {
        var shape = GetComponent<ParticleSystem>().shape;
        shape.skinnedMeshRenderer = meshRenderer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

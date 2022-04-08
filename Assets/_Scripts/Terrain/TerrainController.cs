using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public Terrain terrainM;
    public float detailDistance;
    void Start()
    {
        terrainM = gameObject.GetComponent<Terrain>();
        terrainM.detailObjectDistance = detailDistance;
    }
}

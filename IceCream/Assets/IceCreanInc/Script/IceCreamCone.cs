using UnityEngine;
using UnityEngine.Splines;


public class IceCreamCone : MonoBehaviour
{
    [SerializeField] private Transform iceCreamStorage;
    [SerializeField] private SplineContainer splineContainer;


    public Transform GetCreamStorage()
    {
        return iceCreamStorage;
    }

    public SplineContainer GetSplineContainer()
    {
        return splineContainer;
    }

}

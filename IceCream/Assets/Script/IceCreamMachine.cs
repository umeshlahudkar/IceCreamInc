using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;

public class IceCreamMachine : MonoBehaviour
{
    [SerializeField] private IceCreamCone iceCreamCone;
    [SerializeField] private Transform thisTransform;
    [SerializeField] private Transform creamOutlet;
    [SerializeField] private GameObject iceCreamPiecePrefab;

    [SerializeField] private float pouringSpeed;

    [SerializeField] private Flavor[] flavors;

    private SplineContainer splineContainer;
    private float initialHeight;
    private Vector3 position;
    private bool isRunning;
    private float elapcedTime;

    private Flavor currentFlavour;

    private ObjectPool<MeshRenderer> pool;


    private void Start()
    {
        initialHeight = gameObject.transform.position.y;
        splineContainer = iceCreamCone.GetSplineContainer();

        pool = new ObjectPool<MeshRenderer>(iceCreamPiecePrefab.GetComponent<MeshRenderer>(), iceCreamCone.GetCreamStorage() ,500);
    }

    private void FixedUpdate()
    {
        if(isRunning && elapcedTime <= 1.0f)
        {
            elapcedTime += Time.fixedDeltaTime * pouringSpeed;
            position = splineContainer.EvaluatePosition(elapcedTime);

            float y = position.y;

            position.y = initialHeight;
            transform.position = position;

            position.y = y;

            SetNewIceCreamPiece();
        }
    }

    private void SetNewIceCreamPiece()
    {
        MeshRenderer newPiece = pool.GetObjectFromPool();
        newPiece.transform.position = creamOutlet.position;
        //newPiece.gameObject.transform.SetParent(iceCreamCone.GetCreamStorage());
        //GameObject newCreamPiece = Instantiate(iceCreamPiecePrefab, creamOutlet.position, Quaternion.identity, iceCreamCone.GetCreamStorage());
        //newCreamPiece.GetComponent<MeshRenderer>().material.color = currentFlavour.flavorColor;
        newPiece.material.color = currentFlavour.flavorColor;
        //newCreamPiece.transform.DOMove(position, 1f);
        newPiece.transform.DOMove(position, 1f);
    }

    public void ChangeFlavor(FlavorType flavorType)
    {
        isRunning = true;
        ChangeCreamFlavour(flavorType);
    }

    public void StopMachine()
    {
        isRunning = false;
    }

    private void ChangeCreamFlavour(FlavorType flavorType)
    {
        currentFlavour = GetFlavor(flavorType);
    }

    public Flavor GetFlavor(FlavorType flavorType)
    {
        for(int i = 0; i < flavors.Length; i++)
        {
            if(flavorType == flavors[i].flavorType)
            {
                return flavors[i];
            }
        }
        return default;
    }
}

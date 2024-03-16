using UnityEngine;
using UnityEngine.UI;

public class FlavorButton : MonoBehaviour
{
    [SerializeField] private IceCreamMachine creamMachine;
    [SerializeField] private FlavorType flavorType;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Text buttonText;

    private void OnEnable()
    {
        SetButton();
    }

    private void SetButton()
    {
        buttonImage.color = creamMachine.GetFlavor(flavorType).flavorColor;
        buttonText.text = flavorType.ToString();
    }

    public void OnButtonDown()
    {
        creamMachine.ChangeFlavor(flavorType);
    }

    public void OnButtonUp()
    {
        creamMachine.StopMachine();
    }
}

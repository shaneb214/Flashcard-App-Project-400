using BlitzyUI;
using UnityEngine;
using UnityEngine.UI;

//Button which pops a screen when clicked (pops top screen).
//Store variable for screen to be popped? This just pops the top screen at the moment.

[RequireComponent(typeof(Button))]
public class BtnPopScreen : MonoBehaviour
{
    private Button myButton;
    [SerializeField] protected bool CallPopSequence;

    //Start.
    private void Awake() => myButton = GetComponent<Button>();
    private void Start() => myButton.onClick.AddListener(OnButtonClick);

    public virtual void OnButtonClick()
    {
        BlitzyUI.Screen topScreen = UIManager.Instance.GetTopScreen();

        if (CallPopSequence && topScreen.AllowStartPoppingSequence)
            topScreen.StartPoppingSequence(null); 
        else
            UIManager.Instance.QueuePop(null);
    }
}

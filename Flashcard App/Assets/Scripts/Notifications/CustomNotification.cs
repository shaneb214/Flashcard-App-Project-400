using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class CustomNotification : MonoBehaviour
{
    public Sprite icon;
    public string title = "Notification Title";
    [TextArea] public string description = "Notification description";

    [Header("Components")]
    [SerializeField] private Image iconObj;
    [SerializeField] private TextMeshProUGUI titleObj;
    [SerializeField] private TextMeshProUGUI descriptionObj;
    private Animator notificationAnimator;

    public bool enableTimer = true;
    public float timer = 3f;
    public bool isOn = false;
    public StartBehaviour startBehaviour = StartBehaviour.Disable;
    public CloseBehaviour closeBehaviour = CloseBehaviour.Disable;

    public UnityEvent onOpen;
    public UnityEvent onClose;

    public enum StartBehaviour { None, Disable }
    public enum CloseBehaviour { None, Disable, Destroy }

    void Awake()
    {
        isOn = false;

        if (notificationAnimator == null) { notificationAnimator = gameObject.GetComponent<Animator>(); }
        if (startBehaviour == StartBehaviour.Disable) { gameObject.SetActive(false); }
    }

    public void OpenNotification()
    {
        if (isOn == true)
            return;

        gameObject.SetActive(true);
        isOn = true;

        StopCoroutine("StartTimer");
        StopCoroutine("DisableNotification");

        notificationAnimator.Play("In");
        onOpen.Invoke();

        if (enableTimer == true) { StartCoroutine("StartTimer"); }
    }

    public void CloseNotification()
    {
        if (isOn == false)
            return;

        isOn = false;
        notificationAnimator.Play("Out");
        onClose.Invoke();

        StartCoroutine("DisableNotification");
    }

    public void UpdateUI()
    {
        try
        {
            iconObj.sprite = icon;
            titleObj.text = title;
            descriptionObj.text = description;
        }

        catch { Debug.LogError("<b>[Notification]</b> Cannot update the component due to missing variables.", this); }
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timer);

        CloseNotification();
        StartCoroutine("DisableNotification");
    }

    IEnumerator DisableNotification()
    {
        yield return new WaitForSeconds(1f);

        if (closeBehaviour == CloseBehaviour.Disable) { gameObject.SetActive(false); isOn = false; }
        else if (closeBehaviour == CloseBehaviour.Destroy) { Destroy(gameObject); }
    }
}

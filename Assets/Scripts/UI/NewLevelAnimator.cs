using System.Collections;
using UnityEngine;
using Utils;

/// <summary>
/// <para>Animates the level up text</para>
/// </summary>
public class NewLevelAnimator : MonoBehaviour
{
    [SerializeField] [Tooltip("Animator for animating the new level text")]
    private Animator animator;

    [SerializeField] [Tooltip("Time the pop up will remain open")]
    private float popUpTime;

    private static readonly int Popup = Animator.StringToHash("popUp");
    private static readonly int Dismiss = Animator.StringToHash("dismiss");
    private bool opened;
    private WaitForSeconds waitForSeconds;
    private CoroutineDelegate dismissCoroutine;

    private void Awake()
    {
        waitForSeconds = new WaitForSeconds(popUpTime);
        dismissCoroutine = DismissCoroutine;
    }

    /// <summary>
    /// <para>Sets the popup trigger of the animator. The animator shows the level up text</para>
    /// </summary>
    public void PopUp()
    {
        if (opened) return;
        
        animator.SetTrigger(Popup);
        opened = true;
    }

    /// <summary>
    /// <para>Starts the dismissCoroutine if the text is opened</para>
    /// <remarks>This method is called by the Animations of the Animator</remarks>
    /// </summary>
    public void FinishAnimation()
    {
        if(opened) StartCoroutine(dismissCoroutine());
    }
    
    /// <summary>
    /// <para>Waits the popUpTime and then sets the trigger dismiss of the animator.
    /// The animator closes the text</para>
    /// </summary>
    /// <returns></returns>
    private IEnumerator DismissCoroutine()
    {
        yield return waitForSeconds;
        animator.SetTrigger(Dismiss);
        opened = false;
    }
}
using UnityEngine;
/// <summary>
/// Class for the PopUpText
/// </summary>
public class PopUpText : MonoBehaviour
{
    //TODO: TP2 - Syntax - Fix declaration order
    [SerializeField]private Animator animator;
    public bool isActive = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    /// <summary>
    /// Activates the "Pop" animation
    /// If the Box is Active will not play
    /// </summary>
    public void ActiveBox()
    {
        if (isActive) return;
            isActive = true;
        //TODO - Fix - Hardcoded value
        animator.SetTrigger("Pop");
    } 
    /// <summary>
    /// Actiavte the "Close" animation
    /// </summary>
    public void DeactivateBox()
    {
        isActive = false;
        //TODO - Fix - Hardcoded value
        animator.SetTrigger("Close");
    }
  
    
}

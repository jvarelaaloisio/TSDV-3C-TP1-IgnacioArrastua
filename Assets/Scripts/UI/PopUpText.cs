using UnityEngine;
/// <summary>
/// Class for the PopUpText
/// </summary>
public class PopUpText : MonoBehaviour
{
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
        animator.SetTrigger("Pop");
    } 
    /// <summary>
    /// Actiavte the "Close" animation
    /// </summary>
    public void DeactivateBox()
    {
        isActive = false;
        animator.SetTrigger("Close");
    }
  
    
}

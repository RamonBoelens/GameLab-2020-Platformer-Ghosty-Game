using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class MenuMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("onHover", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("onHover", false);
    }
}

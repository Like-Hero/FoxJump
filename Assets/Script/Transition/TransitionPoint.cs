using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint : MonoBehaviour
{
    public enum TransitionType
    {
        SameScene,
        DifferentScene
    }

    [Header("Transition Info")]
    public string sceneName;
    public TransitionType transitionType;
    public TransitionDestination.DestinationTag destinationTag;
    private Collider2D m_collider;
    public LayerMask layerMask;
    private bool m_isTranstion;
    private void Awake()
    {
        m_collider = GetComponent<Collider2D>();
    }
    private void Update()
    {
        if (Physics2D.OverlapBox(m_collider.bounds.center, m_collider.bounds.size, 0, layerMask))
        {
            SceneController.Ins.InstantiateTransitionButton(transform);
            if (m_isTranstion)
            {
                SceneController.Ins.TransitionToDestination(this);
            }
        }
        else
        {
            SceneController.Ins.DestoryTransitionButton();
        }
    }
    public void TranstionEvent()
    {
        m_isTranstion = true;
    }
}

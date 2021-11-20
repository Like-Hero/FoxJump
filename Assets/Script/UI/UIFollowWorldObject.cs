using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowWorldObject : MonoBehaviour
{
    private Camera m_camera;
    private Transform m_target;
    public bool alwaysFollow = true;

    private bool hasFollowed = false;
    private Canvas m_canvas;
    public void Init(Camera camera, Transform target, Canvas canvas)
    {
        m_camera = camera;
        m_target = target;
        m_canvas = canvas;
        FollowObject();
    }

    public void Update()
    {
        FollowObject();
    }

    private void FollowObject()
    {
        if (!alwaysFollow && hasFollowed)
            return;

        if (m_camera != null && m_target != null)
        {
            Vector2 pos = m_camera.WorldToScreenPoint(m_target.transform.position);
            Vector2 point;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, pos, m_canvas.worldCamera, out point))
            {
                transform.localPosition = new Vector3(point.x, point.y, 0);
                hasFollowed = true;
            }
        }
    }
}

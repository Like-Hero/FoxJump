using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [Header("Point")]
    public RectTransform endPoint;
    private RectTransform m_rect;
    [Header("Line")]
    public GameObject linePrefab;
    public float lineWidth;
    private GameObject m_line;
    private RectTransform m_lineRect;
    private void Awake()
    {
        m_rect = GetComponent<RectTransform>();
    }
    private void Start()
    {
        m_line = CreateLine(m_rect.anchoredPosition, endPoint.anchoredPosition);
        m_line.transform.SetAsFirstSibling();
    }
    private void Update()
    {
        DrawStraightLine(m_line, m_rect.anchoredPosition, endPoint.anchoredPosition);
    }
    /// <summary>
    /// 创建一条两点之间的线
    /// </summary>
    /// <param name="startPoint">起始点</param>
    /// <param name="endPoint">结束点</param>
    public GameObject CreateLine(Vector2 startPoint, Vector2 endPoint)
    {
        //实例化需要显示的线段图片pfb
        GameObject line = Instantiate(linePrefab, transform.parent);
        m_lineRect = line.GetComponent<RectTransform>();
        return line;
    }
    
    //划线功能
    private void DrawStraightLine(GameObject line, Vector2 a, Vector2 b)
    {
        float distance = Vector2.Distance(a, b);//求距离
        float angle = Vector2.SignedAngle(a - b, Vector2.left);//求夹角
        line.GetComponent<RectTransform>().anchoredPosition = (a + b) / 2;
        line.GetComponent<RectTransform>().sizeDelta = new Vector2(distance, lineWidth);//长度，宽度
        line.transform.localRotation = Quaternion.AngleAxis(-angle, Vector3.forward);
    }

}

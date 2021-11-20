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
    /// ����һ������֮�����
    /// </summary>
    /// <param name="startPoint">��ʼ��</param>
    /// <param name="endPoint">������</param>
    public GameObject CreateLine(Vector2 startPoint, Vector2 endPoint)
    {
        //ʵ������Ҫ��ʾ���߶�ͼƬpfb
        GameObject line = Instantiate(linePrefab, transform.parent);
        m_lineRect = line.GetComponent<RectTransform>();
        return line;
    }
    
    //���߹���
    private void DrawStraightLine(GameObject line, Vector2 a, Vector2 b)
    {
        float distance = Vector2.Distance(a, b);//�����
        float angle = Vector2.SignedAngle(a - b, Vector2.left);//��н�
        line.GetComponent<RectTransform>().anchoredPosition = (a + b) / 2;
        line.GetComponent<RectTransform>().sizeDelta = new Vector2(distance, lineWidth);//���ȣ����
        line.transform.localRotation = Quaternion.AngleAxis(-angle, Vector3.forward);
    }

}

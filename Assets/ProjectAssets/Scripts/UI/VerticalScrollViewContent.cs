using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerticalScrollViewContent : MonoBehaviour
{
    [SerializeField] List<RectTransform> _initialSiblingsOrder = new List<RectTransform>();
    private int totalElements;
    private RectTransform rect;
    private VerticalLayoutGroup verticalLayout;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        verticalLayout = GetComponent<VerticalLayoutGroup>();
    }
    private void OrganizeInitialSiblings()
    {
        for (int i = 0; i < _initialSiblingsOrder.Count; i++)
        {
            _initialSiblingsOrder[i].SetSiblingIndex(i);
        }
    }
    private void Start()
    {
        UpdateSize();
        OrganizeInitialSiblings();
    }
    public void UpdateSize()
    {
        totalElements = transform.childCount;
        Vector2 size = new Vector2(0,rect.sizeDelta.y);
        float height = verticalLayout.padding.top + verticalLayout.padding.bottom;
        for (int i = 0; i < totalElements; i++)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy)
            {
                height += transform.GetChild(i).GetComponent<LayoutElement>().preferredHeight;
                height += verticalLayout.spacing;
            }
        }
        height -= verticalLayout.spacing;
        size.y = height;
        rect.sizeDelta = size;
    }
}

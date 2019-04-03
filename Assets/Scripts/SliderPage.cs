using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///此种方法可使用PC和Android(DoTween)
/// </summary>
public class SliderPage : MonoBehaviour
{
    [Tooltip("Images页面集合")]
    public Transform m_Images;
    private void Awake()
    {
        if (m_Images == null)
        {
            m_Images = transform.Find("Image");
        }
    }
    private void Start()
    {
        //初始化Image页面 排列位置
        for (int i = 0; i < m_Images.childCount; i++)
        {
            //初始化m_Images的父物体的位置
            if (i.Equals(0))
            {
                m_Images.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                //设置第一个image的位置
                m_Images.GetChild(i).GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                //排列每个Imagesd的位置(除了第一个Images外)
                float PerWidth = .5f * (m_Images.GetChild(i - 1).GetComponent<RectTransform>().rect.width + m_Images.GetChild(i).GetComponent<RectTransform>().rect.width);
                m_Images.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2(i * PerWidth, 0);
            }
            //随机设置Image的颜色
            Color color = new Color((Random.Range(0, 256) / 255f), (Random.Range(0, 256f) / 255f), (Random.Range(0, 256) / 255f));
            m_Images.GetChild(i).GetComponent<Image>().color = color;
        }
    }
    private void Update()
    {
        OnSliderPage();
    }
    bool isDown = false;
    float mousePosX;
    float runtimePos;
    float deltaX;
    int sliderIndex = 0;
    void OnSliderPage()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDown = true;
            mousePosX = Input.mousePosition.x;
            runtimePos = m_Images.GetComponent<RectTransform>().anchoredPosition.x;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDown = false;
            float width = m_Images.GetChild(0).GetComponent<RectTransform>().rect.width;
            if (Mathf.Abs(deltaX) >= (width / 6) || Mathf.Abs(Input.GetAxis("Mouse X")) >= 3)
            {
                if (deltaX > 0)
                {
                    //向右
                    sliderIndex++;
                    if (sliderIndex > 0)

                        sliderIndex = 0;
                }
                else
                {
                    //向左
                    sliderIndex--;
                    if (sliderIndex < (-m_Images.childCount + 1))

                        sliderIndex = -m_Images.childCount + 1;
                }
                m_Images.GetComponent<RectTransform>().DOAnchorPosX(sliderIndex * width, 0.5f);
            }
            else
            {
                m_Images.GetComponent<RectTransform>().DOAnchorPosX(sliderIndex * width, 0.5f);
                //恢复
            }
        }
        if (isDown)
        {
            deltaX = Input.mousePosition.x - mousePosX;
            m_Images.GetComponent<RectTransform>().anchoredPosition = new Vector2(deltaX + runtimePos, 0);
        }
    }
}




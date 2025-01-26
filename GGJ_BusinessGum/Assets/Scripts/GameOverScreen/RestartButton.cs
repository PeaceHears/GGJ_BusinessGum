using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite hoverSprite;
    public Sprite clickedSprite;

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void ChangeScene()
    {
        image.sprite = clickedSprite;
        SceneManager.LoadScene("GameplayScene");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = defaultSprite;
    }
}

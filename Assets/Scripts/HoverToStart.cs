using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HoverToStart : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    public Image progressCircle;
    public float hoverTime = 2f;
    public GameObject greenPanel;
    public GameObject pinkPanel;

    private float timer = 0f;
    private bool hovering = false;

    void Update()
    {
        if (hovering)
        {
            timer += Time.deltaTime;

            progressCircle.fillAmount = timer / hoverTime;

            if (timer >= hoverTime)
            {
                hovering = false;
                greenPanel.SetActive(true);
                pinkPanel.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        timer = 0f;
        progressCircle.fillAmount = 0f;
    }
}
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    private Entity entity => GetComponentInParent<Entity>();
    private CharacterStats myStats => GetComponentInParent<CharacterStats>();
    private RectTransform myTransform;
    private Slider slider;


    private void Start()
    {
        myTransform = GetComponent<RectTransform>();
        //entity = GetComponentInParent<Entity>();
        slider = GetComponentInChildren<Slider>();
        //myStats = GetComponentInParent<CharacterStats>();


        UpdateHealthUI();
    }

    private void OnEnable()
    {
        entity.onFlipped += FlipUI;
        myStats.onHealthChanged += UpdateHealthUI;
    }

    private void Update()
    {
        UpdateCurrentHealth();
    }

    private void UpdateCurrentHealth()
    {
        slider.value = myStats.currentHealth;
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = myStats.GetMaxHealthValue();
        slider.value = myStats.currentHealth;
    }


    private void OnDisable()
    {
        if (entity != null)
            entity.onFlipped -= FlipUI;
        if (myStats != null)
            myStats.onHealthChanged -= UpdateHealthUI;
    }
    private void FlipUI() => myTransform.Rotate(0, 180, 0);
}

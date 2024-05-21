using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public Image imageFill;

    float hp;
    float maxHp;

    // Update is called once per frame
    void Update()
    {
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, this.hp / this.maxHp, Time.deltaTime * 5f);
    }

    public void OnInit(float maxHp)
    {
        this.hp = maxHp;
        this.maxHp = maxHp;

        imageFill.fillAmount = 1;
    }

    public void SetNewHp(float hp)
    {
        this.hp = hp;
        
    }
}

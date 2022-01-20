using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private TextMeshPro text;
    private float disappearTimer;
    private Color textColor;

    public static DamagePopUp Create(Vector3 position, float damageAmount, bool isCrit, Transform damagePopUpPrefab)
    {
        Transform damagePopUpTransform = Instantiate(damagePopUpPrefab, position, Quaternion.identity);

        DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
        damagePopUp.Setup(damageAmount, isCrit);
        return damagePopUp;
    }

    private void Awake()
    {
        text = transform.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        float moveSpeed = 6f;
        transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            text.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Setup(float damageAmount, bool isCrit)
    {
        text.SetText(damageAmount.ToString());
        if (isCrit)
        {
            textColor = new Color(255, 69, 0, 1);
        }
        else
        {
            textColor = Color.red;
        }
        text.color = textColor;
        disappearTimer = 1f;
    }

}

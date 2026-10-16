using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    private float HP = 100f;
    public Image Bar;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            HP -= 20;
            Bar.fillAmount = HP/100f;

            if (HP <= 0)
            {
                Debug.Log("Игрок умер");
            }
        }
    }
}

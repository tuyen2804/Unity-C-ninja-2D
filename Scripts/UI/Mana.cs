using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    public float mana = 1f;
    public Image manaImage;
    private float time1=0f;
    private float time2=2f;
    public void ControllMana(float manaUsed)
    {
        mana-=manaUsed;

    }
    private void FixedUpdate()
    {
        manaImage.fillAmount = mana;
        if (mana < 1f)
        {
            time1 += Time.fixedDeltaTime;
            if(time1 > time2)
            {
                if (mana < 0.9f)
                {
                    time1 = 0f;
                    mana += 0.1f;
                }
                else { mana = 1f; }
            }
        }
    }

}

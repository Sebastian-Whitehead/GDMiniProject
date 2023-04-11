using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPCharges : MonoBehaviour
{
    public Image[] manaPoints;
    public float mana, maxMana = 9;

    // Update is called once per frame
    void Update()
    {
        ManaBarFiller();
    }


    void ManaBarFiller()
    {
        for (int i = 0; i < manaPoints.Length; i++)
        {
            manaPoints[i].enabled = !DisplayManaPoints(mana, i);
        }
    }

    bool DisplayManaPoints(float _mana, int pointNumber)
    {
        return (pointNumber * (maxMana / manaPoints.Length) >= _mana);
    }
    
    public void RegenCP(float RegenPoints)
    {
        if (mana > 0)
            mana += RegenPoints;
    }

    public void Expend(float expendPoints)
    {
        if (mana < maxMana)
            mana -= expendPoints;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TextAnswer : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().DOFade(1,1.5f);
    }
}

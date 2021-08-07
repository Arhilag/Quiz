using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour
{
    [SerializeField]
    private float percent;
    void Start()
    {
        GetComponent<Image>().DOFade(percent, 1);
    }
}

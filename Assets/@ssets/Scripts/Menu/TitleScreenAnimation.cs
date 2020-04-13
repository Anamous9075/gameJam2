using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenAnimation : MonoBehaviour
{
    [SerializeField] private Button[] titleScreenButtons;

    private void Start()
    {
        InitiateButton();
    }

    private void InitiateButton()
    {
        ShowTitleScreenButtons();
    }

    private void ShowTitleScreenButtons()
    {
        StartCoroutine(ShowTitleScreenButtonsIE());
    }

    private IEnumerator ShowTitleScreenButtonsIE()
    {
        yield return new WaitForSeconds(1);

        foreach(var bttn in titleScreenButtons)
        {
            yield return bttn.transform.DOPunchScale(new Vector3(.3f, .3f, .3f), .5f, 1)
                .OnStart(() => 
                {
                    bttn.gameObject.SetActive(true);
                })
                .WaitForCompletion();
        }
        yield return new WaitForEndOfFrame();
    }
}

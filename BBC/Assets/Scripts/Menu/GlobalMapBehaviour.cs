using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapBehaviour : MonoBehaviour
{
    public GameObject WorldMapPanel;
    public GameObject ChooseLevelLabel;
    public GameObject BackToMainMenuButton;

    public IEnumerator ShowGlobalMap_COR()
    {
        WorldMapPanel.GetComponent<Animator>().Play("ScaleGlobalMapUp");
        yield return new WaitForSeconds(0.75f);
        PlayMarkersAnimation("AppearMapMarker");
        ChooseLevelLabel.GetComponent<Animator>().Play("DrawChooseLevelLabel");
        BackToMainMenuButton.GetComponent<Animator>().Play("DrawReturnToMenuFromMapButton");
    }

    public IEnumerator HideGlobalMap_COR()
    {
        ChooseLevelLabel.GetComponent<Animator>().Play("EraseChooseLevelLabel");
        BackToMainMenuButton.GetComponent<Animator>().Play("EraseReturnToMenuFromMapButton");
        PlayMarkersAnimation("EraseMapMarker");
        yield return new WaitForSeconds(1f);
        WorldMapPanel.GetComponent<Animator>().Play("ScaleGlobalMapDown");     
        yield return new WaitForSeconds(0.75f);
    }

    private void PlayMarkersAnimation(string animationName)
    {
        for (var i = 0; i < WorldMapPanel.transform.GetChild(0).GetChild(0).childCount; i++)
            WorldMapPanel.transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<Animator>().Play(animationName);
    }

    private void Start()
    {
        PlayMarkersAnimation("EraseMapMarker");
    }
}

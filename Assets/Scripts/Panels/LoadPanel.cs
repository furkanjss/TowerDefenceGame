using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanel : MonoBehaviour
{
    [SerializeField] private Button loadButton;
    [SerializeField] private Button freshButton;
    void Start()
    {
        loadButton.onClick.AddListener(LoadData);
        freshButton.onClick.AddListener(FreshStart);

    }

    void LoadData()
    {
        GameManager.Instance.LoadData();
        DOVirtual.DelayedCall(.3f, () =>
        {
            gameObject.SetActive(false);

        });

    }
    void FreshStart()
    { 
        gameObject.SetActive(false);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int _price;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBehaviour playerBehaviour)) // check if touch is player
        {
            DOVirtual.DelayedCall(0.2f, () => VFXManager.instance.DollarVFX(transform));
            EventManager.IncreasePokeballCount(_price);

        }
        transform.DOScale(new Vector3(0.1f, 0.1f, 01f), 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            DOTween.Kill(transform);
            Destroy(gameObject);
        });
    }
}


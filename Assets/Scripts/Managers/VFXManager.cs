using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{
    [SerializeField] private GameObject _dustPoof;
    [SerializeField] private GameObject _dollarBlast;

    
    public void DustVFX(Transform VFXPos)
    {
        GameObject tempVFX = Instantiate(_dustPoof, VFXPos.position, _dustPoof.transform.rotation, transform);
        Destroy(tempVFX, 2f);
    }
    public void DollarVFX(Transform VFXPos)
    {
        GameObject tempVFX = Instantiate(_dollarBlast, VFXPos.position, _dollarBlast.transform.rotation, transform);
        Destroy(tempVFX, 2f);
    }
}

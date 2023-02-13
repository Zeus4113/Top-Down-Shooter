using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectable
{
    public void SetSlowed();
    public void SetFasted();
    public IEnumerator Slowed();
    
}

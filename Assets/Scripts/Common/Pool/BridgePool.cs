using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePool : GenericPool<Bridge>
{
    public override Bridge Get()
    {
        var bridge = base.Get();
        bridge.transform.localScale = new Vector3(1, 0, 0);
        Debug.Log($"retrieved bridge: {bridge.gameObject.name}", bridge.gameObject);
        return bridge;
    }
}

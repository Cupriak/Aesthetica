using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerMaskHelper
{
    public static bool IsLayerInLayerMask(int layer, LayerMask layerMask)
    {
        return (layerMask == (layerMask | (1 << layer)));
    }
}

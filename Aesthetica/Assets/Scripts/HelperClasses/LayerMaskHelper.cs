using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that help with LayerMask operations
/// </summary>
public static class LayerMaskHelper
{
    /// <summary>
    /// Check if layer is in layermask
    /// </summary>
    /// <param name="layer">layer to check</param>
    /// <param name="layerMask">LayerMask where method look for layer</param>
    /// <returns>true if layer is in layermask false otherwise</returns>
    public static bool IsLayerInLayerMask(int layer, LayerMask layerMask)
    {
        return (layerMask == (layerMask | (1 << layer)));
    }
}

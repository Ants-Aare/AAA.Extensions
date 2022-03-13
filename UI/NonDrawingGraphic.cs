 using UnityEngine;
 using UnityEngine.UI;
 
 /// A concrete subclass of the Unity UI `Graphic` class that just skips drawing.
 /// Useful for providing a raycast target without actually drawing anything.
[RequireComponent(typeof(CanvasRenderer))]
public class NonDrawingGraphic : Graphic
{
    public override void SetMaterialDirty()
    {
        return;
    }

    public override void SetVerticesDirty()
    {
        return;
    }
    
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        return;
    }
}
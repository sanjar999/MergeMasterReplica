using UnityEngine;
using UnityEngine.Rendering;

public class UnitOutline : MonoBehaviour
{
    public Renderer OutlinedObject;

    public Material WriteObject;
    public Material ApplyOutline;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hitSelectable = Physics.Raycast(ray, out var hit) && hit.transform.CompareTag("Unit");
            if (hitSelectable)
            {
                OutlinedObject = hit.transform.GetComponent<Renderer>();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            OutlinedObject = null;
        }


    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //setup stuff
        var commands = new CommandBuffer();
        int selectionBuffer = Shader.PropertyToID("_SelectionBuffer");
        commands.GetTemporaryRT(selectionBuffer, source.descriptor);
        //render selection buffer
        commands.SetRenderTarget(selectionBuffer);
        commands.ClearRenderTarget(true, true, Color.clear);
        if (OutlinedObject != null)
        {
            commands.DrawRenderer(OutlinedObject, WriteObject);
        }
        //apply everything and clean up in commandbuffer
        commands.Blit(source, destination, ApplyOutline);
        commands.ReleaseTemporaryRT(selectionBuffer);

        //execute and clean up commandbuffer itself
        Graphics.ExecuteCommandBuffer(commands);
        commands.Dispose();
    }
}
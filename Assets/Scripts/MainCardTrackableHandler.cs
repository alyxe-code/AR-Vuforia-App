using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;

[Serializable]
public class ObjectState
{
    public GameObject Object;
    public bool Selected;
    public string Name;

    public string GetName()
    {
        return Name == null ? Object.name : Name;
    }
}

public class MainCardTrackableHandler : CustomTrackableHandler
{

    public ObjectState[] Stats;

    public Material DefaultMaterial;
    public Material SelectedMaterial;

    public int DefaultSelect;

    protected override void Start()
    {
        base.Start();
        Select(Stats[DefaultSelect].GetName(), force: true);
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        Select(Stats[DefaultSelect].GetName());
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
    }

    public void ChangeFocus()
    {
        Select(WidgetImageDescription.Selected);
    }

    public void Select(string name, bool force = false)
    {
        if (!Found && !force) return;

        name = name.ToLower().Trim();
        int index = 0;
        foreach (var item in Stats)
        {
            if (item.Object == null)
            {
                item.Selected = false;
                continue;
            }
            item.Selected = item.GetName().ToLower().Trim() == name;
            item.Object.GetComponent<Renderer>().material = item.Selected
                            ? SelectedMaterial
                            : DefaultMaterial;
            index++;
        }

        WidgetImageDescription.Show(new ImageDescription
        {
            Main = Stats[index].Object.name,
            Left = index == 0
                        ? Stats[Stats.Length - 1].GetName()
                        : Stats[index - 1].GetName(),
            Right = index == 0
                        ? Stats[Stats.Length - 2].GetName()
                        : index == Stats.Length - 1
                            ? Stats[0].GetName()
                            : Stats[index + 1].GetName()
        });
    }

}

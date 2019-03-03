using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTrackableHandler : DefaultTrackableEventHandler
{

    public ImageDescription ImageDescription;
    [SerializeField]
    protected WidgetImageDescription WidgetImageDescription;

    public bool Found;

    private void OnValidate()
    {
        if (WidgetImageDescription == null)
        {
            WidgetImageDescription = FindObjectOfType<WidgetImageDescription>();
            if (WidgetImageDescription == null)
            {
                Debug.LogWarning($"{nameof(WidgetImageDescription)} cannot be found. Program may work incorrectly");
            }
        }
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        WidgetImageDescription.Show(ImageDescription);
        Found = true;
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        WidgetImageDescription.Clear();
        Found = false;
    }

}

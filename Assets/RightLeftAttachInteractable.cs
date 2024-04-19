using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RightLeftAttachInteractable : XRGrabInteractable
{
    [SerializeField] private Transform rightHandAttachTransform;
    [SerializeField] private Transform leftHandAttachTransform;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("RightHandController"))
        {
            attachTransform = rightHandAttachTransform;
        }

        if (args.interactorObject.transform.CompareTag("LeftHandController"))
        {
            attachTransform = leftHandAttachTransform;
        }

        base.OnSelectEntering(args);
    }
}

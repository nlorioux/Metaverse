using UnityEngine;

[RequireComponent(typeof(RayInteraction))]
public class RayInteractionEnabler : MonoBehaviour
{
    #region Members
    protected RayInteraction rayInteraction;
    protected OVRControllerHelper controllerHelper;
    #endregion

    #region MonoBehaviour callbacks
    protected void Awake()
    {
        rayInteraction = GetComponent<RayInteraction>();
        controllerHelper = GetComponentInChildren<OVRControllerHelper>();

        SetEnabled();
    }

    protected void Update()
    {
        SetEnabled();
    }
    #endregion

    #region Internal methods
    protected void SetEnabled()
    {
        bool enabled = false;

        foreach (Transform t in controllerHelper.transform)
        {
            if (t.gameObject.activeInHierarchy)
            {
                enabled = true;

                break;
            }
        }

        if (enabled != rayInteraction.enabled)
        {
            rayInteraction.enabled = enabled;
        }
    }
    #endregion
}

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(OVRSkeleton))]
[RequireComponent(typeof(SkinnedMeshRenderer))]
public class HandUtils : MonoBehaviour
{
    public enum Hand
    {
        LEFT,
        RIGHT
    }

    public Hand hand;
    [Range(0.001f, 0.1f)]
    public float pinchThreshold = 0.02f;
    [Range(0.1f, 10.0f)]
    public float rayLength = 1;
    public bool showRay = false;

    protected GameObject ray;
    protected SkinnedMeshRenderer handRenderer;

    public Transform Thumb { get; protected set; }
    public Transform Index { get; protected set; }
    public GameObject Pointed { get; protected set; }
    public Vector3 RayDirection { get => (hand == Hand.LEFT ? -1 : 1) * Vector3.right; }
    public bool Pinch { get; protected set; }

    protected void Awake()
    {
        Pinch = false;

        handRenderer = GetComponent<SkinnedMeshRenderer>();

        StartCoroutine(InitHand(GetComponent<OVRSkeleton>()));
    }

    protected void OnDisable()
    {
        Pinch = false;

        ray?.SetActive(false);

        SetHighLight(Pointed, false);

        Pointed = null;
    }

    protected void Update()
    {
        bool show_ray = showRay && (handRenderer?.enabled ?? false);

        ray?.SetActive(show_ray);

        // Detect pinch between index and thumb
        Pinch = Vector3.Distance(Thumb.position, Index.position) < pinchThreshold;
        Debug.Log(Pinch); 
        //TODO

        RaycastHit hit;

        if (Physics.Raycast(new Ray(Index.position, Index.TransformDirection(RayDirection)), out hit, rayLength, (1 << (int)Layer.OBJECT)))
        {
            SetHighLight(Pointed, false);

            Pointed = hit.collider.gameObject;

            if (show_ray)
            {
                SetHighLight(Pointed, true);
            }
        }
        else
        {
            SetHighLight(Pointed, false);

            Pointed = null;
        }
    }

    protected IEnumerator InitHand(OVRSkeleton skeleton)
    {
        while (!skeleton.IsInitialized)
        {
            yield return null;
        }

        foreach (OVRBone bone in skeleton.Bones)
        {
            switch (bone.Id)
            {
                case OVRSkeleton.BoneId.Hand_IndexTip:
                    Index = bone.Transform;

                    ray = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

                    ray.name = "Ray";
                    ray.transform.parent = Index;
                    ray.transform.localPosition = 0.5f * rayLength * RayDirection;
                    ray.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
                    ray.transform.localScale = new Vector3(0.002f, 0.5f * rayLength, 0.002f);

                    Destroy(ray.GetComponent<Collider>());

                    break;
                case OVRSkeleton.BoneId.Hand_ThumbTip:
                    Thumb = bone.Transform;

                    break;
            }
        }

        foreach (OVRBoneCapsule capsule in skeleton.Capsules)
        {
            if (skeleton.Bones[capsule.BoneIndex].Id == OVRSkeleton.BoneId.Hand_Index3)
            {
                capsule.CapsuleCollider.isTrigger = true;
                capsule.CapsuleRigidbody.gameObject.AddComponent<Pinch>();
                capsule.CapsuleRigidbody.gameObject.AddComponent<VoodooHand>();

                break;
            }
        }
    }

    protected static void SetHighLight(GameObject go, bool set)
    {
        if (go != null)
        {
            Highlighter highlight = go.GetComponent<Highlighter>();

            if (highlight != null)
            {
                if (set)
                {
                    highlight.disableAfterDuration = false;
                    highlight.highlight = true;
                }
                else
                {
                    highlight.highlight = false;
                }
            }
        }
    }
}

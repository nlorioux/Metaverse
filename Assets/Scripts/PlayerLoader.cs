using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadyPlayerMe;

public class PlayerLoader : MonoBehaviour
{
    private GameObject avatar;
    //private GameObject avatarPUNPrefab;
    private void Start()
    {
        LoadAvatar();
    }

    private void LoadAvatar()
    {
        //LoadAvatar();

        //ApplicationData.Log();
        //avatarPUNPrefab = GameObject.FindGameObjectWithTag("Controller");
        var avatarLoader = new AvatarLoader();
        avatarLoader.OnCompleted += (_, args) =>
        {
            avatar = args.Avatar;

            avatar.transform.parent = gameObject.transform;
            avatar.transform.position = avatar.transform.parent.position - new Vector3(0, 1f, 0);
            avatar.transform.rotation = Quaternion.LookRotation(gameObject.transform.forward);
            avatar.GetComponent<Animator>().applyRootMotion = false;
            //gameObject.GetComponent<Rigidbody>().mass = 1.5f;
            AvatarAnimatorHelper.SetupAnimator(args.Metadata.BodyType, avatar);
        };
        string avatarURL = "https://models.readyplayer.me/640f12e15ff9a2cd66c48c70.glb";
        avatarLoader.LoadAvatar(avatarURL);
    }


    private void OnDestroy()
    {
        if (avatar != null) Destroy(avatar);
    }

    /*private void LoadAvatar()
    {
        AvatarLoader avatarLoader = new AvatarLoader();
        avatarLoader.LoadAvatar(avatarURL);
    }

    private void AvatarLoadComplete (object sender, CompletionEventArgs args)
    {
        var context = new AvatarContext();
        var avatar = (GameObject)context.Data;
        avatar.transform.parent = avatarPUNPrefab.transform;
        Debug.Log($"Avatar loaded");
    }

    private void AvatarLoadFail(object sender, FailureEventArgs args)
    {
        Debug.Log($"Avatar loading failed with error message: {args.Message}");
    }
    */
}

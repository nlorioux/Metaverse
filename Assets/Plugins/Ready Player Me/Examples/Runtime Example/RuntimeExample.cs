using UnityEngine;

namespace ReadyPlayerMe
{
    public class RuntimeExample : MonoBehaviour
    {

        private GameObject avatar;

        private void Start()
        {
            ApplicationData.Log();
            var avatarLoader = new AvatarLoader();
            avatarLoader.OnCompleted += (_, args) =>
            {
                avatar = args.Avatar;
                AvatarAnimatorHelper.SetupAnimator(args.Metadata.BodyType, avatar);
            };
            string avatarUrl = "https://models.readyplayer.me/640f12e15ff9a2cd66c48c70.glb";
            avatarLoader.LoadAvatar(avatarUrl);
        }

        private void OnDestroy()
        {
            if (avatar != null) Destroy(avatar);
        }
    }
}

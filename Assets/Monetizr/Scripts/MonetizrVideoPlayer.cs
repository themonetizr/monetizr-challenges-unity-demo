using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Monetizr.Challenges
{

    public class MonetizrVideoPlayer : MonoBehaviour
    {
        public VideoPlayer videoPlayer;
        Action<bool> onComplete;
        private bool isSkipped = false;

        void Awake()
        {

        }

        public void Play(string path, Action<bool> onComplete)
        {
            this.onComplete = onComplete;
            var videoPath = MonetizrManager.Instance.GetAsset<string>(MonetizrManager.Instance.GetActiveChallenge(), AssetsType.VideoFilePathString);

          
            var videoPlayer = GetComponent<VideoPlayer>();

            videoPlayer.playOnAwake = false;
            //videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
            //videoPlayer.targetCameraAlpha = 0.5F;
            videoPlayer.url = videoPath;
            videoPlayer.frame = 100;
            videoPlayer.isLooping = false;
                       
            videoPlayer.loopPointReached += EndReached;

            videoPlayer.Play();

            Debug.Log($"{videoPlayer.width} {videoPlayer.height}");

            MonetizrManager.Instance.SoundSwitch(false);

            MonetizrManager.Analytics.BeginShowAdAsset(AdType.Video);
        }

        void EndReached(VideoPlayer vp)
        {
            MonetizrManager.Analytics.EndShowAdAsset(AdType.Video);

            MonetizrManager.Instance.SoundSwitch(true);

            onComplete.Invoke(isSkipped);

            //GameObject.Destroy(this);
        }

        public void OnSkip()
        {
            Debug.Log("OnSkip!");

            isSkipped = true;

            EndReached(videoPlayer);
        }
    

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}

}

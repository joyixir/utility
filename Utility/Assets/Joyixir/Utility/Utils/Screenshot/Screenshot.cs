using System.Collections.Generic;
using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Joyixir.Utility.Utils.Screenshot
{
    // Mostly from Wappen https://gist.github.com/wappenull/668a492c80f7b7fda0f7c7f42b3ae0b0
    public class Screenshot : MonoBehaviour
    {
        #if UNITY_EDITOR
        [SerializeField] private bool singleShot = false;
        [SerializeField] private List<string> sizes = new List<string>(){"1242x2208", "1242x2688", "2048x2732"};
        private static bool _alreadyWorking;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (singleShot)
                    TakeScreenshot();
                else
                    StartCoroutine(TakeScreenshots());
            }
        }

        private IEnumerator TakeScreenshots()
        {
            if(_alreadyWorking)
                yield break;
            
            foreach (var size in sizes)
            {
                var successStatus = false;
                #if UNITY_ANDROID
                successStatus = GameViewUtils.TrySetSize(size, GameViewSizeGroupType.Android);
                #elif UNITY_IOS
                successStatus = GameViewUtils.TrySetSize(size, GameViewSizeGroupType.iPhone);
                #elif UNITY_STANDALONE
                successStatus = GameViewUtils.TrySetSize(size, GameViewSizeGroupType.Standalone);
                #endif
                if (!successStatus)
                {
                    Debug.LogError($"Did not take an screenshot for the size {size}");
                    continue;
                }
                yield return new WaitForEndOfFrame();
                TakeScreenshot();
            }
            _alreadyWorking = false;
        }

        private void TakeScreenshot()
        {
            ScreenCapture.CaptureScreenshot(GetSaveDestination());
        }

        private string GetSaveDestination()
        {
            var screenshotNumber = 1;
            // if the file exists put a number at the end of new file
            while (System.IO.File.Exists(GetFileName(screenshotNumber)))
            {
                screenshotNumber++;
            }

            return GetFileName(screenshotNumber);
        }

        private static string GetFileName(int screenshotNumber)
        {
            var destination = $"{Application.productName}-{Screen.width}x{Screen.height}-Screenshot-{screenshotNumber}.png";
            destination = destination.Replace("/", "-");
            destination = destination.Replace(" ", "_");
            destination = destination.Replace(":", "-");
            return destination;
        }
        #endif
    }

    // From https://answers.unity.com/questions/956123/add-and-select-game-view-resolution.html
    // https://www.programmersought.com/article/8025486338/
}
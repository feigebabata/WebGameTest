using System.Collections;
using System.Collections.Generic;
using FGUFW;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Test : MonoBehaviour
{



    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    IEnumerator Start()
    {
        AnimationClip animationClip = default;
        yield return loadAsset("Assets/Test/AnimatorTest/move.anim",animationClip);
        Debug.Log(animationClip);
    }

    IEnumerator loadAsset<T>(string key,T asset) where T: UnityEngine.Object
    {
        var loader = Addressables.LoadAssetAsync<T>(key);
        yield return loader;
        asset = loader.Result;
        Debug.Log(loader.Result);
    }

}

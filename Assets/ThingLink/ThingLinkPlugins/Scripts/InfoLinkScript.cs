using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoLinkScript : MonoBehaviour
{
    public void OpenInfoURL()
    {
        Application.OpenURL("https://support.thinglink.com/hc/en-us/articles/10134817735703");
    }
}

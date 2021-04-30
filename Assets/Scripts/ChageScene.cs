using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChageScene : MonoBehaviour
{
    public void ChengeLevel(int index=1)
    {
        SceneManager.LoadScene(index);
    }
}

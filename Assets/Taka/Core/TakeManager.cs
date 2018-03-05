using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeManager : MonoBehaviour
{

    #region Instance

    static string path = "Take/TakeManager";

    // Use this for initialization
    static TakeManager instance;

    public static TakeManager Instance()
    {
        if (instance == null)
        {
            instance = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(path)).GetComponent<TakeManager>();
            instance.InitPlatList();
            DontDestroyOnLoad(instance.gameObject);
        }
        return instance;
    }

    #endregion

    public List<PlatHandler> platList;

    public void AddPlat(PlatHandler plat)
    {
        try
        {
            if (platList.Find((p) => p.ID == plat.ID) != null)
            {
                throw new Exception("Plat ID 重覆註冊 。ID = " + plat.ID);
            }
            platList.Add(plat);
            plat.isFreeze = false;
            plat.HardHide();
            plat.isFreeze = true;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log(transform.GetPath());
            throw;
        }
    }

    public void RemovePlatContent(int id)
    {
        try
        {
            InitPlatList();
            PlatHandler content = GetPlatContent(id);
            if (content == null)
            {
                throw new Exception();
            }
            platList.Remove(content);
        }
        catch (Exception e)
        {

        }
    }

    public PlatHandler GetPlatContent(int id)
    {
        try
        {
            PlatHandler content = platList.Find((plat) => plat.ID == id);
            return content;
        }
        catch (Exception)
        {
            Debug.Log("遺失或未註冊的Plat。ID=" + id);
            throw;
        }
    }

    void InitPlatList()
    {
        if (platList == null)
        {
            platList = new List<PlatHandler>();
        }
    }
}

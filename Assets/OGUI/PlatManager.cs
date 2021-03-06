﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatManager : MonoBehaviour {

    #region Instance

    static string path ="OGUI/PlatManager";
    
    // Use this for initialization
    static PlatManager instance;

	public static  PlatManager Instance()
    {
        if (instance == null)
        {
            instance = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(path)).GetComponent<PlatManager>();
            DontDestroyOnLoad(instance);
        }
        
        return instance;
    }

    #endregion

    public bool isDebug;
    public List<PlatHandler> platList;
    public GameObject platItemPrefabs;
    public Transform platListContent;

    public void Awake()
    {
        instance = this;
        foreach(PlatHandler plat in platList)
        {
            plat.Initialize(new TakeManager());
        }
    }

    public List<PlatContent> platContentList;
    
    public bool AddPlatContent(PlatContent platContent)
    {
        InitPlatList();
        if (platContentList.Find((plat) => plat.id == platContent.id) != null)
        {
            return false;
        }else
        {
            platContentList.Add(platContent);
            platContent.isFreeze = false;
            platContent.HardHide();
            platContent.isFreeze = true;
            return true;
        }
    }

    public bool RemovePlatContent(int id)
    {
        InitPlatList();
        PlatContent content = GetPlatContent(id);
        if (content != null)
        {
            platContentList.Remove(content);
            return true;
        }
        else
        {
            return false;
        }
    }

    public PlatContent GetPlatContent(int id)
    {
        InitPlatList();
        PlatContent content = platContentList.Find((plat) => plat.id == id);
        return content;
    }

    void InitPlatList()
    {
        if (platContentList == null)
        {
            platContentList = new List<PlatContent>();
        }
    }
    
    void AddPlatItem(PlatHandler platHandler)
    {
        if (isDebug)
        {
            var item = GameObject.Instantiate<GameObject>(platItemPrefabs, platListContent).GetComponent<PlatDebugItem>();
            item.Initialize(platHandler);
        }
    }

}

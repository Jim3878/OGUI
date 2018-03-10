using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
            //instance.InitPlatList();
            DontDestroyOnLoad(instance.gameObject);
        }
        return instance;
    }

    #endregion

    public bool isDebug;
    public List<PlatHandler> platList = new List<PlatHandler>();
    [Header("for debug")]
    public GameObject platDebugItemPrefabs;
    public Transform ItemListContent;
    public Transform debugPanel;
    public Button debugBtn;

    bool isManager;
    List<PlatDebugItem> platDebutItemList = new List<PlatDebugItem>();

    private void Awake()
    {
        if (instance == null)
        {
            //Manger
            isManager = true;
            instance = this;
            InitialPlatList();
            DontDestroyOnLoad(this.gameObject);

            if (isDebug)
            {
                debugPanel.gameObject.SetActive(false);
                debugBtn.onClick.AddListener(this.OnDebugBtnClick);
            }
            else if(debugBtn!=null)
            {
                debugBtn.gameObject.SetActive(false);
            }
        }
        else
        {
            //非Manager
            isManager = false;
            RegistPlatListToManager();
            isDebug = false;
        }
    }

    void InitialPlatList()
    {
        foreach (PlatHandler handler in platList)
        {
            handler.Initialize();
            handler.isFreeze = false;
            handler.HardHide();
            AddPlatItem(handler);
        }
    }

    void RegistPlatListToManager()
    {
        foreach (PlatHandler handler in platList)
        {
            instance.RegistPlatHandler(handler);
            instance.AddPlatItem(handler);
        }
    }

    void RemovePlatListFromManager()
    {
        foreach (PlatHandler handler in platList)
        {
            try
            {
                instance.RemovePlatHandler(handler.ID);
                instance.RemovePlatItem(handler.ID);
            }
            catch (NullReferenceException)
            {
                Debug.LogError(transform.GetPath() + "\n我想幫我的platHandler從主要manager中移除時，發現有handler不見了，不過應該沒什麼關係所以我就把錯誤拋出吃掉了~");
            }
        }
    }

    public void RegistPlatHandler(PlatHandler plat)
    {
        if (platList.Find((x) => x.ID == plat.ID) != null)
        {
            RemovePlatHandler(plat.ID);
            Debug.Log("有重覆的ID註冊是不允許的！不過既然上一個註冊的platHanlder不知道跑去哪了，那就把他註銷掉吧~" + plat.transform.GetPath());
        }

        platList.Add(plat);
        plat.Initialize();
        plat.isFreeze = false;
        plat.HardHide();
    }

    public void RemovePlatHandler(int id)
    {
        platList.Remove(platList.Find((x) => x.ID == id));
    }

    public PlatHandler GetPlatContent(int id)
    {
        try
        {
            PlatHandler content = platList.Find((x) => x.ID == id);
            return content;
        }
        catch (Exception)
        {
            Debug.Log("遺失或未註冊的Plat。ID=" + id);
            throw;
        }
    }

    void AddPlatItem(PlatHandler plat)
    {
        if (platDebutItemList.Find((x) => x.handlerID == plat.ID) != null)
        {
            RemovePlatItem(plat.ID);
            Debug.Log("剛剛有把他註銷嗎？算了，先把他從debug內移除再說。" + plat.transform.GetPath());
        }

        var item = GameObject.Instantiate<GameObject>(platDebugItemPrefabs, ItemListContent).GetComponent<PlatDebugItem>();
        item.Initialize(plat);
        platDebutItemList.Add(item);

    }

    void RemovePlatItem(int ID)
    {
        platDebutItemList.Remove(platDebutItemList.Find((x) => x.handlerID == ID));

    }

    private void OnDestroy()
    {
        if (!isManager)
        {
            RemovePlatListFromManager();
        }
    }

    bool isOpen = false;
    void OnDebugBtnClick()
    {
        isOpen = !debugPanel.gameObject.activeSelf;
        debugPanel.gameObject.SetActive(isOpen);

    }
}

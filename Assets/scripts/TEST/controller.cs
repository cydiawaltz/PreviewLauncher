using UnityEngine;
using UnityEngine.UI;
using System.Windows.Forms;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

public class controller : MonoBehaviour
{
    public int buttonNum;//押されたアイコンの番号
    public bool isPreviewed;//説明文が出てくる画面か
    public bool readyforBack;//戻る準備ができているか
    public GameObject GameTitle;
    [SerializeField] GameObject DownBar;
    [SerializeField] RawImage exText;
    public GameObject icons;//各種アイコンの親(origin)
    public GameObject OverUIs;//上に重なるUIの親(アイコンの親)
    [SerializeField] GameObject Base;//説明テキストでぼかす部分
    [SerializeField] GameObject Backs;//ぼかしてる時の解除用の背景
    public RectTransform afterMove;//移動後アイコンの位置系の情報

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPreviewed = false;
        Backs.SetActive(false);
        DownBar.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
        exText.color = new Color(1, 1, 1, 0);
        GameTitle.GetComponent<RawImage>().color = new Color(1, 1, 1, 0);
        if(IsNewYearPeriod())
        {
            MessageBox.Show("　　謹　賀　新　年　　","PineApple Computer,Inc.");
        }
    }
    bool IsNewYearPeriod()
    {
        DateTime now = DateTime.Now;

        return now.Month == 1 && now.Day >= 1 && now.Day <= 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickIcon()
    {
        Debug.Log("icon clicked");
        //アイコン移動
        isPreviewed = true;
        var seq = DOTween.Sequence();
        seq.Join(DownBar.GetComponent<RectTransform>().DOPivot(new Vector2(0.5f,0),0.3f));
        seq.Join(exText.DOColor(new Color(1, 1, 1, 1), 0.3f));
        seq.Play();
        Backs.gameObject.SetActive(true);
    }

    public void OnClickLaunch()
    {
        MessageBox.Show("本番verはここでゲームが起動します。");
        Debug.Log("App Launched");
    }
    public void OnClickReadMe()
    {
        MessageBox.Show("本番verはここでReadMeが開きます。");
        Debug.Log("ReadME opened");
    }
    public void OnClickCancel()
    {
        isPreviewed = false;
        readyforBack = true;
        Backs.SetActive(false);
        var seq = DOTween.Sequence();
        seq.Join(DownBar.GetComponent<RectTransform>().DOPivot(new Vector2(0.5f, 1), 0.3f));
        seq.Join(exText.DOColor(new Color(1, 1, 1, 0), 0.3f));
        seq.Play();
    }
    public void OnclickHelp()
    {
        SceneManager.LoadScene("Preview_help");
    }
}

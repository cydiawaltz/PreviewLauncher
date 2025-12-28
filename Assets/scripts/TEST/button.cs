using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Windows.Forms;
using UnityEngine.EventSystems;

public class buttonscript : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] controller Controller;
    [SerializeField] int thisNum;
    [SerializeField] UnityEngine.UI.Button thisButton;
    [SerializeField] Vector2 anchored;
    [SerializeField] Quaternion Quaternion;
    [SerializeField] Vector2 delta;
    [SerializeField] Vector3 originalScale;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int num = 0;
        if(int.TryParse(this.name,out num))
        {}
        else
        {
            Debug.LogError("ﾎﾞﾀﾝの名前が数字になってねーぞ！！！！");
        }
        thisNum = num;
        thisButton = GetComponent<UnityEngine.UI.Button>();
        thisButton.onClick.AddListener(OnClick);
        var origin = this.gameObject.GetComponent<RectTransform>();
        anchored = origin.anchoredPosition; Quaternion = origin.rotation; delta = origin.sizeDelta;
        //アイコンデカくする準備
        originalScale = origin.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        Controller.buttonNum = thisNum;
        //this.gameObject.transform.parent = controller.OverUIs.transform;
        this.gameObject.transform.SetParent(Controller.OverUIs.transform, true);
        Debug.Log("Button " + thisNum + " clicked.");
        //MessageBox.Show("Button " + thisNum + " clicked.");
        thisButton.enabled = false;
        var seq = DOTween.Sequence();
        seq.Join(this.gameObject.GetComponent<RectTransform>().DOAnchorPos(Controller.afterMove.anchoredPosition, 0.3f));
        seq.Join(this.gameObject.GetComponent<RectTransform>().DORotateQuaternion(Controller.afterMove.rotation,0.3f));
        seq.Join(this.gameObject.GetComponent<RectTransform>().DOSizeDelta(Controller.afterMove.sizeDelta,0.3f));
        seq.Play();
        StartCoroutine(waitForEnd());
    }
    IEnumerator waitForEnd()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitWhile(() => !Controller.readyforBack);
        Debug.Log("Return Button " + thisNum + " to icons.");
        Controller.readyforBack = false;
        this.gameObject.transform.SetParent(Controller.icons.transform, true);
        thisButton.enabled = true;
        var seq = DOTween.Sequence();
        seq.Join(this.gameObject.GetComponent<RectTransform>().DOAnchorPos(anchored, 0.3f));
        seq.Join(this.gameObject.GetComponent<RectTransform>().DORotateQuaternion(Quaternion, 0.3f));
        seq.Join(this.gameObject.GetComponent<RectTransform>().DOSizeDelta(delta, 0.3f));
        seq.Play();
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Controller.GameTitle.GetComponent<RectTransform>().anchoredPosition = this.gameObject.GetComponent<RectTransform>().anchoredPosition;
        var seq = DOTween.Sequence();
        seq.Join(this.gameObject.GetComponent<RectTransform>().DOScale(originalScale * 1.2f, 0.05f));
        seq.Join(Controller.GameTitle.GetComponent<RawImage>().DOColor(new Color(1, 1, 1, 1),0.05f));
        seq.Play();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        var seq = DOTween.Sequence();
        seq.Join(this.gameObject.GetComponent<RectTransform>().DOScale(originalScale, 0.05f));
        seq.Join(Controller.GameTitle.GetComponent<RawImage>().DOColor(new Color(1, 1, 1, 0), 0.05f));
        seq.Play();
    }
}

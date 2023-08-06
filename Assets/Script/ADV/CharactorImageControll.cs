using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
/// <summary>
/// ADVシーンにてキャラクターの動きを制御するクラス
/// DOTWeenの存在を知る前に作成したためpositionやColorで頑張って動かしていたが後にDOTWeenに差し替える予定
/// </summary>
public class CharactorImageControll : MonoBehaviour
{
    [SerializeField] CharaImageBehavior charaImageBehavior;  //キャラクター立ち絵取得
    [SerializeField] Image body, face;
    int moveDistance = 50;　//キャラクター立ち絵移動距離

    SpriteAtlas atlas;　//キャラクター表情

    float graySpeed = 0.1f; //グレーアウトのスピード
    float fadeSpeed = 0.1f;        // 透明度が変わるスピード
    float bRed, bGreen, bBlue, bAlfa;   // bodyの色
    float fRed, fGreen, fBlue, fAlfa;   // faceの色

    //以下bool値をtrueにすることで各動作が開始される
    //テストのためにアクセス修飾子をpublicにしている
    public bool isFadeOut = false;
    public bool isFadeIn = false;
    public bool isGrayOut = false;
    public bool isGrayIn = false;
    public bool isCharaChange = false;

    public int posChangeStep = 1;　//キャラクター差し替え動作のステップ数
    string changedName;  //キャラクター差し替え先のキャラ名
    string changedFace;  //キャラクター差し替え先の表情名

    public bool isMove = true;　//キャラクター移動終了ステータス
    public bool isColorChange = true; //キャラクター色変更終了ステータス
    float dis;

    //キャラクター表情
    public enum faceStateEnum
    {
        Normal, Smail, Angry
    }

    public faceStateEnum faceState = faceStateEnum.Normal;

    //キャラクター立ち位置状態
    public enum positionStateEnum
    {
        Normal, FadeIn, FadeOut, GrayIn, GrayOut
    }

    public positionStateEnum positionState = positionStateEnum.Normal;
    public bool isPosStateNormal => positionState == positionStateEnum.Normal;
    public bool isPosStateFadeIn => positionState == positionStateEnum.FadeIn;
    public bool isPosStateFadeOut => positionState == positionStateEnum.FadeOut;
    public bool isPosStateGrayIn => positionState == positionStateEnum.GrayIn;
    public bool isPosStateGrayOut => positionState == positionStateEnum.GrayOut;

    //キャラクター立ち位置移動状態
    public enum moveStateEnum
    {
        Stop, Move
    }

    public moveStateEnum moveState = moveStateEnum.Stop;
    public bool isMoveStateMove => moveState == moveStateEnum.Move;

    Transform myTransform;
    float startX_Position;
    float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        bRed = body.color.r;
        bGreen = body.color.g;
        bBlue = body.color.b;
        bAlfa = body.color.a;

        fRed = face.color.r;
        fGreen = face.color.g;
        fBlue = face.color.b;
        fAlfa = face.color.a;

        myTransform = body.GetComponent<Transform>();
        startX_Position = myTransform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadeIn && (isPosStateFadeOut || isPosStateFadeIn))
        {
            positionState = positionStateEnum.FadeIn;
            moveState = moveStateEnum.Move;
            StartFadeIn();
        }
        else if (isFadeOut && (isPosStateNormal || isPosStateFadeOut))
        {
            moveState = moveStateEnum.Move;
            positionState = positionStateEnum.FadeOut;
            StartFadeOut();
        }
        else if (isGrayIn && (isPosStateGrayOut || isPosStateGrayIn))
        {
            moveState = moveStateEnum.Move;
            positionState = positionStateEnum.GrayIn;
            GrayIn();
        }
        else if (isGrayOut && (isPosStateNormal || isPosStateGrayOut))
        {
            moveState = moveStateEnum.Move;
            positionState = positionStateEnum.GrayOut;
            GrayOut();
        }
        else if (isCharaChange)
        {
            moveState = moveStateEnum.Move;
            CharaChange();
        }
        else
        {
            isFadeOut = false;
            isFadeIn = false;
            isGrayOut = false;
            isGrayIn = false;
        }
    }

    /// <summary>
    /// キャラクターが後ろに下がりつつフェードアウトする
    /// </summary>
    void StartFadeOut()
    {
        if (bAlfa > 0)              
        {
            bAlfa -= fadeSpeed;
            fAlfa -= fadeSpeed;
            SetAlpha();        
        }
        else if(bAlfa <= 0)
        {
            isColorChange = false;
        }

        if(dis >= moveDistance)
        {
            isMove = false;
        }
        else
        {
            dis = OutMove();
        }

        if (!isColorChange && !isMove)             
        {
            moveState = moveStateEnum.Stop;
            isMove = true;
            isColorChange = true;
            isFadeOut = false;
            dis = 0;
            Debug.Log("キャラXポジション：　" + myTransform.position.x);
        }
    }

    /// <summary>
    /// キャラクターが前に出つつフェードインする
    /// </summary>
    void StartFadeIn()
    {
        if (bAlfa < 1)           
        {
            bAlfa += fadeSpeed;  
            fAlfa += fadeSpeed;  
            SetAlpha();          
        }
        else if (bAlfa >= 1)
        {
            isColorChange = false;
        }

        if (dis >= moveDistance)
        {
            isMove = false;
        }
        else
        {
            dis = InMove();

        }

        if (!isColorChange && !isMove)           
        {
            moveState = moveStateEnum.Stop;
            positionState = positionStateEnum.Normal;
            isMove = true;
            isColorChange = true;
            isFadeIn = false;  
            dis = 0;
            Debug.Log("キャラXポジション：　" + myTransform.position.x);
        }
    }

    /// <summary>
    /// キャラクターが後ろに下がりつつグレーアウトする
    /// </summary>
    void GrayOut()
    {
        if (bRed > 0.5)           
        {
            SetGray(-graySpeed);  
            SetAlpha();
        }
        else if (bRed <= 0.5)
        {
            isColorChange = false;
        }

        Debug.Log(dis + ">=" + moveDistance);
        if (dis >= moveDistance)
        {
            isMove = false;
        }
        else
        {
            dis = OutMove();

        }

        if (!isColorChange && !isMove)            
        {
            moveState = moveStateEnum.Stop;
            isMove = true;
            isColorChange = true;
            isGrayOut = false;   
            dis = 0;
            Debug.Log("キャラXポジション：　" + myTransform.position.x);
        }
    }

    /// <summary>
    /// キャラクターが前に出つつグレーインする
    /// </summary>
    void GrayIn()
    {
        if (bRed < 1) 
        {
            SetGray(graySpeed);
            SetAlpha();
        }
        else if (bRed >= 1)
        {
            isColorChange = false;
        }

        if(dis >= moveDistance)
        {
            isMove = false;
        }
        else
        {
            dis = InMove();

        }

        if (!isColorChange && !isMove)
        {
            moveState = moveStateEnum.Stop;
            positionState = positionStateEnum.Normal;
            isMove = true;
            isColorChange = true;
            isGrayIn = false;     
            dis = 0;
        }
    }

    void SetGray(float graySpeed)
    {
        bRed += graySpeed;
        bGreen += graySpeed;
        bBlue += graySpeed;

        fRed += graySpeed;
        fGreen += graySpeed;
        fBlue += graySpeed;
    }

    void SetAlpha()
    {
        body.color = new Color(bRed, bGreen, bBlue, bAlfa);
        face.color = new Color(fRed, fGreen, fBlue, fAlfa);
    }

    float OutMove()
    {
        var pos = myTransform.position;
        if (transform.rotation.y == 0)
        {
            pos.x -= moveSpeed;
        }
        else
        {
            pos.x += moveSpeed;
        }
        
        myTransform.position = pos;
        return Mathf.Abs(startX_Position - pos.x);
    }

    float InMove()
    {
        var pos = myTransform.position;
        if (transform.rotation.y == 0)
        {
            pos.x += moveSpeed;
        }
        else
        {
            pos.x -= moveSpeed;
        }
        myTransform.position = pos;
        return Mathf.Abs(pos.x - startX_Position);
    }

    public void FaceChange(string state)
    {
        switch (state)
        {
            case "Normal":
                face.sprite = atlas.GetSprite("normal");
                break;
            case "Normal-1":
                face.sprite = atlas.GetSprite("normal-1");
                break;
            case "Smail":
                face.sprite = atlas.GetSprite("smail");
                break;
            case "Complain":
                face.sprite = atlas.GetSprite("complain");
                break;
            case "Angry":
                face.sprite = atlas.GetSprite("angry");
                break;
            case "Fear":
                face.sprite = atlas.GetSprite("fear");
                break;
            default:
                return;
        }
    }

    /// <summary>
    /// 各立ち絵モーション起動メソッド
    /// </summary>
    /// <param name="moveMotion">モーション指定</param>
    /// <param name="changedName">キャラクター立ち絵変更時名前を指定</param>
    /// <param name="changedFace">キャラクター立ち絵変更時表情指定</param>
    public void PositionMove(string moveMotion, string changedName = "", string changedFace = "")
    {
        switch (moveMotion)
        {
            case "FadeIn":
                isFadeIn = true;
                break;
            case "FadeOut":
                isFadeOut = true;
                break;
            case "GrayIn":
                isGrayIn = true;
                break;
            case "GrayOut":
                isGrayOut = true;
                break;
            case "Change":
                this.changedName = changedName;
                this.changedFace = changedFace;
                isCharaChange = true;
                break;
            default:
                return;
        }
    }

    public void SetStartPosition()
    {
        var pos = myTransform.position;
        if (transform.rotation.y == 0)
        {
            pos.x -= moveDistance;
        }
        else
        {
            pos.x += moveDistance;
        }
        myTransform.position = pos;
        bAlfa = 0;
        fAlfa = 0;
        SetAlpha();
        positionState = positionStateEnum.FadeOut;
    }

    public void SetAtlas(string atlasName, string face)
    {
        if(atlasName == null || face == null)
        {
            Debug.LogError("一番最初に発言するキャラクターもしくは表情が設定されていません。");
        }
        atlas = charaImageBehavior.GetAtlas(atlasName);
        body.sprite = atlas.GetSprite("body");
        FaceChange(face);
    }

    /// <summary>
    /// 会話途中でキャラクター立ち絵を差し替える時に使用
    /// </summary>
    void CharaChange()
    {
        if ((isPosStateFadeOut || isPosStateGrayOut) && posChangeStep == 1)
        {
            if (bAlfa > 0)             
            {
                bAlfa -= fadeSpeed;    
                fAlfa -= fadeSpeed;    
                SetAlpha();            
            }
            else if (bAlfa <= 0)
            {
                if (bRed < 1)
                {
                    bRed = 1;
                    bGreen = 1;
                    bBlue = 1;

                    fRed = 1;
                    fGreen = 1;
                    fBlue = 1;
                }
                SetAlpha();
                posChangeStep = 2;
            }
        }
        else if (isPosStateNormal && posChangeStep == 1)
        {
            isFadeOut = true;
        }

        if(posChangeStep == 2)
        {
            positionState = positionStateEnum.FadeOut;
            SetAtlas(changedName, changedFace);
            FaceChange("Normal");
            posChangeStep = 3;
        }

        if (posChangeStep == 3)
        {
            if (isPosStateFadeOut)
            {
                isFadeIn = true;
            }
            if (isPosStateNormal)
            {
                posChangeStep = 1;
                isCharaChange = false;
            }
        }
    }

    public bool IsAtlasNullChack()
    {
        if(!atlas)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string GetAtlasName()
    {
        return atlas.name;
    }
}

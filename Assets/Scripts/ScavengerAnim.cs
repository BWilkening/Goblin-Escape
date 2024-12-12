using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScavengerAnim : MonoBehaviour
{
    public float _speed = 1f;
    public int _FrameRate = 30;
    public bool _Loop = false;
    public bool moving = true;
    private Image mImage = null;
    RectTransform picture;
    Vector2 origin;
    [SerializeField] private RectTransform dest;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index = 0;
    public float wordSpeed = 0.6f;
    private Sprite[] mSprites = null;
    private float mTimePerFrame = 0f;
    private float mElapsedTime = 0f;
    public float distance = 0f;
    private int mCurrentFrame = 0;

    // Start is called before the first frame update
    void Start()
    {
        picture = GetComponent<RectTransform>();
        origin = picture.position;
        mImage = GetComponent<Image>();
        enabled = false;
        loadSpriteSheet1();
    }

    private void loadSpriteSheet1()
    {
        mSprites = Resources.LoadAll<Sprite>("oldman-walk");
        if (mSprites != null && mSprites.Length > 0)
        {
            mTimePerFrame = 1f / _FrameRate;
            Play();
        }
        else
        {
            Debug.LogError("Failed to load sprite sheet");
        }
    }

    private void loadSpriteSheet2()
    {
        mSprites = Resources.LoadAll<Sprite>("oldman-idle");
        if (mSprites != null && mSprites.Length > 0)
        {
            mTimePerFrame = 1f / _FrameRate;
            Play();
        }
        else
        {
            Debug.LogError("Failed to load sprite sheet");
        }
    }

    public void shiftLeft()
    {
        picture.position = Vector2.MoveTowards(picture.position, dest.position, (100 * Time.deltaTime));
        if (distance <= 5)
        {
        loadSpriteSheet2();
        mCurrentFrame = 0;
        moving = false;
        _Loop = false;
        }
    }

    public void Play()
    {
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(picture.position, dest.position);

        if(distance >= 0.5f)
        {
            moving = true;
            _Loop = true;
            shiftLeft();
        }

        mElapsedTime += Time.deltaTime * _speed;
        if(mElapsedTime >= mTimePerFrame)
        {
            mElapsedTime = 0f;
            ++mCurrentFrame;
            SetSprite();
            if(mCurrentFrame >= mSprites.Length)
            {
                if(_Loop)
                {
                    mCurrentFrame = 0;
                }
            }
        }
        if(mCurrentFrame >= mSprites.Length && _Loop == false)
        {
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
        }
    }

    private void SetSprite()
    {
        if(mCurrentFrame >= 0 && mCurrentFrame < mSprites.Length)
        {
            mImage.sprite = mSprites[mCurrentFrame];
        }
    }

    public void ResetPosition()
    {
        picture.position = new Vector2(origin.x, origin.y);
        loadSpriteSheet1();
        dialogueText.text = "";
        index = Random.Range(0, dialogue.Length);
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        //foreach(char letter in dialogue[index].ToCharArray())
        //{
            //dialogueText.text += letter;
            dialogueText.text = dialogue[index];
            yield return new WaitForSeconds(wordSpeed);
        //}
    }
}

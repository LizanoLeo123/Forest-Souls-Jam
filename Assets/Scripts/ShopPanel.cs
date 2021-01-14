using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [System.Serializable] class ShopItem
    {
        public Sprite icon;
        public int price;
        public bool isPremium = false;
        public bool isPurchased = false;
    }

    [SerializeField] List<ShopItem> ShopItemsList;

    public GameObject ItemPrefab;
    public Transform Container;
    public Sprite premiumSprite;

    public Text coinsLabel;
    public Text coinsMinusLabel;

    public Text diamondsLabel;
    public Text diamondsMinusLabel;

    //Instantiate shop items
    GameObject item;
    Button buyBtn;

    private Animator _animator;

    private int itemsBatch;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

        //First batch of items
        itemsBatch = 0;

        //Set current coins and diamonds
        coinsLabel.text = Game.Instance.coins.ToString();
        diamondsLabel.text = Game.Instance.diamonds.ToString();

        //int len = ShopItemsList.Count;

        //Show first three items
        for (int i = 0; i < 3; i++)
        {
            item = Instantiate(ItemPrefab, Container);
            item.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].icon;
            item.transform.GetChild(1).GetComponent<Text>().text = ShopItemsList[i].price.ToString();
            buyBtn = item.transform.GetChild(3).GetComponent<Button>();
            buyBtn.interactable = !ShopItemsList[i].isPurchased;
            if (ShopItemsList[i].isPurchased)
            {
                buyBtn.transform.GetChild(0).GetComponent<Text>().text = "OWNED";
            }

            //Add custom event to each button
            buyBtn.AddEventListener(i, OnShopItemBtnClicked);
        }
    }

    void OnShopItemBtnClicked(int itemIndex)
    {
        //If player has enough coins for item
        if (Game.Instance.HasEnoughCoins(ShopItemsList[itemIndex].price, ShopItemsList[itemIndex].isPremium))
        {
            Game.Instance.UseCoins(ShopItemsList[itemIndex].price, ShopItemsList[itemIndex].isPremium);

            //Check if item is premium
            if (ShopItemsList[itemIndex].isPremium)
            {
                //Update diamonds minus label
                diamondsMinusLabel.text = "-" + ShopItemsList[itemIndex].price.ToString();
                //Call Animation
                _animator.SetTrigger("PurchasedPremium");
                //Update current diamonds
                StartCoroutine(UpdateCurrentDiamonds());
            }
            else
            {
                //Update coins minus label
                coinsMinusLabel.text = "-" + ShopItemsList[itemIndex].price.ToString(); 
                //Call Animation
                _animator.SetTrigger("Purchased");
                //Udate current coins
                StartCoroutine(UpdateCurrentCoins());
            }
                
            // Purchase item logic
            ShopItemsList[itemIndex].isPurchased = true;

            //Disable the button
            int ind = Mathf.Abs(itemsBatch * 3 - itemIndex);
            buyBtn = Container.GetChild(ind).GetChild(3).GetComponent<Button>();
            buyBtn.interactable = false;
            buyBtn.transform.GetChild(0).GetComponent<Text>().text = "OWNED";
        }
        else //if player doesn't have enough coins
        {
            _animator.SetTrigger("NotEnoughCoins");
        }
    }

    public void LoadNextItemsBatch()
    {
        //Call animation
        _animator.SetTrigger("NextItems");

        itemsBatch++;
        //There should always be a mmultiple of 3 items in the store in total 3, 6, 9, 12...
        if (ShopItemsList.Count / 3 <= itemsBatch) //Reached items limit, starts again
            itemsBatch = 0;
        StartCoroutine(ChangeStoreItems());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenStore()
    {
        _animator.SetBool("ShowStore", true);
    }

    public void CloseStore()
    {
        _animator.SetBool("ShowStore", false);
    }

    public IEnumerator UpdateCurrentCoins()
    {
        yield return new WaitForSeconds(1f);
        coinsLabel.text = Game.Instance.coins.ToString();
    }

    public IEnumerator UpdateCurrentDiamonds()
    {
        yield return new WaitForSeconds(1f);
        diamondsLabel.text = Game.Instance.diamonds.ToString();
    }

    public IEnumerator ChangeStoreItems()
    {
        //Wait until Rope caontainer out of screen
        yield return new WaitForSeconds(0.7f);

        //Destroy old items
        GameObject[] oldItems = GameObject.FindGameObjectsWithTag("StoreItem");
        for (int i = 0; i < oldItems.Length; i++)
        {
            Destroy(oldItems[i]);
        }

        //Instantiate next items batch
        for (int i = 0; i < 3; i++)
        {
            item = Instantiate(ItemPrefab, Container);
            item.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[itemsBatch * 3 + i].icon;
            item.transform.GetChild(1).GetComponent<Text>().text = ShopItemsList[itemsBatch * 3 + i].price.ToString();
            //Check if premium to change coin icon
            if(ShopItemsList[itemsBatch * 3 + i].isPremium)
            {
                item.transform.GetChild(2).GetComponent<Image>().sprite = premiumSprite;
            }
            
            
            buyBtn = item.transform.GetChild(3).GetComponent<Button>();
            buyBtn.interactable = !ShopItemsList[itemsBatch * 3 + i].isPurchased;
            if (ShopItemsList[itemsBatch * 3 + i].isPurchased)
            {
                buyBtn.transform.GetChild(0).GetComponent<Text>().text = "OWNED";
            }

            //Add custom event to each button
            buyBtn.AddEventListener(itemsBatch * 3 + i, OnShopItemBtnClicked);
        }
    }
}

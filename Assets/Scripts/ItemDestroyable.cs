using UnityEngine;

public class ItemDestroyable : MonoBehaviour
{
    public enum TypeItem
    {
        STREET_LIGHT,
        HYDRANT,
        VENDING_MACHINE,
        NEWSPAPER_STAND
    }

    public TypeItem typeItem;

    public GameObject CAN_BLUE;
    public GameObject CAN_RED;
    public GameObject CAN_GREEN;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player1Attack") || collision.CompareTag("Player2Attack"))
        {
            GameObject can = null;
            switch (typeItem)
            {
                case TypeItem.STREET_LIGHT:
                    can = CAN_BLUE;
                    break;
                case TypeItem.HYDRANT:
                    can = CAN_RED;
                    break;
                case TypeItem.VENDING_MACHINE:
                    can = CAN_RED;
                    break;
                case TypeItem.NEWSPAPER_STAND:
                    can = CAN_GREEN;
                    break;
                default:
                    break;
            }
            if (can != null)
                Instantiate(can, transform.position, Quaternion.identity);

            Destroy(gameObject, 0.5f);
        }

    }

}

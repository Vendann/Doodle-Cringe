using UnityEngine;

public class Doodler : MonoBehaviour
{
    public float MoveSpeed; // скорость движения/перемещения по X
    public float JumpForce; // сила прыжка по Y
    public float MoveDecreaser; // замедление движения по X
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) // столкновение с твёрдым телом
    {
        if (rb.velocity.y <= 0) // Падает ли объект
            {
            Vector2 Velocity = rb.velocity; // Velocity = вектор текущей скорости объекта
            Velocity.y = JumpForce; // Сила прыжка
            rb.velocity = Velocity; // Новый вектор текущей скорости объекта
        }
    }

    void Update()
    {
        Vector2 Velocity = rb.velocity; // Вектор текущей скорости объекта

        if (Input.GetButton("Fire1")) //проверка нажатия ЛКМ
        {
            if (Input.mousePosition.x < Screen.width / 2) // проверка позиции мыши по X меньше половины ширины экрана
            {
                Velocity.x = -MoveSpeed; // Отрицательная скорость движения
            }
            else
            {
                Velocity.x = MoveSpeed; // Положительная скорость движения
            }
            rb.velocity = Velocity; // Новый вектор текущей скорости объекта
        }
        else
        {
            Velocity.x *= MoveDecreaser; // корректируем составляющую X Velocity
            rb.velocity = Velocity; // замедляем скорость движения по X (если MoveDecreaser < 1)
        }
    }
}

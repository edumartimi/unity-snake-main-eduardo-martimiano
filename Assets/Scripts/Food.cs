using UnityEngine;

public class Food : MonoBehaviour
{
    public Collider2D gridArea;

    private void Start()
    {
        //chamando o metodo que coloca a comida em uma posição aleatoria
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        //criando uma variavel onde pode esta as comidas
        Bounds bounds = this.gridArea.bounds;

        //criando variaveis q falam o maximo onde a comida pode ir
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // randomizando o lugar da comida
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        this.transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //quando encostar na comida chama o metodo q cria uma comida e coloca ela num lugar aleatorio no mapa
        RandomizePosition();
    }

}

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    //craiando uma lista com os tranforms
    private List<Transform> _segments = new List<Transform>();
    //criando variavel com o trasform
    public Transform segmentPrefab;
    //criando uma varivel de direção
    public Vector2 direction = Vector2.right;
    //criando uma variavel que dita o tamanho inicial
    public int initialSize = 4;
        
    //quando iniciar o jogo
    private void Start()
    {
        //chamando o metodo resetstate
        ResetState();
    }
    
    //metodo q atualiza cada frame do jogo
    private void Update()
    {
        //se a direção x for diferente de 0
        if (this.direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                this.direction = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                this.direction = Vector2.down;
            }
        }
        //se a direção Y for diferente de 0
        else if (this.direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                this.direction = Vector2.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                this.direction = Vector2.left;
            }
        }
    }

    private void FixedUpdate()
    {
        //for percorrendo o _segments.count
        for (int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i - 1].position;
        }

        //somando os valores e atribuindo as variaveis
        float x = Mathf.Round(this.transform.position.x) + this.direction.x;
        float y = Mathf.Round(this.transform.position.y) + this.direction.y;

        this.transform.position = new Vector2(x, y);
    }
    
    //criando um metodo grow
    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }
    
    //criando um metodo resetstate
    public void ResetState()
    {
        //atribuindo os valores do de direção a 2 vetores
        this.direction = Vector2.right;
        this.transform.position = Vector3.zero;

        // percorrendo o segments.count e destruindo os objetos
        for (int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }

        //utilizando os metodos de _segments
        _segments.Clear();
        _segments.Add(this.transform);

        //percorrendo o this.initialsize e chamando o metodo grow()
        for (int i = 0; i < this.initialSize - 1; i++) {
            Grow();
        }
    }

    //metodo para quando encostar no trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        //se a tag do triger for igual a food
        if (other.tag == "Food") {
            Grow();
        } 
        //se a tag do trigger for igual a obstacle
        else if (other.tag == "Obstacle") {
            ResetState();
        }
    }

}

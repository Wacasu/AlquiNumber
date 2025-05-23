using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class QuizDragAndDrop : MonoBehaviour
{
    public GameObject draggableItem; // El item que se puede arrastrar
    public GameObject cauldron; // El caldero donde se debe soltar el item
    public GameObject questionPanel; // Panel que contiene las preguntas
    public TextMeshProUGUI questionText; // Texto de la pregunta
    public Button[] answerButtons; // Botones de respuesta
    public TextMeshProUGUI[] answerTexts; // Textos de las respuestas

    private bool isDragging = false;
    private Vector3 originalPosition;
    private bool isQuestionActive = false;

    // Estructura para almacenar preguntas y respuestas
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers;
        public int correctAnswerIndex;
    }

    public Question[] questions; // Array de preguntas

    void Start()
    {
        // Inicializar el panel de preguntas como inactivo
        questionPanel.SetActive(false);
        
        // Configurar el evento de arrastre
        EventTrigger trigger = draggableItem.AddComponent<EventTrigger>();
        
        // Evento de inicio de arrastre
        EventTrigger.Entry beginDrag = new EventTrigger.Entry();
        beginDrag.eventID = EventTriggerType.BeginDrag;
        beginDrag.callback.AddListener((data) => { OnBeginDrag(); });
        trigger.triggers.Add(beginDrag);

        // Evento de arrastre
        EventTrigger.Entry drag = new EventTrigger.Entry();
        drag.eventID = EventTriggerType.Drag;
        drag.callback.AddListener((data) => { OnDrag(); });
        trigger.triggers.Add(drag);

        // Evento de fin de arrastre
        EventTrigger.Entry endDrag = new EventTrigger.Entry();
        endDrag.eventID = EventTriggerType.EndDrag;
        endDrag.callback.AddListener((data) => { OnEndDrag(); });
        trigger.triggers.Add(endDrag);
    }

    void OnBeginDrag()
    {
        if (!isQuestionActive)
        {
            isDragging = true;
            originalPosition = draggableItem.transform.position;
        }
    }

    void OnDrag()
    {
        if (isDragging)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    void OnEndDrag()
    {
        if (isDragging)
        {
            isDragging = false;
            
            // Verificar si el item está sobre el caldero
            if (IsOverCauldron())
            {
                ShowRandomQuestion();
            }
            else
            {
                // Devolver el item a su posición original
                draggableItem.transform.position = originalPosition;
            }
        }
    }

    bool IsOverCauldron()
    {
        // Obtener la posición del mouse en el mundo
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Verificar si el mouse está sobre el caldero
        Collider2D cauldronCollider = cauldron.GetComponent<Collider2D>();
        return cauldronCollider.OverlapPoint(mousePosition);
    }

    // Este método debe ser llamado cuando el item correcto es soltado en el slot
    public void OnCorrectItemDropped()
    {
        ShowRandomQuestion();
    }

    void ShowRandomQuestion()
    {
        if (questions.Length > 0)
        {
            isQuestionActive = true;
            questionPanel.SetActive(true);
            
            // Seleccionar una pregunta aleatoria
            int randomIndex = Random.Range(0, questions.Length);
            Question currentQuestion = questions[randomIndex];
            questionText.text = currentQuestion.questionText;

            // Configurar los botones de respuesta
            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i < currentQuestion.answers.Length)
                {
                    answerTexts[i].text = currentQuestion.answers[i];
                    int answerIndex = i; // Necesario para el closure
                    answerButtons[i].onClick.RemoveAllListeners();
                    answerButtons[i].onClick.AddListener(() => CheckAnswer(answerIndex, currentQuestion.correctAnswerIndex));
                }
                else
                {
                    answerButtons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    void CheckAnswer(int selectedAnswerIndex, int correctAnswerIndex)
    {
        if (selectedAnswerIndex == correctAnswerIndex)
        {
            // Respuesta correcta
            Debug.Log("¡Respuesta correcta!");
            // Aquí puedes agregar efectos de sonido o animaciones
        }
        else
        {
            // Respuesta incorrecta
            Debug.Log("Respuesta incorrecta");
            // Aquí puedes agregar efectos de sonido o animaciones
        }

        // Ocultar el panel de preguntas
        questionPanel.SetActive(false);
        isQuestionActive = false;
        
        // Devolver el item a su posición original
        draggableItem.transform.position = originalPosition;
    }
} 
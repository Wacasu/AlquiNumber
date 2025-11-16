# 📚 Documentación Técnica - AlquiNumber

## 🎯 Propósito de este Documento
Este documento explica cada script del proyecto para facilitar la comprensión del código y servir como evidencia del trabajo realizado.

**Nota:** El proyecto está enfocado en juegos de destreza: 🧩 Puzzles, ➕ Ejercicios Matemáticos y 🎴 Memorama.

---

## 📂 Estructura de Scripts

### 1. **MenuInicial.cs** - Controlador del Menú Principal

#### 📝 Descripción
Controla las acciones del menú inicial del juego, permitiendo navegar entre escenas y salir de la aplicación.

#### 🔧 Funcionalidades
```csharp
// Navega a la siguiente escena (inicio del juego)
public void Jugar()

// Cierra la aplicación
public void Salir()

// Regresa a la escena anterior
public void Atras()
```

#### 💡 Explicación Técnica
- Utiliza `SceneManager` de Unity para cargar escenas
- `buildIndex` identifica el número de escena en el Build Settings
- `Application.Quit()` cierra la aplicación (solo funciona en builds, no en editor)

#### 📊 Uso en el Proyecto
Conectado a botones de UI mediante eventos de Unity. Es el punto de entrada principal del juego.

---

### 2. **ProblemaManager.cs** - Gestor de Problemas y Preguntas

#### 📝 Descripción
Lee problemas matemáticos desde un archivo CSV y genera preguntas de opción múltiple de forma aleatoria.

#### 🔧 Funcionalidades Principales

**Carga de CSV:**
```csharp
// Lee el archivo CSV asignado en el Inspector
string[] lineas = archivoCSV.text.Split(...)
```

**Selección Aleatoria:**
```csharp
// Elige un problema aleatorio de la lista
string[] fila = filasValidas[Random.Range(0, filasValidas.Count)]
```

**Generación de Opciones:**
- Selecciona 1 método correcto
- Selecciona 3 métodos incorrectos
- Mezcla las opciones aleatoriamente

#### 💡 Explicación Técnica
- **Formato CSV Esperado:**
  - Primera fila: Encabezados (tipos de juegos: Puzzle, Matemáticas, Memorama)
  - Filas siguientes: Problema, seguido de 1 (correcto) o 0 (incorrecto) para cada tipo
  
**Ejemplo:**
```
Problema,Puzzle,EjercicioMatematico,Memorama
Resolver sudoku 3x3,1,0,0
Calcular 15+23,0,1,0
Emparejar símbolos,0,0,1
```

- Usa LINQ (`System.Linq`) para filtrar y ordenar datos
- Valida que haya suficientes opciones antes de generar la pregunta

#### 📊 Uso en el Proyecto
Componente central del juego. Se asigna un CSV con problemas en el Inspector de Unity y genera preguntas automáticamente.

---

### 3. **MetodoNumericoSelector.cs** - Enumeración de Tipos de Juego

#### 📝 Descripción
Define un enum con los tipos de juegos de destreza disponibles. **NOTA: Este archivo necesita actualizarse para reflejar los nuevos tipos de juegos.**

#### 🔧 Estructura Actual (Requiere Actualización)
```csharp
public enum MetodoNumerico
{
    Biseccion,
    NewtonRaphson,
    // ... otros métodos numéricos
}
```

#### 🔧 Estructura Sugerida (Nueva)
```csharp
public enum TipoJuego
{
    Puzzle,
    EjercicioMatematico,
    Memorama
}
```

#### 💡 Explicación Técnica
- Enum proporciona tipo de dato seguro para tipos de juegos
- Evita errores de tipeo al usar strings
- Facilita autocompletado en el IDE

#### 📊 Uso en el Proyecto
Referenciado por `Ingrediente.cs` para asignar tipos de juego a elementos del juego.

---

### 4. **PlayerProgress.cs** - Sistema de Progreso Persistente

#### 📝 Descripción
Gestiona el progreso del jugador usando el patrón Singleton. Guarda y carga datos entre sesiones.

#### 🔧 Funcionalidades Principales

**Singleton Pattern:**
```csharp
// Garantiza una única instancia en todo el juego
if (Instance == null) {
    Instance = this;
    DontDestroyOnLoad(gameObject); // Persiste entre escenas
}
```

**Guardado de Datos:**
```csharp
void GuardarProgreso() {
    PlayerPrefs.SetInt("NivelMax", nivelMaxDesbloqueado);
    PlayerPrefs.SetInt("XP", experiencia);
}
```

**Carga de Datos:**
```csharp
void CargarProgreso() {
    nivelMaxDesbloqueado = PlayerPrefs.GetInt("NivelMax", 1);
    experiencia = PlayerPrefs.GetInt("XP", 0);
}
```

#### 💡 Explicación Técnica
- **Singleton**: Asegura que solo exista una instancia en toda la aplicación
- **DontDestroyOnLoad**: Evita que el objeto se destruya al cambiar de escena
- **PlayerPrefs**: Sistema de Unity para guardar datos simples (int, float, string)
- Los datos se guardan automáticamente en el sistema del usuario

#### 📊 Uso en el Proyecto
Accesible desde cualquier script mediante `PlayerProgress.Instance`. Mantiene el progreso del jugador durante toda la sesión y entre sesiones.

---

### 5. **DragDrop.cs** - Sistema de Arrastrar y Soltar

#### 📝 Descripción
Implementa la funcionalidad de drag and drop usando el sistema de eventos de Unity UI.

#### 🔧 Interfaces Implementadas
```csharp
IPointerDownHandler    // Cuando se presiona sobre el elemento
IBeginDragHandler      // Cuando comienza el arrastre
IDragHandler           // Durante el arrastre
IEndDragHandler        // Cuando termina el arrastre
```

#### 💡 Explicación Técnica
- **CanvasGroup**: Controla transparencia y bloqueo de raycasts
  - `alpha = 0.6f`: Hace el objeto semi-transparente durante el arrastre
  - `blocksRaycasts = false`: Permite detectar elementos debajo durante el arrastre
  
- **RectTransform.anchoredPosition**: Posición del elemento en el Canvas
- `eventData.delta`: Movimiento del mouse/touch en píxeles
- `canvas.scaleFactor`: Ajusta según la escala del Canvas

#### 📊 Uso en el Proyecto
Aplicado a elementos UI que el jugador puede arrastrar (ingredientes, opciones, etc.)

---

### 6. **ItemSlot.cs** - Zona de Soltado

#### 📝 Descripción
Define áreas donde se pueden soltar elementos arrastrados. Valida y procesa el soltado.

#### 🔧 Funcionalidades
```csharp
public void OnDrop(PointerEventData eventData) {
    // Reproduce sonido
    // Muestra efecto visual
    // Desactiva el elemento soltado
    // Activa condición lógica (conditionMet = true)
}
```

#### 💡 Explicación Técnica
- **OnDrop**: Se ejecuta cuando un elemento se suelta sobre este objeto
- **AudioSource.PlayClipAtPoint**: Reproduce sonido en posición 3D
- **Instantiate**: Crea efecto visual (partículas, animación, etc.)
- **conditionMet**: Bandera booleana para validar si se completó una acción

#### 📊 Uso en el Proyecto
Colocado en áreas objetivo donde el jugador debe soltar elementos para validar respuestas.

---

### 7. **Ingrediente.cs** - Componente de Elemento Interactivo

#### 📝 Descripción
Define propiedades de elementos que pueden ser arrastrados en el juego.

#### 🔧 Propiedades
```csharp
public string nombreIngrediente;      // Nombre visible del elemento
public MetodoNumerico metodoAsignado; // Método numérico asociado
```

#### 💡 Explicación Técnica
- Componente simple de datos (Data Component)
- Asocia un método numérico a un elemento visual
- Permite validar si el jugador eligió el método correcto

#### 📊 Uso en el Proyecto
Aplicado a objetos GameObjects que representan opciones o ingredientes en el juego.

---

### 8. **LevelMenuController.cs** - Controlador de Menú de Niveles

#### 📝 Descripción
Gestiona la interfaz de selección de niveles, bloqueando niveles no desbloqueados.

#### 🔧 Funcionalidades
```csharp
// Habilita solo niveles desbloqueados
if (levelIndex <= maxLevelUnlocked) {
    levelButtons[i].interactable = true;
}
```

#### 💡 Explicación Técnica
- **Button.interactable**: Habilita/deshabilita botones
- Usa índices para cargar escenas: `"Level" + levelIndex`
- Los listeners se asignan dinámicamente al iniciar

#### 📊 Uso en el Proyecto
Controla la pantalla donde el jugador selecciona qué nivel jugar. Debe sincronizarse con `PlayerProgress` para obtener niveles desbloqueados.

---

### 9. **CambiaEscena.cs** - Navegador de Escenas

#### 📝 Descripción
Utilidad simple para avanzar a la siguiente escena en el orden del Build Settings.

#### 🔧 Funcionalidad
```csharp
public void CargarSiguienteEscena() {
    int escenaActual = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(escenaActual + 1);
}
```

#### 💡 Explicación Técnica
- Obtiene el índice de la escena actual
- Suma 1 para cargar la siguiente escena
- Requiere que las escenas estén ordenadas correctamente en Build Settings

#### 📊 Uso en el Proyecto
Conectado a botones "Siguiente" o "Continuar" en diferentes pantallas.

---

## 🔄 Flujo de Datos del Sistema

```
MenuInicial → LevelMenuController → Escena de Juego
                                        ↓
                            ProblemaManager (lee CSV)
                                        ↓
                            Genera pregunta aleatoria
                                        ↓
                            DragDrop + ItemSlot (jugador interactúa)
                                        ↓
                            PlayerProgress (guarda resultado)
```

---

## 🛠️ Tecnologías Utilizadas

- **Unity Engine**: Motor de juego
- **C#**: Lenguaje de programación
- **Unity UI System**: Sistema de interfaz de usuario
- **Event System**: Sistema de eventos para interacciones
- **PlayerPrefs**: Persistencia de datos simple
- **LINQ**: Consultas y manipulación de datos
- **CSV Parsing**: Lectura de archivos CSV

---

## 📝 Notas para Desarrollo

1. **CSV debe estar en formato correcto**: Primera fila con encabezados (Puzzle, EjercicioMatematico, Memorama), siguientes con datos
2. **Escenas deben estar en Build Settings**: Ordenadas secuencialmente
3. **PlayerProgress es Singleton**: No crear múltiples instancias
4. **Canvas debe estar configurado**: Para que DragDrop funcione correctamente
5. **Actualizar MetodoNumericoSelector.cs**: Cambiar a TipoJuego con los nuevos tipos (Puzzle, EjercicioMatematico, Memorama)


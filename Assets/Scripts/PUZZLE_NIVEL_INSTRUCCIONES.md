# 🧩 Guía de Integración del Puzzle como Nivel

## 📋 Descripción
Este documento explica cómo usar e integrar el sistema de puzzle como nivel en el juego AlquiNumber.

## 🎯 Resumen
El sistema de puzzle permite crear niveles donde el jugador debe arrastrar y colocar piezas de un rompecabezas en sus posiciones correctas para completar una imagen.

---

## 📂 Archivos Creados

### Scripts Principales

1. **PuzzleNivel.cs** - Controlador principal del nivel de puzzle
   - Gestiona la lógica del rompecabezas
   - Controla el sistema de drag and drop
   - Maneja la detección de victoria
   - Integra con el sistema de progreso del juego

2. **PiezaPuzzle.cs** - Controlador de cada pieza individual
   - Detecta cuando una pieza está en su posición correcta
   - Maneja la selección y movimiento de piezas
   - Proporciona feedback visual

3. **PuzzleMenuController.cs** - Controlador de selección de niveles
   - Permite seleccionar qué nivel de puzzle jugar
   - Maneja el desbloqueo de niveles
   - Integra con PlayerProgress

---

## 🚀 Configuración en Unity

### Paso 1: Preparar la Escena del Puzzle

1. **Crear una nueva escena** llamada `PuzzleNivel.unity` (o el nombre que prefieras)

2. **Configurar la cámara**:
   - Asegúrate de que la cámara tenga un componente `Physics2D Raycaster`
   - Configurar el tamaño de la cámara según necesites

3. **Crear las piezas del puzzle**:
   - Necesitas crear 36 GameObjects nombrados exactamente: `Pieza (0)`, `Pieza (1)`, ..., `Pieza (35)`
   - **IMPORTANTE**: Cada pieza debe tener esta estructura:
   
   ```
   Pieza (0)  ← GameObject padre (nombre exacto con paréntesis y espacio)
   ├── Components del padre:
   │   ├── Transform
   │   ├── SpriteRenderer (opcional, puede ir en el hijo)
   │   ├── SortingGroup (componente necesario)
   │   ├── PiezaPuzzle (script que agregamos)
   │   ├── BoxCollider2D o CircleCollider2D (para detectar clics)
   │   └── Tag: "Puzzle" (muy importante)
   │
   └── Puzzle  ← GameObject HIJO (nombre exacto "Puzzle")
       └── Components del hijo:
           ├── Transform
           └── SpriteRenderer (AQUÍ va el sprite del puzzle)
   ```
   
   **¿Por qué un hijo "Puzzle"?**
   
   El código en `PuzzleNivel.cs` busca específicamente este hijo para asignarle el sprite:
   ```csharp
   Transform puzzleTransform = pieza.transform.Find("Puzzle");
   if (puzzleTransform != null)
   {
       SpriteRenderer spriteRenderer = puzzleTransform.GetComponent<SpriteRenderer>();
       if (spriteRenderer != null)
       {
           spriteRenderer.sprite = spriteNivel; // Asigna el sprite del puzzle
       }
   }
   ```
   
   **Razón del diseño**:
   - El GameObject padre (`Pieza (0)`) maneja la posición, colisión y lógica de encaje
   - El GameObject hijo (`Puzzle`) contiene solo el SpriteRenderer que muestra la imagen
   - Esto permite separar la lógica del objeto de su representación visual
   - Cada pieza muestra la MISMA imagen completa del puzzle, pero solo se ve una parte según su posición

### Paso 2: Configurar PuzzleNivel

1. **Agregar el script PuzzleNivel** a un GameObject vacío o a la cámara

2. **Configurar los campos en el Inspector**:
   - **Niveles Puzzle**: Array de Sprites con las imágenes de los puzzles (Puzzle1.png, Puzzle2.png, etc.)
   - **Panel Victoria**: GameObject que se muestra al completar el puzzle
   - **Panel Game Over**: GameObject opcional para cuando se pierda
   - **Textos UI**: TextMeshProUGUI para mostrar nivel y progreso
   - **Botones**: Botones para menú, siguiente nivel, reintentar
   - **Sonidos**: AudioClips para victoria y pieza encajada
   - **Total Piezas**: Número de piezas (por defecto 36 para un puzzle 6x6)

### Paso 3: Configurar las Piezas (PASO A PASO)

**Opción A: Crear manualmente cada pieza**

1. **Crear el GameObject padre**:
   - Crea un GameObject vacío
   - Renómbralo exactamente como `Pieza (0)` (con paréntesis y espacio)
   - Posiciónalo donde quieras que esté cuando esté completada (esta será su posición correcta)

2. **Configurar componentes del padre**:
   - Agrega `SortingGroup` → Component > Rendering > Sorting Group
   - Agrega `PiezaPuzzle` → Component > Scripts > PiezaPuzzle
   - Agrega `BoxCollider2D` → Component > Physics 2D > Box Collider 2D
   - Configura el **Tag** como "Puzzle" (si no existe, créalo en Edit > Project Settings > Tags and Layers)

3. **Crear el hijo "Puzzle"**:
   - Clic derecho sobre `Pieza (0)` → Create Empty
   - Renómbralo exactamente como `Puzzle` (sin paréntesis, sin espacios extra)
   - Posiciónalo en (0, 0, 0) relativo al padre (esto es importante)

4. **Configurar el hijo "Puzzle"**:
   - Agrega `SpriteRenderer` → Component > Rendering > Sprite Renderer
   - El sprite se asignará automáticamente por el script `PuzzleNivel`

5. **Repetir para todas las piezas**:
   - Crea `Pieza (1)`, `Pieza (2)`, ..., hasta `Pieza (35)`
   - Cada una debe tener su hijo "Puzzle" con SpriteRenderer

**Estructura visual completa**:
```
Pieza (0)  ← Padre
├── [Transform] (posición inicial = posición correcta)
├── [SortingGroup]
├── [PiezaPuzzle] script
├── [BoxCollider2D]
└── [Tag: "Puzzle"]
    │
    └── Puzzle  ← Hijo
        ├── [Transform] (posición local 0,0,0)
        └── [SpriteRenderer] ← Aquí se asignará el sprite del puzzle
```

**Nota importante**: El nombre del hijo DEBE ser exactamente "Puzzle" (sin comillas, sin espacios, sin números). El código usa `transform.Find("Puzzle")` para encontrarlo.

### Paso 4: Importar los Sprites de Puzzle

1. **Copiar los sprites** desde `Puzzle/Assets/Sprites/Niveles/` a tu proyecto:
   - Copiar `Puzzle1.png` a `Puzzle10.png`
   - Importar como Sprites en Unity

2. **⚠️ IMPORTANTE: Configurar los sprites para que sean legibles**:
   - Selecciona cada sprite (Puzzle1.png, Puzzle2.png, etc.) en el Project
   - En el Inspector, busca la sección **"Advanced"**
   - Marca la casilla **"Read/Write Enabled"** ✅
   - Haz clic en **"Apply"**
   - **¿Por qué?** El código necesita leer los píxeles de la textura para dividirla en 36 piezas. Si no está habilitado, verás un error en la consola.

3. **Asignar los sprites** al array `Niveles Puzzle` en el componente `PuzzleNivel`:
   - Selecciona el GameObject con el script `PuzzleNivel`
   - En el Inspector, encuentra el campo "Niveles Puzzle"
   - Arrastra los 10 sprites (Puzzle1 a Puzzle10) al array

### Paso 5: Configurar la UI

1. **Crear paneles de UI**:
   - Panel de Victoria (se muestra al completar)
   - Panel de Game Over (opcional)
   - Texto de progreso
   - Texto de nivel
   - Botones de navegación

2. **Configurar los botones**:
   - Botón "Menú Principal" → Regresar al menú
   - Botón "Siguiente Nivel" → Pasar al siguiente puzzle
   - Botón "Reintentar" → Reiniciar el puzzle actual

---

## 🎮 Cómo Funciona

### Sistema de Drag and Drop

1. **Selección**: El jugador hace clic en una pieza para seleccionarla
2. **Movimiento**: La pieza sigue el cursor del mouse
3. **Encaje**: Cuando la pieza está cerca de su posición correcta (< 0.5 unidades), se encaja automáticamente
4. **Progreso**: Cada pieza encajada incrementa el contador

### Sistema de Niveles

- **Niveles**: Hay 10 niveles de puzzle (0-9) almacenados en `PlayerPrefs`
- **Progreso**: Se guarda cuándo se completa cada nivel
- **Desbloqueo**: Los niveles se desbloquean secuencialmente

### Integración con PlayerProgress

- Al completar un puzzle, se otorgan 100 puntos de experiencia
- El progreso se guarda automáticamente
- Se integra con el sistema de niveles principal

---

## 🔧 Personalización

### Ajustar la Dificultad

En `PiezaPuzzle.cs`:
- **distanciaEncaje**: Distancia mínima para encajar (por defecto 0.5)
- **rangoX**: Rango aleatorio de posiciones iniciales en X
- **rangoY**: Rango aleatorio de posiciones iniciales en Y

### Cambiar el Número de Piezas

En `PuzzleNivel.cs`:
- **totalPiezas**: Cambiar el número de piezas (debe coincidir con las piezas en la escena)

### Agregar Más Niveles

1. Agregar más sprites al array `nivelesPuzzle`
2. Los niveles se cargarán automáticamente

---

## 📝 Notas Importantes

1. **Nombres de Objetos**: Las piezas DEBEN llamarse exactamente `Pieza (0)`, `Pieza (1)`, etc.
2. **Tags**: Las piezas DEBEN tener el tag "Puzzle" para que funcione la detección
3. **SortingGroup**: Necesario para que las piezas se superpongan correctamente al arrastrarlas
4. **Collider2D**: Requerido para la detección de clics con Physics2D Raycast

---

## 🐛 Solución de Problemas

### Las piezas no se seleccionan
- Verificar que las piezas tengan tag "Puzzle"
- Verificar que haya un Collider2D en cada pieza
- Verificar que la cámara tenga Physics2D Raycaster

### Las piezas no se encajan
- Verificar que `distanciaEncaje` no sea demasiado pequeño
- Verificar que `posicionCorrecta` esté configurada correctamente en cada pieza

### Todas las piezas muestran la misma imagen completa
- **Solución**: Asegúrate de que los sprites tengan "Read/Write Enabled" activado
- Ve a cada sprite → Inspector → Advanced → Read/Write Enabled ✅
- Si el error persiste, verifica que el sprite esté configurado correctamente como "Sprite (2D and UI)" en el tipo de textura

### El progreso no se guarda
- Verificar que PlayerProgress esté en la escena y configurado correctamente
- Verificar que los PlayerPrefs se guarden correctamente

---

## 🎯 Próximos Pasos

1. Crear la escena `PuzzleNivel.unity` con las piezas configuradas
2. Importar los sprites de los puzzles
3. Configurar la UI
4. Probar el sistema completo
5. Integrar con el menú principal usando `PuzzleMenuController`

---

## 📚 Referencias

- Scripts originales del proyecto Puzzle: `Puzzle/Assets/Scripts/`
- Sprites originales: `Puzzle/Assets/Sprites/Niveles/`
- Sistema de progreso: `PlayerProgress.cs`
- Sistema de navegación: `LevelMenuController.cs`


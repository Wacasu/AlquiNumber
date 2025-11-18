# 🎨 Guía de Configuración de la Pantalla del Puzzle

## 📋 Resumen
Esta guía te explica paso a paso cómo configurar todos los elementos de UI necesarios para que el `GameManagerPuzz` funcione correctamente.

---

## 🚀 Paso 1: Crear el Canvas Principal

1. **Crear un Canvas**:
   - Clic derecho en la jerarquía → **UI → Canvas**
   - Esto creará automáticamente un Canvas, EventSystem y GraphicRaycaster

2. **Configurar el Canvas**:
   - Selecciona el Canvas
   - En el Inspector, asegúrate de que:
     - **Render Mode**: Screen Space - Overlay
     - **Canvas Scaler**: Scale With Screen Size
     - **Reference Resolution**: 1920 x 1080 (o la resolución que uses)

---

## ⏱️ Paso 2: Crear el Texto del Timer

1. **Crear el texto del tiempo**:
   - Clic derecho sobre el Canvas → **UI → Text - TextMeshPro**
   - Si te pide importar TMP Essentials, haz clic en "Import TMP Essentials"

2. **Configurar el texto**:
   - Renómbralo como `TimerTexto`
   - Posiciónalo en la esquina superior (por ejemplo: Top-Left)
   - En el Inspector:
     - **Text**: "Tiempo: 120s" (texto inicial)
     - **Font Size**: 36 (o el tamaño que prefieras)
     - **Color**: Blanco o el color que prefieras
     - **Alignment**: Left (o Center)
     - **Rect Transform**: 
       - **Anchor**: Top-Left
       - **Position**: X: 50, Y: -50 (ajusta según necesites)

---

## 🎉 Paso 3: Crear el Panel de Victoria

1. **Crear el panel**:
   - Clic derecho sobre el Canvas → **UI → Panel**
   - Renómbralo como `PantallaVictoria`

2. **Configurar el panel**:
   - **Rect Transform**:
     - **Anchor**: Stretch-Stretch (esquinas)
     - **Left, Right, Top, Bottom**: 0 (para que cubra toda la pantalla)
   - **Image Component**:
     - **Color**: Negro con transparencia (R:0, G:0, B:0, A:200) o el color que prefieras
   - **Inicialmente desactivado**: Desmarca el checkbox en la parte superior del Inspector

3. **Agregar contenido al panel**:
   - **Texto de Victoria**:
     - Clic derecho sobre `PantallaVictoria` → **UI → Text - TextMeshPro**
     - Renómbralo como `TextoVictoria`
     - **Text**: "¡Felicidades! ¡Completaste el Puzzle!"
     - **Font Size**: 48
     - **Alignment**: Center
     - **Color**: Verde o dorado
     - **Rect Transform**: Centrado en el panel

   - **Botón "Menú Principal"**:
     - Clic derecho sobre `PantallaVictoria` → **UI → Button - TextMeshPro**
     - Renómbralo como `BotonMenuVictoria`
     - **Text del hijo**: "Menú Principal"
     - **Rect Transform**: Posiciónalo en la parte inferior del panel

   - **Botón "Siguiente Nivel"** (opcional):
     - Clic derecho sobre `PantallaVictoria` → **UI → Button - TextMeshPro**
     - Renómbralo como `BotonSiguienteNivel`
     - **Text del hijo**: "Siguiente Nivel"
     - **Rect Transform**: Posiciónalo al lado del botón de menú

---

## 💀 Paso 4: Crear el Panel de Game Over

1. **Crear el panel**:
   - Clic derecho sobre el Canvas → **UI → Panel**
   - Renómbralo como `PantallaGameOver`

2. **Configurar el panel** (igual que el de Victoria):
   - **Rect Transform**: Stretch-Stretch, todos los valores en 0
   - **Image Color**: Rojo oscuro con transparencia o el color que prefieras
   - **Inicialmente desactivado**: Desmarca el checkbox

3. **Agregar contenido**:
   - **Texto de Derrota**:
     - Clic derecho sobre `PantallaGameOver` → **UI → Text - TextMeshPro`
     - Renómbralo como `TextoDerrota`
     - **Text**: "¡Se acabó el tiempo! Inténtalo de nuevo"
     - **Font Size**: 48
     - **Alignment**: Center
     - **Color**: Rojo
     - **Rect Transform**: Centrado

   - **Botón "Menú Principal"**:
     - Clic derecho sobre `PantallaGameOver` → **UI → Button - TextMeshPro`
     - Renómbralo como `BotonMenuGameOver`
     - **Text del hijo**: "Menú Principal"
     - **Rect Transform**: Posiciónalo en la parte inferior

   - **Botón "Reintentar"** (opcional):
     - Clic derecho sobre `PantallaGameOver` → **UI → Button - TextMeshPro`
     - Renómbralo como `BotonReintentar`
     - **Text del hijo**: "Reintentar"
     - Puedes agregar un script que llame a `GameManagerPuzz.Reiniciar()`

---

## 🔊 Paso 5: Configurar los Sonidos

1. **Agregar AudioSource al GameManagerPuzz**:
   - Selecciona el GameObject que tiene el script `GameManagerPuzz`
   - En el Inspector, haz clic en **Add Component**
   - Busca y agrega **Audio Source**
   - Configura:
     - **Play On Awake**: Desmarcado
     - **Volume**: 1.0 (o el volumen que prefieras)

2. **Importar los clips de audio**:
   - Crea o importa tus archivos de audio (`.mp3`, `.wav`, `.ogg`)
   - Colócalos en una carpeta como `Assets/Audio/` o `Assets/Sounds/`

3. **Asignar los clips en el Inspector**:
   - Selecciona el GameObject con `GameManagerPuzz`
   - En el Inspector, encuentra la sección **"Sonidos"**:
     - **Sonido Victoria**: Arrastra el clip de audio de victoria
     - **Sonido Derrota**: Arrastra el clip de audio de derrota

---

## 🔗 Paso 6: Asignar Referencias en GameManagerPuzz

1. **Selecciona el GameObject con GameManagerPuzz**:
   - Debe ser el mismo GameObject que tiene el script `GameManagerPuzz`

2. **En el Inspector, asigna todas las referencias**:

   **Sección "UI"**:
   - **Timer Texto**: Arrastra el `TimerTexto` que creaste
   - **Pantalla Game Over**: Arrastra el `PantallaGameOver`
   - **Pantalla Victoria**: Arrastra el `PantallaVictoria`

   **Sección "Botones Pantallas"**:
   - **Boton Menu Game Over**: Arrastra el `BotonMenuGameOver`
   - **Boton Menu Victoria**: Arrastra el `BotonMenuVictoria`
   - **Boton Siguiente Nivel**: Arrastra el `BotonSiguienteNivel`

   **Sección "Sonidos"**:
   - **Sonido Victoria**: Arrastra el AudioClip de victoria
   - **Sonido Derrota**: Arrastra el AudioClip de derrota

   **Sección "Configuración del juego"**:
   - **Tiempo Limite**: 120 (segundos) o el tiempo que prefieras

---

## 📐 Estructura Final de la Jerarquía

Tu jerarquía debería verse así:

```
Canvas
├── TimerTexto (TextMeshProUGUI)
├── PantallaVictoria (Panel)
│   ├── TextoVictoria (TextMeshProUGUI)
│   ├── BotonMenuVictoria (Button)
│   └── BotonSiguienteNivel (Button)
└── PantallaGameOver (Panel)
    ├── TextoDerrota (TextMeshProUGUI)
    └── BotonMenuGameOver (Button)

GameManagerPuzz (GameObject con el script)
└── AudioSource (Component)
```

---

## ✅ Checklist de Configuración

Antes de probar, verifica que:

- [ ] Canvas creado y configurado
- [ ] TimerTexto creado y asignado
- [ ] PantallaVictoria creada, desactivada inicialmente, y asignada
- [ ] PantallaGameOver creada, desactivada inicialmente, y asignada
- [ ] Todos los botones creados y asignados
- [ ] AudioSource agregado al GameManagerPuzz
- [ ] Clips de audio importados y asignados
- [ ] Todas las referencias asignadas en el Inspector del GameManagerPuzz
- [ ] Tiempo Limite configurado (120 segundos por defecto)

---

## 🎨 Personalización Opcional

### Cambiar Colores y Estilos

- **Panel de Victoria**: Puedes cambiar el color de fondo, agregar una imagen de fondo, etc.
- **Panel de Game Over**: Similar al de victoria, pero con colores más oscuros
- **Botones**: Puedes cambiar el color, agregar efectos hover, etc.

### Agregar Animaciones

- Puedes agregar animaciones de entrada para los paneles usando Animator
- O usar código para hacer fade in/out

### Agregar Más Elementos

- Puntuación final
- Tiempo restante al completar
- Estrellas o calificación
- Efectos de partículas

---

## 🐛 Solución de Problemas

### Los paneles no se muestran
- Verifica que los paneles estén desactivados inicialmente (checkbox desmarcado)
- Verifica que el código los active correctamente

### Los sonidos no se reproducen
- Verifica que el AudioSource esté asignado
- Verifica que los AudioClips estén asignados
- Verifica que el volumen del AudioSource no esté en 0

### Los botones no funcionan
- Verifica que los botones estén asignados correctamente
- Verifica que el EventSystem esté en la escena (se crea automáticamente con el Canvas)

### El timer no se actualiza
- Verifica que TimerTexto esté asignado
- Verifica que el texto esté visible en pantalla
- Verifica que el GameManagerPuzz esté activo

---

## 📝 Notas Finales

- Los paneles deben estar **desactivados** al inicio del juego
- El script los activará automáticamente cuando corresponda
- Asegúrate de que los nombres de las escenas en los botones coincidan con los nombres reales de tus escenas en Unity
- Si usas "Menu" como nombre de escena, asegúrate de que exista en Build Settings

---

¡Listo! Tu pantalla del puzzle debería estar completamente configurada. 🎉


# 🎮 AlquiNumber - Funcionalidades Principales

## 📋 Resumen del Proyecto
**AlquiNumber** es un juego educativo interactivo desarrollado en Unity que enseña destrezas a través de diferentes tipos de juegos: **Puzzles**, **Ejercicios Matemáticos** y **Memorama**, utilizando mecánicas de juego tipo drag-and-drop y selección múltiple.

---

## 🚀 Funcionalidades Principales

### 1. **Sistema de Menú y Navegación** 🎯
- **Menú Inicial**: Punto de entrada del juego con opciones de Jugar y Salir
- **Selección de Niveles**: Sistema de niveles desbloqueables basado en progreso
- **Navegación entre Escenas**: Sistema fluido para moverse entre diferentes pantallas del juego

**Scripts Involucrados:**
- `MenuInicial.cs` - Controla el menú principal
- `LevelMenuController.cs` - Gestiona la selección de niveles
- `CambiaEscena.cs` - Maneja cambios de escena

---

### 2. **Sistema de Gestión de Problemas** 🧠
- **Carga de Problemas desde CSV**: Lee problemas y sus soluciones desde archivo CSV
- **Selección Aleatoria**: Presenta problemas de forma aleatoria
- **Sistema de Opciones Múltiples**: Genera preguntas con 1 respuesta correcta y 3 incorrectas
- **Tipos de Juegos de Destreza Soportados**:
  - 🧩 **Puzzles**: Rompecabezas lógicos y desafíos de ingenio
  - ➕ **Ejercicios Matemáticos**: Problemas aritméticos, álgebra, geometría
  - 🎴 **Memorama**: Juegos de memoria y emparejamiento

**Scripts Involucrados:**
- `ProblemaManager.cs` - Gestiona la lógica de problemas
- `MetodoNumericoSelector.cs` - Define los tipos de juegos disponibles (requiere actualización)

---

### 3. **Sistema Drag & Drop (Arrastrar y Soltar)** 🖱️
- **Interfaz Interactiva**: Permite arrastrar elementos de la UI
- **Feedback Visual**: Efectos visuales al arrastrar (transparencia)
- **Validación de Zonas**: Sistema de slots que valida cuando se suelta un elemento

**Scripts Involucrados:**
- `DragDrop.cs` - Maneja el arrastre de elementos
- `ItemSlot.cs` - Valida y procesa elementos soltados
- `Ingrediente.cs` - Define ingredientes/elementos que se pueden arrastrar

---

### 4. **Sistema de Progreso del Jugador** 📊
- **Persistencia de Datos**: Guarda el progreso entre sesiones usando PlayerPrefs
- **Niveles Desbloqueados**: Sistema de progresión que desbloquea niveles
- **Sistema de Experiencia (XP)**: Acumula puntos de experiencia por completar niveles
- **Singleton Pattern**: Mantiene una única instancia del progreso en toda la aplicación

**Scripts Involucrados:**
- `PlayerProgress.cs` - Gestiona el progreso del jugador

---

### 5. **Sistema de UI y Tooltips** 💡
- **Tooltips Informativos**: Muestra información adicional sobre elementos
- **Feedback Auditivo**: Sonidos para acciones del jugador
- **Efectos Visuales**: Animaciones y efectos al completar acciones

**Scripts Involucrados:**
- `ItemTooltip.cs` - Muestra tooltips
- `Tooltip.cs` - Componente base de tooltips
- `UIButtonSound.cs` - Agrega sonidos a botones
- `levitacion.cs` - Efectos de animación (probablemente)

---

## 🎯 Funcionalidades Clave por Módulo

### Módulo de Navegación
✅ Menú principal funcional  
✅ Selección de niveles con bloqueo/desbloqueo  
✅ Navegación hacia adelante y atrás  

### Módulo de Juego
✅ Carga de problemas desde CSV  
✅ Sistema de preguntas con opciones múltiples  
✅ 3 tipos de juegos de destreza (Puzzles, Matemáticas, Memorama)  
✅ Selección aleatoria de problemas  

### Módulo Interactivo
✅ Drag & Drop funcional  
✅ Validación de respuestas  
✅ Feedback visual y auditivo  

### Módulo de Persistencia
✅ Guardado automático del progreso  
✅ Sistema de desbloqueo de niveles  
✅ Contador de experiencia  

---

## 📈 Prioridades de Funcionalidades

### 🔴 **ALTA PRIORIDAD** (Core del Juego)
1. Sistema de gestión de problemas (`ProblemaManager.cs`)
2. Sistema de progreso del jugador (`PlayerProgress.cs`)
3. Navegación básica (`MenuInicial.cs`, `CambiaEscena.cs`)

### 🟡 **MEDIA PRIORIDAD** (Mejora de UX)
4. Sistema Drag & Drop (`DragDrop.cs`, `ItemSlot.cs`)
5. Selección de niveles (`LevelMenuController.cs`)
6. Tooltips y feedback (`ItemTooltip.cs`, `UIButtonSound.cs`)

### 🟢 **BAJA PRIORIDAD** (Nice to Have)
7. Efectos visuales avanzados (`levitacion.cs`)
8. Sonidos y música

---

## 🎓 Propósito Educativo

Este juego está diseñado para ayudar a los estudiantes a:
- Desarrollar destrezas mentales a través de puzzles
- Practicar ejercicios matemáticos de forma interactiva
- Mejorar la memoria con juegos de memorama
- Mantener motivación a través de progreso y desbloqueos
- Aprender mediante juego y repetición


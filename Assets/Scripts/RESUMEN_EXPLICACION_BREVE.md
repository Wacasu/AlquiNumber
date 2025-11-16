# 📖 Resumen Breve - Explicación de Archivos

## 🎯 Propósito
Este documento contiene explicaciones **muy breves** de cada archivo para referencia rápida.

---

## 📂 ARCHIVOS Y SUS FUNCIONES

### **MenuInicial.cs**
Controla botones del menú (Jugar, Salir, Atrás). Navega entre escenas.

### **ProblemaManager.cs** ⭐ CORE
Lee problemas desde CSV. Selecciona aleatoriamente y genera 4 opciones (1 correcta + 3 incorrectas).

### **PlayerProgress.cs** ⭐ CORE
Guarda progreso del jugador (niveles, experiencia). Persiste entre sesiones usando Singleton.

### **DragDrop.cs**
Permite arrastrar elementos de la UI. Hace elementos semi-transparentes durante el arrastre.

### **ItemSlot.cs**
Zona donde se sueltan elementos. Valida respuestas, reproduce sonidos y efectos.

### **Ingrediente.cs**
Almacena nombre y tipo de elemento arrastrable (Puzzle, Matemáticas, Memorama).

### **LevelMenuController.cs**
Gestiona selección de niveles. Bloquea niveles no desbloqueados.

### **CambiaEscena.cs**
Utilidad simple para avanzar a la siguiente escena.

### **MetodoNumericoSelector.cs** ⚠️ ACTUALIZAR
Enum de tipos. **Necesita actualizarse** a: Puzzle, EjercicioMatematico, Memorama.

### **ItemTooltip.cs**
Muestra información al pasar el mouse sobre elementos.

### **Tooltip.cs**
Componente base para mostrar tooltips en diferentes posiciones.

### **UIButtonSound.cs**
Agrega sonidos a botones (hover y click).

### **levitacion.cs** (FloatingItem)
Crea efecto visual de flotación en elementos de la UI.

---

## 🎮 TIPOS DE JUEGOS
- 🧩 **Puzzle**: Rompecabezas lógicos
- ➕ **Ejercicio Matemático**: Problemas y cálculos
- 🎴 **Memorama**: Juegos de memoria y emparejamiento

---

**Para explicaciones detalladas y diagramas, ver:** `EXPLICACION_CODIGO_Y_DIAGRAMAS.md`


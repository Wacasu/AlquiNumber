# 🎮 AlquiNumber - Proyecto Educativo de Juegos de Destreza

## 📖 Bienvenida
Este proyecto es un juego educativo desarrollado en Unity para enseñar destrezas mediante diferentes tipos de juegos: **🧩 Puzzles**, **➕ Ejercicios Matemáticos** y **🎴 Memorama**.

---

## 📚 Documentación Disponible

### 🎯 [Funcionalidades Principales](./FUNCIONALIDADES_PRINCIPALES.md)
Documento que describe todas las funcionalidades del juego y su priorización.

**Incluye:**
- Resumen de funcionalidades por módulo
- Priorización de tareas
- Propósito educativo

---

### 📝 [Documentación Técnica del Código](./DOCUMENTACION_CODIGO.md)
Documentación detallada de cada script para evidencias del trabajo.

**Incluye:**
- Explicación técnica de cada script
- Funcionalidades implementadas
- Flujo de datos del sistema
- Tecnologías utilizadas

---

### 📘 [Explicación de Código y Diagramas](./EXPLICACION_CODIGO_Y_DIAGRAMAS.md) ⭐ NUEVO
Explicaciones breves de cada archivo y diagramas de flujo listos para convertir en gráficos.

**Incluye:**
- Explicación breve de cada script
- 8 diagramas de flujo en formato texto
- Instrucciones para crear gráficos
- Resumen rápido

---

### 👥 [Flujo de Trabajo para el Equipo](./FLUJO_TRABAJO_COMPANEROS.md)
Guía completa para colaborar en el proyecto.

**Incluye:**
- Asignación de tareas por funcionalidad
- Checklists de trabajo
- Guía de colaboración
- Template de reportes
- Solución de problemas comunes

---

### 📖 [Resumen Breve](./RESUMEN_EXPLICACION_BREVE.md)
Referencia rápida de todos los archivos del proyecto.

---

### 📊 [Clases para Diagrama UML](./CLASES_PARA_DIAGRAMA_UML.md) ⭐ NUEVO
Información completa de todas las clases para crear diagrama UML de relaciones.

**Incluye:**
- 12 clases con atributos y métodos
- 1 enumeración (MetodoNumerico)
- Relaciones entre clases
- Interfaces implementadas
- Patrón Singleton
- Guía para crear el diagrama

---

## 🚀 Inicio Rápido

### Para Desarrolladores
1. Abre el proyecto en Unity
2. Revisa [FUNCIONALIDADES_PRINCIPALES.md](./FUNCIONALIDADES_PRINCIPALES.md) para entender el proyecto
3. Lee [EXPLICACION_CODIGO_Y_DIAGRAMAS.md](./EXPLICACION_CODIGO_Y_DIAGRAMAS.md) para explicaciones breves y diagramas
4. Consulta [DOCUMENTACION_CODIGO.md](./DOCUMENTACION_CODIGO.md) para detalles técnicos
5. Sigue [FLUJO_TRABAJO_COMPANEROS.md](./FLUJO_TRABAJO_COMPANEROS.md) para trabajar en equipo

### Para Evidencias
1. Revisa `EXPLICACION_CODIGO_Y_DIAGRAMAS.md` - explicaciones breves y diagramas de flujo
2. Revisa `DOCUMENTACION_CODIGO.md` - explica cada script en detalle
3. Usa `CLASES_PARA_DIAGRAMA_UML.md` - para crear diagrama UML de clases
4. Captura funcionamiento del juego
5. Documenta funcionalidades completadas
6. Convierte diagramas de texto a gráficos usando Draw.io o similar

---

## 📂 Estructura del Proyecto

```
Assets/
└── Scripts/
    ├── MenuInicial.cs              # Menú principal
    ├── LevelMenuController.cs       # Control de niveles
    ├── ProblemaManager.cs          # ⭐ CORE - Gestión de problemas
    ├── PlayerProgress.cs           # ⭐ CORE - Sistema de progreso
    ├── DragDrop.cs                 # Sistema drag & drop
    ├── ItemSlot.cs                 # Validación de drops
    └── ... (otros scripts)
```

---

## 🎯 Funcionalidades Core

### 1. Sistema de Problemas
- Carga problemas desde CSV (Puzzles, Ejercicios Matemáticos, Memorama)
- Genera preguntas aleatorias
- Valida respuestas

### 2. Sistema de Progreso
- Guardado persistente
- Desbloqueo de niveles
- Sistema de experiencia

### 3. Sistema Drag & Drop
- Interfaz interactiva
- Validación de respuestas
- Feedback visual

---

## 📊 Prioridades

### 🔴 Alta (Hacer Primero)
- ProblemaManager.cs
- PlayerProgress.cs
- Navegación básica

### 🟡 Media (Después)
- Drag & Drop
- Conexión entre sistemas
- Validación de respuestas

### 🟢 Baja (Mejoras)
- Tooltips y feedback
- Sonidos y efectos

---

## 📸 Evidencias Requeridas

- [ ] Documentación de código completa
- [ ] Capturas del juego funcionando
- [ ] Scripts sin errores críticos
- [ ] Funcionalidades probadas

---

## 🤝 Colaboración

Para trabajar en equipo:
1. Lee `FLUJO_TRABAJO_COMPANEROS.md`
2. Sigue las reglas de trabajo
3. Comunica cambios importantes
4. Actualiza documentación

---

## 📞 Recursos

- **Funcionalidades**: Ver `FUNCIONALIDADES_PRINCIPALES.md`
- **Explicación Breve y Diagramas**: Ver `EXPLICACION_CODIGO_Y_DIAGRAMAS.md` ⭐
- **Clases para UML**: Ver `CLASES_PARA_DIAGRAMA_UML.md` ⭐
- **Documentación Técnica**: Ver `DOCUMENTACION_CODIGO.md`
- **Flujo de Trabajo**: Ver `FLUJO_TRABAJO_COMPANEROS.md`
- **Resumen Rápido**: Ver `RESUMEN_EXPLICACION_BREVE.md`

---

## 🎮 Tipos de Juegos
- 🧩 **Puzzles**: Rompecabezas lógicos y desafíos de ingenio
- ➕ **Ejercicios Matemáticos**: Problemas aritméticos, álgebra, geometría
- 🎴 **Memorama**: Juegos de memoria y emparejamiento

---

**Proyecto desarrollado para enseñanza de destrezas mediante gamificación**


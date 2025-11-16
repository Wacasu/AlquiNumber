# 👥 Guía de Flujo de Trabajo para el Equipo - AlquiNumber

## 🎯 Objetivo
Este documento establece el flujo de trabajo colaborativo para el desarrollo del proyecto AlquiNumber.

---

## 📋 Índice de Contenidos
1. [Información General del Proyecto](#información-general)
2. [Estructura del Proyecto](#estructura)
3. [Flujo de Trabajo por Funcionalidad](#flujo-por-funcionalidad)
4. [Guía de Colaboración](#guía-colaboración)
5. [Checklist de Evidencias](#checklist-evidencias)

---

## 🚀 Información General del Proyecto

### ¿Qué es AlquiNumber?
Juego educativo de Unity que enseña destrezas mediante diferentes tipos de juegos: **🧩 Puzzles**, **➕ Ejercicios Matemáticos** y **🎴 Memorama**, utilizando mecánicas interactivas de drag-and-drop y selección múltiple.

### Arquitectura
- **Motor**: Unity (versión compatible con C#)
- **Patrón Principal**: Singleton (PlayerProgress)
- **Persistencia**: PlayerPrefs
- **Datos**: CSV para problemas y soluciones

---

## 📂 Estructura del Proyecto

```
Assets/
└── Scripts/
    ├── MenuInicial.cs              ← Sistema de navegación
    ├── LevelMenuController.cs       ← Control de niveles
    ├── CambiaEscena.cs             ← Cambios de escena
    ├── ProblemaManager.cs          ← Lógica de problemas (CORE)
    ├── MetodoNumericoSelector.cs   ← Definición de métodos
    ├── PlayerProgress.cs           ← Sistema de guardado (CORE)
    ├── DragDrop.cs                 ← Sistema drag & drop
    ├── ItemSlot.cs                 ← Validación de drops
    ├── Ingrediente.cs              ← Componente de elementos
    ├── ItemTooltip.cs              ← Tooltips informativos
    ├── Tooltip.cs                  ← Base de tooltips
    ├── UIButtonSound.cs            ← Sonidos de UI
    └── levitacion.cs               ← Efectos visuales
```

---

## 🔄 Flujo de Trabajo por Funcionalidad

### **FASE 1: Sistema de Navegación** 🧭

#### Responsabilidades
- Configurar menú principal
- Implementar navegación entre escenas
- Sistema de selección de niveles

#### Scripts Involucrados
- `MenuInicial.cs`
- `LevelMenuController.cs`
- `CambiaEscena.cs`

#### Checklist de Trabajo
```
□ Crear escenas en Unity (MenuInicial, LevelSelect, GameScene)
□ Configurar botones en MenuInicial.cs
□ Conectar LevelMenuController con PlayerProgress
□ Probar navegación hacia adelante y atrás
□ Verificar que los niveles se bloquean/desbloquean correctamente
```

#### Persona Asignada: _______________

---

### **FASE 2: Sistema de Problemas (CORE)** 🧠

#### Responsabilidades
- Crear estructura de CSV con problemas
- Implementar carga y parsing de CSV
- Generar preguntas aleatorias
- Validar respuestas

#### Scripts Involucrados
- `ProblemaManager.cs` ⭐ (CRÍTICO)
- `MetodoNumericoSelector.cs`

#### Checklist de Trabajo
```
□ Crear archivo CSV con formato correcto
  - Primera fila: Problema, Método1, Método2, ...
  - Filas siguientes: Texto problema, 1 (correcto) o 0 (incorrecto)
□ Asignar CSV en Inspector de ProblemaManager
□ Probar carga de múltiples problemas
□ Verificar generación aleatoria de opciones
□ Validar que siempre hay 1 correcta y 3 incorrectas
□ Conectar con sistema de validación de respuestas
```

#### Formato CSV Ejemplo:
```csv
Problema,Puzzle,EjercicioMatematico,Memorama
Resolver sudoku 3x3,1,0,0
Calcular 15+23,0,1,0
Emparejar símbolos matemáticos,0,0,1
Completar secuencia numérica,1,1,0
```

#### Persona Asignada: _______________

---

### **FASE 3: Sistema de Progreso (CORE)** 📊

#### Responsabilidades
- Implementar guardado de progreso
- Sistema de desbloqueo de niveles
- Contador de experiencia

#### Scripts Involucrados
- `PlayerProgress.cs` ⭐ (CRÍTICO)

#### Checklist de Trabajo
```
□ Verificar que PlayerProgress es Singleton
□ Implementar guardado al completar nivel
□ Implementar desbloqueo automático de siguiente nivel
□ Agregar sistema de XP por nivel completado
□ Probar persistencia entre sesiones
□ Conectar con LevelMenuController para mostrar niveles desbloqueados
```

#### Cómo Probar:
1. Completar un nivel
2. Cerrar el juego
3. Abrir nuevamente
4. Verificar que el progreso se mantuvo

#### Persona Asignada: _______________

---

### **FASE 4: Sistema Drag & Drop** 🖱️

#### Responsabilidades
- Implementar arrastre de elementos
- Crear zonas de soltado
- Validar respuestas al soltar

#### Scripts Involucrados
- `DragDrop.cs`
- `ItemSlot.cs`
- `Ingrediente.cs`

#### Checklist de Trabajo
```
□ Aplicar DragDrop.cs a elementos arrastrables
□ Crear ItemSlot en áreas objetivo
□ Configurar Canvas correctamente (importante para drag & drop)
□ Probar arrastre con mouse y touch (si aplica)
□ Validar que conditionMet se activa al soltar correctamente
□ Agregar feedback visual (transparencia durante arrastre)
□ Conectar con sistema de validación de respuestas
```

#### Configuración Requerida:
- Canvas debe tener GraphicRaycaster
- Elementos arrastrables deben tener CanvasGroup
- ItemSlots deben tener Image o Collider

#### Persona Asignada: _______________

---

### **FASE 5: Sistema de UI y Feedback** 💡

#### Responsabilidades
- Tooltips informativos
- Sonidos de interfaz
- Efectos visuales

#### Scripts Involucrados
- `ItemTooltip.cs`
- `Tooltip.cs`
- `UIButtonSound.cs`
- `levitacion.cs`

#### Checklist de Trabajo
```
□ Implementar tooltips en elementos interactivos
□ Agregar sonidos a botones principales
□ Configurar efectos visuales al completar acciones
□ Probar feedback en diferentes acciones del juego
□ Optimizar efectos para rendimiento
```

#### Persona Asignada: _______________

---

## 🤝 Guía de Colaboración

### **Reglas de Trabajo**

#### ✅ ANTES de Trabajar
1. **Hacer Pull/Sync** del repositorio
   ```bash
   git pull origin main
   ```
2. **Verificar** qué scripts están siendo modificados actualmente
3. **Comunicar** qué funcionalidad vas a trabajar

#### ⚠️ DURANTE el Trabajo
1. **No modificar** scripts que otros están usando sin consultar
2. **Comentar** código complejo o importante
3. **Probar** tu código antes de commitear

#### ✅ DESPUÉS de Trabajar
1. **Probar** que el juego funciona correctamente
2. **Hacer Commit** con mensaje descriptivo
   ```
   Ejemplo: "feat: Agregado sistema de guardado en PlayerProgress"
   ```
3. **Hacer Push** al repositorio
4. **Actualizar** documentación si es necesario

### **Comunicación**

#### Template para Reportar Trabajo:
```
👤 Nombre: _______________
📅 Fecha: _______________
✅ Trabajo Completado:
- [Funcionalidad completada]

🐛 Problemas Encontrados:
- [Si hay problemas, describirlos]

📝 Notas:
- [Observaciones importantes]
```

### **Resolución de Conflictos**

Si hay conflictos de merge:
1. **NO hacer force push**
2. **Comunicar** al equipo
3. **Resolver** manualmente o con ayuda
4. **Probar** después de resolver

---

## 📊 Checklist de Evidencias

### Para Subir Evidencias del Trabajo

#### ✅ Evidencias Mínimas Requeridas

**1. Documentación de Código**
- [ ] `DOCUMENTACION_CODIGO.md` actualizado
- [ ] Comentarios en código complejo
- [ ] Explicación de decisiones técnicas

**2. Funcionalidades Implementadas**
- [ ] Scripts funcionando correctamente
- [ ] Pruebas básicas realizadas
- [ ] Sin errores críticos en consola

**3. Capturas/Videos**
- [ ] Capturas de pantalla del juego funcionando
- [ ] Video corto demostrando funcionalidad (opcional pero recomendado)

**4. Reporte de Trabajo**
- [ ] Documento de funcionalidades completadas
- [ ] Problemas encontrados y resueltos
- [ ] Tiempo aproximado de desarrollo

#### 📸 Qué Capturar

**Obligatorio:**
1. Menú principal funcionando
2. Selección de niveles
3. Gameplay básico (arrastre o selección)
4. Sistema de progreso (guardado funcionando)

**Recomendado:**
5. Video walkthrough de 30-60 segundos
6. Consola de Unity sin errores críticos
7. Inspector mostrando configuración de scripts

---

## 🎯 Priorización de Tareas

### 🔴 **URGENTE** (Hacer Primero)
1. Sistema de Problemas (`ProblemaManager.cs`)
2. Sistema de Progreso (`PlayerProgress.cs`)
3. Navegación básica funcionando

### 🟡 **IMPORTANTE** (Siguiente)
4. Drag & Drop funcional
5. Conexión entre sistemas
6. Validación de respuestas

### 🟢 **MEJORAS** (Después)
7. Tooltips y feedback
8. Sonidos y efectos
9. Pulido visual

---

## 📝 Template de Asignación de Tareas

```markdown
## Tarea: [Nombre de la Funcionalidad]

**Asignado a:** _______________
**Fecha límite:** _______________
**Prioridad:** 🔴 / 🟡 / 🟢

**Descripción:**
[Descripción de lo que hay que hacer]

**Scripts a Modificar:**
- Script1.cs
- Script2.cs

**Dependencias:**
[¿Qué debe estar listo antes?]

**Criterios de Aceptación:**
- [ ] Criterio 1
- [ ] Criterio 2
- [ ] Criterio 3
```

---

## 🚨 Problemas Comunes y Soluciones

### ❌ Error: "ProblemaManager no encuentra CSV"
**Solución:** Asignar el archivo CSV en el Inspector de Unity al campo `archivoCSV`

### ❌ Error: "PlayerProgress no persiste datos"
**Solución:** Verificar que el GameObject tenga `DontDestroyOnLoad` activado

### ❌ Error: "DragDrop no funciona"
**Solución:** 
- Verificar que el Canvas tiene GraphicRaycaster
- Verificar que el elemento tiene CanvasGroup
- Verificar configuración de EventSystem

### ❌ Error: "Niveles no se desbloquean"
**Solución:** 
- Conectar LevelMenuController con PlayerProgress.Instance
- Verificar que maxLevelUnlocked se actualiza correctamente

### ❌ Error: "Tipos de juego no coinciden"
**Solución:** 
- Actualizar MetodoNumericoSelector.cs para usar TipoJuego (Puzzle, EjercicioMatematico, Memorama)
- Actualizar CSV con los nuevos tipos de juegos
- Verificar que Ingrediente.cs usa el nuevo enum

---

## 📞 Contacto y Soporte

**Para dudas sobre:**
- Estructura del proyecto → Ver `DOCUMENTACION_CODIGO.md`
- Funcionalidades → Ver `FUNCIONALIDADES_PRINCIPALES.md`
- Problemas técnicos → Comunicar al equipo

---

## ✅ Checklist Final antes de Entrega

- [ ] Todos los scripts principales funcionan
- [ ] Sin errores críticos en consola
- [ ] Progreso se guarda correctamente
- [ ] Problemas se cargan desde CSV
- [ ] Drag & Drop funciona correctamente
- [ ] Documentación actualizada
- [ ] Evidencias capturadas
- [ ] Código comentado donde sea necesario

---

**Última actualización:** _______________
**Versión del documento:** 1.0


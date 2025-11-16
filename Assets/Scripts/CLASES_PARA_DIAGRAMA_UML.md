# 📊 Clases para Diagrama UML - AlquiNumber (SIMPLIFICADO)

## 🎯 Propósito
Este documento contiene la información esencial para crear un diagrama UML simple con solo las funcionalidades principales del proyecto.

---

## 📋 CLASES PRINCIPALES (CORE)

### 1. **MenuInicial** 
**Funcionalidad:** Controla el menú principal del juego

**Métodos Principales:**
- `Jugar()` - Inicia el juego
- `Salir()` - Cierra la aplicación

---

### 2. **ProblemaManager** ⭐ CORE
**Funcionalidad:** Gestiona problemas y preguntas del juego

**Atributos:**
- `archivoCSV` - Archivo con problemas

**Métodos Principales:**
- `MostrarProblema()` - Carga y muestra problema aleatorio

---

### 3. **PlayerProgress** ⭐ CORE (Singleton)
**Funcionalidad:** Guarda el progreso del jugador

**Atributos:**
- `Instance` (static) - Instancia única
- `nivelMaxDesbloqueado` - Niveles desbloqueados
- `experiencia` - Puntos ganados

**Métodos Principales:**
- `GanarExperiencia(int)` - Suma experiencia
- `DesbloquearNivel(int)` - Desbloquea niveles

---

### 4. **LevelMenuController**
**Funcionalidad:** Controla selección de niveles

**Atributos:**
- `levelButtons[]` - Botones de niveles

**Métodos Principales:**
- `LoadLevel(int)` - Carga un nivel

---

### 5. **DragDrop**
**Funcionalidad:** Sistema de arrastrar y soltar

**Métodos Principales:**
- `OnBeginDrag()` - Inicia arrastre
- `OnDrag()` - Durante arrastre
- `OnEndDrag()` - Termina arrastre

---

### 6. **ItemSlot**
**Funcionalidad:** Zona donde se sueltan elementos

**Atributos:**
- `conditionMet` - Indica si se completó acción

**Métodos Principales:**
- `OnDrop()` - Valida elemento soltado

---

### 7. **Ingrediente**
**Funcionalidad:** Elemento que se puede arrastrar

**Atributos:**
- `nombreIngrediente` - Nombre del elemento
- `metodoAsignado` - Tipo de juego (enum)

---

## 📌 ENUM

### **MetodoNumerico**
Valores: Biseccion, NewtonRaphson, Secante, etc.
*(Debería cambiarse a: Puzzle, EjercicioMatematico, Memorama)*

---

## 🔗 RELACIONES PRINCIPALES

```
PlayerProgress (Singleton)
    ↓ (debería consultar)
LevelMenuController

DragDrop
    ↓ (arrastra a)
ItemSlot
    ↑ (valida)
Ingrediente

Ingrediente → MetodoNumerico (enum)
```

---

## 📊 DIAGRAMA UML SIMPLE SUGERIDO

```
┌─────────────────────────────────────────┐
│         NAVEGACIÓN                      │
├─────────────────────────────────────────┤
│  MenuInicial                            │
│  + Jugar()                              │
│  + Salir()                              │
│                                         │
│  LevelMenuController                    │
│  - levelButtons[]                      │
│  + LoadLevel()                          │
└─────────────────────────────────────────┘
           ▲
           │ consulta
           │
┌──────────┴─────────────────────────────┐
│         PROGRESO                        │
├─────────────────────────────────────────┤
│  PlayerProgress (Singleton)             │
│  + Instance                            │
│  - nivelMaxDesbloqueado                │
│  - experiencia                          │
│  + GanarExperiencia()                  │
│  + DesbloquearNivel()                  │
└─────────────────────────────────────────┘

┌─────────────────────────────────────────┐
│         JUEGO                           │
├─────────────────────────────────────────┤
│  ProblemaManager                       │
│  - archivoCSV                           │
│  + MostrarProblema()                   │
└─────────────────────────────────────────┘

┌─────────────────────────────────────────┐
│         DRAG & DROP                     │
├─────────────────────────────────────────┤
│  DragDrop ────► ItemSlot               │
│  + OnDrag()     + OnDrop()             │
│                                         │
│  Ingrediente                            │
│  - nombreIngrediente                   │
│  - metodoAsignado ───► MetodoNumerico  │
└─────────────────────────────────────────┘
```

---

## ✅ ELEMENTOS MÍNIMOS PARA EL DIAGRAMA

- [ ] 7 clases principales
- [ ] 1 enum (MetodoNumerico)
- [ ] Herencia de MonoBehaviour (opcional mostrar)
- [ ] Relación PlayerProgress → LevelMenuController
- [ ] Relación DragDrop → ItemSlot
- [ ] Relación Ingrediente → MetodoNumerico
- [ ] Indicar Singleton en PlayerProgress

---

## 🎨 HERRAMIENTAS RECOMENDADAS

1. **Draw.io** - Simple y gratuito
2. **PlantUML** - Ver ejemplo simplificado abajo

---

**Nota:** Este es un diagrama simplificado. Para versión completa con todas las clases, ver sección de detalles adicionales.

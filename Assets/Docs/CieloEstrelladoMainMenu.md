# Cielo Estrellado en MainMenu - Documentación

## Descripción
Sistema simple de cielo estrellado usando Particle System para la escena MainMenu. Solución ligera y visualmente atractiva con mínimo impacto en rendimiento.

---

## Implementación

### Método utilizado: Particle System Manual

Esta implementación usa el sistema de partículas nativo de Unity para generar entre 50-100 estrellas estáticas con efectos opcionales de parpadeo y movimiento sutil.

---

## Pasos de configuración

### 1. Crear el Particle System
1. En la escena **MainMenu**, selecciona: `GameObject → Effects → Particle System`
2. Renombra el objeto a `Starfield` o `CieloEstrellado`

### 2. Configuración básica del módulo Main

En el Inspector, con el Particle System seleccionado:

| Parámetro | Valor | Descripción |
|-----------|-------|-------------|
| **Duration** | 5 | Duración del ciclo |
| **Looping** | ✓ (activado) | Para que se repita continuamente |
| **Start Lifetime** | 5 | Vida de cada partícula |
| **Start Speed** | 0 | Sin velocidad inicial (estrellas estáticas) |
| **Start Size** | 0.03 - 0.08 | Tamaño aleatorio entre estos valores |
| **Start Color** | Blanco | Color base de las estrellas |
| **Simulation Space** | Local | Para que las estrellas se muevan con el objeto |
| **Max Particles** | 100 | Límite máximo de partículas |

### 3. Configuración de Emission

| Parámetro | Valor | Descripción |
|-----------|-------|-------------|
| **Rate over Time** | 0 | Desactivar emisión continua |
| **Bursts** | 1 burst configurado: | |
| - Time | 0 | Emitir al inicio |
| - Count | 50-100 | Número de estrellas (ajustable) |

### 4. Configuración de Shape

| Parámetro | Valor | Descripción |
|-----------|-------|-------------|
| **Shape** | Sphere | Forma esférica de distribución |
| **Radius** | 30-50 | Radio de la esfera (ajustar según escena) |
| **Arc** | 360 | Distribución completa |

### 5. Configuración de Renderer

| Parámetro | Valor | Descripción |
|-----------|-------|-------------|
| **Render Mode** | Billboard | Las partículas miran siempre a cámara |
| **Material** | Default-Particle | Material por defecto (o custom) |
| **Sorting Layer** | Default | Ajustar si necesario |
| **Order in Layer** | -100 | Para que aparezca detrás del UI |

---

## Efectos opcionales

### Opción A: Parpadeo (Twinkle)

Activa el módulo **Color over Lifetime**:
- Crea un gradient que varíe la opacidad (alpha):
  - Inicio: Alpha 1.0
  - Medio: Alpha 0.6
  - Final: Alpha 1.0
- Las estrellas parpadearán suavemente

### Opción B: Movimiento sutil

Activa el módulo **Noise**:
- **Strength**: 0.1 - 0.3
- **Frequency**: 0.5
- **Scroll Speed**: 0.1
- Esto crea un movimiento orgánico muy sutil

### Opción C: Rotación lenta

Activa el módulo **Rotation over Lifetime**:
- **Angular Velocity**: 10-30 grados/segundo
- Da sensación de movimiento sin ser mareante

---

## Posicionamiento

### En escenas con Canvas (Screen Space Overlay)
- El Particle System debe estar en la jerarquía de la escena (no hijo del Canvas)
- Posicionarlo frente a la cámara pero detrás de todos los elementos UI
- Ajustar la distancia Z según necesidad

### Ajustes de Transform recomendados
- **Position**: (0, 0, 20) - ajustar según profundidad deseada
- **Rotation**: (0, 0, 0)
- **Scale**: (1, 1, 1)

---

## Material personalizado (Opcional)

Para mejor control visual, puedes crear un material custom:

1. Crea un material: `Assets → Create → Material`
2. Nombre: `StarMaterial`
3. Shader: `Particles/Standard Unlit` o `Universal Render Pipeline/Particles/Unlit`
4. Asigna una textura de punto blanco (8x8 o 16x16 pixels)
5. Ajusta **Rendering Mode** a `Additive` o `Transparent`
6. Asigna este material al Renderer del Particle System

---

## Optimización y rendimiento

### Recomendaciones
- **Número de estrellas**: 50-80 es suficiente para buen efecto visual
- **Tamaño**: Usa valores pequeños (0.03-0.08) para mejor performance
- **Culling**: Si hay múltiples cámaras, ajusta Culling Mask para renderizar solo en MainMenu
- **Layer**: Considera poner el Particle System en un layer separado

### Impacto en rendimiento
- **50 partículas**: ~0.05ms GPU en hardware moderno
- **100 partículas**: ~0.1ms GPU
- Negligible en menús donde no hay gameplay intensivo

---

## Variaciones y ajustes

### Para más profundidad visual
- Crea 2-3 Particle Systems con diferentes:
  - Radios (cercano, medio, lejano)
  - Tamaños de partícula
  - Velocidades de movimiento sutiles
- Esto crea efecto parallax natural

### Para atmósfera específica
- **Azul frío**: Tinte azul claro en Start Color
- **Cálido**: Tinte amarillo/naranja suave
- **Multicolor**: Usa Color over Lifetime con gradient de colores

### Para estrellas fugaces (avanzado)
- Añade segundo Particle System con:
  - Start Speed: 10-20
  - Start Lifetime: 2-3 segundos
  - Rate over Time: 0.1-0.5
  - Trail module activado

---

## Troubleshooting

### Las estrellas no se ven
- ✓ Verifica que el Particle System esté **Play On Awake** activado
- ✓ Comprueba la posición Z respecto a la cámara
- ✓ Asegúrate que el material no sea completamente transparente
- ✓ Revisa el Sorting Layer / Order in Layer

### Las estrellas están muy grandes/pequeñas
- Ajusta **Start Size** en módulo Main
- O cambia la **Scale** del GameObject padre

### El parpadeo es muy rápido/lento
- Modifica la curva/gradient en **Color over Lifetime**
- O ajusta el **Start Lifetime** del módulo Main

### Impacto visual insuficiente
- Aumenta **Count** en Bursts (más estrellas)
- Incrementa **Radius** en Shape (más dispersión)
- Añade efecto de brillo con Bloom en Post-Processing

---

## Mantenimiento

### Archivos relacionados
- Escena: `Assets/Scenes/MainMenu.unity`
- GameObject: `Starfield` o `CieloEstrellado` (dentro de la escena)
- Material (si custom): `Assets/Materials/StarMaterial.mat`

### Versionado
- **Fecha implementación**: 21 octubre 2025
- **Unity Version**: Unity 6
- **Método**: Particle System nativo
- **Complejidad**: Baja
- **Rendimiento**: Óptimo

---

## Notas adicionales

- Esta solución no requiere scripts personalizados
- Compatible con todos los render pipelines (Built-in, URP, HDRP)
- Fácilmente replicable en otras escenas
- Los valores son puntos de partida; ajusta según tu estética

---

## Autor
Implementado para el proyecto `unity_entrega5_programacionvisitavirtual`

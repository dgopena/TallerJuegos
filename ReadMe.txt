Al hacer un layout nuevo:

> Ir a File > Build Settings.
> En "Scenes in Build", estando en el layout que quieran agregar, apreten add current

Lo anterior a�ade la escena al build oficial.

Como rearmar un countgrid:

> La camara siempre esta en el punto (0,0). Unity cuenta con un grid en pantalla que divide el layout en valores enteros
> Vean la ubicacion de la esquina inferior izquierda del objeto Image del countgrid (el que se va a llenar)
Debe quedar en valores enteros.
Los valores importantes del componente object grid son:
> El valor x de la posicion de esta esquina es lo que se debe colocar en "Orig Vertical"
> El valor y es lo que se coloca en "Orig Horizontal"
> El tama�o horizontal de la imagen a llenar es lo que se pone en "len Hor"
> El tama�o verical va en "len Ver"
> El box collider del grid debe ser del tama�o exacto de la imagen a llenar.
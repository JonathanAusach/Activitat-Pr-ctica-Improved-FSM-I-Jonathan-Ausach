using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorState : MonoBehaviour
{
    //Hacemos los estado con una enumeracion
    enum StateDoor {Open, Close, Closing, Opening }

    //creamos las variable de la enumeracion anterior
    StateDoor MyDoor;

    //Un Transform para mover las puertas
    [SerializeField] Transform door1;
    [SerializeField] Transform door2;

    [SerializeField] Button abrir;
    [SerializeField] Button cerrar;

    //velocidad de cerrarse
    float speed = 2.0f;

    //Vectores 

    //cerrar
    Vector3 CerrarDoor1 = new Vector3(-3,1,0);
    Vector3 CerrarDoor2 = new Vector3(2,1,0);

    //Abrir
    Vector3 AbrirDoor1 = new Vector3(-6,1,0);
    Vector3 AbrirDoor2 = new Vector3(4,1,0);

    private void Awake()
    {
        //Cerramos puerta para que no entren intrusos
        door1.position = CerrarDoor1;
        door2.position = CerrarDoor2;
        MyDoor = StateDoor.Close;
    }

    private void Update()
    {
        switch (MyDoor)
        {
            case StateDoor.Open:
                //la puerta esta abierta
                OpenDoor();
                break;
            case StateDoor.Close:
                //la puerta esta cerrada
                CloseDoor();
                break;
            case StateDoor.Closing:
                //aquí tendremos que hacer intervalos de tiempo para ver como se cierra poco a poco
                CloseingDoor();
                break;
            case StateDoor.Opening:
                //y los mismo que closing, pero para abrir la puerta
                OpeningDoor();
                break;
        }
    }

    //Funciones para los botones
    public void CerraDoor()
    {
        MyDoor = StateDoor.Closing;
    }

    public void AbrirDoor()
    {
        MyDoor = StateDoor.Opening;
    }

    //el estado abierto, se podra interuar con el boton cerrado, pero el abierto no se podra utilizar
    void OpenDoor()
    {
        abrir.interactable = false;
        cerrar.interactable = true;
    }

    //lo mismo que el estado abierto, pero a la inversa
    void CloseDoor()
    {
        cerrar.interactable = false;
        abrir.interactable = true;
    }

    //Estado que se esta abriendo la puerta, se llamara la funcion de mover puerta por cada puerta, para abrir
    void OpeningDoor()
    {
        MoverDoor(door1,AbrirDoor1,MyDoor);
        MoverDoor(door2,AbrirDoor2, MyDoor);
        
    }

    //Estado que se cierra la puerta, se llama la funcion de mover puerta por cada puerta, para cerrar
    void CloseingDoor()
    {
        MoverDoor(door1,CerrarDoor1, MyDoor);
        MoverDoor(door2,CerrarDoor2, MyDoor);
       
    }

    //funcion para cerara la puerta
    void MoverDoor(Transform Objecto, Vector3 objectivo, StateDoor Stat)
    {
        //Movemos la puerta
        Objecto.position = Vector3.MoveTowards(
            Objecto.position,
            objectivo,
            speed * Time.deltaTime
            );

        //comprobamos que haya llegado a su objectivo
        if (Vector3.Distance(Objecto.position, objectivo) < 0.01f)
        {
            Objecto.position = objectivo;
            //Comprobamos el estado que este, para cambiar el estado, si estamos abriendo la puerta pues el estado seria abierto
            if (Stat == StateDoor.Opening)
            {
                MyDoor = StateDoor.Open;
            }
            //en caso de cerrar la puerta, el estado seria cerrado
            else if (Stat == StateDoor.Closing)
            {
                MyDoor = StateDoor.Close;
            }
        }
    }

}
